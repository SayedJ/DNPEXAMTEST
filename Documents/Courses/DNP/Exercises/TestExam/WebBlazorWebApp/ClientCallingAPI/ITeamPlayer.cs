using WebBlazorWebApp.Models;

namespace WebBlazorWebApp.ClientCallingAPI
{
    public interface ITeamPlayer
    {

        Task<List<Team>> GetTeams();

        Task<string> AddPlayer(Player player, string TeamName);

        Task<string> RemovePlayer(int playerId);

        Task<string> CreateTeam(Team team);

        Task<List<Team>> GetTeamsFiltered(string? name, int? ranking);
    }
}
