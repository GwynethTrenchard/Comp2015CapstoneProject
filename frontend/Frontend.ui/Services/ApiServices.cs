using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

namespace Frontend.ui.Services
{

    //public class Character
    //{
    //    public int character_id { get; set; }
    //    public int campaign_id { get; set; }
    //    public string name { get; set; }
    //    public string character_class { get; set; }
    //    public int level { get; set; }

    //}

    //public class CharacterStats
    //{
    //    public int Strength { get; set; } = 10;
    //    public int Dexterity { get; set; } = 10;
    //    public int Constitution { get; set; } = 10;
    //    public int Intelligence { get; set; } = 10;
    //    public int Wisdom { get; set; } = 10;
    //    public int Charisma { get; set; } = 10;
    //}
    public class Campaign
    {
        public int campaign_id { get; set; }
        public string campaign_name { get; set; }
        public string description { get; set; }
        public int dm_id { get; set; }
        public object created_date { get; set; }
        public object last_run_date { get; set; }
    }
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
        public static int? ActiveCampaignId { get; set; }
    }
    public class ApiService
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("http://35.183.174.68:5000")
        };


        //tried this as well but was having issues so i commented it out. Couldn't figure out how to make it work
        //public async Task<bool> CreateQuest(int timelineId, string title, string description)
        //{
        //    var payload = new
        //    {
        //        TimelineId = timelineId,
        //        QuestName = title,
        //        Description = description,
        //        Outcome = "",
        //        DmNotes = "",
        //        Access = true,
        //        Complete = false
        //    };  

        //    var response = await _client.PostAsJsonAsync("/quests", payload);

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        string errorBody = await response.Content.ReadAsStringAsync();
        //        System.Diagnostics.Debug.WriteLine($"SERVER ERROR: {response.StatusCode} - {errorBody}");
        //    }
        //    return response.IsSuccessStatusCode;
        //}




        //Samething with this. couldn't figure out how to get it to work so i commented it out.

        //public async Task<bool> CreateCharacter(int campaignId, string name, string charClass, int level, string statsJson)
        //{
        //    var payload = new
        //    {
        //        campaign_id = campaignId,
        //        name = name,
        //        character_class = charClass,
        //        level = level,
        //        stats = statsJson // This is the serialized string from your textboxes
        //    };

        //    var response = await _client.PostAsJsonAsync("/characters", payload);
        //    return response.IsSuccessStatusCode;
        //}
        public async Task<List<Campaign>> GetUserCampaigns()
        {
            try
            {
                var campaigns = await _client.GetFromJsonAsync<List<Campaign>>("/campaigns");

                if (campaigns == null) return new List<Campaign>();

                int currentUserId = UserSession.CurrentUserId ?? 0;
                return campaigns.Where(c => c.dm_id == currentUserId).ToList();
            }
            catch
            {
                return new List<Campaign>();
            }
        }

        public async Task<bool> CreateCampaign(string name, string description, int dmId)
        {
            var newCampaign = new
            {
                campaignname = name,
                description = description,
                dmid = dmId, 
                archived = false,
                created_date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                last_run_date = (string?)null
            };

            var response = await _client.PostAsJsonAsync("/campaigns", newCampaign);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> RegisterUser(string username, string email, string password)
        {
            try
            {
                var userData = new { Username = username, Email = email, PasswordHash = password }; 
                var response = await _client.PostAsJsonAsync("/users", userData);

                return response.IsSuccessStatusCode;

            }
            catch(Exception ex) 
            {
                System.Diagnostics.Debug.WriteLine($"Network Error: {ex.Message}");
                return false;
            }
        }

        // removed && u.password_hash == password from the LoginUser logic for now
        // also removed , string password from parameters
        public async Task<bool> LoginUser(string username)
        {
            try
            {
                var users = await _client.GetFromJsonAsync<List<UserResponse>>("/users");

                
                var foundUser = users?.FirstOrDefault(u => u.username == username);

                if (foundUser != null)
                { 
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
