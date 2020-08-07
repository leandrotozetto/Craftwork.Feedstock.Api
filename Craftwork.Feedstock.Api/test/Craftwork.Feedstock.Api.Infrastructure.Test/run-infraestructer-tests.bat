
rmdir BuildReports /s /q 

dotnet test --results-directory ./BuildReports/UnitTests /p:CollectCoverage=true /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=cobertura /p:Include="[*]Craftwork.Feedstock.Api.Infrastructure.*"

dotnet %userprofile%\.nuget\packages\reportgenerator\4.5.3\tools\netcoreapp3.0\ReportGenerator.dll "-reports:BuildReports\Coverage\coverage.cobertura.xml" "-targetdir:BuildReports\Coverage"

start BuildReports\Coverage\index.htm

##pause