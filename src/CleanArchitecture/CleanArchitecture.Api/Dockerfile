FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# ENV ASPNETCORE_URLS=http://+:5000

# USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# ARG configuration=Release
WORKDIR /src

COPY ["src/CleanArchitecture/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "src/CleanArchitecture/CleanArchitecture.Domain/"]
COPY ["src/CleanArchitecture/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj", "src/CleanArchitecture/CleanArchitecture.Infrastructure/"]
COPY ["src/CleanArchitecture/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "src/CleanArchitecture/CleanArchitecture.Application/"]
COPY ["src/CleanArchitecture/CleanArchitecture.Api/CleanArchitecture.Api.csproj", "src/CleanArchitecture/CleanArchitecture.Api/"]
RUN dotnet restore "src/CleanArchitecture/CleanArchitecture.Api/CleanArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/CleanArchitecture/CleanArchitecture.Api"
RUN dotnet build "CleanArchitecture.Api.csproj" -c Debug -o /app/build

FROM build AS publish
# ARG configuration=Release
RUN dotnet publish "CleanArchitecture.Api.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.Api.dll"]
