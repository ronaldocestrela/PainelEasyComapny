# 🎯 Painel de Acompanhamento

Sistema de dashboard e acompanhamento detalhado para métricas de campanhas digitais, desenvolvido com **Blazor Server** (.NET 9).

## 📊 Funcionalidades

- **Dashboard Principal** - Visão geral com métricas consolidadas
- **Acompanhamento Detalhado** - Tabela completa com análise por campanha
- **Interface Responsiva** - Adaptada para desktop e mobile
- **Filtros Avançados** - Por período, status e origem
- **Indicadores Visuais** - Gráficos, barras de progresso e badges

## 🚀 Executando com Docker

### Pré-requisitos
- Docker
- Docker Compose

### Comandos Rápidos

```bash
# Subir a aplicação
./docker-setup.sh up

# Ver logs
./docker-setup.sh logs

# Parar aplicação
./docker-setup.sh down
```

### Acesso
- **URL Principal:** http://localhost:5798
- **Dashboard:** http://localhost:5798/
- **Acompanhamento:** http://localhost:5798/acompanhamento

## 🛠️ Desenvolvimento Local

### Pré-requisitos
- .NET 9 SDK
- Visual Studio Code ou Visual Studio

### Comandos

```bash
# Restaurar dependências
dotnet restore WebCliente/

# Executar em modo desenvolvimento
dotnet run --project WebCliente/

# Executar com hot reload
dotnet watch run --project WebCliente/
```

## 📁 Estrutura do Projeto

```
Painel/
├── WebCliente/                 # Aplicação Blazor
│   ├── Components/
│   │   ├── Pages/             # Páginas Razor
│   │   │   ├── Dashboard.razor
│   │   │   └── Acompanhamento.razor
│   │   └── Layout/            # Layout components
│   ├── wwwroot/               # Arquivos estáticos
│   │   ├── dashboard.css
│   │   └── acompanhamento.css
│   └── Program.cs             # Configuração da aplicação
├── Dockerfile                 # Configuração Docker
├── docker-compose.yml         # Orquestração de containers
└── docker-setup.sh           # Script de automação
```

## 🎨 Design System

### Cores Principais
- **Azul Principal:** #0d6efd
- **Azul Gradiente:** #1e3a8a → #3b82f6
- **Sucesso:** #198754
- **Aviso:** #ffc107
- **Erro:** #dc3545

### Componentes
- Cards com gradientes e sombras
- Tabelas responsivas com hover effects
- Filtros com dropdowns customizados
- Badges e indicadores visuais
- Barras de progresso animadas

## 📊 Métricas Monitoradas

### Dashboard
- **Comissão Total:** Valor consolidado em R$
- **Funil de Conversão:** Cadastros → FTDs → CPA Qualificado
- **RVS por Campanha:** Receita estimada por origem
- **CPA por Campanha:** Custo por aquisição detalhado

### Acompanhamento Detalhado
- **Clicks:** Volume de cliques com tendências
- **CPA:** Custo por aquisição e conversões
- **FTD:** First Time Deposit com progresso visual
- **Taxa de Conversão:** Percentual com classificação colorida
- **Status:** Situação atual da campanha

## 🔧 Configurações

### Variáveis de Ambiente
- `ASPNETCORE_ENVIRONMENT`: Ambiente de execução
- `ASPNETCORE_URLS`: URLs de escuta (padrão: http://+:5798)

### Portas
- **Desenvolvimento:** 5242
- **Produção (Docker):** 5798

## 🐳 Docker

### Build Manual
```bash
# Build da imagem
docker build -t painel-acompanhamento .

# Executar container
docker run -p 5798:5798 painel-acompanhamento
```

### Docker Compose
```bash
# Subir todos os serviços
docker-compose up -d

# Ver logs
docker-compose logs -f

# Parar serviços
docker-compose down
```

## 📱 Responsividade

O sistema é totalmente responsivo e se adapta a:
- **Desktop:** Layout completo com todas as funcionalidades
- **Tablet:** Adaptação de grid e componentes
- **Mobile:** Interface otimizada com navegação touch-friendly

## 🔍 Health Check

Endpoint de monitoramento disponível em:
- **URL:** `/health`
- **Método:** GET
- **Resposta:** Status 200 OK quando saudável

## 📈 Performance

### Otimizações Implementadas
- CSS minificado e otimizado
- Componentes Blazor Server para melhor performance
- Imagens Docker multi-stage para reduzir tamanho
- Health checks para monitoramento de disponibilidade

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

---

**Desenvolvido com ❤️ usando Blazor Server e .NET 9**