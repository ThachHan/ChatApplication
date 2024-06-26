using System.Reflection;

namespace ChatApp.Core;
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
