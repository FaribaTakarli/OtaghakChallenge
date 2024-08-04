
using AutoMapper;
using MediatR;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Application.SMSServices;
using OtaghakChallenge.Domain;


namespace OtaghakChallenge.Application.ProductApplication.Commands.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductView>
{
    private readonly IMapper _mapper;
    private readonly IApplicationUnitOfWork _unitOfWork;
    private readonly ISMSService _sMSService;


    public AddProductCommandHandler(IApplicationUnitOfWork unitOfWork, IMapper mapper, ISMSService sMSService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _sMSService = sMSService;
    }

    public async Task<ProductView> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {

        Product product = _mapper.Map<Product>(request);

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.CompletedAsync(cancellationToken);
        _sMSService.Send("");
        return _mapper.Map<ProductView>(product); ;
    }
}