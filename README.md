# Contact Manager CLI

A command-line Contact Management System built in C# (.NET 8).  
Contacts are stored locally in a `contacts.json` file.

---

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Verify with:
```
dotnet --version
```

---

## How to Run

```bash
git clone https://github.com/Mohammed-3tef/Contact_Manager_CLI
cd Contact_Manager_CLI
dotnet run
```

To publish a standalone binary:
```bash
dotnet publish -c Release -o ./out
./out/Contact_Manager_CLI
```

---

## Menu

```
  1. Add Contact       5. List Contacts
  2. Edit Contact      6. Search
  3. Delete Contact    7. Filter
  4. View Contact      8. Save
                       9. Exit
```

---

## Contact Fields

| Field         | Details                          |
|---------------|----------------------------------|
| Id            | Auto-generated GUID              |
| Name          | Min. 2 characters                |
| Phone         | Min. 7 characters                |
| Email         | Must contain @ and .             |
| Creation Date | Auto-generated timestamp         |

---

## Filter Fields

When using **Filter**, enter one of: `name`, `phone`, `email`, `date`  
Date matches against `yyyy-MM-dd` format (e.g. `2026-02`).

---

## Project Structure

```
ContactManager/
├── Models/
│   └── Contact.cs
├── Interfaces/
│   ├── IContactRepository.cs
│   └── IStorageService.cs
├── Services/
│   ├── JsonStorageService.cs
│   ├── ContactRepository.cs
│   └── ContactService.cs
├── Program.cs
└── ContactManager.csproj
```

---

## Design

- **OOP** — Models, Services, Interfaces, and UI are fully separated.  
- **SOLID** — Interfaces for storage and repository allow clean extension without modification. Business logic is isolated in `ContactService`. `Program.cs` wires dependencies together.
- **JSON Storage** — Human-readable `contacts.json` in the working directory.
