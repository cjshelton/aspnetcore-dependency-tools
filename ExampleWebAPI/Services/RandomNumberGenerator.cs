using System;
using ExampleWebAPI.Contracts;

namespace ExampleWebAPI.Services
{
    /// <summary>
    /// Basic implementation of <c>IRandomNumberGenerator</c> to test the <c>WebDependencyValidator</c>.
    /// </summary>
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Random _rand;

        public RandomNumberGenerator()
        {
            _rand = new Random();
        }
        
        public int Generate()
        {
            return _rand.Next();
        }
    }
}
