Param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName
)

$ErrorActionPreference = 'Stop'
$source = split-path -parent $MyInvocation.MyCommand.Definition
$dest = [IO.Path]::Combine([IO.Path]::Combine($env:userprofile, "Projects"), $projectname)

pushd $source
try {
    try {
        dotnet new uninstall MyPrivates.Templates
    } catch {
    }
    dotnet build
    dotnet pack
    dotnet new install $source\bin\Release\MyPrivates.Templates.1.0.0.nupkg
} finally {
    popd
}

md $dest
pushd $dest
try {
    dotnet new package
    git init .
    git add .
    git commit -m "Created the $ProjectName project."

    Invoke-Item "$ProjectName.sln"
} finally {
    popd
}
