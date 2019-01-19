# This command enables Powershell Remoting 
# The -SkipNetworkProfileCheck makes it possible to enable PS Remoting even when the 
# Netwok Connector Profile is set to Public instead of Private. The PS Remoting enabled
# with this command option is allowed only from a remote computer node located withing the
# same subnet.
Enable-PSRemoting -SkipNetworkProfileCheck
Write-Output "just run Enable-PSRemoting -SkipNetworkProfileCheck"

# Environment Variables can be used to know more about the user that is running the script
$myUserName = $env:UserName
$myDomain = $env:UserDomain
$thisComputerNodeName = $env:ComputerName

Write-Output "username = $myUserName"
Write-Output "domain = $myDomain"
Write-Output "computer name = $thisComputerNodeName"

# This command tries to open a PS Session on the loca computer node for the 
# user that is running the script. It will succeed only if the Enable-PSRemoting
# above was successful and the domain\user credential has enough permission on the
# current node to start a WinRM session.
# The script may prompt for the password of the domain\user.
# You know that the script has work as in the console the prompt changes to 
# [nodename]: PS somepath>
Write-Output "opening WinRM session on $thisComputerNodeName for $myDomain\$myUserName"
Write-Output "to exit the WinRM Session type exit."
Enter-PSSession -ComputerName $thisComputerNodeName -Credential $myDomain\$myUserName