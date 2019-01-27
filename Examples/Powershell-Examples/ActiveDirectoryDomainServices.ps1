# The Active Directory Domain Sevice might not be installed by default on your Windoes OS. 
# The term 'Get-ADUser' is not recognized as the name of a cmdlet

# Even when the ADDS is installed you might still need to import the 
# corresponding Powershell module.
# https://stackoverflow.com/questions/17548523/the-term-get-aduser-is-not-recognized-as-the-name-of-a-cmdlet
import-module activedirectory

# To check whether the ADDS Powershell Module is already istalled.
# check whather a module exists on your system
get-module -listavailable

# ------------------------------------------------------------
# 1-Install the Active Directory PowerShell Module on Windows 10
# 2-Turn on the RSAT services.
# 3-Make sure that the ActiceDirectory Powershell module is installed.

# https://blogs.technet.microsoft.com/ashleymcglone/2016/02/26/install-the-active-directory-powershell-module-on-windows-10/
# https://social.technet.microsoft.com/Forums/en-US/35b8af8b-a266-495e-aca8-f7b4efd3b41c/powershell-install-active-directory-modules-on-windows-10-from-windows-2016-dc?forum=winserverpowershell

# This is especially useful
# https://stackoverflow.com/questions/38345129/install-active-directory-module-powershell-for-windows-10

# A brief summary
# Remote Server Administration Tools (RSAT) for Windows operating systems.
# Remote Server Administration Tools (RSAT) enables IT administrators to remotely manage roles and features.
# You cannot install RSAT on computers that are running Home or Standard editions of Windows.
# You can install RSAT only on Professional or Enterprise editions of the Windows client operating system. 
# The tools are not automatically available after you download and install RSAT. You must enable the tools that you want to use by using Control Panel.
# click Start, click Control Panel, click Programs and Features, and then click Turn Windows features on or off. 

# https://support.microsoft.com/en-us/help/2693643/remote-server-administration-tools-rsat-for-windows-operating-systems

# Remote Server Administration Tools for Windows 10 download.
# https://www.microsoft.com/en-us/download/details.aspx?id=45520
# ------------------------------------------------------------