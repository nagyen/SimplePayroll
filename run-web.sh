#set environment variables
export ASPNETCORE_ENVIRONMENT=Development
# restore packages and run web
dotnet restore
cd ./web
dotnet run