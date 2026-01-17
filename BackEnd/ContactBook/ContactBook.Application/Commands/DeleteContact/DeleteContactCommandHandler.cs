using MediatR;
using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Commands.DeleteContact
{
    internal class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit>
    {
        private readonly IContactRepository _contactRepository;
        public DeleteContactCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository; 
        }
        public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            Contact contact = await _contactRepository.GetByIdAsync(request.Id);
            _contactRepository.DeleteAsync(contact);
            await _contactRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
