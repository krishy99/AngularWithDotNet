using System.ComponentModel;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
[ApiController]
[Route("api/[Controller]")]
public class UsersController : ControllerBase
{
    private readonly ApplicationDBContext _db;
    public UsersController(ApplicationDBContext _context){
        _db = _context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        var lstuserrs = await _db.Users.ToListAsync();
        if(lstuserrs == null){
            return NotFound();
        }
        return Ok(lstuserrs);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUsers(int id){
        var user = await _db.Users.FindAsync(id);
        if(user == null){
            return NotFound();
        }
        return Ok(user);

    }


}
