# Windows PowerShell providers let you access a variety of data stores 
# as though they were file system drives.
help Get-PSProvider

Get-PSProvider

#Name                 Capabilities                        Drives                            
#----                 ------------                        ------                            
#Registry             ShouldProcess, Transactions         {HKLM, HKCU}

# HKCU = HKEY_CURRENT_USER
# HKLM = HKEY_LOCAL_MACHINE
# Access the registry through the HKLM drive.
# This will change the Powershell promt into PS HKLM:\>
# as if the HKLM section of the Registry were a drive on the file system.
# Type dir to see the contents of HKLM.
# PS HKLM:\> dir 
# fails unless an machine admin account is specified.
# PS HKLM:\> software PS HKLM:\> dir
# shows info on all the applications installed for all users on the machine.
Set-Location HKLM:

# Sets the value of a key in the registry.
Get-ItemProperty -Path .\7-Zip

#--------------------------------------------------------------------------------------------
# Sets the value of a key in the registry.
# In this example the value of the PackageInstalled property for the key .\7-Zip is set yo 1.
#Set-ItemProperty -Path .\7-Zip -Name PackageInstalled -Value 1
#--------------------------------------------------------------------------------------------