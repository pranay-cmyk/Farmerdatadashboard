using System.Data;
using Microsoft.Data.SqlClient;

public class DbHelper
{
    private readonly IConfiguration _config;

    public DbHelper(IConfiguration config)
    {
        _config = config;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}