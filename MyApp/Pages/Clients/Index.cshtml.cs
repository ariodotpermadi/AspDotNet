using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;


namespace MyApp.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
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
                                clientInfo.created_at =  reader.GetDateTime(5).ToString();

                                listClients.Add(clientInfo);

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

    //Model, Controller untuk Modal Pop Up Index
    public class DataController : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMess = "";
        public string SuccessMess = "";

        public void onGet()
        {

        }

        public void onPost()
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                ErrorMess = "All the field are required.";
                return;
            }

            //Save data into database
           try
            {
                String connectionDB = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";

                using SqlConnection conn = new SqlConnection(connectionDB);
                conn.Open();

                String sql = "INSERT INTO clients " +
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@name", clientInfo.name);
                    command.Parameters.AddWithValue("@email", clientInfo.email);
                    command.Parameters.AddWithValue("@phone", clientInfo.phone);
                    command.Parameters.AddWithValue("@address", clientInfo.address);

                    command.ExecuteNonQuery();
                }
                conn.Close();
            }

            catch (Exception ex)
            {
                ErrorMess = ex.ToString();                                
            }

            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            SuccessMess = "New client added correctly.";

            Response.Redirect("/Clients/Index");
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


