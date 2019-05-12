
$outputReport = '.\bin\html';

$solutionPath = '..\src\MultiTenancySolution';

$testResultOutput = './bin/BuildReports';

$testResultOutput = mkdir $testResultOutput -Force | % { $_.FullName }

# $coverageOutput = ( Resolve-Path $testResultOutput | % { $_.Path } ) + "\coverage.cobertura.xml" ;

function ExecTests {
    &dotnet build $solutionPath /p:DebugType="Full"

    &dotnet test $solutionPath -o $testResultOutput `
        --logger "trx" `
        --results-directory $testResultOutput  `
        --collect "Code Coverage" `
        -v 'm'  `
        /p:CollectCoverage=True `
        /p:CoverletOutput="$testResultOutput/cobertura.xml" `
        /p:DebugType=Full `
        /p:UseSourceLink=true `
        /p:Include="[MultiTenancy*]"        `
        /p:CoverletOutputFormat=cobertura;

}

function CreateCodeCoverageReport {
    &reportgenerator "-reports:$testResultOutput/cobertura.xml" "-targetdir:$outputReport" "-reporttypes:HTML;HTMLSummary;HtmlInline_AzurePipelines;XMLSummary";
    #"-assemblyfilters:-*.Tests"
}

function InstallDependency {

     
    if ($coverlet = Get-Command 'coverlet' -ErrorAction SilentlyContinue) {
        return $coverlet;
    }
    else {
        % { &dotnet tool install --global coverlet.console } -ErrorAction Ignore -Verbose
 
    }
    return (Get-Command 'coverlet');
}

#&InstallDependency;

&ExecTests;

#&coverlet $testResultOutput

#&CreateCodeCoverageReport

#&http-server $outputReport -c-1 -g -e .htm