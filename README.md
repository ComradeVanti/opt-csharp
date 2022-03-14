# opt

[![Nuget](https://img.shields.io/nuget/v/ComradeVanti.CSharpTools.Opt)](https://www.nuget.org/packages/ComradeVanti.CSharpTools.Opt)  
A C# library that mimics F#'s optionals. Since the functionality and in most
cases even the method names are directly taken from F#, go check
out [the documentation there](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html)
for details.

[Changelog](https://github.com/ComradeVanti/opt-csharp/blob/main/CHANGELOG.md)

## Features

Methods for creating optionals are located on the `Opt` class. Methods
like `Map` or `Bind` are available as extension methods on `Opt` instances for
easy chaining.

`Opts` are immutable reference-types. They are compared using equality even when
using `==`.

### Optional instantiation

- Some
- None
- FromNullable
- FromOp (From function result)

### Optional extension methods

- IsSome
- IsNone
- Get
- DefaultValue
- Map
- Bind
- Match (for actions and functions)
- Iter
- Contains
- Count
- DefaultWith
- Exists
- Filter
- Flatten
- Fold
- FoldBack
- ForAll
