using MediatR;

namespace OtaghakChallenge.Application.ProductApplication.Commands.RemoveProduct;

public class RemoveProductCommand : IRequest
{
    public int Id { get; init; }
}