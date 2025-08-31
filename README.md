# gender-nationalize-api-app
A .NET API that predicts gender and nationality based on a name input.

## Getting Started

1. Install .NET SDK 8.0
2. Clone the repo
3. Run `dotnet restore`
4. Start the API with `dotnet run` to check port then exit (also located on launchSettings.json)
5. create .env file on /GenderNationalizeAPI folder , FRONTEND_URL=http://localhost:5173 - change the port on where vite is listening to
4. Start the API with `dotnet run`

GENDERIZE_API=https://api.genderize.io
GENDERIZE_API_KEY=
NATIONALIZE_API=https://api.nationalize.io
NATIONALIZE_API_KEY=
TEST_NATIONALIZE_API=https://api.nationalize.io
TEST_GENDERIZE_API=https://api.genderize.io
FRONTEND_URL=http://localhost:5173 

## Dependencies

This project uses the following NuGet packages:

- DotNetEnv (3.1.1)
- Microsoft.AspNetCore.OpenApi (8.0.6)
- Newtonsoft.Json (13.0.1)
- Swashbuckle.AspNetCore (6.4.0)
- xUnit (2.9.3)
- xUnit Runner Visual Studio (3.1.4)
- Microsoft.NET.Test.Sdk (17.10.0)

## API Endpoint

`GET /api/profile?name=cram`

This can be tested in "http://localhost:5017/swagger/index.html" - change the port on where dotnet is listening to

Returns:
```json
{
  "name": "cram",
  "gender": "male",
  "country": "Philippines"
}
```
# gender-nationalize-frontend
1. Install .NET SDK 8.0
2. Clone the repo
3. Run `npm install`
4. Run `npm run dev` to check port then exit
5. create .env file on /GenderNationalize folder with value VITE_API_URL=http://localhost:5017 - change the port on where backend api is listening to
6. Run `npm run dev`
