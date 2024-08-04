
using MediatR;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Application.SMSServices;
using OtaghakChallenge.Domain.Enums;

namespace OtaghakChallenge.Application.ProductApplication.Commands.RemoveProduct;

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand>
{
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ISMSService _sMSService;


    public RemoveProductCommandHandler(IApplicationUnitOfWork unitOfWork, ISMSService sMSService)
    {
        _unitOfWork = unitOfWork;
        _sMSService = sMSService;
    }

    public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {

        //var context = ContextFactory.GetApplicationDbContext();

        //this is good solution when we dont have updateBy and updateOn columns

        //Product product = new Product() { Id = id, Status = Status.UnderActive };
        //context.Attach(product);
        // context.Entry<Product>(product).Property(c => c.Status).IsModified = true;


        //Product product = context.Products.SingleOrDefault(x => x.Id == request.Id);

        var product = await _unitOfWork.Products.GetSingleAsync(x => x.Id == request.Id, asNoTrack: false);

        if (product is null)
            throw new Exception("resourceNotFound");

        product.Status = Status.UnderActive;

        await _unitOfWork.CompletedAsync();
        _sMSService.Send("");
    }
}