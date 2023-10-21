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
    [Consumes("application/json")]
    public class LocalityController : ControllerBase
    {
        protected readonly ILocalityRepository _localityRepository;
        protected readonly IMapper _mapper;

        public LocalityController(ILocalityRepository repos, IMapper mapper)
        {
            _localityRepository = repos;
            _mapper = mapper;
        }


        /// <summary>
        /// Apresenta todos os registros de localidades cadastrados
        /// </summary>
        /// <returns>Retorna uma lista com todos os Registros</returns>        
        [HttpGet("/Consulta-Todos")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
                return Ok(localitiesDto);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
        /// <summary>
        /// Obtém um registro a partir do ID da localidade conforme cadastro do IBGE informado
        /// </summary>
        /// <param name="id">Digite o ID de acordo com o cadastro do IBGE</param>
        /// <returns>Retorna a localidade correspondente ao ID informado, caso não localizado o retorno informara o ID pesquisado e a mensagem de não localizada.</returns>
        [HttpGet("/Consulta-ID/{id}", Name = "ObterLocalidade")]
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
        /// <summary>
        /// Retorna uma lista com as localidades, onde o nome da cidade contenham de forma parcial ou integral o campo digitado
        /// </summary>
        /// <param name="city">Digite o nome da Cidade</param>
        /// <returns>Caso sejam localizados registros com o nome indicado, será apresentado uma lista. Caso de nenhum registro será informado que o nome digitado não contém nenhum registro no Banco de Dados. Caso o valor digitado contenha menos de 3 caracteres uma mensagem informado será apresentada.</returns>
        [HttpGet("/Consulta-Cidade/{city}")]
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
        /// <summary>
        /// Apresenta uma lista com todos os registro que façam parte do Estado informado
        /// </summary>
        /// <param name="state">Digite a sigla referente ao Estado</param>
        /// <returns>Caso sejam localizados registros com a sigla do Estado informado, será apresentado uma lista.</returns>

        [HttpGet("/Consulta-SiglaEstado/{state}")]
        [ProducesResponseType(typeof(LocalityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<LocalityDTO>>> GetState(string state)
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
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
        /// <summary>
        /// Cadastra Localidade
        /// </summary>
        /// <param name="locality"></param>
        /// <returns>Retorna objeto cadastrado</returns>
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

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }

        }
        /// <summary>
        /// Altera os dados de uma localidade
        /// </summary>
        /// <param name="id">Id do IBGE </param>
        /// <param name="locality">Dados da Localidade</param>
        /// <returns>Retorna a localidade alterada</returns>
        [HttpPut("/Alterar-Localidade/{id}")]
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

                locality.State.ToUpper();
                return Ok(locality);
            }
            catch (Exception )
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro na executação da sua solicitação!");
            }


        }
        /// <summary>
        /// Deleta um registro da Localidade
        /// </summary>
        /// <param name="id">Id do IBGE Cadastrado</param>
        /// <returns>Retorna a localidade que foi excluida</returns>
        [HttpDelete("/Deletar-Localidade/{id}")]
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
