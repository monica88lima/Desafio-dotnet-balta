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
    public class LocalityController : ControllerBase
    {
        protected readonly ILocalityRepository _localityRepository;

        public LocalityController(ILocalityRepository repos)
        {
            _localityRepository = repos;
        }

        
        [HttpPost("/CadastroIBGE")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult> Save(LocalityModel locality)
        {
            try
            {

                if (locality is null)
                {
                    return BadRequest();
                }
                locality = LocalityService.FormatFields(locality);
                
                _localityRepository.Add(locality);
                await _localityRepository.Commit();



                return Ok(locality);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
       
        [HttpGet("/ConsultaID/{id}", Name = "ObterLocalidade")]
        [ProducesResponseType(typeof(LocalityModel), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocalityModel>> GetId(string id)
        {
            try
            {
                var local = await _localityRepository.GetSpecificLocality(X => X.Id == id);

                if (local is null)
                {
                    return NotFound($"Localidade com id: {id} Não encontrada!");
                }

                return Ok(local);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("/ConsultaCity/{City}")]
        [ProducesResponseType(typeof(LocalityModel), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<LocalityModel>>> GetCity(string city)
        {
            try
            {
                var local = await _localityRepository.GetSpecificLocality(x => x.City.Contains(city));

                if (local is null)
                {
                    return NotFound($"Municipio com nome: {city} Não encontrada!");
                }

                return Ok(local);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("/ConsultaState/{state}")]
        [ProducesResponseType(typeof(LocalityModel), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<LocalityModel>>> GetState(string state)
        {
            try
            {
                var local = await _localityRepository.GetSpecificLocality(x => x.City.Contains(state));

                if (local is null)
                {
                    return NotFound($"Estado com sigla: {state} Não encontrada!");
                }

                return Ok(local);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
