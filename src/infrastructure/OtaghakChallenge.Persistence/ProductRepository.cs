
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OtaghakChallenge.Application.Repository;
using OtaghakChallenge.Domain;
using System.Linq.Expressions;

namespace OtaghakChallenge.Persistence;

public class ProductRepository : GenericRepository<Product, int>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}