using DesafioDotnet_balta.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.Interface;

namespace DesafioDotnet_balta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository repos)
        {
            _userRepository = repos;
                
        }

        [Authorize]
        [HttpPost("/CadastroUsuario")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Save(UserModel user)
        {
            try
            {
                
                if (user is null)
                {
                    return BadRequest();
                }
                _userRepository.Add(user);
                await _userRepository.Commit();

               

                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("/Login")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(UserModel user)
        {
            try
            {
                var person = _userRepository.Get(user.Email, user.Password);
                if (person == null) return NotFound("Usuário e/ou Senha inválido.");

                var token = TokenService.Generate(person);

                return token;


            }
            catch (Exception)
            {

                throw;
            }
            


        }

    }
}
