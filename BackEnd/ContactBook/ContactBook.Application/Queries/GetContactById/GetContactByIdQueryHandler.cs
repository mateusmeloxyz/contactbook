using MediatR;
using ContactBook.Domain.Entities;
using ContactBook.Persistance.Repositories;
using System.Collections.Generic;

namespace ContactBook.Application.Queries.GetScheduleById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Contact>
    {
        private readonly IContactRepository _scheduleRepository;
        public GetContactByIdQueryHandler(IContactRepository scheduleRepository) => _scheduleRepository = scheduleRepository;
        public async Task<Contact> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            Contact schedule = await _scheduleRepository.GetByIdAsync(request.Id);
            return schedule;
        }
    }
}
