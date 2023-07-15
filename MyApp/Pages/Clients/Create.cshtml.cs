using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyApp.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["email"];
            clientInfo.phone = Request.Form["phone"];
            clientInfo.address = Request.Form["address"];

            if (clientInfo.name.Length == 0 || clientInfo.email.Length == 0 || clientInfo.phone.Length == 0 || clientInfo.address.Length == 0)
            {
                ErrorMessage = "All the field are required.";
                    return;
            }

            //Save the new client into the database
            try
            {
                String connnectionString = "Data Source=.\\sqlexpress;Initial Catalog=MyDatabase;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connnectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " + 
                                 "(name, email, phone, address) VALUES " +
                                 "(@name, @email, @phone, @address);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@email", clientInfo.email);
                        command.Parameters.AddWithValue("@phone", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }


            clientInfo.name = ""; clientInfo.email = ""; clientInfo.phone = ""; clientInfo.address = "";
            SuccessMessage = "New client added correctly.";

            Response.Redirect("/Clients/Index");
        }
    }
}
