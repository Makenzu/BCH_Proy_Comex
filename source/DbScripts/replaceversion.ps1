param([Parameter(Mandatory=$true)][string]$version, [Parameter(Mandatory=$true)][string]$sqlfolder)

$sqlfolder = $sqlfolder.Trim('\"');
 
$files = Get-ChildItem $sqlfolder -Filter *2.sql

$versionArray = $version.Split(".")
$version = ([string]::Join(".",$versionArray[0..2]))

ForEach ($file In $files) {
    (Get-Content -Encoding UTF8 $file.FullName) `
    -replace "\/\*replace-begin\*\/select '[^']*'\/\*replace-end\*\/", "/*replace-begin*/select '$version'/*replace-end*/" |
    Out-File -Encoding UTF8 ($file.FullName)
}

ForEach ($file In $files) {
    (Get-Content -Encoding UTF8 $file.FullName) `
    -replace "\/\*replace-begin\*\/select ''[^']*''\/\*replace-end\*\/", "/*replace-begin*/select ''$version''/*replace-end*/" |
    Out-File -Encoding UTF8 ($file.FullName)
}