using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabalhoAPI.Data;
using TrabalhoAPI.Models;

namespace TrabalhoAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar permissões de acesso
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PermissoesController : ControllerBase
    {
        private readonly ControleInternoContext _context;

        public PermissoesController(ControleInternoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as permissões
        /// </summary>
        /// <returns>Lista de permissões</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permissao>>> GetPermissoes()
        {
            try
            {
                var permissoes = await _context.Permissoes.ToListAsync();
                return Ok(permissoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca uma permissão específica pelo ID
        /// </summary>
        /// <param name="id">ID da permissão</param>
        /// <returns>Permissão encontrada</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Permissao>> GetPermissao(int id)
        {
            try
            {
                var permissao = await _context.Permissoes.FindAsync(id);

                if (permissao == null)
                {
                    return NotFound($"Permissão com ID {id} não encontrada.");
                }

                return Ok(permissao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Cria uma nova permissão
        /// </summary>
        /// <param name="permissao">Dados da permissão a ser criada</param>
        /// <returns>Permissão criada</returns>
        [HttpPost]
        public async Task<ActionResult<Permissao>> PostPermissao(Permissao permissao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Permissoes.Add(permissao);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPermissao), new { id = permissao.Id }, permissao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Exclui uma permissão específica
        /// </summary>
        /// <param name="id">ID da permissão a ser excluída</param>
        /// <returns>Resultado da operação</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermissao(int id)
        {
            try
            {
                var permissao = await _context.Permissoes.FindAsync(id);

                if (permissao == null)
                {
                    return NotFound($"Permissão com ID {id} não encontrada.");
                }

                _context.Permissoes.Remove(permissao);
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
