Param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectName,
    [Parameter(Mandatory=$true)]
    [string]$Replacement
)

#Rename all files and folders
ls -r -exclude *.ps1 | %{
    $mapped = New-Object System.Object
    $mapped | Add-Member -type NoteProperty -name 'Depth' -value ($_.FullName -replace '[^\\]', '').Length
    $mapped | Add-Member -type NoteProperty -name 'Value' -value $_
    $mapped
} | sort -descending -property Depth | %{
    $name = $_.Value.Name
    $newname = $name.Replace($ProjectName, $Replacement)
    if ($name -ne $newname) {
        ren $_.Value.FullName $newname
    }
}

#Replace project name in contents
ls -r -exclude @('*.ps1', '*.exe') | ? { !$_.PSIsContainer } | %{
    $content = [IO.File]::ReadAllText($_.FullName)
    $newcontent = $content.Replace($ProjectName, $Replacement)
    if ($content -ne $newcontent) {
        [IO.File]::WriteAllText($_.FullName, $newcontent)
    }
}
