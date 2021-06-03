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

        /// <summary>
        /// �����û���Ϣ��
        /// </summary>
        [HttpGet]
        public User getUserInfo(int id)
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

        /// <summary>
        /// ɾ��ָ��ID���û���
        /// </summary>
        [HttpDelete]
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

        /// <summary>
        /// �޸��û���Ϣ��
        /// </summary>
        [HttpPut]
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

        /// <summary>
        /// �û�ע�ᡣ
        /// </summary>
        [HttpPost]
        public bool UserRegistration(string username, string password)
        {
            if (JudgeLegality(username) == false || JudgeLegality(password) == false)
                return false;
            try
            {
                User newUser = new User();
                newUser.Username = username;
                newUser.Password = MD5(password);
                int newid = new Random((int) DateTime.Now.Ticks).Next(0, 65535);
                while (NewsSystemContext.Users.Where(u => u.Id == newid).ToList().Count > 0)
                {
                    newid = new Random((int) DateTime.Now.Ticks).Next(0, 65535);
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

        /// <summary>
        /// �û���¼��
        /// </summary>
        [HttpGet("Login")]
        public bool UserLogin(string id, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ���û�����MD5����
        /// </summary>
        [NonAction]
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

        /// <summary>
        /// �����ַ����Ϸ���
        /// </summary>
        [NonAction]
        public bool JudgeLegality(string str)
        {
            string pattern = @"^[A-Za-z0-9]+$"; //@��˼����ת�壬+ƥ��ǰ��һ�λ��Σ�$ƥ���β

            Match match = Regex.Match(str, pattern);

            return match.Success;
        }
    }
}