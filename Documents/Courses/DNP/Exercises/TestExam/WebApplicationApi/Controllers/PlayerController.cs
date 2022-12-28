using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationApi.DatabaseContext;
using WebApplicationApi.Model;

namespace WebApplicationApi.Controllers;

public class PlayerController : ControllerBase
{
    private readonly FootballContext _context;

    public PlayerController(FootballContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("api/[controller]/{teamName}")] // GET}
    public async Task<IEnumerable<Player>> GetPlayersOfThisTeam(string teamName)
    {
        var team= await _context.Teams.Include(c=> c.Players).Where(c => c.TeamName == teamName).FirstOrDefaultAsync();
        return team.Players;
    }

    [HttpPut]
    [Route("api/[controller]/{teamName}")]
    public async Task AddPlayer(string teamName, [FromBody]Player player)
    {
       Team team = new Team();

       
        try
        {
           team= await _context.Teams.Where(c => c.TeamName == teamName).FirstOrDefaultAsync();
           team.Players = new List<Player>{ new Player(){ 
               GoalsThisSeason = player.GoalsThisSeason,
               Name = player.Name,
               Position = player.Position,
               Salary = player.Salary,
               ShirtNumber = player.ShirtNumber
          }};
          
            _context.Teams.Update(team);
            // await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
  
    }

    [HttpDelete]
    [Route("api/[controller]/{id}")] // GET}
    public async Task DeletePlayer(int id)
    {
        try
        {
            var playeer = await _context.Players.FindAsync(id);
            _context.Players.Remove(playeer);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
       
    }
    
    
}