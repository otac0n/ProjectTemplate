Param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName
)
echo "Creating project $ProjectName..."

$replacements = @{
    '$projectname$' = $ProjectName;
    '$year$' = (Get-Date).Year;
}
for ($i = 1; $i -le 10; $i = $i + 1) {
    $guid = [guid]::NewGuid()
    $replacements['$guid' + $i + '$'] = $guid.ToString()
    $replacements['$guid' + $i + '.lower$'] = $guid.ToString().ToLower()
    $replacements['$guid' + $i + '.upper$'] = $guid.ToString().ToUpper()
}

$source = split-path -parent $MyInvocation.MyCommand.Definition
$dest = [IO.Path]::Combine([IO.Path]::Combine($env:userprofile, "Projects"), $projectname)
$encoding = [Text.Encoding]::GetEncoding(28591);
ls -r -exclude *.ps1 | %{
    $name = $_.FullName.Substring($source.Length)
    $replacements.Keys | %{
        $name = $name -replace [Text.RegularExpressions.Regex]::Escape($_), $replacements[$_]
    }
    $path = $dest + $name
    if ($_.PSIsContainer) {
        echo "Creating $name..."
        md $path | Out-Null
    } else {
        echo "Copying $name..."
        $content = [IO.File]::ReadAllText($_.FullName, $encoding)
        $newcontent = $content
        $replacements.Keys | %{
            $newcontent = $newcontent -replace [Text.RegularExpressions.Regex]::Escape($_), $replacements[$_]
        }
        [IO.File]::WriteAllText($path, $newcontent, $encoding)
    }
}

pushd $dest
    git init .
    git add .
    git commit -m "Created the $ProjectName project."

    Invoke-Item "$ProjectName.sln"
popd
