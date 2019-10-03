# Targets vs Runtimes

## Compilation and Execution

### CoreApp Core3

Can be built with SDK 3 and run with dotnet v3

![building](pics/coreapp3-build-dotnet3.png)
![running](pics/coreapp3-execution-dotnet3.png)

Cannot be built with MSBuild for 4.7.2

![building](pics/coreapp3-build-472.png)
![running](pics/coreapp3-build-472-error.png)


### Console Application 4.7.2

Can be build both with Core 3 and 4.7.2

![running](pics/coreapp3-472-build-core3.png)

But must be run with 4.7.2. Cannot be run with Core 3

![running](pics/472-run-core3.png)


## Referencing projects

### Core App 3

Can reference a 4.7.2 library as a project!

![build](pics/coreapp3-references-472.png)

When using the old csproj format, not even a warning is produce.

On the contrary, if using the new csproj format, the compilation produces a warning:

![build](pics/coreapp3-references-472-csproj2.png)

Core3 can run the result

![build](pics/coreapp3-references-472-execution.png)

### Console Application referencing a 4.7.2 Library using WCF

* Can be built both with 4.7.2 and Core 3
* Can be only run by 4.7.2
* Does not even start with Core3

![running](pics/472-wcf-run-core3.png)


### CoreApp referencing a 4.7.2 Library using WCF

* Can be compiled with Core 3, but the compiler warns that the framework is not compatible

![running](pics/coreapp3-wcf-compile-core3.png)
