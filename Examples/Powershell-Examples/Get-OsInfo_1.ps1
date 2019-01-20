#For this script the following requirement must be met.

#-1 WinRM must be enabled on the local or remote computer node when the script is to be run
#-2 The user that runs the script in a Powershell session must have sufficient permission
#   to be able to open a WinRM PS Remoting session.

$YourComputerName = Hostname
Write-Output "Your Computer Name is $YourComputerName" 
$ComputerName = Read-Host "Enter Computername"

if ($ComputerName -eq ""){ 
$Computername = $YourComputerName
}

Write-Output "Selected Host = $Computername" 

Get-CimInstance -ClassName Win32_OperatingSystem `
-ComputerName $ComputerName |
Select-Object -Property CSName,LastBootUpTime
