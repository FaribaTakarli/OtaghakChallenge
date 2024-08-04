
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Logging;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using OtaghakChallenge.Application.Repository;




namespace OtaghakChallenge.Application.ProductApplication.Queries.GetProduct
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductView>>
    {
        public readonly IMapper _mapper;
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductsQueryHandler> _logger;


        public GetProductsQueryHandler(IMapper mapper
            , IApplicationUnitOfWork ApplicationUnitOfWork,
            ILogger<GetProductsQueryHandler> logger



            ) {
            _mapper = mapper;
            _unitOfWork=ApplicationUnitOfWork;
            _logger = logger;

        }

        public async Task<IEnumerable<ProductView>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            var products = await _unitOfWork.Products.GetListAsync<ProductView>(x => x.Id > 0 );

            return products;
        }
    }
}
