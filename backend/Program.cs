using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using MySqlConnector;
using System.Collections.Generic;
using System;

namespace BackendAPI
{
    record UserInput(string Username, string Email, string PasswordHash);
    record SubscriptionInput(int UserId, bool Subscribed);
    record CampaignInput(string CampaignName, string Description, int DmId, bool Archived, DateTime? CreatedDate, DateTime? LastRunDate);
    record PlayerInput(int PlayerId, int CampaignId, bool Dm);
    record InviteInput(int CampaignId, string Code, string Link, DateTime? Expires);
    record JournalInput(int CampaignId, int UserId, DateTime? LastModified, string Notes);
    record TimelineInput(int CampaignId, string Description);
    record QuestInput(int TimelineId, string QuestName, string Description, string Outcome, string DmNotes, bool Access, bool Complete);
    record SessionInput(int JournalId, int QuestId, DateTime? SessionDate);
    record CharacterInput(int CampaignId, int UserId, bool Npc, string Name, string Biography);
    record CharacterSheetInput(int CharacterId, string Stats);

    internal class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string cs = builder.Configuration.GetConnectionString("Default")!;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader());
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

            app.MapGet("/users", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Users;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        user_id = reader["user_id"],
                        username = reader["username"],
                        email = reader["email"]
                    });
                }
                return results;
            });

            app.MapPost("/users", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<UserInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Users (username, email, password_hash) VALUES (@username, @email, @password_hash);",
                    conn
                );
                cmd.Parameters.AddWithValue("@username", form.Username);
                cmd.Parameters.AddWithValue("@email", form.Email);
                cmd.Parameters.AddWithValue("@password_hash", form.PasswordHash);
                cmd.ExecuteNonQuery();
                return Results.Ok("User added successfully!");
            });

            app.MapGet("/subscriptions", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Subscription;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        subscription_id = reader["subscription_id"],
                        user_id = reader["user_id"],
                        subscribed = reader["subscribed"]
                    });
                }
                return results;
            });

            app.MapPost("/subscriptions", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<SubscriptionInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Subscription (user_id, subscribed) VALUES (@user, @subscribed);",
                    conn
                );
                cmd.Parameters.AddWithValue("@user", form.UserId);
                cmd.Parameters.AddWithValue("@subscribed", form.Subscribed);
                cmd.ExecuteNonQuery();
                return Results.Ok("Subscription added successfully!");
            });

            app.MapGet("/campaigns", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Campaign;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        campaign_id = reader["campaign_id"],
                        campaign_name = reader["campaign_name"],
                        description = reader["description"],
                        dm_id = reader["dm_id"],
                        archived = reader["archived"],
                        created_date = reader["created_date"],
                        last_run_date = reader["last_run_date"]
                    });
                }
                return results;
            });

            app.MapPost("/campaigns", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<CampaignInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Campaign (campaign_name, description, dm_id, archived, created_date, last_run_date) VALUES (@name, @desc, @dm, @archived, @created, @last);",
                    conn
                );
                cmd.Parameters.AddWithValue("@name", form.CampaignName);
                cmd.Parameters.AddWithValue("@desc", form.Description);
                cmd.Parameters.AddWithValue("@dm", form.DmId);
                cmd.Parameters.AddWithValue("@archived", form.Archived);
                cmd.Parameters.AddWithValue("@created", form.CreatedDate);
                cmd.Parameters.AddWithValue("@last", form.LastRunDate);
                cmd.ExecuteNonQuery();
                return Results.Ok("Campaign added successfully!");
            });

            app.MapGet("/players", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Player;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        player_id = reader["player_id"],
                        campaign_id = reader["campaign_id"],
                        dm = reader["dm"]
                    });
                }
                return results;
            });

            app.MapPost("/players", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<PlayerInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Player (player_id, campaign_id, dm) VALUES (@player, @campaign, @dm);",
                    conn
                );
                cmd.Parameters.AddWithValue("@player", form.PlayerId);
                cmd.Parameters.AddWithValue("@campaign", form.CampaignId);
                cmd.Parameters.AddWithValue("@dm", form.Dm);
                cmd.ExecuteNonQuery();
                return Results.Ok("Player added successfully!");
            });

            app.MapGet("/invites", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Invite;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        invite_id = reader["invite_id"],
                        campaign_id = reader["campaign_id"],
                        code = reader["code"],
                        link = reader["link"],
                        expires = reader["expires"]
                    });
                }
                return results;
            });

            app.MapPost("/invites", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<InviteInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Invite (campaign_id, code, link, expires) VALUES (@campaign, @code, @link, @expires);",
                    conn
                );
                cmd.Parameters.AddWithValue("@campaign", form.CampaignId);
                cmd.Parameters.AddWithValue("@code", form.Code);
                cmd.Parameters.AddWithValue("@link", form.Link);
                cmd.Parameters.AddWithValue("@expires", form.Expires);
                cmd.ExecuteNonQuery();
                return Results.Ok("Invite added successfully!");
            });

            app.MapGet("/journals", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Journal;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        journal_id = reader["journal_id"],
                        campaign_id = reader["campaign_id"],
                        user_id = reader["user_id"],
                        last_modified = reader["last_modified"],
                        notes = reader["notes"]
                    });
                }
                return results;
            });

            app.MapPost("/journals", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<JournalInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Journal (campaign_id, user_id, last_modified, notes) VALUES (@campaign, @user, @last_modified, @notes);",
                    conn
                );
                cmd.Parameters.AddWithValue("@campaign", form.CampaignId);
                cmd.Parameters.AddWithValue("@user", form.UserId);
                cmd.Parameters.AddWithValue("@last_modified", form.LastModified);
                cmd.Parameters.AddWithValue("@notes", form.Notes);
                cmd.ExecuteNonQuery();
                return Results.Ok("Journal added successfully!");
            });

            app.MapGet("/timelines", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Timeline;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        timeline_id = reader["timeline_id"],
                        campaign_id = reader["campaign_id"],
                        description = reader["description"]
                    });
                }
                return results;
            });

            app.MapPost("/timelines", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<TimelineInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Timeline (campaign_id, description) VALUES (@campaign, @desc);",
                    conn
                );
                cmd.Parameters.AddWithValue("@campaign", form.CampaignId);
                cmd.Parameters.AddWithValue("@desc", form.Description);
                cmd.ExecuteNonQuery();
                return Results.Ok("Timeline added successfully!");
            });

            app.MapGet("/quests", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Quests;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        quest_id = reader["quest_id"],
                        timeline_id = reader["timeline_id"],
                        quest_name = reader["quest_name"],
                        description = reader["description"],
                        outcome = reader["outcome"],
                        dm_notes = reader["dm_notes"],
                        access = reader["access"],
                        complete = reader["complete"]
                    });
                }
                return results;
            });

            app.MapPost("/quests", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<QuestInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Quests (timeline_id, quest_name, description, outcome, dm_notes, access, complete) " +
                    "VALUES (@timeline, @name, @desc, @outcome, @notes, @access, @complete);",
                    conn
                );
                cmd.Parameters.AddWithValue("@timeline", form.TimelineId);
                cmd.Parameters.AddWithValue("@name", form.QuestName);
                cmd.Parameters.AddWithValue("@desc", form.Description);
                cmd.Parameters.AddWithValue("@outcome", form.Outcome);
                cmd.Parameters.AddWithValue("@notes", form.DmNotes);
                cmd.Parameters.AddWithValue("@access", form.Access);
                cmd.Parameters.AddWithValue("@complete", form.Complete);
                cmd.ExecuteNonQuery();
                return Results.Ok("Quest added successfully!");
            });

            app.MapGet("/sessions", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Session;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        session_id = reader["session_id"],
                        journal_id = reader["journal_id"],
                        quest_id = reader["quest_id"],
                        session_date = reader["session_date"]
                    });
                }
                return results;
            });

            app.MapPost("/sessions", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<SessionInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Session (journal_id, quest_id, session_date) VALUES (@journal, @quest, @date);",
                    conn
                );
                cmd.Parameters.AddWithValue("@journal", form.JournalId);
                cmd.Parameters.AddWithValue("@quest", form.QuestId);
                cmd.Parameters.AddWithValue("@date", form.SessionDate);
                cmd.ExecuteNonQuery();
                return Results.Ok("Session added successfully!");
            });

            app.MapGet("/characters", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM Characters;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        character_id = reader["character_id"],
                        campaign_id = reader["campaign_id"],
                        user_id = reader["user_id"],
                        npc = reader["npc"],
                        name = reader["name"],
                        biography = reader["biography"]
                    });
                }
                return results;
            });

            app.MapPost("/characters", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<CharacterInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO Characters (campaign_id, user_id, npc, name, biography) VALUES (@campaign, @user, @npc, @name, @bio);",
                    conn
                );
                cmd.Parameters.AddWithValue("@campaign", form.CampaignId);
                cmd.Parameters.AddWithValue("@user", form.UserId);
                cmd.Parameters.AddWithValue("@npc", form.Npc);
                cmd.Parameters.AddWithValue("@name", form.Name);
                cmd.Parameters.AddWithValue("@bio", form.Biography);
                cmd.ExecuteNonQuery();
                return Results.Ok("Character added successfully!");
            });

            app.MapGet("/charactersheets", () =>
            {
                var results = new List<object>();
                using var conn = new MySqlConnection(cs);
                conn.Open();
                using var cmd = new MySqlCommand("SELECT * FROM CharacterSheet;", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(new
                    {
                        character_id = reader["character_id"],
                        stats = reader["stats"]
                    });
                }
                return results;
            });

            app.MapPost("/charactersheets", async (HttpContext context) =>
            {
                var form = await context.Request.ReadFromJsonAsync<CharacterSheetInput>();
                if (form == null) return Results.BadRequest("Invalid input");

                using var conn = new MySqlConnection(cs);
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO CharacterSheet (character_id, stats) VALUES (@char, @stats);",
                    conn
                );
                cmd.Parameters.AddWithValue("@char", form.CharacterId);
                cmd.Parameters.AddWithValue("@stats", form.Stats);
                cmd.ExecuteNonQuery();
                return Results.Ok("CharacterSheet added successfully!");
            });

            app.Run("http://0.0.0.0:5000");
        }
    }
}