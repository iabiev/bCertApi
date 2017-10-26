$testProjects = "iabi.bCertApi.Tests"
$testRuns = 1;

& dotnet restore
	
$oldResults = Get-ChildItem -Path "$PSScriptRoot\results_$testRuns-*.testresults"
if ($oldResults) {
    Remove-Item $oldResults
}
	
foreach ($testProject in $testProjects){
    & cd "$PSScriptRoot\test\$testProject"
	
    & dotnet.exe xunit `
        -parallel all `
        -xml $PSScriptRoot\results_$testRuns.testresults   
                 
        $testRuns++
}

& cd "$PSScriptRoot"

"Prepending framework to test method name for better CI visualization"
$resultsGlobPattern = "results_*.testresults"
$prependFrameworkScript = ".\AppendxUnitFramework.ps1"
& $prependFrameworkScript $resultsGlobPattern "$PSScriptRoot"
