# Use a imagem base do .NET 9 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar o arquivo de projeto e restaurar dependências
COPY WebCliente/WebCliente.csproj WebCliente/
RUN dotnet restore WebCliente/WebCliente.csproj

# Copiar todo o código fonte
COPY WebCliente/ WebCliente/

# Build da aplicação
WORKDIR /src/WebCliente
RUN dotnet build WebCliente.csproj -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish WebCliente.csproj -c Release -o /app/publish /p:UseAppHost=false

# Use a imagem runtime do ASP.NET Core 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Criar usuário não-root para segurança
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Copiar os arquivos publicados
COPY --from=publish /app/publish .

# Expor a porta 5798
EXPOSE 5798

# Configurar variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:5798
ENV ASPNETCORE_ENVIRONMENT=Production

# Ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "WebCliente.dll"]