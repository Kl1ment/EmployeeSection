# EmployeeSection

### Быстрый старт
Находясь в корневом каталоге проекта запустите команду:
```PowerShell
docker compose up -d
```

---
При запуске приложения из кода, необходимо поднять БД. Можете воспользоваться командой

```PowerShell
docker run --rm -d --name postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=EmployeeSectionDb -p 5432:5432 postgres
```
---
Миграция применяется автоматически, при запуске приложения