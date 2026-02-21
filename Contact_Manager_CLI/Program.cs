using System;
using System.Collections.Generic;
using System.Linq;
using Contact_Manager_CLI.Models;
using Contact_Manager_CLI.Services;

namespace Contact_Manager_CLI
{
    internal class Program
    {
        private static ContactService _contactService = null!;

        public static void Main(string[] args)
        {
            var storage = new JsonStorageService("contacts.json");
            var contacts = storage.Load();
            var repository = new ContactRepository(contacts);
            _contactService = new ContactService(repository, storage);

            Console.WriteLine("=== CONTACT MANAGER CLI ===");
            Console.WriteLine();

            var list = _contactService.ListAll().ToList();
            if (list.Count == 0)
                Console.WriteLine("No contacts yet.");
            else
                PrintService.PrintTable(list);

            Console.WriteLine();

            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Edit Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. View Contact");
                Console.WriteLine("5. List Contacts");
                Console.WriteLine("6. Search");
                Console.WriteLine("7. Filter");
                Console.WriteLine("8. Save");
                Console.WriteLine("9. Exit");
                Console.WriteLine();
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine()?.Trim() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2": 
                        EditContact();    
                        break;
                    case "3": 
                        DeleteContact();  
                        break;
                    case "4": 
                        ViewContact();    
                        break;
                    case "5": 
                        ListContacts();   
                        break;
                    case "6": 
                        Search();         
                        break;
                    case "7": 
                        Filter();         
                        break;
                    case "8": 
                        Save();           
                        break;
                    case "9": 
                        running = Exit(); 
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        public static void AddContact()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Phone: ");
            string phone = Console.ReadLine() ?? "";
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            try
            {
                var c = _contactService.AddContact(name, phone, email);
                Console.WriteLine();
                Console.WriteLine("Contact added.");
                PrintService.PrintContact(c);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void EditContact()
        {
            Console.Write("Contact ID: ");
            string raw = Console.ReadLine() ?? "";
            if (!Guid.TryParse(raw, out Guid id)) { Console.WriteLine("Invalid ID."); return; }

            var existing = _contactService.ViewContact(id);
            if (existing == null) { Console.WriteLine("Contact not found."); return; }

            Console.WriteLine();
            PrintService.PrintContact(existing);
            Console.WriteLine();
            Console.WriteLine("Press Enter to keep the current value.");
            Console.WriteLine();

            Console.Write($"Name [{existing.Name}]: ");
            string name = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(name)) name = existing.Name;

            Console.Write($"Phone [{existing.Phone}]: ");
            string phone = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(phone)) phone = existing.Phone;

            Console.Write($"Email [{existing.Email}]: ");
            string email = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(email)) email = existing.Email;

            try
            {
                var updated = _contactService.EditContact(id, name, phone, email);
                Console.WriteLine();
                Console.WriteLine("Contact updated.");
                PrintService.PrintContact(updated);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void DeleteContact()
        {
            Console.Write("Contact ID: ");
            string raw = Console.ReadLine() ?? "";
            if (!Guid.TryParse(raw, out Guid id)) { Console.WriteLine("Invalid ID."); return; }

            var c = _contactService.ViewContact(id);
            if (c == null) { Console.WriteLine("Contact not found."); return; }

            Console.WriteLine();
            PrintService.PrintContact(c);
            Console.WriteLine();
            Console.Write("Are you sure? (yes / no): ");
            string confirm = Console.ReadLine() ?? "";

            if (confirm.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                confirm.Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                _contactService.DeleteContact(id);
                Console.WriteLine("Contact deleted.");
            }
            else
                Console.WriteLine("Cancelled.");
        }

        public static void ViewContact()
        {
            Console.Write("Contact ID: ");
            string raw = Console.ReadLine() ?? "";
            if (!Guid.TryParse(raw, out Guid id)) { Console.WriteLine("Invalid ID."); return; }

            var c = _contactService.ViewContact(id);
            if (c == null) { Console.WriteLine("Contact not found."); return; }

            Console.WriteLine();
            PrintService.PrintContact(c);
        }

        public static void ListContacts()
        {
            var list = _contactService.ListAll().ToList();
            if (list.Count == 0) { Console.WriteLine("No contacts found."); return; }
            PrintService.PrintTable(list);
            Console.WriteLine();
            Console.WriteLine($"Total: {list.Count} contact(s).");
        }

        public static void Search()
        {
            Console.Write("Search: ");
            string query = Console.ReadLine() ?? "";
            var results = _contactService.Search(query).ToList();
            Console.WriteLine();
            if (results.Count == 0) { Console.WriteLine("No results found."); return; }
            PrintService.PrintTable(results);
            Console.WriteLine($"{results.Count} result(s).");
        }

        public static void Filter()
        {
            Console.WriteLine("Filter by: name | phone | email | date");
            Console.Write("Field: ");
            string field = Console.ReadLine() ?? "";
            Console.Write("Value: ");
            string value = Console.ReadLine() ?? "";
            Console.WriteLine();

            try
            {
                var results = _contactService.Filter(field, value).ToList();
                if (results.Count == 0) { Console.WriteLine("No results found."); return; }
                PrintService.PrintTable(results);
                Console.WriteLine($"{results.Count} result(s).");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void Save()
        {
            _contactService.Save();
            Console.WriteLine("Contacts saved.");
        }

        public static bool Exit()
        {
            if (_contactService.HasUnsavedChanges)
            {
                Console.Write("You have unsaved changes. Save before exiting? (yes / no): ");
                string answer = Console.ReadLine() ?? "";
                if (answer.Trim().Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                    answer.Trim().Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    _contactService.Save();
                    Console.WriteLine("Contacts saved.");
                }
            }
            Console.WriteLine("Goodbye.");
            return false;
        }
    }
}
