 &dotnet test '..\src\MultiTenancySolution' --logger "trx;LogFileName=TestResults.trx" --logger  --results-directory ./BuildReports/UnitTests  /p:CollectCoverage=true /p:CoverletOutput=BuildReports\Coverage\ /p:CoverletOutputFormat=cobertura;
 
 &dotnet reportgenerator "-reports:BuildReports\Coverage\coverage.cobertura.xml" "-targetdir:BuildReports\html" "-reporttypes:HTML;HTMLSummary";