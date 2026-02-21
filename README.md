# Contact Manager CLI

A simple command-line app to manage contacts. Built in C# with JSON file storage.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

Verify with:
```
dotnet --version
```

## Run

```
git clone https://github.com/Mohammed-3tef/Contact_Manager_CLI
cd Contact_Manager_CLI
dotnet run
```

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
│   ├── PrintService.cs
│   └── ContactService.cs
└── Program.cs
```

## Menu

```
1. Add Contact
2. Edit Contact
3. Delete Contact
4. View Contact
5. List Contacts
6. Search
7. Filter
8. Save
9. Exit
```

Contacts are saved to `contacts.json` in the project folder.
