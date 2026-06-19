using System;

public class FarmerDataforDashbordModel
{
    public int? Pk_FarmerID { get; set; }

    public string? FarmerFullname { get; set; }

    public int? ISWOMAN { get; set; }

    public int? FarmerType { get; set; }

    public string? STATENAME { get; set; }

    public string? Branch_Name { get; set; }

    public string? DistrictName { get; set; }

    public string? Center_Name { get; set; }

    public string? VillageName { get; set; }

    public decimal? TotalLand { get; set; }

    public decimal? NoOfAcr { get; set; }

    public decimal? ExpectedYeild_Kg_per_acre { get; set; }

    public int? PK_MarketID { get; set; }

    public string? MarketName { get; set; }

    public string? GinningMillName { get; set; }

    public string? BookingNo { get; set; }

    public int? DayID { get; set; }

    public int? MonthID { get; set; }

    public int? IsCancelled { get; set; }

    public decimal? BookedQty { get; set; }

    public decimal? TAKQTY { get; set; }

    public string? TAKPATTINO { get; set; }

    public string? CURRENTSTATUS { get; set; }

    public string? PayStatus { get; set; }

    public DateTime? TakpattiDate { get; set; }

    public DateTime? PaymentSentDate { get; set; }

    public DateTime? PayDate { get; set; }

    public decimal? TotalPaymentAmt { get; set; }

    public decimal? MSP { get; set; }

    public string? CasteCategory { get; set; }

    public string? AspirationalDistricts { get; set; }
}