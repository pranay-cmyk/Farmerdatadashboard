using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using KapasKisanDashboard.Models;

namespace KapasKisanDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalPositionController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ArrivalPositionController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult GetArrivalPositionData(
            int pageNumber = 1,
            string? state_Name = null
        )
        {
            using (SqlConnection con = new SqlConnection(
                _config.GetConnectionString("Emarkets")))
            {
                SqlCommand cmd = new SqlCommand(
                    "GetArrivalPositionData",
                    con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);

                cmd.Parameters.AddWithValue("@PageSize", 10);

                cmd.Parameters.AddWithValue(
                    "@STATE_NAME",
                    string.IsNullOrWhiteSpace(state_Name)
                    ? DBNull.Value
                    : state_Name
                );

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<ArrivalPositionModel> arrivalData =
                    new List<ArrivalPositionModel>();

                while (reader.Read())
                {
                    arrivalData.Add(new ArrivalPositionModel
                    {
                        CROP_YEAR =
                            reader["CROP_YEAR"]?.ToString(),

                        STATE_NAME =
                            reader["STATE_NAME"]?.ToString(),

                        DISTRICT_NAME =
                            reader["DISTRICT_NAME"]?.ToString(),

                        BRANCH =
                            reader["BRANCH"]?.ToString(),

                        PURCHASE_CENTER =
                            reader["PURCHASE_CENTER"]?.ToString(),

                        VARITY_GRADE =
                            reader["VARITY_GRADE"]?.ToString(),

                        PURCHASE_DATE =
                            reader["PURCHASE_DATE"] == DBNull.Value
                            ? null
                            : Convert.ToDateTime(
                                reader["PURCHASE_DATE"]),

                        ARRIVAL_IN_QUINTALS =
                            reader["ARRIVAL_IN_QUINTALS"] == DBNull.Value
                            ? null
                            : Math.Round(
                                Convert.ToDecimal(
                                    reader["ARRIVAL_IN_QUINTALS"]), 2),

                        CCIL_PURCHASE_IN_Qtl =
                            reader["CCIL_PURCHASE_IN_Qtl"] == DBNull.Value
                            ? null
                            : Math.Round(
                                Convert.ToDecimal(
                                    reader["CCIL_PURCHASE_IN_Qtl"]), 2)
                    });
                }

                return Ok(new
                {
                    Data = arrivalData
                });
            }
        }
    }
}