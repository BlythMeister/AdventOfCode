$Year = Read-Host -Prompt "Enter Year"
$Day = Read-Host -Prompt "Enter Day"

$Day = $Day.PadLeft(2, '0')

New-Item -Path "$PSScriptRoot\..\AdventOfCode$Year\Data\Day$Day.dat" -ItemType File
New-Item -Path "$PSScriptRoot\..\AdventOfCode$Year\Data\Day$Day-Sample.dat" -ItemType File
Copy-Item -Path "$PSScriptRoot\Dayx.cs" -Destination "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs"

((Get-Content -path "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs" -Raw) -replace "Dayx","Day$Day" -replace "AdventOfCodex","AdventOfCode$Year") | Set-Content -Path "$PSScriptRoot\..\AdventOfCode$Year\Day$Day.cs"