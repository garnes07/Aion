using Aion.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.Models.Utils
{
    public class HMenu
    {
        public List<Channel> Channels { get; set; }

        public bool newSelection(string a)
        {
            string[] b = a.Split('_');
            bool result = false;

            if (b[0] == "S")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.storeNum == b[1]);
                if(i.Count() > 0)
                {
                    HttpContext.Current.Session["_menuSelection"] = "S" + i.First().storeNum;
                    HttpContext.Current.Session["_menuSearch"] = i.First().text;
                }
            }
            else if(b[0] == "R")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    HttpContext.Current.Session["_menuSelection"] = "R" + i.First().text;
                    HttpContext.Current.Session["_menuSearch"] = i.First().text;
                }
            }
            else if(b[0] == "D")
            {
                var i = Channels.SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    HttpContext.Current.Session["_menuSelection"] = "D" + i.First().text;
                    HttpContext.Current.Session["_menuSearch"] = i.First().text;
                }
            }
            else if(b[0] == "C")
            {
                var i = Channels.Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    HttpContext.Current.Session["_menuSelection"] = "C" + i.First().text;
                    HttpContext.Current.Session["_menuSearch"] = i.First().text;
                }
            }

            return result;
        }
        
        public HMenu(List<sp_GetBranchMenu_Result> data)
        {
            Channels = new List<Channel>();
            foreach(var _channel in data.GroupBy(x => x.Channel).Select(x => x.Key))
            {
                Channels.Add(new Channel
                {
                    text = _channel.ToString()
                });
                var a = Channels.Where(x => x.text == _channel.ToString()).Single();
                foreach (var _division in data.Where(x => x.Channel == _channel.ToString()).GroupBy(x => x.Division).Select(x => x.Key))
                {
                    a.nodes.Add(new Division
                    {
                        text = _division.ToString()
                    });
                    var b = a.nodes.Where(x => x.text == _division.ToString()).Single();
                    foreach (var _region in data.Where(x => x.Division == _division.ToString()).GroupBy(x => x.RegionNo).Select(x => x.Key))
                    {
                        b.nodes.Add(new Region
                        {
                            text = _region.ToString()
                        });
                        var c = b.nodes.Where(x => x.text == _region.ToString()).Single();
                        foreach (var _store in data.Where(x => x.RegionNo == _region.ToString()))
                        {
                            c.nodes.Add(new Store
                            {
                                text = string.Format("{0} - {1}", _store.CST_CNTR_ID, _store.StoreName),
                                storeNum = _store.CST_CNTR_ID.ToString()
                            });
                        }
                    };
                }
            }
        }

        public class Channel
        {
            public string text { get; set; }
            public string href
            {
                get
                {
                    return "home/test?a=C_" + text;
                }
            }
            public List<Division> nodes { get; set; }

            public Channel()
            {
                nodes = new List<Division>();
            }
        }

        public class Division
        {
            public string text { get; set; }
            public string href
            {
                get
                {
                    return "home/test?a=D_" + text;
                }
            }
            public List<Region> nodes { get; set; }

            public Division()
            {
                nodes = new List<Region>();
            }
        }

        public class Region
        {
            public string text { get; set; }
            public string href
            {
                get
                {
                    return "home/test?a=R_" + text;
                }
            }
            public List<Store> nodes { get; set; }

            public Region()
            {
                nodes = new List<Store>();
            }
        }

        public class Store
        {
            public string text { get; set; }
            public string storeNum { get; set; }
            public string href
            {
                get
                {
                    return "home/test?a=S_" + storeNum;
                }
            }
        }
    } 
}