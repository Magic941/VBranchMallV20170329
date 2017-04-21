using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal class RSAUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static string RsaSign(string content, string publicKey)
        {
            var rsaCsp = LoadCertificateFile(publicKey);
            var dataBytes = Encoding.UTF8.GetBytes(content);
            var signatureBytes = rsaCsp.SignData(dataBytes, "SHA1");
            return ByteToHexStr(signatureBytes).ToString(CultureInfo.InvariantCulture).ToLower();
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="originalString">原文：UTF8编码</param>
        /// <param name="signatureString">签名：base64编码的字节</param>
        /// <param name="publicKey">银商公钥信息</param>
        /// <returns>验签结果</returns>
        public static bool Verify(string originalString, string signatureString, string publicKey)
        {
            var result = false;
            var byteData = Encoding.UTF8.GetBytes(originalString);
            var data = StrToToHexByte(signatureString);
            var paraPub = ConvertFromPublicKey(publicKey);
            var rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);
            SHA1 sh = new SHA1CryptoServiceProvider();
            result = rsaPub.VerifyData(byteData, sh, data);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pemFileConent"></param>
        /// <returns></returns>
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {
            var keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            var pemModulus = new byte[128];
            var pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            var para = new RSAParameters { Modulus = pemModulus, Exponent = pemPublicExponent };
            return para;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pemstr"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider LoadCertificateFile(string pemstr)
        {
            var pkcs8Privatekey = Convert.FromBase64String(pemstr);
            try
            {
                var rsa = DecodeRSAPrivateKey(pkcs8Privatekey);
                return rsa;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="privkey"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            // --------- Set up stream to decode the asn.1 encoded RSA private key ------  
            var mem = new MemoryStream(privkey);
            var binr = new BinaryReader(mem);  //wrap Memory Stream with BinaryReader for easy reading  
            try
            {
                var twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte();    //advance 1 byte  
                        break;
                    case 0x8230:
                        binr.ReadInt16();    //advance 2 bytes  
                        break;
                    default:
                        return null;
                        break;
                }

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number  
                    return null;
                var bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;

                //------ all private key components are Integer sequences ----  
                var elems = GetIntegerSize(binr);
                var modulus = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var e = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var d = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var p = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dp = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dq = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var iq = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  
                var cspParameters = new CspParameters { Flags = CspProviderFlags.UseMachineKeyStore };
                var rsa = new RSACryptoServiceProvider(1024, cspParameters);
                var rsAparams = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = e,
                    D = d,
                    P = p,
                    Q = q,
                    DP = dp,
                    DQ = dq,
                    InverseQ = iq
                };
                rsa.ImportParameters(rsAparams);
                return rsa;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                binr.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binr"></param>
        /// <returns></returns>
        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            var count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            switch (bt)
            {
                case 0x81:
                    count = binr.ReadByte();
                    break;
                case 0x82:
                    var highbyte = binr.ReadByte();
                    var lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt16(modint, 0);
                    break;
                default:
                    count = bt;
                    break;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] StrToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            const string returnStr = "";
            return bytes == null ? returnStr : bytes.Aggregate(returnStr, (current, t) => current + t.ToString("X2"));
        }
    }
}
