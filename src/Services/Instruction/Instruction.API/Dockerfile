#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Instruction/Instruction.API/Instruction.API.csproj", "Services/Instruction/Instruction.API/"]
COPY ["Services/Instruction/Instruction.Domain/Instruction.Domain.csproj", "Services/Instruction/Instruction.Domain/"]
COPY ["Services/Instruction/Instruction.Infrastructure/Instruction.Infrastructure.csproj", "Services/Instruction/Instruction.Infrastructure/"]
RUN dotnet restore "Services/Instruction/Instruction.API/Instruction.API.csproj"
COPY . .
WORKDIR "/src/Services/Instruction/Instruction.API"
RUN dotnet build "Instruction.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Instruction.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Instruction.API.dll"]