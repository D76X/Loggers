$Services = Get-Service | where Status -eq "Stopped"
Foreach($s in $Services){
$Name = $s.DisplayName
$Status = $s.Status
Write-Output "$Name is $Status"
}
