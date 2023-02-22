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

- `Some` Creates a present optional
- `None` Creates a missing optional
- `FromNullable` Creates an optional from a nullable, which will be missing if the given value is null
- `FromOp` Creates an optional from an operation that might fail (throw an exception)

### Optional extension methods

- `IsSome` Checks if the optional is present
- `IsNone` Checks if the optional is missing
- `Get` Attempts to get the value from the optional and throws an exception if the value is missing
- `DefaultValue` Gets the value from this optional or a replacement if it is missing
- `Map` Maps an optional from one type to another using a mapping-function
- `Bind` Maps an optional from one type to another using a mapping-function which itself produces an optional
- `Match` Executes either a onSome or onNone action depending on if the optional is present
- `Iter`  Executes the given action if the optional is present, passing in the contained value
- `Contains` Checks if this optional contains a specific value
- `Count` Gets the "count" of this optional, meaning 1 if it is present and 0 if it is missing
- `DefaultWith` Gets the value or the result of the replacement-function if it is missing
- `Exists` Checks if the value in the optional satisfies a predicate. Returns false if the optional is missing
- `Filter` Filters the optional with a predicate
- `Flatten` Collapses a nested optional into a flat one
- `Fold`/`FoldBack` Executes a folder-function with the value if present
- `ForAll` Checks if the value in the optional satisfies a predicate. Returns true if the optional is missing

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

- `TryGet` Attempts to get a value from the dictionary

#### IEnumerable

- `TryFirst` Attempts to get the first element from the sequence
- `TryLast` Attempts to get the last element from the sequence
- `TryElementAt` Attempts to get an element from the sequence by its index
- `Choose` Selects and maps all items in the sequence where the given choose function returned some
- `TryFind` Attempts to get the first item in the sequence for which the given predicate holds
- `TryFindBack` Attempts to get the last item in the sequence for which the given predicate holds
- `Collect` Collects a sequence of optionals
- `TrySingle` Attempts to get the only item in this collection
- `FilterSome` Filters out any missing values and returns only the present ones
- `TryMax` Attempts to get the maximum element from the collection
- `TryMin` Attempts to get the minimum element from the collection