using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen ürün adını giriniz")
                .MaximumLength(150)
                .MinimumLength(2)
                    .WithMessage("Ürün adı 2 ile 150 karakter arası olmalıdır");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen stok bilgisini giriniz")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgsi sıfırdan küçük olamaz");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen fiyat bilgisini giriniz")
                .Must(pr => pr >= 0)
                    .WithMessage("fiyat bilgsi sıfırdan küçük olamaz");
        }
    }
}
