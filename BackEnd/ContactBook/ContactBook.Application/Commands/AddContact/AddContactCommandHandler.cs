using MediatR;
using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Commands.AddContact
{
    internal class AddContactCommandHandler : IRequestHandler<AddContactCommand, Guid>
    {
        private readonly IContactRepository _contactRepository;
        public AddContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository; 
        }
        public async Task<Guid> Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            Contact contact = new(request.Name, request.Email!, request.Phone!);
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveChangesAsync();

            return contact.Id;
        }
    }
}
