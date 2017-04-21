
namespace Maticsoft.Payment.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : IUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户Email
        /// </summary>
        public string Email { get; set; }
    }
}