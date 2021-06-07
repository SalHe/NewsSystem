using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Server.Domain.VO
{
    public class NewsApiModel
    {
        /// <summary>
        /// 带有页码的新闻结构体（建立特定新闻与页码的映射关系）。
        /// </summary>
        public struct NewsWithPage
        {
            public News News { get; set; }
            public int Page { get; set; }
        }

        public class ReleaseModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string AbstractContent { get; set; }
            public string OriginalUrl { get; set; }
            public NewsCategory Category { get; set; }
        }

        public class ChangeModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public NewsCategory Category { get; set; }
        }
    }
}