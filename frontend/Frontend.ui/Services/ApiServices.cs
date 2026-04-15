using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Frontend.ui.Services
{
    public class UserResponse
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
    }

    public static class UserSession
    {
        public static string? CurrentUsername { get; set; }
        public static int? CurrentUserId { get; set; }
    }
    public class ApiService
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://35.182.248.161:5000")
        };

        public async Task<bool> CreateCampaign(string name, string description, int dmId)
        {
            var newCampaign = new
            {
                campaignname = name,
                description = description,
                dmid = dmId, 
                archived = false,
                created_date = DateTime.Now.ToString("yyyy-MM-dd")
            };

            var response = await _client.PostAsJsonAsync("/campaigns", newCampaign);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RegisterUser(string username, string email, string password)
        {
            try
            {
                var userData = new { Username = username, Email = email, PassHash = password };
                var response = await _client.PostAsJsonAsync("/users", userData);
                return response.IsSuccessStatusCode;
            }
            catch
            {

                return false;
            }
        }


        public async Task<bool> LoginUser(string username, string password)
        {
            try
            {
                var users = await _client.GetFromJsonAsync<List<UserResponse>>("/users");

                
                var foundUser = users?.FirstOrDefault(u => u.username == username && u.password_hash == password);

                if (foundUser != null)
                {
                    // SAVING THE DATA: This is where we get the user_id for the backpack
                    UserSession.CurrentUsername = foundUser.username;
                    UserSession.CurrentUserId = foundUser.user_id;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
 





        }
}
