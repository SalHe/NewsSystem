using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whu.BLM.NewsSystem.Shared;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;
using Whu.BLM.NewsSystem.Server.Data.Context;
using System.Text.RegularExpressions;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public NewsSystemContext NewsSystemContext { get; set; }

        public UserController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
        }
        [HttpGet]
        /// <summary>
        /// �����û���Ϣ��
        /// </summary>
        public User GetUserInfo(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id);
                return u.First();
            }
            catch
            {
                return new User();
            }

        }
        [HttpDelete]
        /// <summary>
        /// ɾ��ָ��ID���û���
        /// </summary>
        public bool DeleteUser(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id);
                NewsSystemContext.Users.Remove(u.FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPut]
        /// <summary>
        /// �޸��û���Ϣ��
        /// </summary>
        public bool ChangeUserInfo(int id, string newUserName, string newPassWord)
        {
            if (JudgeLegality(newUserName) == false || JudgeLegality(newPassWord) == false)
                return false;
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id).FirstOrDefault();
                u.Username = newUserName;
                u.Password = MD5(newPassWord);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("register")]
        /// <summary>
        /// �û�ע�ᡣ
        /// </summary>
        public bool UserRegistration(string username,string password)
        {
            if (JudgeLegality(username) == false || JudgeLegality(password) == false)
                return false;
            try
            {
                User newUser = new User();
                newUser.Username = username;
                newUser.Password = MD5(password);
                int newid = new Random((int)DateTime.Now.Ticks).Next(0,65535);
                while(NewsSystemContext.Users.Where(u => u.Id == newid).ToList().Count > 0)
                {
                    newid = new Random((int)DateTime.Now.Ticks).Next(0,65535);
                }
                newUser.Id = newid;
                NewsSystemContext.Users.Add(newUser);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserViewed")]
        /// <summary>
        /// ��ʷ��¼��
        /// </summary>
        public bool Viewed(int idOfUser, int idOfNews)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                u.VisitedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfUser).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserLiked")]
        /// <summary>
        /// �û�ϲ�������š�
        /// </summary>
        public bool Liked(int idOfUser, int idOfNews)
        {
            try
            {
                NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault().LikedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserDisliked")]
        /// <summary>
        /// �û���ϲ�������š�
        /// </summary>
        public bool Disliked(int idOfUser, int idOfNews)
        {
            try
            {
                NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault().DislikedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [NonAction]
        /// <summary>
        /// ���û�����MD5����
        /// </summary>
        public static string MD5(string Text)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(Text);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }
        [NonAction]
        /// <summary>
        /// �����ַ����Ϸ���
        /// </summary>
        public bool JudgeLegality(string str)
        {

            string pattern = @"^[A-Za-z0-9]+$";  //@��˼����ת�壬+ƥ��ǰ��һ�λ��Σ�$ƥ���β

            Match match = Regex.Match(str, pattern);

            return match.Success;

        }
    }
}