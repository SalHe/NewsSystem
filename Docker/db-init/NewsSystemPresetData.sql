create schema cs_news_sys_db;
use cs_news_sys_db;

-- table

create table if not exists NewsCategories
(
    Id   int auto_increment
        primary key,
    Name varchar(255) not null,
    constraint IX_NewsCategories_Name
        unique (Name)
);

create table if not exists News
(
    Id              int auto_increment
        primary key,
    Title           varchar(255) not null,
    AbstractContent longtext     null,
    OringinUrl      varchar(255) not null,
    NewsCategoryId  int          null,
    constraint IX_News_OringinUrl_Title
        unique (OringinUrl, Title),
    constraint FK_News_NewsCategories_NewsCategoryId
        foreign key (NewsCategoryId) references NewsCategories (Id)
);

create index IX_News_NewsCategoryId
    on News (NewsCategoryId);

create table if not exists Users
(
    Id        int auto_increment
        primary key,
    Username  varchar(255) not null,
    Password  longtext     not null,
    UserGroup int          not null,
    constraint IX_Users_Username
        unique (Username)
);

create table if not exists NewsUser
(
    VisitedById   int not null,
    VisitedNewsId int not null,
    primary key (VisitedById, VisitedNewsId),
    constraint FK_NewsUser_News_VisitedNewsId
        foreign key (VisitedNewsId) references News (Id)
            on delete cascade,
    constraint FK_NewsUser_Users_VisitedById
        foreign key (VisitedById) references Users (Id)
            on delete cascade
);

create index IX_NewsUser_VisitedNewsId
    on NewsUser (VisitedNewsId);

create table if not exists NewsUser1
(
    LikedById   int not null,
    LikedNewsId int not null,
    primary key (LikedById, LikedNewsId),
    constraint FK_NewsUser1_News_LikedNewsId
        foreign key (LikedNewsId) references News (Id)
            on delete cascade,
    constraint FK_NewsUser1_Users_LikedById
        foreign key (LikedById) references Users (Id)
            on delete cascade
);

create index IX_NewsUser1_LikedNewsId
    on NewsUser1 (LikedNewsId);

create table if not exists NewsUser2
(
    DislikedById   int not null,
    DislikedNewsId int not null,
    primary key (DislikedById, DislikedNewsId),
    constraint FK_NewsUser2_News_DislikedNewsId
        foreign key (DislikedNewsId) references News (Id)
            on delete cascade,
    constraint FK_NewsUser2_Users_DislikedById
        foreign key (DislikedById) references Users (Id)
            on delete cascade
);

create index IX_NewsUser2_DislikedNewsId
    on NewsUser2 (DislikedNewsId);

create table if not exists __EFMigrationsHistory
(
    MigrationId    varchar(150) not null,
    ProductVersion varchar(32)  not null,
    primary key (MigrationId)
);


-- For EF Core

INSERT INTO cs_news_sys_db.__EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20210520140546_Init', '5.0.6');

-- DATA

-- 新闻类别
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (10, '体育');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (15, '健康');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (14, '教育');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (16, '新闻');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (13, '旅游');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (12, '科技');
INSERT INTO cs_news_sys_db.NewsCategories (Id, Name) VALUES (11, '财经');
-- 用户
INSERT INTO cs_news_sys_db.Users (Id, Username, Password, UserGroup) VALUES (21129, 'testuser', 'e10adc3949ba59abbe56e057f20f883e', 0);
INSERT INTO cs_news_sys_db.Users (Id, Username, Password, UserGroup) VALUES (51895, 'salhe', 'e10adc3949ba59abbe56e057f20f883e', 1);
-- 新闻

INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (11, '111易建联郭艾伦等老将退出 中国男篮的机会在哪里？', '由于不同程度的伤病，易建联、郭艾伦、王哲林3位主力暂时告别中国男篮，将不会随队出征即将开始的亚洲杯预选赛以及奥运会落选赛。对于处在新老交替周期的中国男篮来说，这既是艰难的选择，也是迎难而上、培养新生力量的契机。', 'https://www.163.com/sports/article/GC6SBL3U0005877V.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (12, '同一招连打4次!雄鹿竟束手无策 布朗89秒轰出8分', '北京时间6月11日，雄鹿与篮网的系列赛第三场在密尔沃基进行，篮网大比分2比0领先。首节雄鹿完美30比11，但第二节，情况有些不妙了。', 'https://www.163.com/sports/article/GC6S6TC70005877U.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (13, '英格尔斯：我和乔治之前的对战已经是旧事 那时候还是爵士打雷霆', '直播吧6月11日讯 今日10点，爵士主场迎战快船，赛前爵士球员英格尔斯接受了记者的采访。', 'https://www.163.com/dy/article/GC6RHDGI0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (14, '字母哥连送两记蛮暴劈扣 雄鹿开局9-0打停篮网', '比赛首节开局将近4分钟的时间内，雄鹿打出了一波9-0的攻击波将篮网打停。这波小高潮中，字母哥已经贡献了两记精彩的单臂劈扣，拿下了7分3篮板。', 'https://www.163.com/dy/article/GC6RHGKS0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (15, '斯基拉：蒙扎不会续约巴洛特利，他将成为自由球员', '直播吧6月11日讯 据意大利记者斯基拉透露，蒙扎决定不与巴洛特利续约，这名意大利前锋将以自由球员的身份离队。', 'https://www.163.com/dy/article/GC6RHD7J0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (16, '尴尬了！字母哥超长前摇两罚不中 第二罚直接三不沾', '直播吧6月11日讯 雄鹿VS篮网G3首节，字母哥罚球三不沾。

　　当时字母哥造成犯规走上罚球线，依旧是长时间的准备动作，第一罚偏出之后，第二罚字母哥直接罚出一个三不沾。', 'https://www.163.com/dy/article/GC6RHCJS0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (17, '意媒：米兰决定在季前赛评估卡尔达拉状况，再决定其未来', '直播吧6月11日讯 据米兰新闻网报道，AC米兰已经决定暂时无视那些针对卡尔达拉的报价，球队将在季前赛期间对球员作出评估。', 'https://www.163.com/dy/article/GC6RHDG20529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (18, '字母哥&米德尔顿二人包揽雄鹿首节全部得分', '直播吧6月11日讯 雄鹿VS篮网的比赛首节结束，雄鹿暂时领先。', 'https://www.163.com/dy/article/GC6RHCRK0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (19, '维尔通亨：比利时不是欧洲杯大热门，法国和英格兰才是', '直播吧6月11日讯 比利时国脚维尔通亨和默滕斯日前出席了球队的欧洲杯赛前发布会，他们表示法国和英格兰是夺冠大热门。', 'https://www.163.com/dy/article/GC6RHCL80529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (20, '泰晤士：英超下赛季将采用加粗的VAR划线来避免“体毛”越位', '直播吧6月11日讯 据《泰晤士报》报道，英超下赛季将对VAR越位引入更粗的划线方式，以应对备受争议的越位判罚。', 'https://www.163.com/dy/article/GC6RHDD80529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (21, '帕托：现在的米兰能让人充满梦想，红黑军团会越来越好', '直播吧6月11日讯 在现场直播中，米兰旧将帕托谈到了关于米兰的话题，他表示现在的米兰可以让球迷们充满梦想。', 'https://www.163.com/dy/article/GC6RHCLL0529AQIE.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (22, '美国通胀超预期落地 全球市场不怕了 A股跟吗？', '上证指数本月第二次报收于3600点上方，就随即迎来新的考验。

本周三，A股放量上涨，成交金额再超万亿元。北上资金净流入68.21亿元，本周净流入59.64亿元，本月净流入96.64亿元。', 'https://www.163.com/money/article/GC6RDF2E00259DLP.html', 11);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (23, '苏宁易购1950万质押股遭强平 交易所监管函来了', '“被强制执行30亿”风波还未平，6月10日晚间，因触发协议约定违约条款，苏宁电器收深交所监管函。苏宁电器为苏宁易购第二大流通股股东。', 'https://www.163.com/money/article/GC6R314G00259DLP.html', 11);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (24, '网传中国最大房地产商碧桂园旗下一高管出大事 受贿金额惊人', '6月9日，一则“碧桂园文商旅集团原总经理张强涉嫌索贿受贿”的消息在网络流传。这显示，以管理严格著称的碧桂园集团也出现腐败情况，容易导致形象不佳。', 'https://www.163.com/dy/article/GC6833VG0511BBQE.html', 11);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (25, '大动作！这一次，西安正式向上海广州成都看齐', '西安与咸阳的关系，一直是无数西安人的一个心结。但自2017年西安代管西咸新区之后，它们的关系，再未获得突破性进展。', 'https://www.163.com/dy/article/GC5VFJTP0519FM90.html', 11);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (26, '？？？谷歌用AI设计AI芯片，6小时完成工程师数月工作', '6月11日消息，谷歌称其正在使用机器学习系统帮助工程师设计新一代机器学习芯片。谷歌工程师表示，算法设计的芯片质量和人工设计“相当”甚至“还要更好”，但完成速度要快得多。谷歌表示，人工智能可以在不到6小时的时间内完成人工需要数月时间完成的芯片设计工作。', 'https://www.163.com/tech/article/GC6PTB4500097U7T.html', 12);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (27, '未来公开课｜刘科：电池技术没突破，一味追求发展电动车会很危险？？', '“如果电网里67%的电力还是火电（靠烧煤发电），电动车不能称之为清洁能源”、“光顾发展电动车不考虑电池回收的问题，每个人会付出代价”、“电不好储，储氢也不那么简单”。', 'https://www.163.com/tech/article/GC50GBLI00099BD3.html', 12);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (28, '亚马逊要求办公室员工9月复岗 每周可远程工作2天', '6月11日消息，美国当地时间周四，亚马逊宣布了经过调整的员工复岗计划，将施行远程和现场办公相结合的混合办公模式。亚马逊宣布，员工将在9月初复岗，每周可远程工作两天。', 'https://www.163.com/tech/article/GC6Q4JIC00097U7R.html', 12);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (29, '欧盟反垄断"铁娘子"：希望苹果能允许第三方应用商店', '6月11日消息，欧盟反垄断监管机构负责人玛格丽特·维斯特格(Margrethe Vestager)说，她希望推动苹果和其他科技公司按照新法规开放他们控制的市场。', 'https://www.163.com/tech/article/GC6QEHPO00097U7T.html', 12);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (30, '广西防城港的古渔村，保留着原始的风貌，历史也有几百年', '我们来到广西防城港的簕山古渔村，喜欢吃海产的人来这里体验丰富的美食是不错的选择，渔村里的海产是非常的多，且丰富的。这个渔村是保留着原始的风貌，历史也有几百年了。渔村的地理位置是在海边这一带，在渔村里是完全能享受得到在海边游玩的那种沐浴阳光的感觉。', 'https://www.163.com/dy/article/GC6GGH2E05520J4X.html', 13);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (31, '几内亚湾，大自然之外的，非洲千年文明之美', '在赤道与本初子午线交汇的地方，就是非洲最大的海湾——大西洋东岸的几内亚湾。15世纪欧洲殖民者入侵后，这里成为西非与美洲之间的贸易通道，是掠夺者心中的“奴隶海岸”、“黄金海岸”、“象牙海岸”和“胡椒海岸”。独特的地理位置与多样的地形孕育着热带雨林、热带草原和热带高原、热带沙漠气候，由此也孕育了丰富的矿产、农产品和海产品，尤其是丰富的石油资源，至今仍是世界石油巨头们的必争之地。', 'https://www.163.com/dy/article/GC6946IA05371O9Z.html', 13);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (32, '中部最后的一个香格里拉，就在恩施境内，景美空气好，村民还长寿', '说到香格里拉，对于很多旅游爱好者来说，简直是人间天堂，每次前往香格里拉打卡，都会有不同的体验和感觉。而在中国的中部，还有一个地方也被称为是中部最后的一个香格里拉，这个地方就在恩施境内。', 'https://www.163.com/dy/article/GC65V7LG05481PLX.html', 13);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (33, '泉州考试院回应“考生骑车摔伤错失高考”：手肘骨折，只能明年再来', '高考第二天，6月8日上午，福建泉州考生张明（化名）因共享单车爆胎，摔倒在地致其手肘骨折，错失高考。', 'https://www.163.com/edu/article/GC6VO51Q00297VGM.html', 14);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (34, '花几万元咨询填志愿 能换来更好的人生吗', '高考结束了，考生们将迎来对未来关系重大的志愿填报。据媒体报道，随着多地高考改革落地，志愿填报变得更加复杂，相关服务市场也因此更趋火爆，市面上出现众多以 “专业生涯规划”“人工智能大数据服务”“一对一辅导”为噱头的高考志愿填报服务，价格数千到数万元不等，最高者甚至可达10万元。', 'https://www.163.com/edu/article/GC6VJUVQ00297VGM.html', 14);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (35, '教育部公布2021年高考网上咨询周时间：6月22日至28日', '根据教育部“2021年高考护航行动”安排部署，结合全国各省(区、市)高考志愿填报时间安排，“2021年高考网上咨询周”活动将于6月22日至28日在“阳光高考”信息平台举行。平台将提供文字问答和视频直播两种方式，每日文字问答咨询时间为9点至17点，是否安排视频直播及视频直播时间由高校自行决定，鼓励高校积极采用视频直播方式开展招生宣传和咨询。教育部要求各高校要积极主动加强线上招生宣传，为考生、家长提供准确权威、周到细致的志愿填报指导服务。', 'https://www.163.com/edu/article/GC6UUGB200297VGM.html', 14);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (36, '最新研究表明：早睡早起1小时，能大大降低抑郁症风险', '近日，一项发表在国际顶尖医学期刊 JAMA 子刊 JAMA Psychiatry 的一项新研究表明，早起床一小时可以将一个人患重度抑郁症的风险降低23%。', 'https://www.163.com/dy/article/GC53TDD0053296CT.html', 15);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (37, '生从何来，死往何去：毛囊干细胞来源二三事', '2021年6月9日，日本理化学研究所生物系统动力学研究中心Hironobu Fujiwara研究组发文题为Tracing the origin of hair follicle stem cells，对毛囊干细胞的来源进行了追溯。', 'https://www.163.com/dy/article/GC50CO9Q0532BT7X.html', 15);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (38, '在国内打除皱针，不了解这些可能会毁脸！', '说到肉毒毒素，爱美的宝贝们肯定都不陌生，在医美抗衰界它可是大名鼎鼎的“除皱大师”。', 'https://www.163.com/dy/article/GC4T43UV0514L4CB.html', 15);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (39, '这样的运动饮料不是真正的运动饮料，很多人还在喝', '其实，市面上售卖的有真正的运动饮料，也有很多维生素素饮料、能量饮料、电解质饮料等。如果说有假，后提的三种特殊功能饮料还真不能算为运动饮料。', 'https://www.163.com/dy/article/GC4T224G05118405.html', 15);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (40, '字母米豆各轰15+7!雄鹿首节30-11 711合计12中3', '北京时间6月11日，雄鹿与篮网的系列赛第三场在密尔沃基进行，篮网大比分2比0领先。回到主场，雄鹿的状态非常火热，首节他们30比11大比分领先。', 'https://www.163.com/sports/article/GC6R5I6E0005877U.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (41, '两极分化！篮网9人薪水都不到300万 三巨头1.3亿直播吧6月11日讯 季后赛至今，篮网势如破竹，目前他们在东部半决赛大比分2比0领先雄鹿。', '直播吧6月11日讯 季后赛至今，篮网势如破竹，目前他们在东部半决赛大比分2比0领先雄鹿。', 'https://www.163.com/sports/article/GC6NQ3OF00059ADR.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (42, '篮网前7中0有点懵…字母隔扣KD+格里芬露死亡之瞪', '雄鹿今天的打法非常清楚，上来先让米德尔顿低位打布朗，第一球就转身跳投命中。然后让字母哥抓转换进攻，虽然他第一次上篮没进，但第二次就完成了暴扣。然后是字母哥和霍乐迪挡拆，字母哥顺下接球造了格里芬犯规。', 'https://www.163.com/sports/article/GC6Q5ANR0005877U.html', 10);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (46, '测试添加新闻2021年6月17日 10:10:13', '测试添加新闻2021年6月17日 10:10:13测试添加新闻2021年6月17日 10:10:13测试添加新闻2021年6月17日 10:10:13测试添加新闻2021年6月17日 10:10:13', 'http://www.baidu.com/qqqq', 11);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (47, '香港保安局局长:提醒港人移民前应三思 以免后悔莫及', null, 'https://www.163.com/news/article/GD25JSQI0001899O.html', null);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (48, '阿富汗现状：上午国民军举AR拍照 下午塔利班举AK拍照', null, 'https://www.163.com/dy/article/GDGBH80E051481US.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (49, '"最牛县城"昆山：人均GDP超过了一些西方发达国家', null, 'https://www.163.com/dy/article/GDGENCLF0514BE2Q.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (50, '他戴墨镜口罩出席发布会遮得严严实实 化名"云警官"', null, 'https://www.163.com/news/article/GDGC81NJ0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (51, '坑惨!国有大行员工诈骗41名亲友3300万 炒股亏1400万', null, 'https://www.163.com/dy/article/GDGBII8P0530NLC9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (52, '转发破万！杨利伟文章入选课本：我以为自己要牺牲了', null, 'https://www.163.com/news/article/GDG5PIKL00018AP2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (53, '捷克天灾 台官员发文"我们都是捷克人" 遭网友怒骂', null, 'https://www.163.com/dy/article/GDG8T9L80514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (54, '美媒:美军从阿富汗撤军后6个月内 阿政府或完全垮台', null, 'https://www.163.com/dy/article/GDG8HUDK0543B4S9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (55, '美记者：拜登出访一个字眼出现12次 针对中俄', null, 'https://www.163.com/news/article/GDG8DAFN00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (56, '美国公布UFO报告 NASA局长看保密版后"汗毛立起来了"', null, 'https://www.163.com/dy/article/GDEMDMU2051492T3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (57, '拟任市委书记 到岗后他将成全国地级市最年轻书记', null, 'https://www.163.com/dy/article/GDEVRIGU0530M570.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (58, '4年前向清华写信求"一间陋宿供母子济身"的学生毕业', null, 'https://www.163.com/news/article/GDG52A9O00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (59, '乌克兰撤回加拿大发起的反华声明联署 直接打脸美国', null, 'https://www.163.com/dy/article/GDG74HP60543B4S9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (60, '广州隔离病房高考生成绩出炉！一人超本科线近70分', null, 'https://www.163.com/dy/article/GDFRDU2J05129QAF.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (61, '媒体：喊教皇去加拿大道歉 特鲁多什么意思？', null, 'https://www.163.com/dy/article/GDG5LP1T0512DU6N.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (62, '小伙理发被邀请免费修眉结果结账5000块:一根眉毛88', null, 'https://www.163.com/dy/article/GDF0UDAH0514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (63, '牛弹琴：乌克兰给中国送来特别礼物 外交部加班回应', null, 'https://www.163.com/dy/article/GDG3CGB3052182V3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (64, '任正非:不能因美打压而不向其学习 华为有个最大缺点', null, 'https://www.163.com/news/article/GDG19QFO0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (65, '尿毒症患者承包60亩地种树：不能等死 恶化了就给老婆', null, 'https://www.163.com/dy/article/GDEN1NRT051492T3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (66, '女车主新车被开1600公里 销售：我开的 你还得付我钱', null, 'https://www.163.com/dy/article/GDF7PME70534A4S1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (67, '日本天皇罕见"表态"犯大忌 媒体：极为大胆的"异动"', null, 'https://www.163.com/dy/article/GDEVNFUF05504DOQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (68, '国家安全部重要活动 隐蔽战线英雄的名字首次被官方提及', null, 'https://www.163.com/dy/article/GDF8V97P051482MP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (69, '乌克兰撤销联署反华共同发言 外交部回应', null, 'https://www.163.com/dy/article/GDF83FQ20512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (70, '监管力度层层加码 比特币进入冰河时期', null, 'https://www.163.com/dy/article/GDF6V49005506O99.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (71, '胡锡进:中国必须笑到最后 熬死急疯想搞垮我们的力量', null, 'https://www.163.com/dy/article/GDEV5E9C05504DP0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (72, '白宫新闻秘书称印是"非常重要伙伴" 网友:不要相信美国', null, 'https://www.163.com/dy/article/GDEV27VK0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (73, '美国国务卿:在"抗中"问题上 马克龙想法和我完全一样', null, 'https://www.163.com/news/article/GDEV03390001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (74, '是否延续上任处长"鹰派"作风？新任港警"一哥"回应', null, 'https://www.163.com/dy/article/GDEKD7SG051482MP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (75, '美媒称中方曾要求美国"删除"新冠基因序列 媒体驳斥', null, 'https://www.163.com/dy/article/GDEJ1QB905504DP0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (76, '小伙胸痛1周看急诊拒绝手术离开:凭啥你说心梗就心梗', null, 'https://www.163.com/dy/article/GDEIO5QK051492LM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (77, '拜登会见阿富汗总统：你们需自己决定国家的未来', null, 'https://www.163.com/news/article/GDEFBV0F0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (78, '女孩高考体检查出癌症 被诊断活不过2年 如今成绩出了', null, 'https://www.163.com/news/article/GDDSR8NM00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (79, '福建一高考考点提前160秒打铃 副校长等3人被撤职', null, 'https://www.163.com/dy/article/GDE8F8VQ0530NLC9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (80, '河南712分考生查分当天在网吧打游戏 父亲：他智商高', null, 'https://www.163.com/news/article/GDDVA6DV0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (81, '美国正式公布UFO报告：143起事件无法解释', null, 'https://www.163.com/news/article/GDE2E5720001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (82, '特斯拉召回28万辆车并致歉 系统问题会致车速突增', null, 'https://www.163.com/news/article/GDE0KQFT0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (83, '12箱快递被顺丰送垃圾场 维权者微博已消失 网友怒了', null, 'https://www.163.com/dy/article/GDBUGVCS0514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (84, '加拿大又发现751个无名坟墓 特鲁多道歉:惊恐和羞愧', null, 'https://www.163.com/dy/article/GDDNMSG60514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (85, '俄警告英国下次不排除炸船 英首相:那是乌克兰海域', null, 'https://www.163.com/news/article/GDDQPCDM0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (86, '男子12箱快递被顺丰送垃圾场 亲友：他流泪喘不过气', null, 'https://www.163.com/dy/article/GD9VDIJ1053469LG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (87, '12箱快递被顺丰送垃圾场 当事人:被逼无奈估值500万', null, 'https://www.163.com/news/article/GDDN3PU90001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (88, '获刑22年半！美国警察“跪杀”黑人案宣判 拜登发声', null, 'https://www.163.com/dy/article/GDDLA60D0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (89, '特鲁多：教皇应该来加拿大向原住民道歉', null, 'https://www.163.com/news/article/GDDLR6VK0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (90, '花近1万!女子吃代餐3个月瘦20斤 后反弹50斤至180斤', null, 'https://www.163.com/dy/article/GDBQ222J051492T3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (91, '谭耀宗：比起自己被美国制裁 我更关心另一个点 ', null, 'https://www.163.com/news/article/GDDHQUGJ0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (92, '钟南山聊广州抗疫:怀疑动物引起传播 3天抓70只老鼠', null, 'https://www.163.com/news/article/GDDGKJ690001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (93, '王毅：中国在联合国的这一票 永远属于发展中国家', null, 'https://www.163.com/dy/article/GDDFP32B05504DPG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (94, '毒贩发错货把大麻运上海被警方阻截 总价值超1.5亿', null, 'https://www.163.com/dy/article/GDBUO2J60514EGPO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (95, '越南刚拿到中国疫苗就公然违反双方共识 中方回应', null, 'https://www.163.com/dy/article/GDBR6GNG0515CCSC.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (96, '老三考626分！四胞胎高考成绩揭晓 曾因早产抢救20天', null, 'https://www.163.com/news/article/GDDD5GLJ0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (97, '新任香港保安局局长:比起国家安全 他国制裁不值一提', null, 'https://www.163.com/dy/article/GDBOCOV70514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (98, 'NASA局长：支持"永久排斥中国参与国际空间站合作"', null, 'https://www.163.com/news/article/GDDAHG3K0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (99, '74岁母亲坐副驾帮儿子"押车":我也去看看这花花世界', null, 'https://www.163.com/dy/article/GDCGJNJD0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (100, '钟南山最新发声：国产常用疫苗对德尔塔变异株有效', null, 'https://www.163.com/dy/article/GDCJS31D0512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (101, '河南武术馆火灾:18名遇难者为学员 建筑系村民自建房', null, 'https://www.163.com/news/article/GDCFHFFD00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (102, '广州芳村26天成功围剿"德尔塔" 的姐在车上睡20多天', null, 'https://www.163.com/dy/article/GDC9S9JE0514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (103, '港警"一哥"履新职！要离开工作34年的警队他动情了', null, 'https://www.163.com/dy/article/GDC88CGL051283GO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (104, '《柳叶刀》新冠委员会：世卫赴华专家退出溯源调查', null, 'https://www.163.com/news/article/GDCC5DDA0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (105, '国家安全部副部长近期频繁露面 重点提到两类人', null, 'https://www.163.com/dy/article/GDC20QJA051482MP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (106, '耐克CEO称耐克属于中国、为中国而生 网友评论亮了', null, 'https://www.163.com/dy/article/GDBQIRB8053469LG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (107, '42岁的她 成全国地级市最年轻市长', null, 'https://www.163.com/dy/article/GDCARU33051482MP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (108, '拜登就香港《苹果日报》停刊发表声明 外交部回应', null, 'https://www.163.com/news/article/GDC7Q25S0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (109, '场面尴尬！拜登记者会疑忘提塌楼惨案 哈里斯悄悄提醒', null, 'https://www.163.com/dy/article/GDBG7JNA0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (110, '四川双胞胎姐姐考696分 妹妹考655分：想复读', null, 'https://www.163.com/news/article/GDC6S4O90001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (111, '中方坚定支持阿根廷对马岛主权要求 阿外长表态', null, 'https://www.163.com/dy/article/GDBSGRT10514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (112, '老人小区摔跟头住院18天花万元 物业:给你100买鸡补补', null, 'https://www.163.com/dy/article/GDBT33KK0530U7LS.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (113, '邓亚萍连线安徽截肢考生：清华见！到时候去现场接你', null, 'https://www.163.com/news/article/GDBUD71K0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (114, '京杭大运河北京段全线旅游通航', null, 'https://bj.news.163.com/photoview/75UK0438/3833.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (115, '北京今晨有雾最高温33℃ 明后两天多雷阵雨', null, 'https://www.163.com/news/article/GDFPUTC70001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (116, '关注疫情：北京新增境外输入"1+2" 无本地新增', null, 'https://www.163.com/news/article/GDG0895H00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (117, '男子运毒500克进京，警方抓获贩卖者及其上家', null, 'https://www.163.com/dy/article/GDGA2PB30514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (118, '下周三下班，哪些路已经交通管制？提前掌握下', null, 'https://www.163.com/dy/article/GDG6U6AL0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (119, '开工212部完工119部 今年老楼加装电梯完成过半', null, 'https://www.163.com/dy/article/GDG2QB4P0514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (120, '从燕郊将可乘地铁到北京！平谷线河北段正式开工', null, 'https://www.163.com/dy/article/GDFU225S0514NR4I.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (121, '大运河北京段今起全线旅游通航 购票方法、票价→', null, 'https://www.163.com/dy/article/GDFU1K880514NR4I.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (122, '北大红楼将于29日正式开放 每日可预约接待1000人', null, 'https://www.163.com/dy/article/GDGBTKDP0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (123, '7月5日北京尾号限行重新轮换 提前预告', null, 'https://www.163.com/dy/article/GDCJA6MC05279GM2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (124, '北京高考传闻400分就能上清华北大', null, 'https://www.163.com/dy/article/GDD632AM0545PGSB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (125, 'CBA名宿告别北京首钢 或加盟北控男篮', null, 'https://www.163.com/dy/article/GDDHBNK30529CTQM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (126, '十大经济体首都GDP排名，北京排第六', null, 'https://www.163.com/dy/article/GDBI6QET0519ACLG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (127, '乒乓球大小的雹子，北京昌平啊！', null, 'https://v.163.com/static/1/VQCFFC31P.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (128, '北京普高招生人数要比职高多！', null, 'https://www.163.com/dy/article/GDD8AQQB05454HHT.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (129, '传北京国安俱乐部股权变更成功', null, 'https://www.163.com/dy/article/GDCN5HOF05525PS5.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (130, '北京中考首日，二外附中考点学生有序测温入场', null, 'https://www.163.com/dy/article/GDCLQLQP0525NEF5.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (131, '我国哪里的人最爱吃猪下水，老北京人上榜', null, 'https://www.163.com/dy/article/GDCLIEL10517MMR9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (132, '首轮集中供地满月，北京操盘手到了最难的时候', null, 'https://www.163.com/dy/article/GDCK1GOC0539QLKD.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (133, '北京地铁：从战备需要到世界领先', null, 'https://www.163.com/dy/article/GDCJMCS40512PC69.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (134, '北京一记者实地报道高温天气，不料身后传来巨响', null, 'https://www.163.com/dy/article/GDCJAV380545VALR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (135, '北京退休老太的清晨时光', null, 'https://v.163.com/static/1/VSCEPE00G.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (136, '2021年北京中考拉开帷幕', null, 'https://bj.news.163.com/photoview/75UK0438/3832.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (137, '北京大面积雨水上线营业 7级大风来袭', null, 'https://www.163.com/dy/article/GDASS9R30514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (138, '新款复兴号智能动车组上线 北京站首开G字头列车', null, 'https://www.163.com/dy/article/GDBNNAK20512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (139, '明日摇号！北京超50万家庭申请小客车指标', null, 'https://www.163.com/news/article/GDB2K14500019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (140, '6月24日北京新增1例境外输入确诊病例', null, 'https://www.163.com/news/article/GDARDGGD00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (141, '北京高招录取工作将于7月6日至7月30日进行', null, 'https://www.163.com/news/article/GDB3QLG000019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (142, '高考分数线出炉！北京本科录取控制分数线400分', null, 'https://www.163.com/news/article/GDB2S16000019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (143, '今天国体周边多条公交线路调整 部分地铁站封闭', null, 'https://www.163.com/news/article/GDBAQ3SE00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (144, '庆建党百年 北京47条公交线路调整 多条地铁线封站', null, 'https://bj.news.163.com/21/0625/11/GDB8DLVI04388CSB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (145, '人民日报曝光392所“野鸡大学”！北京竟有这么多', null, 'https://www.163.com/dy/article/GDB0D9T2052193GU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (146, '女生犹豫报清华还是北大 清华招生老师出绝招', null, 'https://www.163.com/news/article/GD9HSGB30001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (147, '清华大学毕业生就业率近五年稳定在98%左右 ', null, 'https://www.163.com/edu/article/GD3HIL2700297VGM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (148, '北京朝阳停车管理公司原董事长吴春生被双开', null, 'https://www.163.com/news/article/GDBKAJD200019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (149, '杭州父母骑自行车20天 到北京参加儿子毕业典礼', null, 'https://v.163.com/static/1/VLCD8QO40.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (150, '探访中国共产党历史展览馆主题邮局', null, 'https://bj.news.163.com/photoview/75UK0438/3831.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (151, '合作抗疫：驰援巴基斯坦抗疫物资捐赠仪式在京举行', null, 'https://www.163.com/dy/article/GDB2LV380514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (152, '西四环主路两车别车斗气 连续超速被刑拘', null, 'https://www.163.com/dy/article/GDAUIMRO0514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (153, '办实事：北京老旧小区停车改造', null, 'https://bj.news.163.com/21/0625/11/GDB8GAQE04388CSB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (154, '垃圾不分类吃罚单？城管队员进小区边盯桶边答疑', null, 'https://bj.news.163.com/21/0625/11/GDB8KABL04388CSB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (155, '146项改革 环境京津跨境贸易便利化再上新台阶', null, 'https://www.163.com/dy/article/GDB0QGIB051496PC.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (156, '蔡奇：打造全球最有影响力的服务贸易展会', null, 'https://bj.news.163.com/21/0625/11/GDB822VD04388CSB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (157, '朝阳门微花园示范中心亮相?推进老城胡同绿色微更新', null, 'https://bj.news.163.com/21/0625/09/GDB2LG17043899E4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (158, '迎接建党百年 长安街立体花坛成热门“打卡地”', null, 'https://www.163.com/dy/article/GDAQMLKR051496PC.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (159, '北京中考作文题公布：袁隆平进入作文题', null, 'https://www.163.com/news/article/GD8IMJDQ00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (160, '注意绕行！北京交警提醒：明天这些道路禁止通行', null, 'https://www.163.com/news/article/GD921VA900019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (161, '近期天安门广场、故宫开放时间公布', null, 'https://www.163.com/dy/article/GD82UK4G0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (162, '为迎接党的生日 市属公园21组主题花坛亮相', null, 'https://www.163.com/dy/article/GD7RK84H051496PC.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (163, '中考首日：北京交警已解决9起考生、家长求助', null, 'https://www.163.com/dy/article/GD8RKSRV0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (164, '台“外长”称：大陆不放弃武力，相信是真的', null, 'https://www.163.com/news/article/GDBAS43800019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (165, '香港《苹果日报》今起停刊，英国欧盟又跳出来', null, 'https://www.163.com/news/article/GD8FL8JG00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (166, '“五毒俱全”的市长被查后，这个东北城市两任市委书记落马', null, 'https://www.163.com/news/article/GD8CRD0500019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (167, '雁默：为《苹果日报》送终，哭墓都凑不齐五子', null, 'https://www.163.com/news/article/GD8AGR9500018AP1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (168, '刚刚，深圳市宝安区新冠肺炎疫情防控指挥部发布通告', null, 'https://www.163.com/news/article/GD1MC31I00019SNS.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (169, '一周反腐看点：中纪委建议“倒查20年”的内蒙古，一煤炭局局长敛财2亿多', null, 'https://www.163.com/news/article/GCTP3KQ600018AP2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (170, '中国空间站的“大力神臂”到底牛在哪？美军司令都“忌惮”它的威力 ', null, 'https://www.163.com/news/article/GCSQP8T00001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (171, '2021年6月16日外交部发言人赵立坚主持例行记者会', null, 'https://www.163.com/news/article/GCL1MQ980001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (172, '赵立坚回应美国捐特多政府80瓶辉瑞疫苗：中国已经捐了10万剂', null, 'https://www.163.com/dy/article/GCL0U2V005504DPG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (173, '中方表示在载人航天领域与多国展开合作，港媒发现：没有美国', null, 'https://www.163.com/news/article/GCKPQ2M90001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (174, '赵立坚：美欧不是自身智商出了问题 就是低估了中国人的智商', null, 'https://www.163.com/news/article/GCKHSRSA0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (175, '台胞来大陆接种疫苗能否不入境隔离？大陆会否认可台产疫苗？国台办回应', null, 'https://www.163.com/news/article/GCK5V9OC00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (176, '亮相！3位航天员在玻璃房内接受采访，这一点与往年见面会不同', null, 'https://www.163.com/news/article/GCK27QKA00018AP2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (177, '中方表明严正立场！', null, 'https://www.163.com/news/article/GCBV93L800018AOQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (178, '杨洁篪正告美方：世界上只有一个体系、一种秩序、一套规则', null, 'https://www.163.com/news/article/GC9U544I00018AOQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (179, '宋鲁郑：两周内三通电话，中美进入“虚打实谈”阶段？', null, 'https://www.163.com/news/article/GC9EK4E500018AP1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (180, '神舟十二号载人飞船整装待发 三名航天员本次出征将解锁哪些新技能？ ', null, 'https://www.163.com/news/article/GC4RL0JK0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (181, '中国驻欧盟使团发言人：任何干涉中国内政的举动都会遭到中国人民坚决有力的反击 ', null, 'https://www.163.com/news/article/GC47BTGR0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (182, '2021年6月7日外交部发言人汪文斌主持例行记者会', null, 'https://www.163.com/news/article/GBVJ1JPN0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (183, '李铁变阵激活国足！锁定小组第二，最后两场拿4分就出线', null, 'https://www.163.com/news/article/GBUVN6EA00018AOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (184, '今天早上，三名美国参议员，搭乘美军运输机抵达台湾省窜访！', null, 'https://www.163.com/dy/article/GBQ79J1O05504DP0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (185, '中国科兴疫苗昨日通过世卫专家组评审? 外交部回应', null, 'https://www.163.com/news/article/GBH09A0Q0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (186, '美209名议员要求中国为美疫情60万死亡负责 中方回应', null, 'https://www.163.com/news/article/GBBPI7VL0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (187, '外交部:美企图用情报力量搞"溯源调查" 动机一目了然', null, 'https://www.163.com/news/article/GB1I6R7K0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (188, '包商银行原董事长：“领导，你给我们撑住腰，等老了一块玩。”监管者求“围猎”获利7亿余元', null, 'https://www.163.com/news/article/GAR6816T00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (189, '台当局坚持“鸵鸟政策”不封城，民众却“自行封城” ', null, 'https://www.163.com/news/article/GA7559J000019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (190, '疫情突然、密接波及多省份，“零号病例”到底是谁？', null, 'https://www.163.com/news/article/GA4H3VAI00018AOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (191, '张文木做客观察者网：解决台湾问题，现在各种条件都越来越成熟了', null, 'https://www.163.com/news/article/G9SI9IMK0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (192, '2021年5月10日外交部发言人华春莹主持例行记者会', null, 'https://www.163.com/news/article/G9LT84660001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (193, '郑若麟：战狼外交，只是“口水战”吗？', null, 'https://www.163.com/news/article/G9KBEDLH00018AP1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (194, '印度新冠疫情恶化是否会波及中国？李兰娟院士这样说  ', null, 'https://www.163.com/news/article/G9FMGA1100019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (195, '继续挑衅！美国国务卿布林肯呼吁允许台湾参与世卫大会', null, 'https://www.163.com/news/article/G9FHUMHS0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (196, '第一次收钱手抖 之后就淡定从容 最终自食恶果————要闻——中央纪委国家监委网站', null, 'https://www.163.com/news/article/G9AASA6L00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (197, '一周外军军评：摆烂的“汉光37号”军演', null, 'https://www.163.com/news/article/G90APGKN0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (198, '超重磅！中央最新定调：防止以学区房等名义炒作房价！10大要点值得关注，解读来了', null, 'https://www.163.com/news/article/G8TF03G900019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (199, '美方称中俄利用疫情展开针对西方的信息战 外交部回应', null, 'https://www.163.com/news/article/G8RUNOS60001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (200, '为何未邀请印度参加六国应对新冠疫情会议 外交部回应', null, 'https://www.163.com/news/article/G8KH7RF80001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (201, '“港独”周竖峰出逃加拿大，曾辱骂内地生为“支那人”', null, 'https://www.163.com/news/article/G8GJUT8700019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (202, '金灿荣：美国反华共识超越“懂王”时代？纯属瞎耽误功夫', null, 'https://www.163.com/news/article/G8G9U8A900018AP1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (203, '王毅国务委员同美国对外关系委员会视频交流致辞全文', null, 'https://www.163.com/dy/article/G8ALM73U0514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (204, '美国一宗教委员会建议美国政府公开表示如中方继续打压宗教自由 美将不会参加北京冬奥会 赵立坚回应', null, 'https://www.163.com/news/article/G8A2KTVM0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (205, '美反华议员再抛“台湾国际参与法案”，专家：意图转移国内矛盾焦点 ', null, 'https://www.163.com/dy/article/G83DGIN10514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (206, '台湾将军撂出一句狠话，我们竟无法反驳……', null, 'https://www.163.com/news/article/G81GVS0A0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (207, '美情报总监:中国对美构成的威胁无以伦比 赵立坚回应', null, 'https://www.163.com/news/article/G7MHN0G90001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (208, '临危受命！超30人被拿下的当天上午，湖北女将跨省救火  ', null, 'https://www.163.com/dy/article/G7LPG08G051482MP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (209, '“80后”正厅董玉毅，新职明确 ', null, 'https://www.163.com/news/article/G7KTU8BH00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (210, '台湾省某人说若开战“台会战斗到底”，国台办回应', null, 'https://www.163.com/news/article/G7I482I60001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (211, '官员伙同司机受贿1024万 得知将被查抓紧时间疯狂', null, 'https://www.163.com/news/article/G7HNCN4900019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (212, '阿里巴巴回应垄断被罚182.28亿：诚恳接受 坚决服从', null, 'https://www.163.com/news/article/G77BCI5A0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (213, '台湾遭遇"56年来最严重"旱灾 金门靠大陆"有水喝"', null, 'https://www.163.com/news/article/G772P6BO0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (214, '香港《苹果日报》停刊，拜登又“施压”', null, 'https://www.163.com/news/article/GDAVPVLG00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (215, '细节曝光！美国幕后逼以色列站队，在人权问题上攻击中国', null, 'https://www.163.com/news/article/GD885M9U0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (216, '加拿大挑头“调查”新疆，90多国声援中方', null, 'https://www.163.com/news/article/GD85SNES00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (217, '张维为：完全有可能把中国政治故事讲得更透彻、更精彩', null, 'https://www.163.com/news/article/GD6ETP720001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (218, '德国工业联合会主席声称：若中国逾越“人权红线”，德国绝不能回避对抗', null, 'https://www.163.com/news/article/GD6AKS530001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (219, '美国从阿富汗“清算式”撤军，对中国会有多大影响？ ', null, 'https://www.163.com/news/article/GD60VAJF0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (220, '斯德哥尔摩法院维持瑞典政府“华为5G禁令”，华为爱立信同时发声', null, 'https://www.163.com/news/article/GD5TVII90001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (221, '中国呼吁：彻查加拿大对原住民犯下的所有罪行', null, 'https://www.163.com/news/article/GD5KAFE10001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (222, '深度 中方罕见劝公民撤离，美方主动约见阿总统，阿富汗局势有多危急？', null, 'https://www.163.com/news/article/GD4J0ALQ00018AP2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (223, '俄大使返美复工先见崔天凯，介绍俄美领导人峰会情况', null, 'https://www.163.com/news/article/GD3P6MDE00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (224, '美媒称拜登政府考虑禁止从新疆进口多晶硅，美智库警告：认清现实', null, 'https://www.163.com/news/article/GD3KL53T00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (225, '“默克尔准接班人”拉舍特：不要与中国发生冷战，中国也是合作伙伴', null, 'https://www.163.com/news/article/GD3KBVNO00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (226, 'CNN被称“中国新闻网”，那我是谁？', null, 'https://www.163.com/news/article/GD3F848200018AOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (227, '新加坡开打中国疫苗 供不应求', null, 'https://www.163.com/news/article/GCUATDNR00019K82.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (228, '外媒：印度新冠灾难，暴露了美国领导下“四方”联盟中最薄弱的环节', null, 'https://www.163.com/news/article/GCT0723V0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (229, '新冠病毒溯源，美国必须给全世界一个交代', null, 'https://www.163.com/news/article/GCSR4C8200018AP1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (230, '澳大利亚政府宣布，将就与中国葡萄酒关税问题向世贸组织提起申诉', null, 'https://www.163.com/news/article/GCSHGDKG0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (231, '空军飞行员成功驱离外机，“如果开干那我就干”', null, 'https://www.163.com/news/article/GCSH0BN70001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (232, '包庇乱港黑暴，加拿大双标露馅儿了', null, 'https://www.163.com/news/article/GCSEGPQT0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (233, '“普拜会”现场一度混乱 俄美记者团为抢机位互有推搡', null, 'https://www.163.com/news/article/GCL63O9P0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (234, '美国立卫生研究院最新发现：2019年12月新冠已在美传播', null, 'https://www.163.com/news/article/GCKQH53A0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (235, '普拜会登场，美媒：拜登壮足了胆，普京气定神闲', null, 'https://www.163.com/news/article/GCKOS7TV0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (236, '丹尼尔·博奇科夫：美欧让中俄更加团结，这怪不得别人', null, 'https://www.163.com/news/article/GCJRH1PH00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (237, '认为印度医疗不错的中国旅店老板，入境遭印军逮捕后被怀疑是中国间谍  ', null, 'https://www.163.com/news/article/GCBSQOH40001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (238, '美正式发布抗衡"一带一路"新倡议 美媒:挑衅性提议', null, 'https://www.163.com/news/article/GCBS5I330001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (239, '菲外长：G7G8G9峰会都没邀请我，很高兴去25世纪大都市重庆参会 ', null, 'https://www.163.com/news/article/GC9DB9PS00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (240, '美防长下达“内部指令”：展开多项行动抗衡“头号挑战”中国', null, 'https://www.163.com/news/article/GC4U8DMC0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (241, '美国务卿参加国会2022财年预算听证会，又搬出了中国', null, 'https://www.163.com/news/article/GC44RNUL0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (242, '万万没想到，外媒的“阴间滤镜”居然被这群大象踩碎 | 锐参考', null, 'https://www.163.com/news/article/GC441U800001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (243, '印度与奥运赞助商李宁解约：照顾本国民众情绪', null, 'https://www.163.com/news/article/GC43SS900001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (244, '加媒：美加曾密谈，想用“暂缓起诉孟晚舟”换“双麦克”', null, 'https://www.163.com/news/article/GBVEQ8SI0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (245, '匈牙利总理回怼德外长：欧盟涉港声明就算重投一百次，也是一样的结果', null, 'https://www.163.com/news/article/GBVATPIC00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (246, '克里姆林宫：美国制裁俄罗斯90多次，没用', null, 'https://www.163.com/news/article/GBV8OB9U0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (247, '拜登将首登国际舞台，美国还“回得来”吗？', null, 'https://www.163.com/news/article/GBV78M1D0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (248, '多方紧盯中国东盟外长会，印媒：这是向美日印澳发出信号 ', null, 'https://www.163.com/dy/article/GBV0O94M0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (249, '特朗普卸任以来“最华丽的亮相”，网友：裤子好像穿反了 ', null, 'https://www.163.com/dy/article/GBTIH3UE0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (250, 'G7达成“历史性协议”：同意15%“全球最低企业税率”', null, 'https://www.163.com/news/article/GBPRV66N0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (251, '不装了？特鲁多声称冬奥会是“向中国施压的机会”', null, 'https://www.163.com/news/article/GBPQTD570001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (252, '俄罗斯总统普京：美国正以自信且坚定的步伐，走在苏联的老路上', null, 'https://www.163.com/news/article/GBO203KJ00019B3E.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (253, '中美6天2通电话，两个细节值得注意', null, 'https://www.163.com/news/article/GBGMCML80001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (254, '约翰逊：G7峰会讨论搞新冠疫苗护照，要为全球抗疫“立规则”', null, 'https://www.163.com/news/article/GBBBO7220001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (255, '世卫大会：日本为“核废水”辩解，中方严正驳斥', null, 'https://www.163.com/news/article/GB9603UB0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (256, '纵观季后赛字母哥：巴特勒，你欠我的要用4-0来还', null, 'https://www.163.com/news/article/GB8R8MCB00018AOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (257, '拜登欲砸1740亿美元扶持电动车：中国领先，忍不了', null, 'https://www.163.com/news/article/GAC2HLC10001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (258, '专访马克斯·布鲁门特尔：想用新疆伤害中国，却腐蚀了美国人', null, 'https://www.163.com/news/article/GABH1N1H0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (259, '内塔尼亚胡发帖感谢25国支持以色列，波黑澄清：我们并没有', null, 'https://www.163.com/dy/article/GAAODQPQ0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (260, '“当今世界需要一个希特勒”，CNN撰稿人一条推文，激怒很多人', null, 'https://www.163.com/news/article/GA6OUCA000018AOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (261, 'CNN要帮祸害中国的恐怖分子“洗白”？！', null, 'https://www.163.com/news/article/GA6L9SOM0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (262, '我外交官反讽：为朋友庆祝“越狱”成功 ', null, 'https://www.163.com/news/article/GA4DG4VM0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (263, '印度政客：新冠病毒有权像我们一样生存', null, 'https://www.163.com/news/article/GA472RRT0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (264, '这是最适合普通人的高考志愿指南', null, 'https://www.163.com/data/article/GDBRGGDE000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (265, '他说：我只是想当一个女人', null, 'https://www.163.com/renjian/article/GDBBTD24000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (266, '生活叫醒了岳父的网红梦', null, 'https://www.163.com/renjian/article/GD8O8AVE000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (267, '小城女同学们的“保险梦”', null, 'https://www.163.com/renjian/article/GD3JI9NE000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (268, '千禧年番禺厂妹打工记事', null, 'https://www.163.com/renjian/article/GD3FB3E1000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (269, '一碗螺蛳粉，我们是彼此的侠客', null, 'https://www.163.com/renjian/article/GD0R4SCF000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (270, '海底捞涨价，我是捞不动了', null, 'https://www.163.com/data/article/GCUU48DK000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (271, '“0元家装”庞氏骗局崩塌始末', null, 'https://www.163.com/renjian/article/GCP6Q03E000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (272, '缉毒3年，他说他还想回边境', null, 'https://www.163.com/renjian/article/GCMGO5EQ000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (273, '赵家堡往事', null, 'https://www.163.com/renjian/article/GCK4CSJC000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (274, '温州从来不缺“江南皮革厂”', null, 'https://www.163.com/renjian/article/GCHGNQSK000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (275, '县城里的民营航运江湖', null, 'https://www.163.com/renjian/article/GCHEP4KQ000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (276, '下一个一线城市，谁最有希望', null, 'https://www.163.com/data/article/GCCKMVA1000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (277, '年轻的文学大师们，在闲鱼告别北上广深', null, 'https://www.163.com/data/article/GCA0QKUV000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (278, '在父亲灵堂上被驱赶的男孩', null, 'https://www.163.com/renjian/article/GC720A6N000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (279, '抓住强奸幼女的凶手之后，她被村里人孤立了', null, 'https://www.163.com/renjian/article/GC4HG78V000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (280, '打脸名家的出版社编辑，被绑上了“贼船”', null, 'https://www.163.com/renjian/article/GC1SC36N000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (281, '被两个孙辈捆在北京7年，她决定不辞而别', null, 'https://www.163.com/renjian/article/GBVF9KKH000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (282, '做住家教师的我，窥见了精英阶层的另类鸡娃', null, 'https://www.163.com/renjian/article/GBVAABLH000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (283, '沙发缝隙的杂志里，藏着我与青春的一场缠斗', null, 'https://www.163.com/renjian/article/GBSV924I000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (284, '中国最魔幻的县城，两个字', null, 'https://www.163.com/data/article/GBO2MQP8000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (285, '坐拥3家公司的富豪，把妻子当移民“工具人“', null, 'https://www.163.com/renjian/article/GBL5VUB6000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (286, '老来拾荒，他还指望着儿子浪子回头', null, 'https://www.163.com/renjian/article/GBID118J000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (287, '老爹一辈子的摇滚梦，终结在这座西北小城', null, 'https://www.163.com/renjian/article/GBFT1DOT000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (288, '癌症确诊后，我想起了鱼缸下的那包老鼠药', null, 'https://www.163.com/renjian/article/GBDA5193000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (289, '超过3亿中国人，在做这件要命的事情', null, 'https://www.163.com/data/article/GBBJGTCT000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (290, '父亲在批发市场的铺子关了，他说怪不了淘宝', null, 'https://www.163.com/renjian/article/GBASIQES000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (291, '月入5万的直播梦碎，被割韭菜的180个家人', null, 'https://www.163.com/renjian/article/GBAMK62I000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (292, '老板最不能拒绝的请假理由，它排第一', null, 'https://www.163.com/data/article/GB10240P000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (293, '38岁的我，是不是找不到工作了', null, 'https://www.163.com/renjian/article/GB0H6CH7000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (294, '十年无性婚姻，一场蓄谋已久的骗局', null, 'https://www.163.com/renjian/article/GATS7DIH000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (295, '野生动物摄影师：我拍的其实只是自己的欲望', null, 'https://www.163.com/renjian/article/GARC42N5000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (296, '抑郁15年才确诊的她，断送了父母的好晚景', null, 'https://www.163.com/renjian/article/GAR9GOHV000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (297, '祖宗菜谱里的渔溪鲶鱼，151年后又出现了', null, 'https://www.163.com/renjian/article/GAOP8R75000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (298, '安乐死的挚友，你走时很漂亮', null, 'https://www.163.com/renjian/article/GAGR0CA7000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (299, '中国最有钱大学排行，北大只能排第三', null, 'https://www.163.com/data/article/GAF0DE8J000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (300, '全班人的出气筒，消失在高考前', null, 'https://www.163.com/renjian/article/GAEDBJ72000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (301, '从开户开始，他们就进入了黄金市场投机的陷阱', null, 'https://www.163.com/renjian/article/GABVS33A000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (302, '上海买房，就像参与一场赌博', null, 'https://www.163.com/renjian/article/GA9AU5KO000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (303, '武汉最后一家老澡堂，曾经也被叫做"新乐园"', null, 'https://www.163.com/renjian/article/GA6Q5PO0000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (304, '砸了12万学费，离高薪“咨询师”还有多远？', null, 'https://www.163.com/renjian/article/GA6O98FD000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (305, '中国大学最劝退的专业，医学只能排第二', null, 'https://www.163.com/data/article/G9VIBF6O000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (306, '中年失独，这对夫妻的艰难自救', null, 'https://www.163.com/renjian/article/G9V04RRC000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (307, '挤进双一流A+学科当讲师，她成了内卷时代的落伍者', null, 'https://www.163.com/renjian/article/G9SH5R2Q000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (308, '好不容易逃离了乡镇税务所，我又想回去了', null, 'https://www.163.com/renjian/article/G9PSHC7U000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (309, '7刀之后，她终于逃离那个家', null, 'https://www.163.com/renjian/article/G9NAFBG8000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (310, '望庄拆迁往事：拆迁把什么东西都拆了出来', null, 'https://www.163.com/renjian/article/G9L1HKD6000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (311, '疯狂鼓吹房价的地产大V，我是他背后的枪手', null, 'https://www.163.com/renjian/article/G9KLHRMJ000181RV.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (312, '比尔盖茨离婚，富豪们的婚姻保鲜期有多长', null, 'https://www.163.com/data/article/G9G956MK000181IU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (313, '逃出体制，去互联网公司996的中年人', null, 'https://www.163.com/renjian/article/G9D3UI7K000181RK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (314, '美国军舰进入黑海 俄罗斯密切监视', null, 'https://www.163.com/news/article/GDGAJQ050001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (315, '“中国第一将军县”的“红色密码”', null, 'https://www.163.com/dy/article/GDGD3T4V0512D3VJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (316, '媒体：20年后，塔利班胜利了吗？', null, 'https://www.163.com/dy/article/GDGBH80E051481US.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (317, '中国人民解放军海军博物馆在青岛开馆', null, 'https://www.163.com/dy/article/GDG51T2H051497H3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (318, '加油！陆军战士负重20斤千米越海泅渡', null, 'https://www.163.com/dy/article/GDF55O8G0514R9KQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (319, '中央红军长征出发地为何选在于都？', null, 'https://www.163.com/dy/article/GDF9M9GF0514R9NP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (320, '俄国防部：英军的挑衅是“史诗级惨败” ', null, 'https://www.163.com/news/article/GDEOS12S0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (321, '拜登会见阿富汗总统：撤军后会继续援助', null, 'https://www.163.com/news/article/GDEFBV0F0001899O.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (322, '俄军火力驱离英舰让人想起33年前的一件往事', null, 'https://www.163.com/dy/article/GDE7SU9T051497H3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (323, '编号第001128号！这是红军总司令的党证', null, 'https://www.163.com/dy/article/GDGAEFJC051485AA.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (324, '美欲建太平洋海军特遣部队 专注应对竞争', null, 'https://www.163.com/dy/article/GDFTNAM90514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (325, '缅甸军方突袭曼德勒一武装组织 抓获组织成员12人', null, 'https://www.163.com/dy/article/GDEOQFLP051497H3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (326, '虐待囚犯、滥杀无辜！英军虚伪假面昭然若揭', null, 'https://www.163.com/dy/article/GDEM9VF70514R9M0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (327, '美军公布UFO相关报告 承认超出现有认知', null, 'https://www.163.com/dy/article/GDE58OKT0534695U.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (328, '印度防长：要把航母母港打造成亚洲第一军港', null, 'https://www.163.com/dy/article/GDDLTOTI0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (329, '我国空军史上首位飞行教官，来自粤北群山', null, 'https://www.163.com/dy/article/GDDP9GEE055004XG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (330, '东部战区03式远火摧毁导弹车画面曝光', null, 'https://www.163.com/dy/article/GDC79G960515DHOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (331, '黄海之滨，陆军炮兵防空兵学院学员进行实弹射击考核', null, 'https://www.163.com/dy/article/GDC0DBNB0514TTKS.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (332, '多支解放军合成部队试装新型单兵携行具', null, 'https://www.163.com/dy/article/GDBSG4190515DHOR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (333, '美军专家”为爱“向真主党泄密 被判23年监禁', null, 'https://www.163.com/dy/article/GDDLTP3N0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (334, '俄罗斯海空军在地中海举行演习', null, 'https://www.163.com/dy/article/GDCLEV900514R9P4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (335, '俄副外长：将通过包括军事手段在内一切手段捍卫俄边界', null, 'https://www.163.com/dy/article/GDC73MDG0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (336, '美军女兵被发现在营房身亡 与半年前一案相似', null, 'https://www.163.com/dy/article/GDC20I7B0514R9L4.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (337, '拜登一边让美军快撤 一边给阿富汗开空头支票', null, 'https://www.163.com/dy/article/GDC176V6053469LG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (338, '200多对军地新人举行集体婚礼', null, 'https://www.163.com/dy/article/GDDJCT4H0514R9M0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (339, '国产远火精度:不用覆盖 直接"点杀"目标', null, 'https://war.163.com/photoview/4T8E0001/2311846.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (340, '美日韩三军联合演习，日本战机空中加油', null, 'https://war.163.com/photoview/4T8E0001/2311847.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (341, '俄罗斯航母维修工期又延期到2023年', null, 'https://war.163.com/photoview/4T8E0001/2311845.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (342, '个头不小！美军为核潜艇装填鱼叉反舰导弹', null, 'https://war.163.com/photoview/4T8E0001/2311844.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (343, '美国和印度联合海上演习，"里根"号出动', null, 'https://war.163.com/photoview/4T8E0001/2311843.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (344, '中方直接点名英国，坚决支持阿根廷对马岛主权要求', null, 'https://www.163.com/dy/article/GDB4G3V10514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (345, '法军“阵风”战机39小时飞抵1.7万公里外基地', null, 'https://www.163.com/dy/article/GDB1EU880514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (346, '英媒观察员解读英国驱逐舰事件：第二次"冷战"打响', null, 'https://www.163.com/dy/article/GDAVKIIF0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (347, '英国绝密特种部队信息意外泄露，任务包括渗透中俄', null, 'https://www.163.com/dy/article/GDAQP56U05504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (348, '美国有人鼓吹派装甲旅到台湾?美媒:若成真影响重大', null, 'https://www.163.com/dy/article/GDAQEBTD05504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (349, '为对付中国美军想损招：让轻型两栖舰混入民船之中', null, 'https://www.163.com/dy/article/GDB97BEL05504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (350, '英首相称军舰闯入克里米亚水域无错，俄方威胁炸船', null, 'https://www.163.com/dy/article/GDB8NDA505504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (351, '俄罗斯发新视频：俄方发出警告后舰炮连续三次开火', null, 'https://www.163.com/dy/article/GDB7KCLK0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (352, '国防部：上半年军事训练全军弹药消耗大幅增加 ', null, 'https://www.163.com/war/article/GDB40REP000181KT.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (353, '白宫发言人终于透露美国从阿富汗撤军原因！', null, 'https://www.163.com/dy/article/GDB3FA6905504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (354, '日本“水陆机动团”将与美澳英部队演练两栖作战', null, 'https://www.163.com/dy/article/GDB39MO80514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (355, '美国国务院同意向菲律宾出售F-16战机和导弹', null, 'https://www.163.com/dy/article/GDBF07QO05504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (356, '美官员:俄美军控谈判不应限于核武器,不会要挟中国', null, 'https://www.163.com/dy/article/GDB8RKJ205504DOH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (357, '俄两架米格-31K派往叙利亚，可挂载高超音速导弹', null, 'https://www.163.com/dy/article/GDB7KUFU0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (358, '英国放慢采购F-35战机:维护成本高 自研导弹难集成', null, 'https://www.163.com/dy/article/GDB70RFI0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (359, '俄媒：俄海军在太平洋演练新战法，包括多种方式消灭假想敌航母', null, 'https://www.163.com/dy/article/GDB6CJE60514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (360, '俄国防部:俄轰炸机在夏威夷海域"击沉"假想敌航母', null, 'https://www.163.com/war/article/GDB44MTN000181KT.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (361, '五角大楼职员警告拜登：不要为台湾卷入战争', null, 'https://www.163.com/dy/article/GDB21RH40514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (362, '俄高官对英国发出警告：下次直接对着"目标"轰炸', null, 'https://www.163.com/dy/article/GDB21EOH0514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (363, '美军反恐培训文件竟将社会主义者与恐怖分子划等号', null, 'https://www.163.com/dy/article/GDB0SJA20514R9OJ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (364, '防内卷!任正非发声:依然要向美国学习', null, 'https://www.163.com/money/article/GDG01FQI00259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (365, '科大讯飞产品因监管下架 盈利模式也遭质疑', null, 'https://www.163.com/dy/article/GDE76Q9V052184FQ.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (366, 'PK!同仁堂市值不足片仔癀零头令股东痛惜', null, 'https://www.163.com/dy/article/GDDPV7C905199FB7.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (367, '图解一周牛熊股：“金针菇蘸酱”5连板', null, 'https://www.163.com/dy/article/GDFVKVRK05198CJN.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (368, '太突然！特斯拉又有事，遭1700亿巨头清仓！', null, 'https://www.163.com/money/article/GDG5ETE300259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (369, '券商被称为"国内绝对龙头" 回应：不！我真不是！', null, 'https://www.163.com/money/article/GDG4AP9M00258152.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (370, '"女人的茅台"8.8亿元进军肉毒素 股价3个月已翻倍', null, 'https://www.163.com/money/article/GDG45T6D00258152.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (371, '骗钱炒股、赌博!国有大行员工假借高息理财"杀熟"', null, 'https://www.163.com/money/article/GDG07EU000259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (372, '上市首日暴跌 生鲜电商第一股成“韭菜第一股”？', null, 'https://www.163.com/money/article/GDFVVPOU00259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (373, '发改委有关负责人：预计7月份煤价将进入下降通道', null, 'https://www.163.com/money/article/GDG1OKBL00259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (374, '1.6万亿资金罕见大动作！牛股扎堆行业首夺冠军', null, 'https://www.163.com/dy/article/GDEKRUBV0512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (375, '浙江女首富变“女首负”：身家百亿 今留一地鸡毛', null, 'https://www.163.com/dy/article/GDE6RMKR0531MYZO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (376, '三年全国店面数量翻25倍！剧本杀“冰火两重天”', null, 'https://www.163.com/dy/article/GDF21BVM05199NPP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (377, 'ofo退还押金还需再等500年！法院：找不到他们', null, 'https://www.163.com/dy/article/GDEKHMH50519D4UH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (378, '致使国家利益遭受特别重大损失，曹纪生被“双开”', null, 'https://www.163.com/dy/article/GDEJNR470530NLC9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (379, '又一资本大佬“跑路”了？卷款30亿！6万人被骗', null, 'https://www.163.com/dy/article/GDE221BR0531MYZO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (380, '一年来首单！广发证券投行"解禁"后新IPO项目现身', null, 'https://www.163.com/money/article/GDG0D51400259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (381, '这家网红基金高层连换两人!年内15家公募换"老大"', null, 'https://www.163.com/money/article/GDG09PGB00259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (382, '16亿播放量！“洗脑”神曲火爆全网…', null, 'https://www.163.com/money/article/GDG3V4V500258152.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (383, '4天暴涨超46% 券商：你是国内绝对龙头！公司回应', null, 'https://www.163.com/dy/article/GDF403O00539LWPU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (384, '危机意识下的万科:人事调整 分拆区域 押注新业务', null, 'https://www.163.com/dy/article/GDEHUKS805199DKK.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (385, '任正非最新发声:华为需要多基因融合突变防止内卷', null, 'https://www.163.com/money/article/GDG2HEKG00259DLP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (386, '600亿估值！又一次，喜茶压奈雪一头', null, 'https://www.163.com/dy/article/GDCKUU2L05199NPP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (387, '互联网大厂“第二总部“加速布局武汉', null, 'https://www.163.com/dy/article/GDG6UH0N05199NPP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (388, '一年暴涨680亿，重庆啤酒为何耍起酒疯？', null, 'https://www.163.com/dy/article/GDG55GH00519SUH3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (389, '打脸美联储？美银：未来四年美国将迎来“超级通胀”', null, 'https://www.163.com/dy/article/GDG2RFTP05198NMR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (390, '比特币遭遇狙击！各地纷纷出手，矿场集体断电，所有矿场被关', null, 'https://www.163.com/dy/article/GDG1KULJ0519D4UH.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (391, '2021新一线城市榜单：一线城市已趋于稳定，二线城市大洗牌', null, 'https://www.163.com/dy/article/GDFDCS7L0539QLKD.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (392, '“虹桥”如何能救上海机场？', null, 'https://www.163.com/dy/article/GDEJ8NK70539LWQ1.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (393, '时隔两周，又有上市公司股东违规减持，理由竟是...', null, 'https://www.163.com/dy/article/GDEIESK10550BXD9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (394, '币安不安，危机来袭，日本金融厅向币安发出警告！', null, 'https://www.163.com/dy/article/GDEBHVET051192U0.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (395, '为什么说杭州是下一个深圳？', null, 'https://www.163.com/dy/article/GDE9SRG50519DP0A.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (396, '新一轮严查来了！新一轮变局强势开启！', null, 'https://www.163.com/dy/article/GDDCDS4U0539GTKT.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (397, '市场要凉了？广州南沙新盘全部房源一口价开卖', null, 'https://www.163.com/dy/article/GDDC7URC0539QLKD.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (398, '最后几天！要退钱的抓紧了！', null, 'https://www.163.com/dy/article/GDG15OTU0514CQIE.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (399, '“买醉”收获5个一字板，公司最新回应！下周解禁市值规模近千亿', null, 'https://www.163.com/dy/article/GDG0TC0V0517BMJU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (400, '大城市买房更难了，除了钱和房票，还需要“贷票”', null, 'https://www.163.com/dy/article/GDF5Q12C05199A0B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (401, '国家发改委：预计7月份煤价将进入下降通道', null, 'https://www.163.com/dy/article/GDFVKJ85053469RG.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (402, '美媒：美国西部90%以上地区正在遭受历史性干旱的威胁', null, 'https://www.163.com/dy/article/GDFV1I6T0512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (403, '万得财经周刊：A股迎中报行情，油价看高至100美元', null, 'https://www.163.com/dy/article/GDFU7RVL05198RSU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (404, '周末汽车行业三件大事，涉及特斯拉、吉利、众泰', null, 'https://www.163.com/dy/article/GDFU542305198RSU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (405, '湖北十大城市出炉：襄阳第三，孝感领先宜昌，咸宁荆门入围', null, 'https://www.163.com/dy/article/GDFRJ9I20539QLK6.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (406, '汉马科技股价四连板 吉利商用车资产重组拉开大幕', null, 'https://www.163.com/money/article/GDF9DDG0002580S6.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (407, '物业服务一周回顾 三家物企通过聆讯 中骏商管7月挂牌上市', null, 'https://www.163.com/dy/article/GDF83DEL0519D45U.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (408, '持续严重亏损，泰航再裁员854人', null, 'https://www.163.com/dy/article/GDF7RT860512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (409, 'B站12岁了 董事长兼CEO陈睿：社区就是要形成共识', null, 'https://www.163.com/dy/article/GDF70DLP0512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (410, '国货李宁越来越威武了', null, 'https://www.163.com/dy/article/GDF4VS6N05199AD2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (411, '迎13年来最高通胀！美国物价“涨”声一片！油价也大涨！啥情况？', null, 'https://www.163.com/dy/article/GDF4JHVH0519814N.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (412, '欧盟在WTO详解疫苗专利豁免替代方案，印度：诊断对，没找对药方', null, 'https://www.163.com/dy/article/GDF4ACMO0519DDQ2.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (413, '售楼处前抢客、主打高赠送 顺义“仁和三兄弟”去化几何？', null, 'https://www.163.com/dy/article/GDF3051G0519DFFO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (414, '专家：实现碳中和拢共分几步？光伏发电能替代煤发电？', null, 'http://live.163.com/room/241898.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (415, '苹果获得新专利， 预示折叠屏/卷轴屏 iPhone 等在望', null, 'https://www.163.com/dy/article/GDG08FD50511B8LM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (416, '我国首台X射线自由电子激光装置首次获得飞秒尺度的X光照片', null, 'https://www.163.com/dy/article/GDG8PGDR051497H3.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (417, '取消大小周，打工人扳回一局', null, 'https://www.163.com/dy/article/GDFV4273051282JL.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (418, 'iPhone 13首张真机图疑似流出！完虐安卓旗舰就靠它？！', null, 'https://www.163.com/dy/article/GDGB5PO20511A99L.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (419, '各国火箭发射实力盘点，把人送去火星谁是主力军？', null, 'https://www.163.com/dy/article/GDG56QF00532E7LA.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (420, '五位互联网人讲述：我在大厂，全靠演技', null, 'https://www.163.com/dy/article/GDG4BATT0531MRZO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (421, '医生紧急提醒：红霉素软膏千万不能长期使用！赶紧告诉家里人…', null, 'https://www.163.com/dy/article/GDFU4DRQ05118OGM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (422, '苹果手机出货量继续递增 新款iPhone SE有惊喜', null, 'https://www.163.com/dy/article/GDFQPRP405118VMB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (423, 'B站12岁了 董事长兼CEO陈睿：社区就是要形成共识，而不是制造争端', null, 'https://www.163.com/dy/article/GDF70DLP0512B07B.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (424, '太牛了！中国预计将于2033年后，开展“三步走”载人登陆火星任务', null, 'https://www.163.com/dy/article/GDG4QSJU0511DIJU.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (425, '英国大都会警方破获 1.14 亿英镑加密货币赃款，约十亿元', null, 'https://www.163.com/dy/article/GDG4HM5H0511B8LM.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (426, '科学家发现一枚“倒退”的恒星，它的旋转方向与其他恒星不同', null, 'https://www.163.com/dy/article/GDG6GN320511D5IC.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (427, '早安太空 · 网罗天下 | 嫦娥五号取回月壤首次在香港展出', null, 'https://www.163.com/dy/article/GDG2MJ3L0531TTYW.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (428, '摩托罗拉Defy 2021发布，骁龙662芯片+三防设计', null, 'https://www.163.com/dy/article/GDFQPSBI05118VMB.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (429, '陈平和任正非讲的是同一个美国吗？任正非回应', null, 'https://www.163.com/tech/article/GDEJV2OD00097U7S.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (430, '载人登陆火星！中国最终希望进行航班化探测', null, 'https://www.163.com/tech/article/GDEB2D3Q00097U81.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (431, '上市首日一度大跌37%，生鲜电商第一股首秀搞砸', null, 'https://www.163.com/tech/article/GDDIBNA3000999D8.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (432, '特斯拉增加音效提示音 以防主动巡航误触', null, 'https://www.163.com/tech/article/GDE26EAF00098IEO.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (433, '钟南山：新冠德尔塔变异株传染性很强，国产常用疫苗有效', null, 'https://www.163.com/tech/article/GDDHKDU7000999LD.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (434, 'Windows 11如何与安卓、iOS竞争，微软CEO回应', null, 'https://www.163.com/tech/article/GDEKKM4N00097U7T.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (435, '这是什么操作？微信iOS内测版功能曝光：优化开屏广告', null, 'https://www.163.com/dy/article/GDDPHNT5051100B9.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (436, '又一菲尔兹奖得主入职清华！任教求真书院，丘成桐：一代大师', null, 'https://www.163.com/dy/article/GDE08QUM0511DSSR.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (437, '马斯克：特斯拉全自动驾驶套件V9升级将再推迟1周', null, 'https://www.163.com/tech/article/GDE02FG200097U7T.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (438, '美国公布UFO报告：没提外星人，还是搞不清是什么', null, 'https://www.163.com/tech/article/GDDIU3O200097U81.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (439, 'Windows 11将如何改变你用电脑的方式', null, 'https://www.163.com/tech/article/GDEGK5DD00097U7T.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (440, '韩国明年将在未来汽车和系统级芯片等三大产业项目投入22亿美元', null, 'https://www.163.com/dy/article/GDDUECTC0511RIVP.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (441, '突然加速的原因？特斯拉在华召回285520辆车', null, 'https://www.163.com/tech/article/GDDSG2GO00097U7T.html', 16);
INSERT INTO cs_news_sys_db.News (Id, Title, AbstractContent, OringinUrl, NewsCategoryId) VALUES (442, 'Win11将至，戴尔、惠普、华硕、宏碁发布支持升级PC列表', null, 'https://www.163.com/dy/article/GDDHIL950511B8LM.html', 16);