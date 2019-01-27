<#

.SYNOPSIS
Gathers basic info on the system.

.DESCRIPTION
Gathers basic info on the system.

.EXAMPLE
Examples of how to use this script.

.EXAMPLE
./Get=OsInfo_2.ps1

.NOTES
# ISE Shortcuts
# https://docs.microsoft.com/en-us/powershell/scripting/components/ise/keyboard-shortcuts-for-the-windows-powershell-ise?view=powershell-6
# use CTRL+SPACER to get intellisense!
# the pipe symbol is ALT+0124 |

.LINK
http://coderambo.com

#>

#----------------------------
#Script name
#Creator
#Date
#Updated
#Refs
#Parameters
#Varibles
#Remarks
#----------------------------
cls

#IP Adress
$computerName = Hostname
Resolve-DnsName $computerName | select Name, IPaddress

#DNS Server Addresses
# break a long command over multiple lines by means of the back tick `
# notice that Get-DnsClientServerAddress needs a Cim Session to work
(Get-DnsClientServerAddress `
-CimSession (New-CimSession -ComputerName $computerName) `
-AddressFamily IPv4 `
).ServerAddresses

#DNS Server Aliases
(Get-DnsClientServerAddress `
-CimSession (New-CimSession -ComputerName $computerName) `
-AddressFamily IPv4 `
).InterfaceAlias

#DNS Server Addresses and Aliases
(Get-DnsClientServerAddress `
-CimSession (New-CimSession -ComputerName $computerName) `
-AddressFamily IPv4) `
| select ServerAddresses,InterfaceAlias `
| Format-Table

#OS Description
(Get-CimInstance Win32_OperatingSystem -ComputerName $computerName).Caption

#System Memory command's build-up
#Notice the usage of the Measure-Object 
csl
Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName
cls
(Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName).Capacity
cls
(Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName).Capacity | measure -Sum
cls
((Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName).Capacity | measure -Sum).Sum
cls
(((Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName).Capacity | measure -Sum).Sum)/1gb

#System Memory all package in a single liner!
# https://stackoverflow.com/questions/11526285/how-to-count-objects-in-powershell
# https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/measure-object?view=powershell-6
# https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/measure-command?view=powershell-6
((((Get-CimInstance Win32_PhysicalMemory -ComputerName $computerName).Capacity | measure -Sum).Sum)/1gb)

# Lat Reboot
(Get-CimInstance -ClassName Win32_OperatingSystem -ComputerName $computerName).LastBootUpTime

# Disk space/Free space
# To run this command you need to set up a WinRM session
#-------------------------------------------------------
# You might have to enable WinRM on the machine...
#Enable-PSRemoting -SkipNetworkProfileCheck
#-------------------------------------------------------
#Create the PS session
#$myUserName = $env:UserName
#$myDomain = $env:UserDomain
#Enter-PSSession -ComputerName $computerName -Credential $myDomain\$myUserName
#-------------------------------------------------------
#Not able to execute 'invoke-command' on a remote machine
#https://social.technet.microsoft.com/Forums/ie/en-US/fbc5eb1c-6b0f-40fb-9f7c-73b8e82379bb/not-able-to-execute-invokecommand-on-a-remote-machine?forum=winserverpowershell
#https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/invoke-command?view=powershell-6
#-------------------------------------------------------
# The -Credential paramter is essential for the Invoke-Command to work.
$computerName = Hostname
$myusername = $env:username
$mydomain = $env:userdomain
(Invoke-Command -ComputerName $computerName -Credential $myDomain\$myUserName {Get-PSDrive | where Name -EQ "C"}).free

# Active Directory
#UserInfo ???
(Get-AdUse)
