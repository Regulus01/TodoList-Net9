
# Task list

Esse é um projeto de uma lista de tarefas utilizando principios de código limpo e arquitetura limpa e .net9.



## Features

- CQRS -> Utilização do padrão com implementação própria de commands e handle (Utilizado somente na inclusão da lista de tarefas)
- Classes de dominio ricas
- Notification pattern
- Encapsulamento
- Inversão de dependência
- Testes unitários
- Utilização de classes genêricas
- Uso de DTO e ViewModels
- Documentação do swagger
- Versionamento
- API Rest

# API Reference

## Task List

#### Post task list

```http
  POST /api/v1/TaskList
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `title` | `string` | **Required**. Titulo  |
| `taskItems` | `taskItem` | Itens da task  |

#### PUT TaskList

```http
  PUT /api/v1/TaskList
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `guid` | **Required**. Id do item para edição  |
| `title` | `string` | **Required**. Titulo  |

#### GET TaskList

```http
  GET /api/v1/TaskList/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id do item buscado |

#### Delete TaskList

```http
  DELETE /api/v1/TaskList/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id do item buscado |

## Task Item

#### Post task Item

```http
  POST /api/v1/TaskItems
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `title` | `string` | **Required**. Titulo  |
| `description` | `string` | Descrição  |
| `dueDate` | `DateTimeOffSet` | Data de expiração |
| `taskListId` | `string` | **Required** id da lista de tarefa ao qual o item pertence |


#### Put task Item

```http
  PUT /api/v1/TaskItems
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `Guid` | **Required**. Id do item para edição  |
| `title` | `string` | **Required**. Titulo  |
| `description` | `string` | Descrição  |
| `dueDate` | `DateTimeOffSet` | Data de expiração |
| `isCompleted` | `bool` | Situação da tarefa |
| `taskListId` | `string` | **Required** id da lista de tarefa ao qual o item pertence |

#### Get taskItem

```http
  GET /api/v1/TaskItem/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id do item buscado |

#### Delete Task item

```http
  DELETE /api/v1/TaskItem/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `Guid` | **Required**. Id do item buscado |


## Authors

- [@jose](https://github.com/Regulus01)

