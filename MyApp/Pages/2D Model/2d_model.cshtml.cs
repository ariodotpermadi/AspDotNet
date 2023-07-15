using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Pages.Clients;
using System.Data.SqlClient;

namespace MyApp.Pages._2D_Model
{
    public class _2d_modelModel : PageModel
    {
        public List<twoinfo> twomodel = new List<twoinfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-O42TT8AG\\SQLEXPRESS;Initial Catalog=HOPE_TOM;Persist Security Info=True;User ID=HPI;Password=12345";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM scada_BHP";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                twoinfo twoinfo = new twoinfo();
                                twoinfo.well = reader.GetString(1);
                                twoinfo.bhp = reader.GetString(2);

                                twomodel.Add(twoinfo);

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
    public class twoinfo
    {
        public string well;
        public string bhp;
    }
}
