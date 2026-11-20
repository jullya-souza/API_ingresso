# 1. Usa a imagem oficial do .NET 11 SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:11.0 AS build
WORKDIR /app

# Copia os arquivos e restaura as dependências
COPY . .
RUN dotnet restore

# Publica a aplicação compilada
RUN dotnet publish -c Release -o /out

# 2. Usa a imagem leve do .NET 11 Runtime para rodar o sistema
FROM mcr.microsoft.com/dotnet/aspnet:11.0
WORKDIR /app
COPY --from=build /out .

# Expõe a porta que a API vai usar
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Comando para iniciar a API
ENTRYPOINT ["dotnet", "projeto1.dll"]