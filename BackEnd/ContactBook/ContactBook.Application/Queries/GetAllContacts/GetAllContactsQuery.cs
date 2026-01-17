using MediatR;
using ContactBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactBook.Application.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<List<Contact>>
    {
    }
}
