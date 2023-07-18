using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Pages.Clients;
using System.Data.SqlClient;

namespace MyApp.Pages._2D_Model
{
    public class _2d_modelModel : PageModel
    {
        public List<Twoinfo> two_model = new List<Twoinfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=HOPE_TOM;Integrated Security=True";

                using (SqlConnection hope_connection = new SqlConnection(connectionString))
                {
                    hope_connection.Open();
                    String sql = "SELECT uwi, value FROM scada_BHP";
                    using (SqlCommand command = new SqlCommand(sql, hope_connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Twoinfo two_info = new Twoinfo();
                                two_info.uwi = reader.GetString(1);
                                two_info.value = reader.GetString(2);

                                two_model.Add(two_info);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class Twoinfo
    {
        public string uwi;
        public string value;
    }
}
