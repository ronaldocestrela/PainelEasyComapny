# Sistema de Login - WebCliente

## Funcionalidades Implementadas

### 1. Tela de Login
- Página acessível em `/login`
- Formulário com validação de email e senha
- Design responsivo e moderno
- Feedback visual durante o carregamento
- **Renderização interativa do servidor** para evitar problemas de JavaScript

### 2. Serviço de Autenticação
- Consome o endpoint `http://localhost:5150/api/login`
- Armazena o token no localStorage do navegador (após renderização completa)
- Configura automaticamente o cabeçalho Authorization para futuras requisições
- Métodos para login e logout
- **Tratamento adequado para renderização estática do Blazor Server**

### 3. Modelos de Dados
- `LoginRequest`: Para envio das credenciais
- `LoginResponse`: Para receber a resposta da API

## Correções Implementadas

### Problema de JavaScript Interop
- **Problema**: Erro "JavaScript interop calls cannot be issued at this time" durante renderização estática
- **Solução**: 
  - Adicionado `@rendermode InteractiveServer` na página de login
  - Separação das operações de autenticação das chamadas JavaScript
  - Salvamento do token no localStorage apenas após `OnAfterRenderAsync`
  - Tratamento de exceções para JavaScript não disponível

## Como Usar

### 1. Executar a Aplicação
```bash
cd WebCliente
dotnet run
```

### 2. Acessar o Login
- Abrir o navegador em `http://localhost:5242/login`
- Ou clicar no link "Login" no menu de navegação

### 3. Credenciais de Teste
- **Email**: admin@admin.com
- **Senha**: Hadouken@69

### 4. Endpoints da API
- **URL Base**: http://localhost:5150
- **Endpoint de Login**: POST /api/login
- **Body Esperado**:
```json
{
    "email": "admin@admin.com", 
    "password": "Hadouken@69"
}
```

- **Resposta Esperada**:
```json
{
    "tokenType": "Bearer",
    "accessToken": "...",
    "expiresIn": 3600,
    "refreshToken": "..."
}
```

## Estrutura de Arquivos Criados

```
WebCliente/
├── Models/
│   ├── LoginRequest.cs
│   └── LoginResponse.cs
├── Services/
│   ├── IAuthService.cs
│   └── AuthService.cs
├── Components/
│   └── Pages/
│       └── Login.razor
└── Program.cs (modificado)
```

## Funcionalidades do AuthService

- `LoginAsync(LoginRequest)`: Realiza o login
- `LogoutAsync()`: Remove o token e limpa a autenticação
- `IsAuthenticated`: Propriedade que indica se o usuário está autenticado
- `Token`: Propriedade que retorna o token atual

## Próximos Passos Sugeridos

1. Implementar middleware de autorização
2. Criar páginas protegidas que requerem autenticação
3. Implementar refresh token automático
4. Adicionar tratamento de erros mais específico
5. Implementar logout automático quando o token expira