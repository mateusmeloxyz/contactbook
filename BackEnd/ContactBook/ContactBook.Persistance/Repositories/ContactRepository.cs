using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactBook.Persistance.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactBookDbContext _contactBookDbContext;
        public ContactRepository() => _contactBookDbContext = new();
        public async Task<List<Contact>> ListAllAsync() => await _contactBookDbContext.Contacts.ToListAsync();
        public async Task<Contact> GetByIdAsync(Guid id) => await _contactBookDbContext.Contacts.SingleOrDefaultAsync(u => u.Id == id);
        public async Task SaveChangesAsync() => await _contactBookDbContext.SaveChangesAsync();
        public async Task AddAsync(Contact contact) => await _contactBookDbContext.Contacts.AddAsync(contact);
        public void DeleteAsync(Contact contact) { _contactBookDbContext.Contacts.Remove(contact); }
    }
}