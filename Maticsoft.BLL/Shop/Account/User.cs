using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.BLL.Shop.Account
{
    public class User
    {
        Maticsoft.SQLServerDAL.Shop.Account.User user = new SQLServerDAL.Shop.Account.User();

        Maticsoft.SQLServerDAL.Members.Users dal = new SQLServerDAL.Members.Users();
        Maticsoft.SQLServerDAL.Members.UsersExp dalexp = new SQLServerDAL.Members.UsersExp();

        public Maticsoft.Accounts.Bus.User GetPhoneUser(string phone,string password = "")
        {
            return user.GetPhoneUser(phone,password);
        }

        public bool UpdateUserName4Accounts(string oldusername, string newusername)
        {
            var x  = new SQLServerDAL.Shop.Account.User();

            return x.UpdateUserName4Accounts(oldusername, newusername);
        }

          //IsPhoneVerify 0为false 1为true
        public bool SetPhoneMark(string username,string phone)
        {
            var x = new SQLServerDAL.Shop.Account.User();

            return x.SetPhoneMark(username,phone);
        }


        public bool  GetPhoneMarkByUserName(string username)
        {
            return user.GetPhoneMarkByUserName(username);
        }

        public bool GetPhoneMarkByID(int userid)
        {
            return user.GetPhoneMarkByID(userid);
        }


        //判断手机号号是否验证过
        public bool CheckPhoneVerify(string phone)
        {
            return user.CheckPhoneVerify(phone);
        }


        public bool UpdateUserName(string oldusername, string newusername)
        {
            return user.UpdateUserName(oldusername,newusername);
        }

        public bool CheckPhoneExits(string phone)
        {
            return user.CheckPhoneExits(phone);
        }
        public Maticsoft.Model.Members.Users GetUsersInfo(int userid)
        {
            return dal.GetModel(userid);
        }

        
     
    }
}
