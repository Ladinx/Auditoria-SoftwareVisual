using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar logs de acesso
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LogsAcessoController : ControllerBase
    {
        private readonly ControleInternoContext _context;

        public LogsAcessoController(ControleInternoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os logs de acesso
        /// </summary>
        /// <returns>Lista de logs de acesso</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogAcesso>>> GetLogsAcesso()
        {
            try
            {
                var logs = await _context.LogsAcesso
                    .OrderByDescending(l => l.DataHora)
                    .ToListAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca um log de acesso específico pelo ID
        /// </summary>
        /// <param name="id">ID do log</param>
        /// <returns>Log de acesso encontrado</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LogAcesso>> GetLogAcesso(int id)
        {
            try
            {
                var log = await _context.LogsAcesso.FindAsync(id);

                if (log == null)
                {
                    return NotFound($"Log de acesso com ID {id} não encontrado.");
                }

                return Ok(log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria um novo log de acesso
        /// </summary>
        /// <param name="logAcesso">Dados do log a ser criado</param>
        /// <returns>Log de acesso criado</returns>
        [HttpPost]
        public async Task<ActionResult<LogAcesso>> PostLogAcesso(LogAcesso logAcesso)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Definir data/hora atual se não foi informada
                if (logAcesso.DataHora == default)
                {
                    logAcesso.DataHora = DateTime.Now;
                }

                _context.LogsAcesso.Add(logAcesso);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLogAcesso), new { id = logAcesso.Id }, logAcesso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui um log de acesso específico
        /// </summary>
        /// <param name="id">ID do log a ser excluído</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogAcesso(int id)
        {
            try
            {
                var log = await _context.LogsAcesso.FindAsync(id);

                if (log == null)
                {
                    return NotFound($"Log de acesso com ID {id} não encontrado.");
                }

                _context.LogsAcesso.Remove(log);
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
