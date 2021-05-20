using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Whu.BLM.NewsSystem.Shared.Entity.Content;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;

namespace Whu.BLM.NewsSystem.Server.Context
{
    public class NewsSystemContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }

        public NewsSystemContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 用户实体的约束
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<User>().HasMany(u => u.VisitedNews).WithMany(n => n.VisitedBy);    // 浏览过的的新闻
            modelBuilder.Entity<User>().HasMany(u => u.LikedNews).WithMany(n => n.LikedBy);      // 喜欢的新闻
            modelBuilder.Entity<User>().HasMany(u => u.DislikedNews).WithMany(n => n.DislikedBy);   // 不喜欢的新闻

            // 新闻
            modelBuilder.Entity<News>().Property(n => n.Title).IsRequired();
            modelBuilder.Entity<News>().Property(n => n.OringinUrl).IsRequired();
            modelBuilder.Entity<News>().HasIndex(n => new { n.OringinUrl, n.Title }).IsUnique(); // 保证(URL, 标题)的唯一性

            // 新闻分类
            modelBuilder.Entity<NewsCategory>().Property(c => c.Name).IsRequired();
            modelBuilder.Entity<NewsCategory>().HasIndex(c => c.Name).IsUnique();    // 类别名唯一

        }
    }
}
