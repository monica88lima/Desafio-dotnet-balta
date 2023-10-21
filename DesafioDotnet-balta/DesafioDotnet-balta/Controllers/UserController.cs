using Asp.Versioning;
using AutoMapper;
using DesafioDotnet_balta.DTOs;
using DesafioDotnet_balta.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using Repository.Interface;

namespace DesafioDotnet_balta.Controllers
{
    
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public UserController(IUserRepository repos, IMapper mapper)
        {
            _userRepository = repos;
            _mapper=mapper;
        }

        /// <summary>
        /// Registro Usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Retorna informações do cadastro registrado</returns>
        
        [HttpPost("/CadastroUsuario")]
        [ProducesResponseType(typeof(UserRegisterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Save(UserRegisterDTO user)
        {
            try
            {
                
                if (user is null)
                {
                    return BadRequest();
                }
                var userModel = _mapper.Map<UserModel>(user);
                _userRepository.Add(userModel);
                await _userRepository.Commit();

                user.Password = "******";
               

                return Ok(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
        /// <summary>
        /// Valida o login e retorna o token de autenticação
        /// </summary>
        /// <param name="user"> Informar os campos de Email e Senha Cadastrados</param>
        /// <returns>Retorna o token para autenticação</returns>
        [HttpPost("/Login")]
        [ProducesResponseType(typeof(UserLoginDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(UserLoginDTO user)
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

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }
            


        }

    }
}
