# devONRTDPJ

## Tecnologias utilizadas

### Backend

* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* BCrypt (Hash de Senhas)

### Frontend

* React
* TypeScript
* Vite
* React Router
* Axios

### Infraestrutura

* Docker
* Docker Compose
* PostgreSQL

---

## Estrutura

```text
devONRTDPJ
│
├── backend
│   ├── authservice
│   └── regservice
│
├── frontend
│
└── docker-compose.yml
```

### AuthService

Responsável pela autenticação e autorização dos usuários.

Funcionalidades:

* Login
* Cadastro de usuários
* Emissão de JWT
* Controle de permissões por perfil
* Seed de usuários padrão

### RegService

Responsável pelo gerenciamento dos registros.

Funcionalidades:

* Cadastro de registros
* Consulta de registros
* Atualização de registros
* Exclusão de registros
* Alteração de status
* Filtros e paginação

### Frontend

Aplicação React responsável pela interface do usuário.

Funcionalidades:

* Login
* Listagem de registros
* Criação de registros
* Edição de registros
* Exclusão de registros
* Filtros
* Paginação
* Controle visual de permissões por perfil

---

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

---

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

---

## Executando o projeto

### Pré-requisitos

* Docker Desktop
* Docker Compose

### Subindo a aplicação

Na raiz do projeto execute:

```bash
docker compose up --build
```
A primeira execução pode demorar porque cria as imagens do Docker.

---

## Acessos

### Frontend

```text
http://localhost:5173
```

### AuthService

```text
http://localhost:5001/swagger
```

### RegService

```text
http://localhost:5002/swagger
```

### PostgreSQL

Auth Database:

```text
localhost:5433
```

Reg Database:

```text
localhost:5434
```

---

## Regras implementadas

### Autenticação

* JWT com expiração
* Controle de permissões por perfil
* Senhas armazenadas com hash utilizando BCrypt

### Registros

* CRUD completo
* Filtros por tipo
* Filtros por status
* Paginação
* Controle de transição de status

### Interface

* Máscara para CPF/CNPJ
* Validação de formulários
* Mensagens de feedback para o usuário
* Modais para ações críticas
* Layout responsivo para dispositivos móveis

---

## Testes

O frontend possui testes automatizados utilizando Vitest e Testing Library.

Cobertura atual:

* Renderização da tela de Login
* Controle de permissões por perfil
* Aplicação de filtros na tela de registros

Para executar os testes:

```bash
cd frontend

npm install

npm run test
```

---

## Considerações

Durante o desenvolvimento foi priorizada a separação de responsabilidades entre autenticação e gestão dos registros, utilizando dois serviços independentes.

A aplicação pode ser executada integralmente através do Docker Compose, permitindo que todo o ambiente seja reproduzido com um único comando.

## Melhorias Futuras

- Utilização de variáveis de ambiente para URLs do frontend
- Ampliação da cobertura de testes automatizados
- Refresh Token
- Pipeline CI/CD
- Monitoramento e observabilidade
- Cobertura completa para dispositivos móveis