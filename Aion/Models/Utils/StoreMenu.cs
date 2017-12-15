using Aion.DAL.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aion.Models.Utils
{
    public class StoreMenu
    {
        public List<Channel> Channels { get; set; }
        private string defaultSelect { get; set; }
        private short accessLvl { get; set; }

        public string _menuSelection
        {
            get
            {
                return HttpContext.Current.Session["_menuSelection"] != null ? HttpContext.Current.Session["_menuSelection"].ToString() : "";
            }
            set
            {
                HttpContext.Current.Session["_menuSelection"] = value;
            }
        }

        public string _menuSearch
        {
            get
            {
                return HttpContext.Current.Session["_menuSearch"] != null ? HttpContext.Current.Session["_menuSearch"].ToString() : "";
            }
            set
            {
                HttpContext.Current.Session["_menuSearch"] = value;
            }
        }

        public string JsonString()
        {
            if (this.accessLvl <= 1)
            {
                return JsonConvert.SerializeObject(Channels.First().nodes.First().nodes);
            }
            else if (this.accessLvl == 2)
            {
                return JsonConvert.SerializeObject(Channels.First().nodes.First().nodes);
            }
            else if (this.accessLvl > 2)
            {
                return JsonConvert.SerializeObject(Channels);
            }
            else
            {
                return "";
            }
        }

        public StoreMenu(List<StoreMaster> data, string defaultSelect, short accessLvl)
        {
            Channels = new List<Channel>();
            foreach (var _channel in data.GroupBy(x => x.Chain).Select(x => x.Key))
            {
                Channels.Add(new Channel
                {
                    text = _channel.ToString()
                });
                var a = Channels.Where(x => x.text == _channel.ToString()).Single();
                foreach (var _division in data.Where(x => x.Chain == _channel.ToString()).GroupBy(x => x.Division).Select(x => x.Key))
                {
                    a.nodes.Add(new Division
                    {
                        text = _division.ToString()
                    });
                    var b = a.nodes.Where(x => x.text == _division.ToString()).Single();
                    foreach (var _region in data.Where(x => x.Division == _division.ToString()).GroupBy(x => x.Region).Select(x => x.Key))
                    {
                        b.nodes.Add(new Region
                        {
                            text = _region.ToString()
                        });
                        var c = b.nodes.Where(x => x.text == _region.ToString()).Single();
                        foreach (var _store in data.Where(x => x.Region == _region))
                        {
                            c.nodes.Add(new Store
                            {
                                text = string.Format("{0} - {1}", _store.StoreNumber, _store.StoreName),
                                storeNum = _store.StoreNumber.ToString()
                            });
                        }
                    };
                }
            }

            this.defaultSelect = defaultSelect;
            this.accessLvl = accessLvl;
            menuSelect(defaultSelect);
        }

        public bool menuSelect(string a)
        {
            if(a == _menuSelection)
            {
                return true;
            }

            string[] b = a.Split('_');
            bool result = false;

            if (b[0] == "S")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.storeNum == b[1]);
                if (i.Count() > 0)
                {
                    _menuSelection = "S_" + i.First().storeNum;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "R")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    _menuSelection = "R_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "D")
            {
                var i = Channels.SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    _menuSelection = "D_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "C")
            {
                var i = Channels.Where(x => x.text == b[1]).ToList();
                if (i.Count() > 0)
                {
                    _menuSelection = "C_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            return result;
        }

        public bool menuReset()
        {
            if (_menuSelection == defaultSelect)
            {
                return true;
            }
            return menuSelect(defaultSelect);
        }

        public bool menuUpOne()
        {
            string[] b = _menuSelection.Split('_');
            bool result = false;

            if (b[0] == "S")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.nodes.Where(y => y.storeNum == b[1]).Count() > 0);
                if (i.Count() > 0)
                {
                    _menuSelection = "R_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "R" && this.accessLvl > 2)
            {
                var i = Channels.SelectMany(x => x.nodes).Where(x => x.nodes.Where(y => y.text == b[1]).Count() > 0);
                if (i.Count() > 0)
                {
                    _menuSelection = "D_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "D")
            {
                var i = Channels.Where(x => x.nodes.Where(y => y.text == b[1]).Count() > 0);
                if (i.Count() > 0)
                {
                    _menuSelection = "C_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            return result;
        }

        public class Channel
        {
            public string text { get; set; }
            public string href
            {
                get
                {
                    return "Profile/MenuSelect?a=C_" + text;
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
                    return "Profile/MenuSelect?a=D_" + text;
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
                    return "Profile/MenuSelect?a=R_" + text;
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
                    return "Profile/MenuSelect?a=S_" + storeNum;
                }
            }
        }
    }
}