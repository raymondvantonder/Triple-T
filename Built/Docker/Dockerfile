FROM mcr.microsoft.com/dotnet/aspnet:3.1
COPY bin/Release/netcoreapp3.1/publish/ App/
WORKDIR /App
ENV ASPNETCORE_ENVIRONMENT=LOC
ENTRYPOINT ["dotnet", "TripleT.API.dll"]