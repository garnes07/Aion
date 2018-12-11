using System;

namespace Aion.Models.WFM
{
    public class ActionPlanView
    {
        public long RowId { get; set; }
        public int EntryId { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public string Text { get; set; }
    }
}