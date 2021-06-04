using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        [HttpGet("login")]
        public async Task<string> Login(string username, string password, string? returnUrl = null)
        {
            if (returnUrl != null) return "请先登录";

            if (ValidateAuthentication(username, password))
            {
                var claims = new List<Claim>
                {
                    new("user", username),
                    new("role", username == "admin" ? "admin" : "user"),
                };
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme, "user", "role")));
                return "登录成功！";
            }

            return "登录失败！";
        }

        [HttpGet("denied")]
        public string Denied()
        {
            return "您的权限不足！";
        }

        [HttpGet("logout")]
        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync();
            return "登出成功";
        }
        [NonAction]
        private bool ValidateAuthentication(string username, string password)
        {
            return !string.IsNullOrEmpty(username) && password.Equals("123456");
        }
    }
}