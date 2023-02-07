using System;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Engine.Emulator.Algorithms;
using System.Linq;

namespace AVS.Trading.Engine.Emulator
{
    public interface IAlgorithmFactory
    {
        IAlgorithm GetAlgorithm(string algorithm);
    }

    public class AlgorithmFactory : IAlgorithmFactory
    {
        private readonly ITypeFinder _typeFinder;
        private Type[] _types;
        protected Type[] Types => _types ?? (_types = _typeFinder.FindClassesOfType<IAlgorithm>().ToArray());

        public AlgorithmFactory(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public IAlgorithm GetAlgorithm(string algorithm)
        {
            Type type = Types.FirstOrDefault(a => a.Name == algorithm);
            if (type == null)
                throw new ArgumentException($"Algorithm of type '{algorithm}' was not found");

            return (IAlgorithm)EngineContext.Current.Resolve(type);
        }
    }

    class AlgorithmFactory<T> : IAlgorithmFactory where T : IAlgorithm
    {
        private readonly Type _type = typeof(T);
        public IAlgorithm GetAlgorithm(string algorithm)
        {
            if (_type.Name != algorithm && _type.FullName != algorithm)
                throw new ArgumentException($"Algorithm type mismatch: {algorithm} does not match with {_type.Name}");
            return (IAlgorithm) EngineContext.Current.Resolve(_type);
        }
    }
}