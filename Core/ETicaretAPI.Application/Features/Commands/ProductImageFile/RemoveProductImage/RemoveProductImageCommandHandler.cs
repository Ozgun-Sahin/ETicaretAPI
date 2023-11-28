using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using static System.Net.Mime.MediaTypeNames;
using p = ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            p.Product? product = await _productReadRepository.Table.Include(p => p.ProductImagesFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            p.ProductImageFile? productImageFile = product?.ProductImagesFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));


            if (productImageFile != null)
            {
                product?.ProductImagesFiles.Remove(productImageFile);
            }
            
            await _productWriteRepository.SaveAsync();
            return new ();
        }
    }
}
