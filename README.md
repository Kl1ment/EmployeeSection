# EmployeeSection

### ������� �����
�������� � �������� �������� ������� ��������� �������:
```PowerShell
docker compose up -d
```

---
��� ������� ���������� �� ����, ���������� ������� ��. ������ ��������������� ��������

```PowerShell
docker run --rm -d --name postgres -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=EmployeeSectionDb -p 5432:5432 postgres
```
---
�������� ����������� �������������, ��� ������� ����������