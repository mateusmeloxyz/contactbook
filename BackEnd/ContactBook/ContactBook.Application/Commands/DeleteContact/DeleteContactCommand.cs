using MediatR;

namespace ContactBook.Application.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest<Unit>
    {
        public Guid Id { get; private set; }

        public DeleteContactCommand(Guid id)
        {
            Id = id;
        }
    }
}
