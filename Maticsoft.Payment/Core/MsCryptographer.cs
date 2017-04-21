
/**
* MsCryptographer.cs
*
* 功 能： 配置文件-密码处理类
* 类 名： MsCryptographer
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
* V0.02 2012/10/10  研发部    姚远   KEY/IV改为常量, 不再从Web.config中读取
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Security.Cryptography;
using System.Text;

namespace Maticsoft.Payment.Core
{
    internal sealed class MsCryptographer : IDisposable
    {
        public const string KEY = "eKcPSVEwC/dTgx3uyop48A==";
        public const string IV = "CPGJZ95gatDfIfnxX8WQUA==";

        private RijndaelManaged cryptoHelper;
        private ICryptoTransform decryptor;
        private bool disposed;
        private ICryptoTransform encryptor;

        public MsCryptographer(bool useDecryptor, bool useEncryptor)
            : this(useDecryptor, useEncryptor, KEY, IV)
        {
        }

        public MsCryptographer(bool useDecryptor, bool useEncryptor, string key, string iv)
        {
            if (useDecryptor || useEncryptor)
            {
                this.cryptoHelper = new RijndaelManaged();
                this.cryptoHelper.Key = Convert.FromBase64String(key);
                this.cryptoHelper.IV = Convert.FromBase64String(iv);
                if (useDecryptor)
                {
                    this.decryptor = this.cryptoHelper.CreateDecryptor();
                }
                if (useEncryptor)
                {
                    this.encryptor = this.cryptoHelper.CreateEncryptor();
                }
            }
        }

        public static bool CompareHash(byte[] plaintext, byte[] hashedText)
        {
            bool flag = false;
            byte[] buffer = CreateHash(plaintext);
            if (buffer.Length == hashedText.Length)
            {
                int index = 0;
                while ((index < buffer.Length) && (buffer[index] == hashedText[index]))
                {
                    index++;
                }
                if (index == buffer.Length)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public static bool CompareHash(string plaintext, string hashedText)
        {
            string x = CreateHash(plaintext);
            return (StringComparer.OrdinalIgnoreCase.Compare(x, hashedText) == 0);
        }

        public static byte[] CreateHash(byte[] plaintext)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            return provider.ComputeHash(plaintext);
        }

        public static string CreateHash(string plaintext)
        {
            byte[] buffer = CreateHash(Encoding.ASCII.GetBytes(plaintext));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public string Decrypt(string ciphertextBase64)
        {
            return Encoding.UTF8.GetString(this.Decrypt(Convert.FromBase64String(ciphertextBase64)));
        }

        public byte[] Decrypt(byte[] ciphertext)
        {
            return this.decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (this.cryptoHelper != null)
                {
                    this.cryptoHelper.Clear();
                }
                if (this.decryptor != null)
                {
                    this.decryptor.Dispose();
                }
                if (this.encryptor != null)
                {
                    this.encryptor.Dispose();
                }
                this.disposed = true;
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        public string Encrypt(string plaintext)
        {
            return Convert.ToBase64String(this.Encrypt(Encoding.UTF8.GetBytes(plaintext)));
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            return this.encryptor.TransformFinalBlock(plaintext, 0, plaintext.Length);
        }

        ~MsCryptographer()
        {
            this.Dispose(false);
        }
    }
}

