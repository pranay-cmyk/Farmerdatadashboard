using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class FarmerController : ControllerBase
{
    private readonly IConfiguration _config;

    public FarmerController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult GetPaged(
        int pageNumber = 1,
        string barcode = null,
        string farmerName = null
    )
    {
        using (SqlConnection con = new SqlConnection(
            _config.GetConnectionString("DefaultConnection")))
        {
            SqlCommand cmd = new SqlCommand(
                "GetFarmerDataPaged",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", 10);

            cmd.Parameters.AddWithValue(
                "@Barcode",
                string.IsNullOrWhiteSpace(barcode)
                    ? DBNull.Value
                    : barcode
            );

            cmd.Parameters.AddWithValue(
                "@FarmerName",
                string.IsNullOrWhiteSpace(farmerName)
                    ? DBNull.Value
                    : farmerName
            );

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            int totalRecords = 0;

            if (reader.Read())
            {
                totalRecords = Convert.ToInt32(
                    reader["TotalRecords"]
                );
            }

            reader.NextResult();

            List<FarmerBooking> list =
                new List<FarmerBooking>();

            while (reader.Read())
            {
                list.Add(new FarmerBooking
                {
                    Barcode = reader["Barcode"]?.ToString(),
                    FarmerFullname = reader["FarmerFullname"]?.ToString(),
                    ISWOMAN = reader["ISWOMAN"] == DBNull.Value ? null : Convert.ToInt32(reader["ISWOMAN"]),
                    FarmerType = reader["FarmerType"]?.ToString(),
                    STATENAME = reader["STATENAME"]?.ToString(),
                    DistrictName = reader["DistrictName"]?.ToString(),
                    MandalName = reader["MandalName"]?.ToString(),
                    VillageName = reader["VillageName"]?.ToString(),

                    TotalLand = reader["TotalLand"] == DBNull.Value
    ? null
    : Math.Round(Convert.ToDecimal(reader["TotalLand"]), 2),

                    NoOfAcr = reader["NoOfAcr"] == DBNull.Value
    ? null
    : Math.Round(Convert.ToDecimal(reader["NoOfAcr"]), 2),
                    ExpectedYeild = reader["ExpectedYeild"] == DBNull.Value
    ? null
    : Math.Round(Convert.ToDecimal(reader["ExpectedYeild"]), 2),

                    MarketName = reader["MarketName"]?.ToString(),
                    GinningMillName = reader["GinningMillName"]?.ToString(),

                    BookingNo = reader["BookingNo"]?.ToString(),

                    DayID = reader["DayID"] == DBNull.Value ? null : Convert.ToInt32(reader["DayID"]),
                    MonthID = reader["MonthID"] == DBNull.Value ? null : Convert.ToInt32(reader["MonthID"]),
                    IsCancelled = reader["IsCancelled"] == DBNull.Value ? null : Convert.ToInt32(reader["IsCancelled"]),


                    BookedQty = reader["BookedQty"] == DBNull.Value
    ? null
    : Math.Round(Convert.ToDecimal(reader["BookedQty"]), 2),
                    TAKQTY = reader["TAKQTY"] == DBNull.Value ? null :Math.Round( Convert.ToDecimal(reader["TAKQTY"]),2),

                    TAKPATTINO =
    reader["TAKPATTINO"] == DBNull.Value ||
    string.IsNullOrWhiteSpace(reader["TAKPATTINO"].ToString()) ||
    reader["TAKPATTINO"].ToString().ToUpper() == "NULL"
        ? null
        : Convert.ToDouble(reader["TAKPATTINO"]).ToString("0"),
                    CURRENTSTATUS = reader["CURRENTSTATUS"]?.ToString(),
                    PayStatus = reader["PayStatus"]?.ToString(),

                    TakpattiDate = reader["TakpattiDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["TakpattiDate"]),
                    PaymentSentDate = reader["PaymentSentDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["PaymentSentDate"]),
                    PayDate = reader["PayDate"] == DBNull.Value ? null : Convert.ToDateTime(reader["PayDate"]),

                    TotalPaymentAmt = reader["TotalPaymentAmt"] == DBNull.Value ? null : Math.Round(Convert.ToDecimal(reader["TotalPaymentAmt"]),2),
                    MSP = reader["MSP"] == DBNull.Value ? null : Convert.ToDecimal(reader["MSP"])
                });
            }

            return Ok(new
            {
                TotalRecords = totalRecords,
                Data = list
            });
        }
    }


}
