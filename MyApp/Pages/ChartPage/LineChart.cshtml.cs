using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.ChartPage
{
    //Model untuk  database
  
    public class LineChartModel : PageModel
    {
        public List<LineChartModel> DataPoint = new List<LineChartModel>();

        public String chartTimeStamp { get; set; }
        public String uwi { get; set; }
        public Double value { get; set; }
        public void OnGet()
        {

            string dataConnection = "Data Source=.\\sqlexpress;Initial Catalog=HOPE_TOM;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(dataConnection))
            {

                string sql = "SELECT uwi, value, timestamp FROM scada_WHP WHERE timestamp BETWEEN '2023-01-01' and '2023-01-03' AND uwi = 'SNR-01'";
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataPoint = new List<LineChartModel>();
                        while (reader.Read())
                        {
                            DataPoint.Add(new LineChartModel()
                            {                           
                                uwi = reader.GetString(0),                                
                                value = reader.GetDouble(1),
                                chartTimeStamp = reader.GetDateTime(2).ToString(),
                            });
                        }
                    }

                }
            }

        }
    }
   
}
