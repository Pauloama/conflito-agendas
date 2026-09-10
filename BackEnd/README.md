# Conflitos Agenda - backend

Backend inicial em ASP.NET Core para solicitações de férias e conflitos de pessoas críticas.

## Executar

```powershell
dotnet run --project .\BackEnd\ConflitosAgenda.Api.csproj
```

## Fluxos disponíveis

- `POST /api/plano-focal`: substitui o plano focal importado.
- `GET /api/plano-focal`: consulta as pessoas críticas carregadas.
- `POST /api/solicitacoes`: cria uma solicitação pendente e rejeita conflitos de períodos sobrepostos.
- `GET /api/solicitacoes`: lista solicitações para visualização.
- `POST /api/solicitacoes/{id}/decisao`: aprova ou recusa uma solicitação; a aprovação revalida o conflito.

O armazenamento atual é em memória para manter o primeiro passo simples. A interface `IPlanoFocalStore` deve ser implementada com Microsoft Graph/SharePoint, e `ISolicitacaoStore` com persistência real antes de publicar o sistema.

O endpoint de decisão está preparado para receber a identidade do coordenador no contrato. Antes de disponibilizar o sistema, ele deve ser protegido com autenticação do Microsoft Entra ID e uma policy/role `Coordenador`; a identidade não deve ser confiada do corpo da requisição.
