using MediatR;
using ContactBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Application.Queries.GetScheduleById
{
    public class GetContactByIdQuery : IRequest<Contact> 
    {
        public Guid Id { get; private set; }

        public GetContactByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
