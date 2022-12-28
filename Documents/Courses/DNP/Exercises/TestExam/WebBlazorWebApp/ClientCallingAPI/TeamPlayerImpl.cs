using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using WebBlazorWebApp.Models;

namespace WebBlazorWebApp.ClientCallingAPI
{
    public class TeamPlayerImpl : ITeamPlayer
    {

        private readonly HttpClient client;
        private string uri = "https://localhost:7154/api/";
        public TeamPlayerImpl(HttpClient client)
        {
            this.client = client;
        }
        [HttpPut]
        public async Task<string> AddPlayer(Player player, string teamName)
        {
            string result = " ";
            HttpResponseMessage response = await client.PutAsJsonAsync(uri+$"Player/{teamName}", player);
        
            if (response.IsSuccessStatusCode)
            {
                result = response.StatusCode.ToString();
            }
            return result;

        }

        public async Task<string> CreateTeam(Team team)
        {
          
            string result = " ";
            
            HttpResponseMessage response = await client.PostAsJsonAsync(uri+"Team/AddTeam", team);
            if (response.IsSuccessStatusCode)
            {
                result = response.StatusCode.ToString();
            }
            else
            {
                result = "Something went Wrong";
            }
            return result;
        }

        public async Task<List<Team>> GetTeams()
        {
            //throw new NotImplementedException();
           
            HttpResponseMessage response = await client.GetAsync(uri+"Team/AllTeams");
            var content = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(content);
            return teams;
        }
        [HttpDelete]
        public async Task<string> RemovePlayer(int playerId)
        {
            string result = " ";

            var response = await client.DeleteAsync(uri + $"player/{playerId}");

            if (response.IsSuccessStatusCode)
            {
                result = response.StatusCode.ToString();
            }
            return result;
        }

        [HttpGet]
        public async Task<List<Team>> GetTeamsFiltered([FromQuery]string? name = null, [FromQuery]int? ranking = null)
        {

            if(ranking == 0)
            {
                ranking = null;
            }
           HttpResponseMessage response;
         
           response = await client.GetAsync(uri + $"team/getTeams?name={name}&ranking={ranking}");
            

            
            var content = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<List<Team>>(content);
            return teams;
        }
      
    }
}
