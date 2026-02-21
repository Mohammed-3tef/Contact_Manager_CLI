using System;
using System.Collections.Generic;
using Contact_Manager_CLI.Interfaces;
using Contact_Manager_CLI.Models;

namespace Contact_Manager_CLI.Services
{
    public class ContactService
    {
        private readonly ContactRepository _repository;
        private readonly IStorageService _storage;
        private bool _hasUnsavedChanges = false;

        public bool HasUnsavedChanges => _hasUnsavedChanges;

        public ContactService(ContactRepository repository, IStorageService storage)
        {
            _repository = repository;
            _storage = storage;
        }

        public Contact AddContact(string name, string phone, string email)
        {
            Validate(name, phone, email);
            var contact = new Contact { Name = name.Trim(), Phone = phone.Trim(), Email = email.Trim() };
            _repository.Add(contact);
            _hasUnsavedChanges = true;
            return contact;
        }

        public Contact EditContact(Guid id, string name, string phone, string email)
        {
            Validate(name, phone, email);
            var contact = _repository.GetById(id) ?? throw new KeyNotFoundException("Contact not found.");
            contact.Name  = name.Trim();
            contact.Phone = phone.Trim();
            contact.Email = email.Trim();
            _repository.Update(contact);
            _hasUnsavedChanges = true;
            return contact;
        }

        public void DeleteContact(Guid id)
        {
            _repository.Delete(id);
            _hasUnsavedChanges = true;
        }

        public Contact? ViewContact(Guid id) => _repository.GetById(id);

        public IEnumerable<Contact> ListAll() => _repository.GetAll();

        public IEnumerable<Contact> Search(string query) => _repository.Search(query);

        public IEnumerable<Contact> Filter(string field, string value) => _repository.Filter(field, value);

        public void Save()
        {
            _storage.Save(_repository.GetAll());
            _hasUnsavedChanges = false;
        }

        private static void Validate(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 2)
                throw new ArgumentException("Name must be at least 2 characters.");
            if (string.IsNullOrWhiteSpace(phone) || phone.Trim().Length < 7)
                throw new ArgumentException("Phone must be at least 7 characters.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
                throw new ArgumentException("Invalid email format.");
        }
    }
}
