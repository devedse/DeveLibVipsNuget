# Stage 1
FROM microsoft/dotnet:2.0.5-sdk-2.1.4-stretch AS builder
WORKDIR /source

# caches restore result by copying csproj file separately
#COPY /NuGet.config /source/
COPY /DeveLibVipsNuget/*.csproj /source/DeveLibVipsNuget/
COPY /DeveLibVipsNuget.ConsoleApp/*.csproj /source/DeveLibVipsNuget.ConsoleApp/
COPY /DeveLibVipsNuget.Tests/*.csproj /source/DeveLibVipsNuget.Tests/
COPY /DeveLibVipsNuget.sln /source/
RUN ls
RUN dotnet restore

# copies the rest of your code
COPY . .
RUN dotnet build --configuration Release
RUN dotnet test --configuration Release ./DeveLibVipsNuget.Tests/DeveLibVipsNuget.Tests.csproj
RUN dotnet publish ./DeveLibVipsNuget.ConsoleApp/DeveLibVipsNuget.ConsoleApp.csproj --output /app/ --configuration Release

# Stage 2
FROM microsoft/aspnetcore:2.0.5-stretch
WORKDIR /app
COPY --from=builder /app .
ENTRYPOINT ["dotnet", "DeveLibVipsNuget.ConsoleApp.dll"]