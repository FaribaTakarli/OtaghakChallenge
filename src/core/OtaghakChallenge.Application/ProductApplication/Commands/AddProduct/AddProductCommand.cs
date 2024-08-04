
using MediatR;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Domain.Enums;

namespace OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;

public class AddProductCommand : IRequest<ProductView>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
}