# 1-settings variable
# 2-creating paramteters
# 3-if & foreach constructs
# 4-member enumeration

# to run this script from Powershell
# cd into the containing folder
# type .\_Ps_Script_Basics_2.ps1
# notice that the .\ prefix is necessary

# ISE Shortcuts
# https://docs.microsoft.com/en-us/powershell/scripting/components/ise/keyboard-shortcuts-for-the-windows-powershell-ise?view=powershell-6
# use CTRL+SPACE to get intellisense!
# the pipe symbol is ALT+0124 |

# 4-member enumeration
#   selection of a subset of properties
#   use the select operation
#   use the . operator
cls
$OS = Get-CimInstance Win32_OperatingSystem |  select Caption
$osV2 = (Get-CimInstance Win32_OperatingSystem).Caption

# the variables $OS and $osV2 are now part of the session and typing their 
# names in teh prompt reveals their current value. However, these are not 
# equal!
Write-Output $OS
Write-Output ""
Write-Output $osV2
