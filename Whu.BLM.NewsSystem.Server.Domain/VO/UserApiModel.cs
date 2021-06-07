namespace Whu.BLM.NewsSystem.Server.Domain.VO
{
    public class UserApiModel
    {
        public class UpdateUserInfoModel
        {
            public string NewUserName { get; set; }
            public string NewPassWord { get; set; }
        }

        public class RegisterModel
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }
    }
}