using AutoMapper;
using OtaghakChallenge.Application.Repository;


namespace OtaghakChallenge.Persistence;

public class ApplicationUnitOfWork : UnitOfWorkBase, IApplicationUnitOfWork
{
    private readonly Lazy<IProductRepository> _productRepository;

    public ApplicationUnitOfWork(ApplicationDbContext context, IMapper mapper) : base(context)
    {
        _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(context, mapper));
    }

    public IProductRepository Products => _productRepository.Value;

}