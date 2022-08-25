# Changelog
## Upcoming
## Added
- `TryGet` extension for `IDictionary`
- `TryFirst` extension for `IEnumerable`
- `TryLast` extension for `IEnumerable`
- `TryElementAt` extension for `IEnumerable`
- `Choose` extension for `IEnumerable`
- `TryFind` extension for `IEnumerable`
- `Collect` extension for `IEnumerable`
## 2.0.1
### Fixed
- `FromNullable` method did not work with some types, like `string` but now it  does
## 2.0.0
### Changed
- Opt is now an interface `IOpt`
### Removed
- Can no longer compare `IOpt` instances with `==` while using value equality, 
instead this will use the usual reference equality.
## 1.1.2
### Fixed
- Incorrect changelog-link in readme
## 1.1.1
### Fixed
- Missing functions in readme
## 1.1.0
### Added
- [Count](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#count) function
- [DefaultWith](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#defaultWith) function
- [Exists](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#exists) function
- [Filter](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#filter) function
- [Flatten](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#flatten) function
- [Fold](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#fold) function
- [FoldBack](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#foldBack) function
- [ForAll](https://fsharp.github.io/fsharp-core-docs/reference/fsharp-core-optionmodule.html#forall) function
### Changed
- Class structure. Moved Ext class to own file
- Test project target framework to .Net 6
## 1.0.2
### Added
- .NetStandard 2.0 target
- Link to changelog in readme
### Changed
- Implementation in order to be compatible with .NetStandard 2.0
## 1.0.1
### Added
- .NetCore 3.1 target
- .NetStandard 2.1 target
- Embedded debugging-symbols
- This changelog yay