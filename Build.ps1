$ErrorActionPreference = 'Stop'
$source = split-path -parent $MyInvocation.MyCommand.Definition

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
