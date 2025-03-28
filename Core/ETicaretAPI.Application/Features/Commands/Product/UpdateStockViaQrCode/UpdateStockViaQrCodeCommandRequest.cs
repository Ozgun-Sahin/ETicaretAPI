using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockViaQrCode
{
    public class UpdateStockViaQrCodeCommandRequest :IRequest<UpdateStockViaQrCodeCommandResponse>
    {
        public string ProductId { get; set;}
        public int Stock { get; set; }
    }
}