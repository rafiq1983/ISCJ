FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ISCJ/ISCJ.csproj", "ISCJ/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
COPY ["MA.Common/MA.Common.csproj", "MA.Common/"]
COPY ["MA.Core/MA.Core.csproj", "MA.Core/"]
COPY ["MA.Core.Web/MA.Core.Web.csproj", "MA.Core.Web/"]
COPY ["ISCJ.Resources/ICSJ.Resource.csproj", "ISCJ.Resources/"]
RUN dotnet restore "ISCJ/ISCJ.csproj"
COPY . .
WORKDIR "/src/ISCJ"
RUN dotnet build "ISCJ.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ISCJ.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ISCJ.dll"]