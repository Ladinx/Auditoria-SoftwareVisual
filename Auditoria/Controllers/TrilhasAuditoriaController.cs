using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar trilhas de auditoria
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TrilhasAuditoriaController : ControllerBase
    {
        private readonly ControleInternoContext _context;

        public TrilhasAuditoriaController(ControleInternoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as trilhas de auditoria
        /// </summary>
        /// <returns>Lista de trilhas de auditoria</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrilhaAuditoria>>> GetTrilhasAuditoria()
        {
            try
            {
                var trilhas = await _context.TrilhasAuditoria
                    .OrderByDescending(t => t.DataHora)
                    .ToListAsync();
                return Ok(trilhas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca uma trilha de auditoria específica pelo ID
        /// </summary>
        /// <param name="id">ID da trilha</param>
        /// <returns>Trilha de auditoria encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TrilhaAuditoria>> GetTrilhaAuditoria(int id)
        {
            try
            {
                var trilha = await _context.TrilhasAuditoria.FindAsync(id);

                if (trilha == null)
                {
                    return NotFound($"Trilha de auditoria com ID {id} não encontrada.");
                }

                return Ok(trilha);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria uma nova trilha de auditoria
        /// </summary>
        /// <param name="trilhaAuditoria">Dados da trilha a ser criada</param>
        /// <returns>Trilha de auditoria criada</returns>
        [HttpPost]
        public async Task<ActionResult<TrilhaAuditoria>> PostTrilhaAuditoria(TrilhaAuditoria trilhaAuditoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Definir data/hora atual se não foi informada
                if (trilhaAuditoria.DataHora == default)
                {
                    trilhaAuditoria.DataHora = DateTime.Now;
                }

                _context.TrilhasAuditoria.Add(trilhaAuditoria);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTrilhaAuditoria), new { id = trilhaAuditoria.Id }, trilhaAuditoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui uma trilha de auditoria específica
        /// </summary>
        /// <param name="id">ID da trilha a ser excluída</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrilhaAuditoria(int id)
        {
            try
            {
                var trilha = await _context.TrilhasAuditoria.FindAsync(id);

                if (trilha == null)
                {
                    return NotFound($"Trilha de auditoria com ID {id} não encontrada.");
                }

                _context.TrilhasAuditoria.Remove(trilha);
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
