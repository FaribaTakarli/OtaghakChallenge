
using MediatR;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Domain.Enums;

namespace OtaghakChallenge.Application.ProductApplication.Commands.ModifyProduct;

public class ModifyProductCommand : IRequest<ProductView>
{
    public int Id { get; init; }

    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
}