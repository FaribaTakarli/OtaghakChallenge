using OtaghakChallenge.Domain.Enums;

namespace OtaghakChallenge.API.Controllers.ProductController.Models
{
    public class ModifyProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
    }
}
