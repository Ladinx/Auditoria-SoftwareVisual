# API de Controle Interno

Sistema de API RESTful para controle interno organizacional, desenvolvido em C# com .NET 7 e Entity Framework Core.

## Funcionalidades

- **Políticas**: Gerenciamento de políticas de controle interno
- **Permissões**: Controle de permissões de acesso
- **Logs de Acesso**: Registro de atividades dos usuários
- **Trilhas de Auditoria**: Rastreamento de mudanças no sistema

## Tecnologias Utilizadas

- .NET 7.0
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

## Estrutura do Projeto

```
Trabalho-API/
├── Controllers/          # Controladores da API
├── Models/              # Modelos de dados
├── Data/                # DbContext e configurações
├── Program.cs           # Configuração da aplicação
└── TrabalhoAPI.csproj   # Arquivo de projeto
```

## Como Executar

1. **Pré-requisitos**:
   - .NET 7.0 SDK instalado
   - Visual Studio 2022 ou VS Code

2. **Executar o projeto**:
   ```bash
   dotnet restore
   dotnet run
   ```

3. **Acessar a API**:
   - Swagger UI: `https://localhost:7000/swagger`
   - API Base: `https://localhost:7000/api`

## Endpoints Disponíveis

### Políticas
- `GET /api/politicas` - Listar todas as políticas
- `GET /api/politicas/{id}` - Buscar política por ID
- `POST /api/politicas` - Criar nova política
- `DELETE /api/politicas/{id}` - Excluir política

### Permissões
- `GET /api/permissoes` - Listar todas as permissões
- `GET /api/permissoes/{id}` - Buscar permissão por ID
- `POST /api/permissoes` - Criar nova permissão
- `DELETE /api/permissoes/{id}` - Excluir permissão

### Logs de Acesso
- `GET /api/logsacesso` - Listar todos os logs
- `GET /api/logsacesso/{id}` - Buscar log por ID
- `POST /api/logsacesso` - Criar novo log
- `DELETE /api/logsacesso/{id}` - Excluir log

### Trilhas de Auditoria
- `GET /api/trilhasauditoria` - Listar todas as trilhas
- `GET /api/trilhasauditoria/{id}` - Buscar trilha por ID
- `POST /api/trilhasauditoria` - Criar nova trilha
- `DELETE /api/trilhasauditoria/{id}` - Excluir trilha

## Dados Iniciais

O sistema é automaticamente populado com dados de exemplo:
- 10 políticas de controle interno
- 10 tipos de permissões
- 50 logs de acesso
- 50 trilhas de auditoria

## Banco de Dados

- **Tipo**: SQLite
- **Arquivo**: `ControleInterno.db`
- **Criação**: Automática na primeira execução
- **Dados**: Populados automaticamente com dados de exemplo

## Exemplos de Uso

### Criar uma nova política
```json
POST /api/politicas
{
  "nome": "Política de Backup",
  "descricao": "Define procedimentos para backup de dados",
  "ativa": true
}
```

### Buscar logs por usuário
```json
GET /api/logsacesso
```

### Criar trilha de auditoria
```json
POST /api/trilhasauditoria
{
  "entidadeAfetada": "Usuario",
  "tipoOperacao": "CREATE",
  "usuario": "admin",
  "dadosNovos": "{\"nome\": \"novo_usuario\"}"
}
```

## Características Técnicas

- **Validação**: Data Annotations para validação de modelos
- **Tratamento de Erros**: Try-catch com mensagens claras
- **Documentação**: Comentários XML em todos os métodos públicos
- **Async/Await**: Operações assíncronas para melhor performance
- **Swagger**: Documentação automática da API

## Desenvolvido por:
- Arthur Soares
- Rebecca Beccari 
= João Gabriel Bender
Sistema desenvolvido como trabalho acadêmico para demonstração de conceitos de API RESTful, Entity Framework Core e integração com banco de dados.
