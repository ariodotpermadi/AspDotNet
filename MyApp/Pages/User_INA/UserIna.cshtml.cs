using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.User_INA
{
    public class UserInaModel : PageModel
    {
        public List<ClientInfo> listClientsIna = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM  clients WHERE  address = 'INA';";
                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                listClientsIna.Add(clientInfo);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
    public class ClientInfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
        public string created_at;
    }
}
