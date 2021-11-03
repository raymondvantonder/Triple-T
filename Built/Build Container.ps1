dotnet publish .\..\Source\Server\TripleT.API\TripleT.API.sln -c Release

docker build -t triple-t-app -f Docker\Dockerfile .\..\Source\Server\TripleT.API\TripleT.API