using Aion.DAL.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Aion.Areas.WFM.ViewModels.Minerva
{
    public class MinervaVm
    {
        public List<vw_StoreLocations> collection { get; set; }

        public string allStores => JsonConvert.SerializeObject(collection);
        public string minervaOnly => JsonConvert.SerializeObject(collection.Where(x => x.MinervaStore == 1 || x.MinervaTransferStore == 1));
    }
}