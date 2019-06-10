FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY FlashcardsApi/*.csproj ./FlashcardsApi/
COPY Flashcards/*.csproj ./Flashcards/
RUN dotnet restore ./FlashcardsApi/*.csproj
RUN dotnet restore ./Flashcards/*.csproj

# copy everything else and build app
COPY FlashcardsApi/. ./FlashcardsApi/
COPY Flashcards/. ./Flashcards/
WORKDIR /app/FlashcardsApi
RUN dotnet publish -c Release -o out

# start
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
EXPOSE 80
COPY --from=build /app/FlashcardsApi/out ./
ENTRYPOINT ["dotnet", "FlashcardsApi.dll"]