# TaskManager

A simple task management console application.

## Features

- Create, list, complete and delete tasks
- User management
- Task assignment

## Usage

```bash
dotnet run
```

## Project Structure

```
TaskManager/
├── Models/
│   ├── TaskItem.cs     # Task model
│   └── User.cs         # User model
├── Services/
│   ├── TaskService.cs  # Task CRUD operations
│   └── UserService.cs  # User CRUD operations
├── Helpers/
│   └── Utils.cs        # Shared utilities
└── Program.cs          # Entry point and console menu
```
