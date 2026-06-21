# devONRTDPJ

## Tecnologias

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Bearer Authentication
- BCrypt
- Swagger/OpenAPI

### Frontend

- React
- TypeScript
- Vite
- React Router
- Axios

### Infraestrutura

- Docker
- Docker Compose
- Nginx
- PostgreSQL

## Estrutura

```text
devONRTDPJ
│
├── backend
│   ├── authservice
│   ├── authservice.tests
│   ├── regservice
│   └── regservice.tests
│
├── frontend
│
└── docker-compose.yml
```

### AuthService

Responsável por:

Funcionalidades:

* Login
* Cadastro de usuários
* Emissão de token JWT
* Controle de permissões por perfil
* Consulta de usuários.
* Autorização por perfil.
* Seed de usuários padrão

### RegService

Responsável por:

* Cadastro de registros
* Consulta de registros
* Atualização de registros
* Exclusão de registros
* Alteração de status
* Filtros e paginação
* Validação de CPF/CNPJ.

### Frontend

Responsável por:

* Login
* Listagem de registros
* Criação de registros
* Edição de registros
* Exclusão de registros
* Alteração de status.
* Tratamento de sessão expirada.
* Feedback por meio de toasts.
* Layout responsivo.
* Filtros
* Paginação
* Controle visual de permissões por perfil

## Perfis de acesso

O sistema possui níveis de permissões:

### Administrador

Possui acesso completo ao sistema.

Permissões:

* Criar registros
* Editar registros
* Alterar status
* Consultar registros
* Excluir registros

### Registrador

Possui acesso limitado aos registros.

Permissões:

* Criar registros
* Editar registros
* Alterar status
* Consultar registros

### Consulta

Possui acesso somente para visualização.

Permissões:

* Consultar registros


## Regras dos registros

### CPF/CNPJ

O CPF ou CNPJ informado é validado antes da criação ou atualização de um registro.

### Status

As transições permitidas são:

```text
Pendente ──────► Registrado
    │
    └──────────► Devolvido ──────► Pendente
```

Um registro com status `Registrado` é considerado finalizado e não pode retornar para outro status.

### Paginação e filtros

A listagem permite:

- Filtrar por tipo.
- Filtrar por status.
- Navegar entre páginas.
- Paginação com limite de 10 registros por página.

## Usuários criados automaticamente

Durante a inicialização da aplicação, usuários padrão são criados para facilitar os testes.

| Perfil        | E-mail                                                |
| ------------- | ----------------------------------------------------- |
| Administrador | [admin@teste.com](mailto:admin@teste.com)             |
| Registrador   | [registrador@teste.com](mailto:registrador@teste.com) |
| Consulta      | [consulta@teste.com](mailto:consulta@teste.com)       |

Senha padrão:

```text
12345678
```

## Executando com Docker

### Pré-requisitos

- Docker Desktop
- Docker Compose

### Iniciar a aplicação

Na raiz do projeto, execute:

```bash
docker compose up --build
```

A primeira execução pode demorar um pouco porque o Docker criará as imagens, bancos, migrations e dados iniciais.

Para executar em segundo plano:

```bash
docker compose up --build -d
```

### Encerrar a aplicação

```bash
docker compose down
```

### Reiniciar completamente os bancos

> Este comando remove os volumes e todos os dados armazenados localmente.

```bash
docker compose down -v
docker compose up --build
```

## Endereços

| Serviço         | Endereço                         |
|-----------------|----------------------------------|
| Frontend        | http://localhost:5173            |
| Auth Swagger    | http://localhost:5001/swagger    |
| Reg Swagger     | http://localhost:5002/swagger    |
| Auth PostgreSQL | localhost:5433                   |
| Reg PostgreSQL  | localhost:5434                   |

## Principais endpoints

### AuthService

