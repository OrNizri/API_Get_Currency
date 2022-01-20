using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebAPiCurrencies.Models;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebAPiCurrencies.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GetCurrencyPairsControllers : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static string connection = "Data Source=DESKTOP-7PJF1K5;Initial Catalog=Currencies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public GetCurrencyPairsControllers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetCurrencies")]
        public JsonResult Get()
        {
            DataTable dt = null;

            using (var conn = new SqlConnection(connection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Proc_Get_Currency_Pairs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        dt = new DataTable();
                        adapter.Fill(dt);
                    }
                }
                conn.Close();

            }
            return new JsonResult(dt);
        }
    }
}
