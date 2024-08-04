
using AutoMapper;
using MediatR;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Application.SMSServices;


namespace OtaghakChallenge.Application.ProductApplication.Commands.ModifyProduct;

public class ModifyProductCommandHandler : IRequestHandler<ModifyProductCommand, ProductView>
{
    private readonly IMapper _mapper;
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ISMSService _sMSService;


    public ModifyProductCommandHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper, ISMSService sMSService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _sMSService = sMSService;

    }

    public async Task<ProductView> Handle(ModifyProductCommand request, CancellationToken cancellationToken)
    {

        var product = await _unitOfWork.Products.GetSingleAsync(X => X.Id == request.Id);

        if (product is null)
            throw new Exception("resourceNotFound");

        product.Modify(request.Name, request.Description, request.Status);


        await _unitOfWork.CompletedAsync(cancellationToken);

        _sMSService.Send("");


        ProductView result = _mapper.Map<ProductView>(product);

        return result;
    }
}