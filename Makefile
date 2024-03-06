build:
	dotnet build
clean:
	dotnet clean
restore:
	dotnet restore
watch:
	dotnet watch --project ./Smartwyre.DeveloperTest.Runner/Smartwyre.DeveloperTest.Runner.csproj run
start:
	dotnet run --project ./Smartwyre.DeveloperTest.Runner/Smartwyre.DeveloperTest.Runner.csproj
test:
	dotnet test
