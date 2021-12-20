$Year = Read-Host -Prompt "Enter Year"
$Day = Read-Host -Prompt "Enter Day"

New-Item -Path "$PSScriptRoot\..\AdventOfCode$Year\Data\Day$Day.dat" -ItemType File
New-Item -Path "$PSScriptRoot\..\AdventOfCode$Year\Data\Day$Day-Sample.dat" -ItemType File
Copy-Item -Path "$PSScriptRoot\Dayx.cs" -Destination "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs"


((Get-Content -path "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs" -Raw) -replace "Dayx","Day$Day") | Set-Content -Path "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs"