FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 8383

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY NybSys.API/NybSys.API.csproj NybSys.API/
COPY NybSys.TMS.BLL1/NybSys.TMS.BLL.csproj NybSys.TMS.BLL1/
COPY NybSys.Models/NybSys.Models.csproj NybSys.Models/
COPY NybSys.Common/NybSys.Common.csproj NybSys.Common/
COPY NybSys.UnitOfWork/NybSys.UnitOfWork.csproj NybSys.UnitOfWork/
COPY NybSys.Repository/NybSys.Repository.csproj NybSys.Repository/
COPY NybSys.DAL/NybSys.DAL.csproj NybSys.DAL/
COPY NybSys.AuditLog.DAL/NybSys.AuditLog.DAL.csproj NybSys.AuditLog.DAL/
COPY NybSys.Session.BLL/NybSys.Session.BLL.csproj NybSys.Session.BLL/
COPY NybSys.Session.DAL/NybSys.Session.DAL.csproj NybSys.Session.DAL/
COPY NybSys.HttpRequest/NybSys.HttpRequest.csproj NybSys.HttpRequest/
COPY NybSys.AuditLog.BLL/NybSys.AuditLog.BLL.csproj NybSys.AuditLog.BLL/
COPY NybSys.Generic.BLL/NybSys.Generic.BLL.csproj NybSys.Generic.BLL/
COPY NybSys.Auth.BLL/NybSys.Auth.BLL.csproj NybSys.Auth.BLL/
COPY NybSys.MassTransit/NybSys.MassTransit.csproj NybSys.MassTransit/
COPY NybSys.Mqtt/NybSys.Mqtt.csproj NybSys.Mqtt/
COPY NybSys.RedisSession.DAL/NybSys.RedisSession.DAL.csproj NybSys.RedisSession.DAL/
RUN dotnet restore NybSys.API/NybSys.API.csproj
COPY . .
WORKDIR /src/NybSys.API
RUN dotnet build NybSys.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish NybSys.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_URLS=http://+:8383
ENTRYPOINT ["dotnet", "NybSys.API.dll"]
