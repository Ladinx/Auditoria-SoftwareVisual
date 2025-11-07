using Microsoft.EntityFrameworkCore;
using Auditoria.Data;
using Auditoria.Models;

namespace Auditoria.Rotas
{
    public static class PostRoutes
    {
        public static void MapPostRoutes(this WebApplication app)
        {
            // ========== ROTAS DE LOGS DE ACESSO ==========

            // Cria um novo log de acesso
            app.MapPost("/api/logsacesso", async (LogAcesso logAcesso, ControleInternoContext context) =>
            {
                try
                {
                    // Validar modelo
                    if (logAcesso == null)
                    {
                        return Results.BadRequest("Dados do log de acesso são obrigatórios.");
                    }

                    // Definir data/hora atual se não foi informada
                    if (logAcesso.DataHora == default)
                    {
                        logAcesso.DataHora = DateTime.Now;
                    }

                    context.LogsAcesso.Add(logAcesso);
                    await context.SaveChangesAsync();

                    return Results.Created($"/api/logsacesso/{logAcesso.Id}", logAcesso);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE PERMISSÕES ==========

            // Cria uma nova permissão
            app.MapPost("/api/permissoes", async (Permissao permissao, ControleInternoContext context) =>
            {
                try
                {
                    // Validar modelo
                    if (permissao == null)
                    {
                        return Results.BadRequest("Dados da permissão são obrigatórios.");
                    }

                    context.Permissoes.Add(permissao);
                    await context.SaveChangesAsync();

                    return Results.Created($"/api/permissoes/{permissao.Id}", permissao);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE POLÍTICAS ==========

            // Cria uma nova política
            app.MapPost("/api/politicas", async (Politica politica, ControleInternoContext context) =>
            {
                try
                {
                    // Validar modelo
                    if (politica == null)
                    {
                        return Results.BadRequest("Dados da política são obrigatórios.");
                    }

                    // Definir data de criação se não foi informada
                    if (politica.DataCriacao == default)
                    {
                        politica.DataCriacao = DateTime.Now;
                    }

                    context.Politicas.Add(politica);
                    await context.SaveChangesAsync();

                    return Results.Created($"/api/politicas/{politica.Id}", politica);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });

            // ========== ROTAS DE TRILHAS DE AUDITORIA ==========

            // Cria uma nova trilha de auditoria
            app.MapPost("/api/trilhasauditoria", async (TrilhaAuditoria trilhaAuditoria, ControleInternoContext context) =>
            {
                try
                {
                    // Validar modelo
                    if (trilhaAuditoria == null)
                    {
                        return Results.BadRequest("Dados da trilha de auditoria são obrigatórios.");
                    }

                    // Definir data/hora atual se não foi informada
                    if (trilhaAuditoria.DataHora == default)
                    {
                        trilhaAuditoria.DataHora = DateTime.Now;
                    }

                    context.TrilhasAuditoria.Add(trilhaAuditoria);
                    await context.SaveChangesAsync();

                    return Results.Created($"/api/trilhasauditoria/{trilhaAuditoria.Id}", trilhaAuditoria);
                }
                catch (Exception ex)
                {
                    return Results.Problem($"Erro interno do servidor: {ex.Message}", statusCode: 500);
                }
            });
        }
    }
}

