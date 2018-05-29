using System.Runtime.CompilerServices;
using NullGuard;

[assembly: InternalsVisibleTo("Pocket.Common.Tests")]
[assembly: NullGuard(ValidationFlags.Methods)]