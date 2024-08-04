using System.Numerics;

namespace OtaghakChallenge.Domain
{
    public class BaseEntity<TKey> where TKey : INumber<TKey>
    {
        public TKey Id { get; set; }

    }

}
