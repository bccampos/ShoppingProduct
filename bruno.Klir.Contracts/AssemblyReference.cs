using System.Reflection;

namespace bruno.Klir.Contracts
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
