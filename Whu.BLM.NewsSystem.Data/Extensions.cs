using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Whu.BLM.NewsSystem.Data
{
    public static class Extensions
    {
        public static DbContextOptionsBuilder UseDbConfig(this DbContextOptionsBuilder options, DbConfig config)
        {
            
            switch (config.DbType.ToUpper())
            {
                case "MYSQL":
                    return options.UseMySql(config.ConnectionString, new MySqlServerVersion(config.ServerVersion));
                default:
                    throw new ArgumentException("不支持的数据库类型");
            }
            return options;
        }
    }
}