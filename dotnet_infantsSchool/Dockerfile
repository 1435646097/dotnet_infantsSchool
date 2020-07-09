#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["dotnet_infantsSchool/dotnet_infantsSchool.csproj", "dotnet_infantsSchool/"]
COPY ["Model/Model.csproj", "Model/"]
COPY ["IServices/IServices.csproj", "IServices/"]
COPY ["IRepository/IRepository.csproj", "IRepository/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "dotnet_infantsSchool/dotnet_infantsSchool.csproj"
COPY . .
WORKDIR "/src/dotnet_infantsSchool"
RUN dotnet build "dotnet_infantsSchool.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "dotnet_infantsSchool.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet_infantsSchool.dll"]