using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar políticas de controle interno
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PoliticasController : ControllerBase
    {
        private readonly ControleInternoContext _context;

        public PoliticasController(ControleInternoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as políticas
        /// </summary>
        /// <returns>Lista de políticas</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Politica>>> GetPoliticas()
        {
            try
            {
                var politicas = await _context.Politicas.ToListAsync();
                return Ok(politicas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca uma política específica pelo ID
        /// </summary>
        /// <param name="id">ID da política</param>
        /// <returns>Política encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Politica>> GetPolitica(int id)
        {
            try
            {
                var politica = await _context.Politicas.FindAsync(id);

                if (politica == null)
                {
                    return NotFound($"Política com ID {id} não encontrada.");
                }

                return Ok(politica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria uma nova política
        /// </summary>
        /// <param name="politica">Dados da política a ser criada</param>
        /// <returns>Política criada</returns>
        [HttpPost]
        public async Task<ActionResult<Politica>> PostPolitica(Politica politica)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Definir data de criação se não foi informada
                if (politica.DataCriacao == default)
                {
                    politica.DataCriacao = DateTime.Now;
                }

                _context.Politicas.Add(politica);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPolitica), new { id = politica.Id }, politica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui uma política específica
        /// </summary>
        /// <param name="id">ID da política a ser excluída</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolitica(int id)
        {
            try
            {
                var politica = await _context.Politicas.FindAsync(id);

                if (politica == null)
                {
                    return NotFound($"Política com ID {id} não encontrada.");
                }

                _context.Politicas.Remove(politica);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
