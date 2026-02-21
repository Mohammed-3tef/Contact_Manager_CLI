using System.Collections.Generic;
using Contact_Manager_CLI.Models;

namespace Contact_Manager_CLI.Interfaces
{
    public interface IStorageService
    {
        List<Contact> Load();
        void Save(IEnumerable<Contact> contacts);
    }
}
