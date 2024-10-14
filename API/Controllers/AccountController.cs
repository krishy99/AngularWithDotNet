using System.Security.Cryptography;
using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AccountController : BaseAPIController
{
    private readonly ApplicationDBContext _db;
    private readonly ITokenService _token;
    public AccountController(ApplicationDBContext _context,ITokenService tokenService)
    {
        _db = _context;
        _token = tokenService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO){
        if(await UserExists(registerDTO.Username)){
            return BadRequest("User Already Exists");
        }
        using var hmac = new HMACSHA512();

        var user = new AppUser{
            FirstName = registerDTO.Username,
            LastName = registerDTO.LastName,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key

        };
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
        return Ok(user);

    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserTokenDTO>> Login(LoginDTO registerDTO){

        var user = await _db.Users.FirstOrDefaultAsync(u => u.FirstName.ToLower() ==
         registerDTO.Username.ToLower());
        if(user == null){
            return Unauthorized("No Access");
        }
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password));
        for(int i= 0;i<computedhash.Length;i++){
            if(computedhash[i] != user.PasswordHash[i]){
                return Unauthorized("No Access");
            }
        }
       
        return new UserTokenDTO{
            UserName = user.FirstName,
            Token = _token.CreateToken(user)

        };
    }

    public async Task<bool> UserExists(string username){
        return await _db.Users.AnyAsync(x => x.FirstName.ToLower() == username.ToLower());
    }

}