| Método | Endpoint                       | Autorização  | Descrição               |
|--------|--------------------------------|--------------|-------------------------|
| POST   | `/api/auth/login`              | Pública      | Autentica o usuário     |
| POST   | `/api/auth/register`           | Pública      | Cadastra um usuário     |
| GET    | `/api/auth/me`                 | Autenticado  | Retorna dados da sessão |
| GET    | `/api/auth/admin`              | Administrador| Valida acesso admin     |
| GET    | `/api/auth/usuarios`           | Administrador| Lista os usuários       |
| PUT    | `/api/auth/usuarios/{id}/role` | Administrador| Altera o perfil         |

### RegService

| Método | Endpoint                     | Perfis                         | Descrição             |
|--------|------------------------------|---------------------------------|-----------------------|
| GET    | `/api/registros`             | Todos                           | Lista os registros    |
| GET    | `/api/registros/{id}`        | Todos                           | Consulta por ID       |
| POST   | `/api/registros`             | Administrador e Registrador     | Cria um registro      |
| PUT    | `/api/registros/{id}`        | Administrador e Registrador     | Atualiza um registro  |
| PATCH  | `/api/registros/{id}/status` | Administrador e Registrador     | Altera o status       |
| DELETE | `/api/registros/{id}`        | Administrador                   | Exclui um registro    |

Os endpoints protegidos esperam o token no cabeçalho:

```http
Authorization: Bearer <access-token>
```

## Autenticação e sessão

Após o login, o AuthService gera um JWT com validade de uma hora.

O frontend:

- Armazena o access token.
- Envia o token nas requisições ao RegService.
- Verifica a expiração da sessão.
- Remove tokens expirados.
- Redireciona o usuário para o login.
- Exibe uma mensagem informando que a sessão expirou.

## Testes automatizados

### Backend

Implementados utilizando:

* XUnit
* FluentAssertions
* Entity Framework InMemory

## Execução dos testes do backend
Na pasta `backend`, execute:

```bash
dotnet test
```

Os testes atuais verificam:

- Cadastro de usuário.
- Perfil padrão de novos usuários.
- Hash de senha.
- Login válido.
- Login com senha inválida.
- Cadastro com e-mail repetido.
- Alteração de perfil.
- Criação de registro.
- Validação de CPF.
- Validação de CNPJ.
- Transições válidas de status.
- Transição inválida de status.
- Busca de registro inexistente.
- Exclusão de registro.

### Frontend

Implementados utilizando:

* Vitest
* React Testing Library

## Execução dos testes do frontend

Na pasta `frontend`, execute:

```bash
npm install
npm run test
```

Os testes atuais verificam:

- Renderização da página de login.
- Ações disponíveis para o perfil Consulta.
- Ações disponíveis para o perfil Administrador.
- Aplicação dos filtros de tipo e status.
- Inicialização básica do ambiente de testes.

### Outras verificações

```bash
npm run build
npm run lint
```

## Decisões técnicas

### JWT - Validação de assinatura local

O RegService valida os tokens JWT localmente, sem realizar chamadas ao AuthService a cada requisição porque reduz o tempo de transmissão de informações entre os serviços.

### Migrations e seed

As migrations são aplicadas automaticamente durante a inicialização dos serviços.

O AuthService cria usuários de demonstração, permitindo avaliar os diferentes perfis sem cadastro manual.

### Controle de acesso

A interface oculta ações não permitidas, mas a segurança não depende do frontend. As permissões também são verificadas nos controllers do backend.

### Responsividade

A interface possui adaptações para dispositivos móveis:

- Filtros empilhados.
- Botões ocupando a largura disponível.
- Modais com rolagem.
- Tabela com rolagem horizontal.
- Toasts adaptáveis para telas menores.

## Limitações e possíveis melhorias

- Refresh token.
- Bloqueio de login para usuários inativos.
- Variáveis de ambiente para URLs do frontend.
- Paginação com total de registros e páginas.
- Limitação do tamanho máximo da página (evitar consultas gigantes)
- Ordenação na lista dos registros

## Considerações finais

O projeto prioriza:

- Separação de responsabilidades.
- Regras de autorização no backend.
- Distribuir as funcionalidades em componentes.
- Validação das regras de negócio.
- Facilidade de execução.
- Experiência consistente no desktop e mobile.
