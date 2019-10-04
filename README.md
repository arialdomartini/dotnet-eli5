# Targets vs Runtimes

## Compilation and Execution

### CoreApp Core3

A Console application targeting DotnetCoreApp3 an be built with SDK 3 and run with `dotnet` v3:

![building](pics/coreapp3-build-dotnet3.png)
![running](pics/coreapp3-execution-dotnet3.png)

The same app cannot be built with MSBuild for 4.7.2. The compiler just refuses to build:

![building](pics/coreapp3-build-472.png)
![running](pics/coreapp3-build-472-error.png)

It must be run with Core 3.

### Console Application 4.7.2

On the contrary, a Console application targeting .NET 4.7.2 can be build both with Core 3 and 4.7.2

![running](pics/coreapp3-472-build-core3.png)

but must be run with 4.7.2. It cannot be run with Core 3: an attempt would result in an error:

![running](pics/472-run-core3.png)


## Referencing projects

### Core App 3

A Core app built on Core 3 can reference a 4.7.2 library as a project! This is by design by Microsoft, apparently to facilitate the migration of legacy Full Framework solutions to Core (see [Documentation](#Documentation)).

During the compilation, the build would produce either the warning [`NU1701`](https://docs.microsoft.com/en-us/nuget/reference/errors-and-warnings/nu1701) if the assembly has been referenced as a NuGet package) or the warning `NU1702` if it's a direct project reference.

Notice that if the referenced project uses the old csproj format (that is, not the new SDK csproj format), not even a warning is produced. On the contrary, if using the new csproj format, the compilation produces a warning:

![build](pics/coreapp3-references-472.png)

![build](pics/coreapp3-references-472-csproj2.png)

As expected, Core3 can run the result:

![build](pics/coreapp3-references-472-execution.png)


### Console Application referencing a 4.7.2 Library using WCF

Let's see what happens at runtime. The starting point is an ordinary .NET 4.7.2 referencing a 4.7.2 class library with some WCF code. 

As a matter of fact:

* It can be built both with 4.7.2 and Core 3
* It can only be run by 4.7.2
* It does not even start with Core3

![running](pics/472-wcf-run-core3.png)


### CoreApp referencing a 4.7.2 Library using WCF

Let's replace the .NET 4.7.2 application with a Core 3 one:

* The Core 3 application can be compiled with Core 3 only;
* The compilation succeeds, but the compiler warns that the framework is not compatible and restores using 4.7.2

![compiling](pics/coreapp3-wcf-compile-core3.png)

* The framework used for the restore dependeds on the version of Core used: with Core 3, .NET 4.7.2 is used; if the CoreApp is build with Core 2, then the compilers restores the other library using .NET 4.6.1. Again, the Microsoft's goal was to make the migration as easy and smooth as possible (see the section [Documentation](#Documentation)).


* As remarked before, notice that the warning is only emitted if the references 4.7.2 project has the new SDK csproj format.

#### Warning is emitted only with SDK csproj format projects

In the solution shown below, the project `Core` is a dotcoreapp2.0, refencing 2 .NET 4.7.2 libraries: `Library472`, which uses the new SDK csproj format, and `Library472-old` which uses the previous format. Notice how the compilation warning only mentions the former library.

![compiling](pics/warning-only-on-new-csproj.png)

## Ignoring warnings or treating warnings as errors

Also notice how the compilation failed to treat the warning `1702` as an error, despite the configuration:

![compiling](pics/warnings-ignored.png)


Treating the warning `NU1701` as an error is a way to enable a strict control on compatibility. To do this, add:

```xml
<PropertyGroup>
    <WarningsAsErrors>NU1701</WarningsAsErrors>
</PropertyGroup>
```

to the project file (the (SDK csproj file).

This will prevent NuGet from adding non-compatible packages:

![running](pics/1701-as-error.png)

See [How do I avoid compatibility warnings with NuGet?](https://stackoverflow.com/a/49265830/202443)


### NU1702 and Referencing assemblies as projects
When an assembly is referenced as a project, not as a NuGet package, the warning is not `NU1701` but `NU1702`.

Therefore, to enable the strict control on compatibility, the csproj should contain:


```xml
<PropertyGroup>
    <WarningsAsErrors>NU1701,NU1702</WarningsAsErrors>
</PropertyGroup>
```

but for some reasons I wasn't able to identify, the setting is just ignored.

This seems to affect other warning codes as well. See the bug report on GitHub [Cannot disable NuGet warnings with dotnet 2.0](https://github.com/NuGet/Home/issues/5769)


## Multiple compilations
Astonishingly, 2 consecutive compilations of the same project produces a different number of warnings:

![compilation1](pics/compile1.png)
![compilation2](pics/compile2.png)

Another strange behaviour is that the warnings `NU*` such as `NU1702` are just ignored by the settings:

```xml
<TreatWarningsAsErrors>True</TreatWarningsAsErrors>
```

A possible explaination is that those are NuGet warnings, differently from `CS*` warnings, which are build related: during the first build, `nuget restore` is invoked, which produces the first warning (ignored by `TreatWarningsAsErrors`, which is a MSBuild directive). During the second build, NuGet is not invoked anymore.

This would be in line with what described by @xlegalles in [this comment](https://github.com/NuGet/Home/issues/5769#issuecomment-323295261).

Finally, it seems that there are also other issues with multi-target projects (see the NuGet issue [Framework Conditioned warning properties are not respected for multi-targeting projects](https://github.com/NuGet/Home/issues/5712))



# Documentation

Referencing .NET Framework 4.6.1 (and below) from .NET Core is a new feature available since .NET Core/Standard 2.0 preview 2 / VS 2017 preview 15.3.

According to MS (see [Reference .NET Framework libraries from .NET Standard](https://devblogs.microsoft.com/dotnet/announcing-net-core-2-0-preview-2/#user-content-reference-net-framework-libraries-from-net-standard)) it can be thought of as a feature that helps you migrate .NET Framework code to .NET Standard or .NET Core over time.

See [dotnet restore warning NU1701](https://stackoverflow.com/a/44999938/202443)
