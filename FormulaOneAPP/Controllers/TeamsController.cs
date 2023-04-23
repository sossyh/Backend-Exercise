using FormulaOneApp.Data;
using FormulaOneAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneAPP.Controllers;

[Route(template: "api/[controller]")]
[ApiController]
public class TeamsController: ControllerBase
{
    private static AppDbContext _context;

    public TeamsController(AppDbContext context)
    {
        _context = context;
    }
    // private static List<Team> teams = new List<Team>()
    // {
    //     new Team()
    //     {
    //         country = "Gremany",
    //         Id = 1,
    //         Name = "Marcedece",
    //         TeamPrinciple = "Toto Wolf"
    //     },

    //     new Team()
    //     {
    //         country = "Italy",
    //         Id = 2,
    //         Name = "Ferari",
    //         TeamPrinciple = "Mattia Binotto"
    //     },
    //     new Team()
    //     {
    //         country = "Swiss",
    //         Id = 3,
    //         Name = "Alpha Romeo",
    //         TeamPrinciple = "Federic vasseur"
    //     }

    // };
    [HttpGet]
    // [Route(template: "GetBestTeam")]
    public async Task<IActionResult>  Get()
    {
        var teams = await _context.Teams.ToListAsync();
        return Ok(teams);
    }

    [HttpGet(template:"{id:int}")]
    // [Route(template: "GetBestTeam")]
    public async Task<IActionResult> Get(int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
        return BadRequest(error:"Invalid Id");
        return Ok(team);
    }
    [HttpPost]
    // [Route(template: "GetBestTeam")]
    public async Task<IActionResult> Post(Team team)
    {
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("Get", routeValues: team.Id, value:team);
    }

    [HttpPatch]
    public async Task<IActionResult> patch(int id, string country)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
        if (team == null)
            return BadRequest(error: "Invalid Id");
        
        team.country = country;

        await _context.SaveChangesAsync();

        return NoContent();

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid id");
        
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}