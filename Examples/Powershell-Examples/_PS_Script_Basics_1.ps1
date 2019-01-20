# 1-settings variable
# 2-creating paramteters
# 3-if & foreach constructs
# 4-member enumeration

# ISE Shortcuts
# https://docs.microsoft.com/en-us/powershell/scripting/components/ise/keyboard-shortcuts-for-the-windows-powershell-ise?view=powershell-6
# use CTRL+SPACER to get intellisense!
# the pipe symbol is ALT+0124 |

# to run this script from Powershell
# cd into the containing folder
# type .\_Ps_Script_Basics_1.ps1
# notice that the .\ prefix is necessary

# 2-create a mandatory parameter
# https://stackoverflow.com/questions/1315140/powershell-2-0-param-keyword-error
# paramters must be declared at the top of the script
# mandatory paramter must be inputted by the user at the PS prompt when requested to do so
# if the users fails to do so the script exists with an error 
# Non mandatory paramters on can be left blank

Param([Parameter(Mandatory=$true)][string]$ComputerName)

$hostName = Hostname
Write-Output "insert ? if you do not know your the local computer name ($hostName) or you do not want to type it."

# 3-use logic construct
if($ComputerName -eq "?" -or $hostName ){$ComputerName = Hostname}
Write-Output "Services for $ComputerName"

# 1-creating a variable to hold some data
#   in the most general case the data is an array of objetcs
$services = Get-Service -ComputerName $ComputerName

# 3-use logic construct such as if, foreach
foreach($s in $services){
    
    # access properties of the objetcs and store them into local variables
    $serviceStatus = $s.Status
    $serviceDisplayName = $s.DisplayName

    if($serviceStatus -eq "Running")
    {
        Write-Output "$serviceDisplayName is Running"
    }
    else
    {
        Write-Output "$serviceDisplayName is $serviceStatus"
    }
}
