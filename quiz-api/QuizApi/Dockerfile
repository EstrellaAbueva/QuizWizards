FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS="https://*:5000"
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QuizApi.csproj", "./"]
RUN dotnet restore "./QuizApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "QuizApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QuizApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QuizApi.dll"]