using OtaghakChallenge.Domain.Enums;

namespace OtaghakChallenge.Domain
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }


        public void Modify(string name, string description, Status status)
        {
            Name = name;
            Description = description;
            Status = status;

        }


    }

}
