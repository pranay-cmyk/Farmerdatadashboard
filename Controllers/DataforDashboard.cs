using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

[Route("api/[controller]")]
[ApiController]
public class FarmerDataForDashboard : ControllerBase
{
    private readonly IConfiguration _config;

    public FarmerDataForDashboard(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult GetPaged(
        int pageNumber = 1,
        string farmerName = null
    )
    {
        using (SqlConnection con = new SqlConnection(
            _config.GetConnectionString("DefaultConnection")))
        {
            SqlCommand cmd = new SqlCommand(
                "GetFarmerDataforDashboard",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", 10);

            cmd.Parameters.AddWithValue(
                "@FarmerName",
                string.IsNullOrWhiteSpace(farmerName)
                ? DBNull.Value
                : farmerName
            );

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            // int totalRecords = 0;

            // if (reader.Read())
            // {
            //     totalRecords = Convert.ToInt32(
            //         reader["TotalRecords"]);
            // }

            reader.NextResult();

            List<FarmerDataforDashbordModel> farmers =
                new List<FarmerDataforDashbordModel>();

            while (reader.Read())
            {
                farmers.Add(new FarmerDataforDashbordModel
                {
                    Pk_FarmerID =
                        Convert.ToInt32(reader["PK_FarmerID"]),

                    FarmerFullname =
                        reader["FarmerFullname"]?.ToString(),

                    ISWOMAN =
                        reader["ISWOMAN"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["ISWOMAN"]),

                    FarmerType =
                        reader["FarmerType"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["FarmerType"]),

                    STATENAME =
                        reader["STATENAME"]?.ToString(),

                    Branch_Name =
                        reader["Branch_Name"]?.ToString(),

                    DistrictName =
                        reader["DistrictName"]?.ToString(),

                    Center_Name =
                        reader["Center_Name"]?.ToString(),

                    VillageName =
                        reader["VillageName"]?.ToString(),

                    TotalLand =
                        reader["TotalLand"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["TotalLand"]), 2),

                    NoOfAcr =
                        reader["NoOfAcr"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["NoOfAcr"]), 2),

                    ExpectedYeild_Kg_per_acre =
                        reader["ExpectedYeild_Kg_per_acre"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["ExpectedYeild_Kg_per_acre"]), 2),

                    PK_MarketID =
                        reader["PK_MarketID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["PK_MarketID"]),

                    MarketName =
                        reader["MarketName"]?.ToString(),

                    GinningMillName =
                        reader["GinningMillName"]?.ToString(),

                    BookingNo =
                        reader["BookingNo"]?.ToString(),

                    DayID =
                        reader["DayID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["DayID"]),

                    MonthID =
                        reader["MonthID"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["MonthID"]),

                    IsCancelled =
                        reader["IsCancelled"] == DBNull.Value
                        ? null
                        : Convert.ToInt32(reader["IsCancelled"]),

                    BookedQty =
                        reader["BookedQty"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["BookedQty"]), 2),

                    TAKQTY =
                        reader["TAKQTY"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["TAKQTY"]), 2),

                    TAKPATTINO =
                        reader["TAKPATTINO"] == DBNull.Value
                        ? null
                        : reader["TAKPATTINO"].ToString(),

                    CURRENTSTATUS =
                        reader["CURRENTSTATUS"]?.ToString(),

                    PayStatus =
                        reader["PayStatus"]?.ToString(),

                    TakpattiDate =
                        reader["TakpattiDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["TakpattiDate"]),

                    PaymentSentDate =
                        reader["PaymentSentDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PaymentSentDate"]),

                    PayDate =
                        reader["PayDate"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["PayDate"]),

                    TotalPaymentAmt =
                        reader["TotalPaymentAmt"] == DBNull.Value
                        ? null
                        : Math.Round(
                            Convert.ToDecimal(reader["TotalPaymentAmt"]), 2),

                    MSP =
                        reader["MSP"] == DBNull.Value
                        ? null
                        : Convert.ToDecimal(reader["MSP"]),

                    CasteCategory =
                        reader["CasteCategory"]?.ToString(),

                    AspirationalDistricts =
                        reader["AspirationalDistricts"]?.ToString()
                });
            }

            return Ok(new
            {
               // TotalRecords = totalRecords,
                Data = farmers
            });
        }
    }
}