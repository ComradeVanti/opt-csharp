# opt

[![Nuget](https://img.shields.io/nuget/v/ComradeVanti.CSharpTools.Opt)](https://www.nuget.org/packages/ComradeVanti.CSharpTools.Opt)  
A C# library that mimics F#'s optionals. Since the functionality and in most
cases even the method names are directly taken from F#, go check
out [the documentation there](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html)
for details.

[Changelog](https://github.com/ComradeVanti/opt-csharp/blob/main/CHANGELOG.md)

## Features

Methods for creating optionals are located on the `Opt` class. Methods
like `Map` or `Bind` are available as extension methods on `IOpt` instances for
easy chaining.

`IOpt` are immutable reference-types. Comparing them with `Equals` will be true
if the both contain a value and those values are equal. Comparing `IOpt`
with `==` will compare them using reference equality.

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

### Pattern matching

You can use [C# pattern-matching](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching)
on `IOpt` instances to check if they have a value or not.

For example:

```csharp
switch (x) // Where x is an IOpt<int>
{
    case ISome<int> { Value: var value}: // value is an int
        break;
    case INone<int> :
        break;
}
```

### Utility extensions

Opt also includes some utility extensions for existing types

#### IDictionary

- TryGet

#### IEnumerable

- TryFirst
- TryLast
- TryElementAt
- Choose
- TryFind
- TryFindBack
- Collect
- TrySingle