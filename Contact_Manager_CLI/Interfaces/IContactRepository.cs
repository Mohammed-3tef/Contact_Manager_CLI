using System;
using System.Collections.Generic;
using Contact_Manager_CLI.Models;

namespace Contact_Manager_CLI.Interfaces
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(Guid id);
        Contact? GetById(Guid id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Search(string query);
        IEnumerable<Contact> Filter(string field, string value);
    }
}
