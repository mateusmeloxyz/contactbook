using MediatR;

namespace ContactBook.Application.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
