using MediatR;
using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Commands.UpdateContact
{
    internal class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Guid>
    {
        private readonly IContactRepository _contactRepository;
        public UpdateContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository; 
        }
        public async Task<Guid> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            Contact contact = await _contactRepository.GetByIdAsync(request.Id);
            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact with ID {request.Id} not found.");
            }
            contact.Update(request.Name, request.Email!, request.Phone!);
            await _contactRepository.SaveChangesAsync();

            return contact.Id;
        }
    }
}
