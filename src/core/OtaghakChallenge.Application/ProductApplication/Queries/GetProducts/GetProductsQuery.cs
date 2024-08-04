using MediatR;
using OtaghakChallenge.Application.ProductApplication.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.ProductApplication.Queries.GetProduct
{
    public class GetProductsQuery:IRequest<IEnumerable<ProductView>>
    {
    }
}
