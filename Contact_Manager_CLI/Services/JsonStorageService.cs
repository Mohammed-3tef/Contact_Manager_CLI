using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Contact_Manager_CLI.Interfaces;
using Contact_Manager_CLI.Models;

namespace Contact_Manager_CLI.Services
{
    public class JsonStorageService : IStorageService
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public JsonStorageService(string filePath)
        {
            _filePath = filePath;
            _options = new JsonSerializerOptions { WriteIndented = true };
        }

        public List<Contact> Load()
        {
            if (!File.Exists(_filePath))
                return new List<Contact>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json, _options) ?? new List<Contact>();
        }

        public void Save(IEnumerable<Contact> contacts)
        {
            string json = JsonSerializer.Serialize(contacts, _options);
            File.WriteAllText(_filePath, json);
        }
    }
}
