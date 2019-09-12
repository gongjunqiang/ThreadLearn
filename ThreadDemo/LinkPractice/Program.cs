using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkPractice
{
    class Program
    {
        //初始化武林高手
        static List<MartialArtsMaster> master = new List<MartialArtsMaster>(){
            new MartialArtsMaster(){ Id = 1, Name = "黄蓉",    Age = 18, Menpai = "丐帮", Kungfu = "打狗棒法",  Level = 9  },
            new MartialArtsMaster(){ Id = 2, Name = "洪七公",  Age = 70, Menpai = "丐帮", Kungfu = "打狗棒法",  Level = 10 },
            new MartialArtsMaster(){ Id = 3, Name = "郭靖",    Age = 22, Menpai = "丐帮", Kungfu = "降龙十八掌",Level = 10 },
            new MartialArtsMaster(){ Id = 4, Name = "任我行",  Age = 50, Menpai = "明教", Kungfu = "葵花宝典",  Level = 1  },
            new MartialArtsMaster(){ Id = 5, Name = "东方不败",Age = 35, Menpai = "明教", Kungfu = "葵花宝典",  Level = 10 },
            new MartialArtsMaster(){ Id = 6, Name = "林平之",  Age = 23, Menpai = "华山", Kungfu = "葵花宝典",  Level = 7  },
            new MartialArtsMaster(){ Id = 7, Name = "岳不群",  Age = 50, Menpai = "华山", Kungfu = "葵花宝典",  Level = 8  }
        };
        //初始化武学
        static List<Kongfu> kongfu = new List<Kongfu>()
        {
            new Kongfu() {KongfuId = 1, KongfuName = "打狗棒法", Lethality = 90},
            new Kongfu() {KongfuId = 2, KongfuName = "降龙十八掌", Lethality = 95},
            new Kongfu() {KongfuId = 3, KongfuName = "葵花宝典", Lethality = 100}
        };


        static void Main(string[] args)
        {
            //var learn1 = from o in master
            //    where o.Level > 8 && o.Menpai == "丐帮"
            //    select o;
            var learn1 = master.Where(o => o.Level > 8 && o.Menpai == "丐帮");
            foreach (var a in learn1)
            {
                Console.WriteLine(a.Name+ "\t"+a.Menpai + "\t" + a.Kungfu);
            }
            Console.ReadKey();
        }
    }
}
