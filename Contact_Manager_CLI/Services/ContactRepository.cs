using System;
using System.Collections.Generic;
using System.Linq;
using Contact_Manager_CLI.Interfaces;
using Contact_Manager_CLI.Models;

namespace Contact_Manager_CLI.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly List<Contact> _contacts;

        public ContactRepository(List<Contact> contacts)
        {
            _contacts = contacts;
        }

        public void Add(Contact contact)
        {
            _contacts.Add(contact);
        }

        public void Update(Contact contact)
        {
            int index = _contacts.FindIndex(c => c.Id == contact.Id);
            if (index == -1) throw new KeyNotFoundException("Contact not found.");
            _contacts[index] = contact;
        }

        public void Delete(Guid id)
        {
            int removed = _contacts.RemoveAll(c => c.Id == id);
            if (removed == 0) throw new KeyNotFoundException("Contact not found.");
        }

        public Contact? GetById(Guid id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contacts.OrderBy(c => c.Name);
        }

        public IEnumerable<Contact> Search(string query)
        {
            string q = query.ToLower();
            return _contacts
                .Where(c => c.Name.ToLower().Contains(q) ||
                            c.Phone.ToLower().Contains(q) ||
                            c.Email.ToLower().Contains(q))
                .OrderBy(c => c.Name);
        }

        public IEnumerable<Contact> Filter(string field, string value)
        {
            string v = value.ToLower();
            return field.ToLower() switch
            {
                "name"  => _contacts.Where(c => c.Name.ToLower().Contains(v)).OrderBy(c => c.Name),
                "phone" => _contacts.Where(c => c.Phone.ToLower().Contains(v)).OrderBy(c => c.Name),
                "email" => _contacts.Where(c => c.Email.ToLower().Contains(v)).OrderBy(c => c.Name),
                "date"  => _contacts.Where(c => c.CreationDate.ToString("yyyy-MM-dd").Contains(v)).OrderBy(c => c.CreationDate),
                _       => throw new ArgumentException($"Unknown field '{field}'. Valid fields: name, phone, email, date.")
            };
        }
    }
}
