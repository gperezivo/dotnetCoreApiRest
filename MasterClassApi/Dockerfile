FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5096
EXPOSE 44387

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MasterClassApi/MasterClassApi.csproj", "MasterClassApi/"]
RUN dotnet restore "MasterClassApi/MasterClassApi.csproj"
COPY . .
WORKDIR "/src/MasterClassApi"
RUN dotnet build "MasterClassApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MasterClassApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MasterClassApi.dll"]