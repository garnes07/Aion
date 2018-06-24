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

        public StoreMenu(List<StoreMaster> data, string defaultSelect, short accessLvl)
        {
            Channels = new List<Channel>();
            foreach (var _channel in data.GroupBy(x => x.Chain).Select(x => x.Key))
            {
                Channels.Add(new Channel
                {
                    text = _channel
                });
                var a = Channels.Single(x => x.text == _channel.ToString());
                foreach (var _division in data.Where(x => x.Chain == _channel.ToString()).GroupBy(x => x.Division).Select(x => x.Key))
                {
                    a.nodes.Add(new Division
                    {
                        text = _division
                    });
                    var b = a.nodes.Single(x => x.text == _division.ToString());
                    foreach (var _region in data.Where(x => x.Division == _division.ToString()).GroupBy(x => x.Region).Select(x => x.Key))
                    {
                        b.nodes.Add(new Region
                        {
                            text = _region.ToString()
                        });
                        var c = b.nodes.Single(x => x.text == _region.ToString());
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
            if(accessLvl == 0)
                return JsonConvert.SerializeObject(Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).SelectMany(x => x.nodes));
            if (accessLvl == 1)
                return JsonConvert.SerializeObject(Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes));             
            if (accessLvl == 2)
                return JsonConvert.SerializeObject(Channels.SelectMany(x => x.nodes));

            return accessLvl > 2 ? JsonConvert.SerializeObject(Channels) : "";
        }

        public bool menuSelect(string a)
        {
            if(a == _menuSelection)
            {
                return true;
            }

            string[] b = a.Split('_');
            bool result = false;
            var selected = new StoreStub();

            if (b[0] == "S")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.storeNum == b[1]);
                if (i.Any())
                {
                    selected = StoreSearch(b[1]);

                    _menuSelection = "S_" + i.First().storeNum;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "R")
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count > 0)
                {
                    selected = RegionSearch(b[1]);

                    _menuSelection = "R_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "D")
            {
                var i = Channels.SelectMany(x => x.nodes).Where(x => x.text == b[1]).ToList();
                if (i.Count > 0)
                {
                    selected = DivisionSearch(b[1]);
                    
                    _menuSelection = "D_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "C")
            {
                var i = Channels.Where(x => x.text == b[1]).ToList();
                if (i.Count > 0)
                {
                    selected = ChainSearch(b[1]);
                    
                    _menuSelection = "C_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            
            HttpContext.Current.Session["_ROIFlag"] = selected.Chain == "ROI";
            HttpContext.Current.Session["_store"] = new StoreStub
            {
                Chain = selected.Chain,
                Division = selected.Division,
                Region = selected.Region
            };
            if(selected.Region == "118" || selected.Region == "109" || selected.Region == "124" || selected.Region == "108")
            {
                HttpContext.Current.Session["_PilotFlag"] = true;
            }
            else
            {
                HttpContext.Current.Session["_PilotFlag"] = false;
            }
            return result;
        }

        public bool menuReset()
        {
            string[] b = defaultSelect.Split('_');
            var selected = new StoreStub();
            switch (b[0])
            {
                case "S":
                    selected = StoreSearch(b[1]);
                    break;
                case "R":
                    selected = RegionSearch(b[1]);
                    break;
                case "D":
                    selected = DivisionSearch(b[1]);
                    break;
                case "C":
                    selected = ChainSearch(b[1]);
                    break;
            }
            HttpContext.Current.Session["_ROIFlag"] = selected.Chain == "ROI";
            HttpContext.Current.Session["_store"] = new StoreStub
            {
                Chain = selected.Chain,
                Division = selected.Division,
                Region = selected.Region
            };

            return _menuSelection == defaultSelect || menuSelect(defaultSelect);
        }

        public bool menuUpOne()
        {
            string[] b = _menuSelection.Split('_');
            bool result = false;

            if (b[0] == "S" && accessLvl >= 1)
            {
                var i = Channels.SelectMany(x => x.nodes).SelectMany(x => x.nodes).Where(x => x.nodes.Any(y => y.storeNum == b[1]));
                if (i.Any())
                {
                    _menuSelection = "R_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "R" && accessLvl >= 2)
            {
                var i = Channels.SelectMany(x => x.nodes).Where(x => x.nodes.Any(y => y.text == b[1]));
                if (i.Any())
                {
                    _menuSelection = "D_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            else if (b[0] == "D")
            {
                var i = Channels.Where(x => x.nodes.Any(y => y.text == b[1]));
                if (i.Any())
                {
                    _menuSelection = "C_" + i.First().text;
                    _menuSearch = i.First().text;
                }
            }
            return result;
        }

        private StoreStub StoreSearch(string storeNum)
        {
            foreach(var channel in Channels)
            {
                foreach(var division in channel.nodes)
                {
                    foreach(var region in division.nodes)
                    {
                        foreach(var store in region.nodes)
                        {
                            if(store.storeNum == storeNum)
                            return new StoreStub
                            {
                                Chain = channel.text,
                                Division = division.text,
                                Region = region.text
                            };
                        }
                    }
                }
            }
            return new StoreStub();
        }

        private StoreStub RegionSearch(string regionNum)
        {
            foreach (var channel in Channels)
            {
                foreach (var division in channel.nodes)
                {
                    foreach (var region in division.nodes)
                    {
                        if (region.text == regionNum)
                            return new StoreStub
                            {
                                Chain = channel.text,
                                Division = division.text,
                                Region = region.text
                            };
                    }
                }
            }
            return new StoreStub();
        }

        private StoreStub DivisionSearch(string divisionName)
        {
            foreach (var channel in Channels)
            {
                foreach (var division in channel.nodes)
                {
                    if (division.text == divisionName)
                        return new StoreStub
                        {
                            Chain = channel.text,
                            Division = division.text,
                            Region = ""
                        };
                }
            }
            return new StoreStub();
        }

        private StoreStub ChainSearch(string chainName)
        {
            foreach (var channel in Channels)
            {
                if (channel.text == chainName)
                    return new StoreStub
                    {
                        Chain = channel.text,
                        Division = "",
                        Region = ""
                    };
            }
            return new StoreStub();
        }

        public class Channel
        {
            public string text { get; set; }
            public string href => "/Profile/MenuSelect?a=C_" + text;
            public List<Division> nodes { get; set; }

            public Channel()
            {
                nodes = new List<Division>();
            }
        }

        public class Division
        {
            public string text { get; set; }
            public string href => "/Profile/MenuSelect?a=D_" + text;
            public List<Region> nodes { get; set; }

            public Division()
            {
                nodes = new List<Region>();
            }
        }

        public class Region
        {
            public string text { get; set; }
            public string href => "/Profile/MenuSelect?a=R_" + text;
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
            public string href => "/Profile/MenuSelect?a=S_" + storeNum;
        }
    }
}