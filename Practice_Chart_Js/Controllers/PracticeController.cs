using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practice_Chart_Js.Controllers
{
    public class PracticeController : Controller
    {
        // GET: Practice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mixed_BasicAndChangeSize()
        {            
            return View();
        }

        public ActionResult Radar_BasicAndChangeSize()
        {
            return View();
        }

        public ActionResult Radar_PlayerFromJson()
        {
            return View();
        }

        public ActionResult Radar_PlayerFromBack()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetDrawPlayerDataJson()
        {
            var random = new Random(new Guid().GetHashCode());

            // 把 Player 的資料轉成畫圖要用的資料
            var result = GetPlayers().Select(x => new
            {
                pointBorderWidth = 2,// 點寬
                borderWidth = 3,// 線寬
                backgroundColor = $"rgba(0,0,0,0)", // 透明
                borderColor = $"rgba({random.Next(0, 255)}, {random.Next(0, 255)},{random.Next(0, 255)},0.6)",
                data = new List<int>() { x.STR, x.AGI, x.VIT, x.INT, x.DEX, x.LUK },
                label = $"{x.Job}-{x.Nickname}",
            });

            return Json(result);
        }


        public IEnumerable<Player> GetPlayers()
        {
            var random = new Random();
            var result = new List<Player>();
            for (int i = 0; i < 5; i++)
            {
                result.Add(new Player()
                {
                    Nickname = RamdonNickname(),
                    Job = RamdonJob(),
                    STR = random.Next(1, 99),
                    AGI = random.Next(1, 99),
                    VIT = random.Next(1, 99),
                    INT = random.Next(1, 99),
                    DEX = random.Next(1, 99),
                    LUK = random.Next(1, 99),
                });
            }
            return result;
        }

        private string RamdonNickname()
        {
            var nickNames = new List<string>() { "無法顯示人物名稱", "銅鑼灣扛報紙", "汗味戰警", "左青龍右胖虎", "靈車甩尾棺材飛",
                "媽你看爸在飛", "怕滑落地", "頭蚊子叮", "紅衣小男孩", "九頭龍閃到腰",
                "善解人衣", "剛好五個字", "打死我也不說", "媽媽來了快關電腦", "怪很強我先走",
                "差一元買多多", "說好不打臉", "不要問 你會怕", "拉鍊裡的猛獸",
                "你看不見我", "轉角遇到怪", "放個屁就", "你點到我了", "抱歉沒點補",
                "網路連線中斷", "拿佛珠砸耶穌", "你已經死了", "綠油精點眼睛", "屎亡筆記本",
                "關完大哥關二哥", "放屁給你聞", "你在大聲什麼啦", "蒙奇D能兒", "名子剛好七個字",
                "苗栗小五郎", "AI人工智障", "放屁給你聞", "蒙其D罩杯",
                "老爺不可以", "夫人會生氣", "憂鬱肉燥飯", "被冰塊燙傷",
                "麻辣白開水", "涼麵趁熱吃" };

            var randomIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, nickNames.Count);

            return nickNames[randomIndex];
        }

        private string RamdonJob()
        {
            var jobs = new List<string>() { "法師", "盜賊", "劍士", "弓箭手", "商人", "祭司" };

            var randomIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, jobs.Count);

            return jobs[randomIndex];
        }


        public class Player
        {
            public string Nickname { get; set; }
            public string Job { get; set; }
            public int STR { get; set; }
            public int AGI { get; set; }
            public int VIT { get; set; }
            public int INT { get; set; }
            public int DEX { get; set; }
            public int LUK { get; set; }
        }
    }
}