# TaskManager

A simple task management console application.

## Features

- Create, list, complete and delete tasks
- Filter tasks by completion status
- Filter tasks by category (General, Work, Personal, Urgent)
- User management

## Usage

```bash
dotnet run
```

## Project Structure

```
TaskManager/
├── Models/
│   ├── TaskItem.cs      # Task model
│   ├── TaskCategory.cs  # Task category enum
│   └── User.cs          # User model
├── Services/
│   ├── TaskService.cs   # Task CRUD and filtering
│   └── UserService.cs   # User CRUD operations
├── Helpers/
│   └── Utils.cs         # Shared utilities
└── Program.cs           # Entry point and console menu
```
