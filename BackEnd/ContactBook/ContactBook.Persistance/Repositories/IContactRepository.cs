using System;
using System.Collections.Generic;
using System.Text;
using ContactBook.Domain.Entities;

namespace ContactBook.Persistance.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(Guid id);
        Task<List<Contact>> ListAllAsync();
        Task AddAsync(Contact contact);
        //Task UpdateAsync(Contact contact);
        void DeleteAsync(Contact contact);
        Task SaveChangesAsync();
    }
}
