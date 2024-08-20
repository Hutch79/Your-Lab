param(
    [switch]$displayHelp,
    [switch]$runMigration,
    [switch]$removeMigrationDirectory,
    [switch]$recreateDatabase
)

function Show-UsageInstructions {
    Write-Output ""
    Write-Output "Usage: .\DatabaseScript.ps1 [-runMigration] [-removeMigrationDirectory] [-recreateDatabase] [-displayHelp]"
    Write-Output "Parameters:"
    Write-Output "  -runMigration               Create new migration."
    Write-Output "  -removeMigrationDirectory   Remove migration directory before executing the script."
    Write-Output "  -displayHelp                Display this help message."
    Write-Output ""
}

if (($PSBoundParameters.Count -eq 0) -or $displayHelp) {
    Show-UsageInstructions
    exit
}

if($removeMigrationDirectory) {
    $directoryPath = "..\Infrastructure\Migrations"

    Write-Output ""
    Write-Output "==================================================" -ForegroundColor Blue
    Write-Output "           DELETING MIGRATION FOLDER"
    Write-Output "==================================================" -ForegroundColor Blue

    if (Test-Path -Path $directoryPath -PathType Container) {
        Write-Output ""
        Write-Output "Deleting directory:"
        Write-Output "$directoryPath"
        Remove-Item -Path $directoryPath -Recurse -Force
        Write-Output ""
        Write-Output "Directory deleted successfully."
    } else {
        Write-Output ""
        Write-Output "Directory not found:"
        Write-Output "$directoryPath"
    }
}

if($runMigration) {
	Write-Output ""
    Write-Output "==================================================" -ForegroundColor Blue
    Write-Output "               CREATING MIGRATION"
    Write-Output "==================================================" -ForegroundColor Blue
    Write-Output ""

    $MigrationName = Read-Output "Please enter the Migration name"

    Set-Location -Path "..\Infrastructure"

    dotnet ef migrations add $MigrationName

    Set-Location -Path "..\scripts"
}

Write-Output ""
Write-Output "==================================================" -ForegroundColor Blue
Write-Output "                  COMPLETED"
Write-Output "==================================================" -ForegroundColor Blue
Write-Output ""
