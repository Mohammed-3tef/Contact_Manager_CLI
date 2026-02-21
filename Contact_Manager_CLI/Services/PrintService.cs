using Contact_Manager_CLI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_CLI.Services
{
    public static class PrintService
    {
        public static void PrintContact(Contact c)
        {
            Console.WriteLine($"ID      : {c.Id}");
            Console.WriteLine($"Name    : {c.Name}");
            Console.WriteLine($"Phone   : {c.Phone}");
            Console.WriteLine($"Email   : {c.Email}");
            Console.WriteLine($"Created : {c.CreationDate:yyyy-MM-dd HH:mm:ss}");
        }

        public static void PrintTable(List<Contact> list)
        {
            Console.WriteLine($"{"Name",-22} {"Phone",-16} {"Email",-26} {"Created",-12}");
            Console.WriteLine(new string('-', 78));
            foreach (var c in list)
            {
                string name = c.Name.Length > 20 ? c.Name[..18] + ".." : c.Name;
                string phone = c.Phone.Length > 14 ? c.Phone[..12] + ".." : c.Phone;
                string email = c.Email.Length > 24 ? c.Email[..22] + ".." : c.Email;
                Console.WriteLine($"{name,-22} {phone,-16} {email,-26} {c.CreationDate:yyyy-MM-dd}");
                Console.WriteLine($"ID: {c.Id}");
            }
        }
    }
}
