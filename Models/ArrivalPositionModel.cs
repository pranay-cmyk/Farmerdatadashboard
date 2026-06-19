namespace KapasKisanDashboard.Models
{
    public class ArrivalPositionModel
    {
        public string? CROP_YEAR { get; set; }

        public string? STATE_NAME { get; set; }

        public string? DISTRICT_NAME { get; set; }

        public string? BRANCH { get; set; }

        public string? PURCHASE_CENTER { get; set; }

        public string? VARITY_GRADE { get; set; }

        public DateTime? PURCHASE_DATE { get; set; }

        public decimal? ARRIVAL_IN_QUINTALS { get; set; }

        public decimal? CCIL_PURCHASE_IN_Qtl { get; set; }
    }
}