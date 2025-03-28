using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateStockViaQrCode
{
    public class UpdateStockViaQrCodeCommandHandler : IRequestHandler<UpdateStockViaQrCodeCommandRequest, UpdateStockViaQrCodeCommandResponse>
    {
       readonly IProductService _productService;
        public UpdateStockViaQrCodeCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateStockViaQrCodeCommandResponse> Handle(UpdateStockViaQrCodeCommandRequest request, CancellationToken cancellationToken)
        {
            await _productService.ProductStockUpdateAsync(request.ProductId, request.Stock);
            return new();

        }
    }
}
