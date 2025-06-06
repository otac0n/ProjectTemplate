Param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName,

    [ValidateSet("package", "sourcegenerator")]
    [string]$Template = "package"
)

$ErrorActionPreference = 'Stop'
$source = split-path -parent $MyInvocation.MyCommand.Definition
$dest = [IO.Path]::Combine([IO.Path]::Combine($env:userprofile, "Projects"), $projectname)

& $source\Build.ps1

md $dest
pushd $dest
try {
    dotnet new $Template
    git init .
    git add .
    git commit -m "Created the $ProjectName project."

    Invoke-Item "$ProjectName.sln"
} finally {
    popd
}
