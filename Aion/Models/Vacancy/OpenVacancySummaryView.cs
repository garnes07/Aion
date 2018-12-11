namespace Aion.Models.Vacancy
{
    public class OpenVacancySummaryView
    {
        public string Chain { get; set; }
        public short StoreNumber { get; set; }
        public short JobCode { get; set; }
        public string FriendlyName { get; set; }
        public short Remaining { get; set; }
        public int Onboarding { get; set; }
        public int Offer { get; set; }
        public int Started { get; set; }
    }
}