using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.DatabaseContext;
using WebApplicationApi.Model;

namespace WebApplicationApi.Controllers;

public class TeamController : ControllerBase
{
    private readonly FootballContext _context;

    public TeamController(FootballContext context)
    {
        _context = context;
    }

    [Route("api/[controller]/AllTeams")]
    [HttpGet]
    public async Task<IEnumerable<Team>> GetAllTeams()
    {
        return  await _context.Teams.Include(c=> c.Players).ToListAsync();
    }
    [HttpPost]
    [Route("api/[controller]/AddTeam")]
    public async Task<string> AddATeam([FromBody] Team team)
    {   string msg = string.Empty; 
        var isExist = await _context.Teams.AnyAsync(c => c.TeamName == team.TeamName);
        if (isExist)
        {
            msg = $"a team with the name {team.TeamName} already exist.";
            throw new Exception(msg);
        }
        else
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            msg = "OK";
        }
        return msg;
      
    }
    [Route("api/[controller]/GetTeams/")]
    [HttpGet]
    public async Task<IEnumerable<Team>> GetTeams(string? name, int? ranking)
    {
        var teams = new List<Team>();
        if (!string.IsNullOrEmpty(name) && ranking != null)
        {
            teams = await _context.Teams.Include(c=>c.Players).Where(c => c.Ranking <= ranking && c.TeamName.Contains(name)).ToListAsync();
        }
        else if (!string.IsNullOrEmpty(name) && ranking == null)
        {
            teams = await _context.Teams.Include(c => c.Players).Where(c => c.TeamName.Contains(name)).ToListAsync();
        }
        else if(string.IsNullOrEmpty(name) && ranking != null)
        {
            teams = await _context.Teams.Include(c => c.Players).Where(c => c.Ranking <= ranking).ToListAsync();

        }
        else
            
            return await _context.Teams.Include(c => c.Players).ToListAsync();

        return  teams;
    }
    
    
}