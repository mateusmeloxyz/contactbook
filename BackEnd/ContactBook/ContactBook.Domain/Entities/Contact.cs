using ContactBook.Domain.Common;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;


namespace ContactBook.Domain.Entities
{
    public class Contact: AuditableEntity
    {
        // Public constructor for domain logic
        public Contact(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public void Update(string name, string email, string telephone)
        {
            Name = name;
            Email = email;
            Phone = telephone;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Delete()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }
        public void Restore() //Admin only, will be added later
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
