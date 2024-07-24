param(
    [switch]$displayHelp,
    [switch]$runMigration,
    [switch]$removeMigrationDirectory,
    [switch]$recreateDatabase
)

function Display-UsageInstructions {
    Write-Host ""
    Write-Host "Usage: .\DatabaseScript.ps1 [-runMigration] [-removeMigrationDirectory] [-recreateDatabase] [-displayHelp]"
    Write-Host "Parameters:"
    Write-Host "  -runMigration               Create new migration."
    Write-Host "  -removeMigrationDirectory   Remove migration directory before executing the script."
    Write-Host "  -displayHelp                Display this help message."
    Write-Host ""
}

if (($PSBoundParameters.Count -eq 0) -or $displayHelp) {
    Display-UsageInstructions
    exit
}

if($removeMigrationDirectory) {
    $directoryPath = "..\Infrastructure\Migrations"

    Write-Host ""
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host "           DELETING MIGRATION FOLDER"
    Write-Host "==================================================" -ForegroundColor Blue

    if (Test-Path -Path $directoryPath -PathType Container) {
        Write-Host ""
        Write-Host "Deleting directory:"
        Write-Host "$directoryPath"
        Remove-Item -Path $directoryPath -Recurse -Force
        Write-Host ""
        Write-Host "Directory deleted successfully."
    } else {
        Write-Host ""
        Write-Host "Directory not found:"
        Write-Host "$directoryPath"
    }
}

if($runMigration) {
	Write-Host ""
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host "               CREATING MIGRATION"
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host ""

    $MigrationName = Read-Host "Please enter the Migration name"

    Set-Location -Path "..\Infrastructure"

    dotnet ef migrations add $MigrationName

    Set-Location -Path "..\scripts"
}

Write-Host ""
Write-Host "==================================================" -ForegroundColor Blue
Write-Host "                  COMPLETED"
Write-Host "==================================================" -ForegroundColor Blue
Write-Host ""
