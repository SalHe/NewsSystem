using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Server.Domain.VO
{
    public class NewsApiModel
    {
        /// <summary>
        /// 去掉IList<User>的Model
        /// </summary>
        public struct NewsWithoutList
        {
            public int Id { get; set; }

            /// <summary>
            /// 新闻标题。
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 摘要内容。
            /// </summary>
            public string AbstractContent { get; set; }

            /// <summary>
            /// 源URL。
            /// </summary>
            public string OringinUrl { get; set; }

            /// <summary>
            /// 类别。
            /// </summary>
            public NewsCategory NewsCategory { get; set; }
        }
        public struct CategoryWithoutNews
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class ReleaseModel
        {
            public string Title { get; set; }
            public string AbstractContent { get; set; }
            public string OriginalUrl { get; set; }
            public int Category { get; set; }
        }

        public class ChangeModel
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public int Category { get; set; }
        }
    }
}