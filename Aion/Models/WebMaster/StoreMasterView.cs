using System.Collections.Generic;

namespace Aion.Models.WebMaster
{
    public class StoreMasterView
    {
        public StoreMasterView()
        {
            this.IpRefs = new HashSet<IpRefView>();
        }

        public short StoreNumber { get; set; }
        public string StoreName { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short Region { get; set; }
        public string KronosName { get; set; }
        public short? BaseStore { get; set; }
        public string IpRange { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public byte? StoreBand { get; set; }
        
        public ICollection<IpRefView> IpRefs { get; set; }
    }
}