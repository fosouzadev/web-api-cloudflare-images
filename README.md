# web-api-upload-cload-flare
Web Api Rest simples para fazer upload de fotos no Cloud Flare

dotnet new sln -n WebApiUploadCloudFlare

dotnet new webapi -n "WebApi" -o "src/WebApi" -f net8.0 -controllers
dotnet new classlib -n "Domain" -o "src/Domain" -f net8.0
dotnet new classlib -n "Infrastructure" -o "src/Infrastructure" -f net8.0

dotnet sln add "src/WebApi" -s src
dotnet sln add "src/Domain" -s src
dotnet sln add "src/Infrastructure" -s src

dotnet add "src/WebApi" reference "src/Domain"
dotnet add "src/WebApi" reference "src/Infrastructure"
dotnet add "src/Infrastructure" reference "src/Domain"

https://www.akamai.com/pt/solutions/content-delivery-network