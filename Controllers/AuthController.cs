using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YungChingAPI.Data;
using YungChingAPI.Dtos;
using YungChingAPI.Models;

namespace YungChingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo){
               _repo=repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto ){
          
          //validate request
          
           userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.UserExists(userForRegisterDto.Username)){
                 return BadRequest("Username already exists");
            }
            var userToCreate =new User{
               Username= userForRegisterDto.Username
            };

            var createdUser =await _repo.Register(userToCreate,userForRegisterDto.Password);
            
            return StatusCode(201);
        }
    }
}