using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Whu.BLM.NewsSystem.Server.Constants;
using Whu.BLM.NewsSystem.Server.Data.Context;
using Whu.BLM.NewsSystem.Server.Exceptions;
using Whu.BLM.NewsSystem.Server.Model;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly NewsSystemContext _newsSystemContext;
        private readonly JwtOptions _jwtOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AccountController(NewsSystemContext newsSystemContext, JwtOptions jwtOptions,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _newsSystemContext = newsSystemContext;
            _jwtOptions = jwtOptions;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        // TODO 调整参数传递方式
        [HttpGet("login")]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            if (returnUrl != null) return Unauthorized("请先登录");

            try
            {
                var user = await ValidateAuthentication(username, password);
                var jwtToken = new JwtSecurityToken(
                    _jwtOptions.Issuer,
                    _jwtOptions.Audience,
                    new[]
                    {
                        new Claim(ClaimTypes.Role, user.UserGroup.ToString()),
                        new Claim(MyClaimTypes.Username, user.Username),
                        new Claim(MyClaimTypes.UserId, user.Id.ToString())
                    },
                    null, DateTime.Now.Add(TimeSpan.FromDays(3)),
                    new SigningCredentials(_jwtOptions.SecurityKey, SecurityAlgorithms.HmacSha256));
                string jwt = _jwtSecurityTokenHandler.WriteToken(jwtToken);
                return Ok(jwt);
            }
            catch (UserNotFoundException e)
            {
                return BadRequest("找不到用户");
            }
            catch (PasswordErrorException e)
            {
                // 虽然可以从信息推断出密码错了
                // 但是我们还是不明文提示
                return BadRequest("用户信息错误");
            }
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
        private async Task<User> ValidateAuthentication(string username, string password)
        {
            var user = await _newsSystemContext.Users.FirstAsync(u => u.Username.Equals(username));
            if (user == null) throw new UserNotFoundException();
            if (!user.Password.Equals(UserController.MD5(password))) throw new PasswordErrorException();
            return user;
        }

        [HttpGet("info")]
        public string TestGetInfo()
        {
            return "可以匿名访问的信息";
        }

        [HttpGet("user_info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<string> TestGetUserInfo()
        {
            var user = await HttpContext.GetCurrentUser(_newsSystemContext.Users);
            return $"普通用户信息：{user.Username}";
        }

        [HttpGet("admin_info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<string> TestGetAdminInfo()
        {
            var user = await HttpContext.GetCurrentUser(_newsSystemContext.Users);
            return $"管理员信息：{user.Username}";
        }
    }
}