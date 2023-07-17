using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace MyApp.Pages.Scada_BHP
{
    public class ScadaBHPModel : PageModel
    {
        public List<ScadaInfo> scadaInfos = new List<ScadaInfo>();
        public void OnGet()
        {
            try
            {
                string hope_connection = "Data Source=.\\sqlexpress;Initial Catalog=HOPE_TOM;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(hope_connection))
                {
                    connection.Open();
                    
                    string sql = "SELECT * FROM pep_code;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                           while (reader.Read())
                           {
                                ScadaInfo scadaInfo = new ScadaInfo();
                                scadaInfo.id = "" + reader.GetInt32(0);
                                scadaInfo.type = reader.GetString(1);
                                scadaInfo.parent_code = reader.GetString(2);
                                scadaInfo.code_desc = reader.GetString(3);
                                scadaInfo.child_code = reader.GetString(4);
                                scadaInfo.subcode_desk = reader.GetString(5);
                                scadaInfo.pep_code = reader.GetString(6);

                                scadaInfos.Add(scadaInfo);
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

    public class  ScadaInfo
    {
        public string id;
        public string type;
        public string parent_code;
        public string code_desc;
        public string child_code;
        public string subcode_desk;
        public string pep_code;
    }
}
