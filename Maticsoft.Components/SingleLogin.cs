using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maticsoft.Web
{
    /// <summary>
    /// 单用户登录类，解决多地登录问题
    /// </summary>
    public class SingleLogin
    {
        private readonly string appKey = "online";
        private readonly string logout = "{$}";

        public SingleLogin() { }

        /// <summary>
        /// 用户登陆操作
        /// </summary>
        /// <param name="id">为用户ID或用户名,这个必须是用户的唯一标识</param>
        public void UserLogin(object id)
        {
            System.Collections.Hashtable ht = (System.Collections.Hashtable)HttpContext.Current.Application[appKey];
            if (ht == null) 
                ht = new System.Collections.Hashtable();

            System.Collections.IDictionaryEnumerator IDE = ht.GetEnumerator();
            while (IDE.MoveNext())
            {
                if (IDE.Value.ToString().Equals(id.ToString()))
                {
                    ht[IDE.Key.ToString()] = logout;
                    break;
                }
            }

            ht[HttpContext.Current.Session.SessionID] = id.ToString();
            HttpContext.Current.Application.Lock();
            HttpContext.Current.Application[appKey] = ht;
            HttpContext.Current.Application.UnLock();
        }

        /// <summary>
        /// 判断某用户是否已经登陆 
        /// </summary>
        /// <param name="id">为用户ID或用户名这个必须是用户的唯一标识</param>
        /// <returns>flase为没有登陆 true为被迫下线</returns>
        public bool IsLogin(object id)
        {
            bool flag = false;            
            System.Collections.Hashtable ht = (System.Collections.Hashtable)HttpContext.Current.Application[appKey];
            if (ht != null)
            {
                System.Collections.IDictionaryEnumerator IDE = ht.GetEnumerator();
                while (IDE.MoveNext())
                {
                    //找到自己的登陆ID
                    if (IDE.Value.ToString().Equals(id.ToString()))
                    //if (IDE.Key.ToString().Equals(HttpContext.Current.Session.SessionID))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 判断某用户是否已经登录，并强迫登录用户下线。可以放到页面基类里面实时监测。
        /// </summary>
        /// <param name="id">为用户ID或用户名这个必须是用户的唯一标识</param>
        /// <returns>flase为没有登陆 true为被迫下线</returns>
        public bool ValidateForceLogin()
        {
            bool flag = false;            
            System.Collections.Hashtable ht = (System.Collections.Hashtable)HttpContext.Current.Application[appKey];
            if (ht != null)
            {
                System.Collections.IDictionaryEnumerator IDE = ht.GetEnumerator();
                while (IDE.MoveNext())
                {
                    //找到自己的登陆ID
                    if (IDE.Key.ToString().Equals(HttpContext.Current.Session.SessionID))
                    {
                        //判断用户是否被注销
                        if (IDE.Value.ToString().Equals(logout))
                        {
                            ht.Remove(HttpContext.Current.Session.SessionID);
                            HttpContext.Current.Application.Lock();
                            HttpContext.Current.Application[appKey] = ht;
                            HttpContext.Current.Application.UnLock();
                            HttpContext.Current.Session.RemoveAll();
                            //HttpContext.Current.Response.Write("<script type='text/javascript'>alert('你的帐号已在别处登陆，你被强迫下线！')</script>");                            
                            flag = true;
                        }
                        break;
                    }
                }
            }
            return flag;
        }


    }
}