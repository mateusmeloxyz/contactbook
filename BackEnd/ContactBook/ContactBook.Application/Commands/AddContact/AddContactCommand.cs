using MediatR;

namespace ContactBook.Application.Commands.AddContact
{
    public class AddContactCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
