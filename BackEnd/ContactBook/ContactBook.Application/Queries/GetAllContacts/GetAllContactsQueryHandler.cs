using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Queries.GetAllContacts
{
    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, List<Contact>>
    {
        private readonly IContactRepository _contactRepository;

        public GetAllContactsQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<Contact>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            var allContacts = (await _contactRepository.ListAllAsync()).OrderBy(x => x.Name);
            return allContacts.ToList();
        }
    }
}
