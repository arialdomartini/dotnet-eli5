# Targets vs Runtimes

## CoreApp Core3

Can be built with SDK 3 and run with dotnet v3

![building](pics/coreapp3-build-dotnet3.png)
![running](pics/coreapp3-execution-dotnet3.png)

Cannot be built with MSBuild for 4.7.2

![building](pics/coreapp3-build-472.png)
![running](pics/coreapp3-build-472-error.png)


## Console Application 4.7.2

Can be build both with Core 3 and 4.7.2

![running](pics/coreapp3-472-build-core3.png)

But must be run with 4.7.2. Cannot be run with Core 3

![running](pics/472-run-core3.png)

