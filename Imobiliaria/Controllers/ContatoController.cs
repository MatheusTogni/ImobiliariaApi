using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using Service;
using Service.Dto;
using System.ComponentModel.DataAnnotations;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como 'api/contato'.
    public class ContatoController : ControllerBase
    {
        private readonly ContatoService _contatoService;

        // Injeção do contexto do banco de dados para inicializar o serviço de contatos.
        public ContatoController(MyDbContext dbContext)
        {
            _contatoService = new ContatoService(new ContatoRepository(dbContext));
        }

        // Endpoint para listar todos os contatos.
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var contatos = _contatoService.GetAllContatos();
                if (contatos == null || !contatos.Any())
                    return NotFound("Nenhum contato cadastrado."); // Retorna 404 se não houver contatos.

                return Ok(contatos); // Retorna 200 com a lista de contatos.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para buscar um contato específico pelo ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var contato = _contatoService.GetContatoById(id);
                if (contato == null)
                    return NotFound("Contato não encontrado."); // Retorna 404 se o contato não existir.

                return Ok(contato); // Retorna 200 com o contato encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para criar um novo contato.
        [HttpPost]
        public IActionResult Create([FromBody] ContatoDto contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _contatoService.CreateContato(contatoDto);
                return CreatedAtAction(nameof(GetAll), null); // Retorna 201 indicando a criação bem-sucedida.
            }
            catch (ValidationException ex)
            {
                return BadRequest($"Erro de validação: {ex.Message}"); // Retorna 400 em caso de erro de validação.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualizar um contato existente.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ContatoDto contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _contatoService.UpdateContato(id, contatoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Contato não encontrado."); // Retorna 404 se o contato não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para atualização parcial de um contato.
        [HttpPatch("{id}")]
        public IActionResult PartialUpdate(int id, [FromBody] ContatoDto contatoDto)
        {
            if (contatoDto == null)
                return BadRequest("Dados inválidos."); // Retorna 400 se os dados enviados forem nulos.

            try
            {
                _contatoService.PartialUpdateContato(id, contatoDto);
                return NoContent(); // Retorna 204 para indicar que a atualização foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Contato não encontrado."); // Retorna 404 se o contato não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }

        // Endpoint para deletar um contato por ID.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Id inválido."); // Retorna 400 se o ID for inválido.

            try
            {
                _contatoService.DeleteContato(id);
                return NoContent(); // Retorna 204 para indicar que a exclusão foi bem-sucedida.
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Contato não encontrado."); // Retorna 404 se o contato não for encontrado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro inesperado.
            }
        }
    }
}
