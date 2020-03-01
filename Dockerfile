FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore APIBlog.csproj

COPY . ./
RUN dotnet publish APIBlog.csproj -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "APIBlog.dll"]`