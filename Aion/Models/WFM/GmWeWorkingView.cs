using System;

namespace Aion.Models.WFM
{
    public class GmWeWorkingView
    {
        public long RowId { get; set; }
        public string Chain { get; set; }
        public string Division { get; set; }
        public short? Region { get; set; }
        public short? StoreNumber { get; set; }
        public string StoreName { get; set; }
        public DateTime? Date { get; set; }
        public byte? day { get; set; }
        public int? Count { get; set; }
        public int? Worked { get; set; }
        public int? Holiday { get; set; }
        public int? NotPunched { get; set; }
        public int? Closed { get; set; }
    }
}