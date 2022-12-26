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
    public async Task AddATeam([FromBody] Team team)
    {
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();
    }
    [HttpGet]
    [Route("api/[controller]/GetTeams")]
    public async Task<IEnumerable<Team>> GetTeams(int? ranking, string? name)
    {
        var teams = new List<Team>();
        if (!string.IsNullOrEmpty(name) && ranking != null)
        {
            teams = await _context.Teams.Where(c => c.Ranking <= ranking && c.TeamName.Contains(name)).ToListAsync();
        }
        else if (!string.IsNullOrEmpty(name) && ranking == null)
        {
            teams = await _context.Teams.Where(c => c.TeamName.Contains(name)).ToListAsync();
        }
        else if(string.IsNullOrEmpty(name) && ranking != null)
        {
            teams = await _context.Teams.Where(c => c.Ranking <= ranking).ToListAsync();

        }
        else
            
            return await _context.Teams.ToListAsync();

        return  teams;
    }
    
    
}