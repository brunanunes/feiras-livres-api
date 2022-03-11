@ECHO ----------------------------------------------------------------------------------
@ECHO Coletando metricas de cobertura de codigo:
@ECHO ----------------------------------------------------------------------------------
dotnet test --collect:"XPlat Code Coverage" --results-directory:"./.coverage"

@ECHO ----------------------------------------------------------------------------------
@ECHO Gerando relatorio:
@ECHO ----------------------------------------------------------------------------------
reportgenerator "-reports:.coverage/**/*.cobertura.xml" "-targetdir:.coverage/report/" "-reporttypes:HTML;"

@ECHO ----------------------------------------------------------------------------------
@ECHO Abrindo relatorio:
@ECHO ----------------------------------------------------------------------------------
START "report" ".coverage\report\index.html"