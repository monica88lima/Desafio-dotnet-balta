using Asp.Versioning;
using AutoMapper;
using DesafioDotnet_balta.DTOs;
using DesafioDotnet_balta.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using Repository.Interface;

namespace DesafioDotnet_balta.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("Application/json")]
    public class LocalityController : ControllerBase
    {
        protected readonly ILocalityRepository _localityRepository;
        protected readonly IMapper _mapper;

        public LocalityController(ILocalityRepository repos, IMapper mapper)
        {
            _localityRepository = repos;
            _mapper = mapper;
        }


                
        [HttpGet("/Consulta-Todos")]
        public async Task<ActionResult<IEnumerable<LocalityDTO>>> GetAll()
        {
            try
            {
                var localities = await _localityRepository.Get().ToListAsync();

                if (localities.Count() == 0)
                {
                    return NotFound("Não há registros Cadastrados!");
                }
                var localitiesDto = _mapper.Map<List<LocalityDTO>>(localities);
                return (localitiesDto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }
               
        }
        [HttpGet("/Consulta-ID", Name = "ObterLocalidade")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocalityDTO>> GetId(string id)
        {
            try
            {
                var local = await _localityRepository.GetById(X => X.Id == id);

                if (local is null)
                {
                    return NotFound($"Localidade com id: {id} Não encontrada!");
                }
                var localitiesDto = _mapper.Map<LocalityDTO>(local);
                return Ok(localitiesDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }

        [HttpGet("/Consulta-Cidade")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<LocalityDTO>>> GetCity(string city)
        {
            try
            {
                if (string.IsNullOrEmpty(city) || city.Length < 3)
                {
                    return BadRequest($"A pesquisa deve contém no minimo 3 caracteres.");
                }
                var local = await _localityRepository.GetSpecificLocality(x => x.City.Contains(city));

                if (local.Count() == 0)
                {
                    return NotFound($"Municipio com nome: {city} Não encontrada!");
                }
                var localitiesDto = _mapper.Map<List<LocalityDTO>>(local);
                return Ok(localitiesDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
       

        [HttpGet("/Consulta-SiglaEstado")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public  async Task<ActionResult<IEnumerable<LocalityDTO>>> GetState(string state)
        {
            try
            {
                var local = await _localityRepository.GetSpecificLocality(x => x.State.Contains(state));

                if (local.Count() == 0)
                {
                    return NotFound($"Estado com sigla: {state} Não encontrada!");
                }
                var localitiesDto = _mapper.Map<List<LocalityDTO>>(local);
                return Ok(localitiesDto);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
        [HttpPost("/Cadastro-Localidade")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Save(LocalityDTO locality)
        {
            try
            {

                if (locality is null)
                {
                    return BadRequest();
                }
                var local = _mapper.Map<LocalityModel>(locality);
                local = LocalityService.FormatFields(local);

                _localityRepository.Add(local);
                await _localityRepository.Commit();


                return new CreatedAtRouteResult("ObterLocalidade",
                    new { id = local.Id }, locality); 
                    
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,"Ocorreu um erro na executação da sua solicitação!");
            }

        }
        [HttpPut("/Alterar-Localidade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocalityDTO>> Update(string id, LocalityDTO locality)
        {
            try
            {
                if (id != locality.Id)
                {
                    return BadRequest();
                }
                var local = _mapper.Map<LocalityModel>(locality);
                local = LocalityService.FormatFields(local);
                local = LocalityService.AlterDate(local);

                _localityRepository.Update(local);
                await _localityRepository.Commit();

                return Ok(locality);
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }


        }
        [Authorize]
        [HttpDelete("/Deletar-Localidade")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocalityDTO>> Delete(string id)
        {
            try
            {
                var locality = await _localityRepository.GetById(x => x.Id == id);
                if (locality == null)
                {
                    return NotFound("Código IBGE não localizado!");
                }
                var localitiesDto = _mapper.Map<LocalityDTO>(locality);

                _localityRepository.Delete(locality);
                await _localityRepository.Commit();

                return Ok(localitiesDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
    }
}
