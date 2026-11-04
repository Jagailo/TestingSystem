# Testing System

Веб-приложение для создания и прохождения тестов: преподаватель (роль `Admin`) создаёт предметы, темы и вопросы, студент проходит тесты и получает результат, суперадминистратор управляет преподавателями и группами.

## Требования

* Visual Studio 2026 (или MSBuild 18 / .NET SDK 10) с рабочей нагрузкой **ASP.NET и веб-разработка**;
* .NET Framework 4.8.1 (targeting pack и runtime);
* SQL Server LocalDB (экземпляр `MSSQLLocalDB`) — используется файл `StudentTestingSystem/App_Data/Database.mdf`.

## Сборка и запуск

1. Откройте `StudentTestingSystem.slnx` в Visual Studio.
2. Дождитесь восстановления пакетов NuGet.
3. Соберите решение (`Build > Build Solution`).
4. Запустите проект **StudentTestingSystem.Web** (`F5`) — сайт откроется по адресу `https://localhost:44323/`.

## Учётные записи в существующей БД

### Роль пользователя

* `user@gmail.com` / `123456789`
* `user2@gmail.com` / `123456789`

### Роль преподавателя

* `admin@gmail.com` / `123456789`
* `admin2@gmail.com` / `123456789`
* `admin3@gmail.com` / `123456789`

### Роль администратора

* `superadmin@gmail.com` / `123456789`

Также в БД есть готовый тест: он находится в предмете _"Язык программирования C#"_ преподавателя _admin@gmail.com_.

## Структура решения

| Проект | Назначение |
| --- | --- |
| `StudentTestingSystem` | Веб-приложение ASP.NET MVC 5 (Controllers, Views, Areas: Admin, Student, SuperAdmin) |
| `StudentTestingSystem.Domain` | Сущности, интерфейсы репозиториев и инфраструктура |
| `StudentTestingSystem.SQLRepository` | EF6-контекст, миграции, репозитории, Unit of Work |
| `StudentTestingSystem.Services` | Сервисы приложения и транспортные модели |
| `StudentTestingSystem.Storage` | Абстракция и локальная реализация файлового хранилища (изображения вопросов) |

## Технологии

* ASP.NET MVC 5.3 / Razor 3.3, ASP.NET Identity 2.2, OWIN (Katana) 4.2
* Entity Framework 6.5 (Code First + миграции)
* Autofac 9 (MVC-интеграция)
* Bootstrap 4, jQuery 3, Font Awesome, Chart.js
