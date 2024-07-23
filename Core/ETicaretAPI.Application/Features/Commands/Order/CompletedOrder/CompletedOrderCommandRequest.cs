using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CompletedOrder
{
    public class CompletedOrderCommandRequest : IRequest<CompletedOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}