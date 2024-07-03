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
    Write-Host "  -runMigration               Execute the migration script."
    Write-Host "  -removeMigrationDirectory   Remove the migration directory before executing the script."
    Write-Host "  -recreateDatabase           Recreate the database."
    Write-Host "  -displayHelp                Display this help message."
    Write-Host ""
}

if (($PSBoundParameters.Count -eq 0) -or $displayHelp) {
    Display-UsageInstructions
    exit
}

if ($recreateDatabase) {

    Write-Host ""
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host "          CLOSING ALL CONNECTIONS"
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host ""
    
    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "ALTER DATABASE Your-Lab_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"

    Write-Host ""
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host "             RECREATING DATABASE"
    Write-Host "==================================================" -ForegroundColor Blue
    Write-Host ""

    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "USE master; DROP DATABASE Your-Lab_DB;"
    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "GO"
    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "EXIT"

    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "CREATE DATABASE Your-Lab_DB;"
    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "GO"
    sqlcmd -S "(LocalDB)\MSSQLLocalDB" -Q "EXIT"
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