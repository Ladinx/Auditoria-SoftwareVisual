using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;

namespace Auditoria.Rotas
{
    public static class DeleteRoutes
    {
        public static void MapDeleteRoutes(this WebApplication app)
        {
            // ========== ROTAS DE LOGS DE ACESSO ==========

            // Exclui um log de acesso específico
            app.MapDelete("/api/logsacesso/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var log = await context.LogsAcesso.FindAsync(id);

                    if (log == null)
                    {
                        return Results.NotFound($"Log de acesso com ID {id} não encontrado.");
                    }

                    context.LogsAcesso.Remove(log);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE PERMISSÕES ==========

            // Exclui uma permissão específica
            app.MapDelete("/api/permissoes/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var permissao = await context.Permissoes.FindAsync(id);

                    if (permissao == null)
                    {
                        return Results.NotFound($"Permissão com ID {id} não encontrada.");
                    }

                    context.Permissoes.Remove(permissao);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE POLÍTICAS ==========

            // Exclui uma política específica
            app.MapDelete("/api/politicas/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var politica = await context.Politicas.FindAsync(id);

                    if (politica == null)
                    {
                        return Results.NotFound($"Política com ID {id} não encontrada.");
                    }

                    context.Politicas.Remove(politica);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE TRILHAS DE AUDITORIA ==========

            // Exclui uma trilha de auditoria específica
            app.MapDelete("/api/trilhasauditoria/{id}", async (int id, ControleInternoContext context) =>
            {
                try
                {
                    var trilha = await context.TrilhasAuditoria.FindAsync(id);

                    if (trilha == null)
                    {
                        return Results.NotFound($"Trilha de auditoria com ID {id} não encontrada.");
                    }

                    context.TrilhasAuditoria.Remove(trilha);
                    await context.SaveChangesAsync();

                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
        }
    }
}

