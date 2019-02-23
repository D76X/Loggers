# The Active Directory Domain Sevice might not be installed by default on your Windwos OS. 

#------------------------------------------------------------------
# For example 
# The term 'Get-ADUser' is not recognized as the name of a cmdlet

# This is the case when the **Active Directive Module for PS** has
# noy yet been installed on your OS which might be the case on the
# Windows 10 OS.
#------------------------------------------------------------------

# The ADDS services are a set of applications that you find installed 
# on Windows 10 as **Windows Administrative Tools**. Check these tools
# from the start menu to see whether the ADDS parts are present.
# If this is not the case you must install then as a separate component. 

# https://www.microsoft.com/en-us/download/details.aspx?id=45520

#------------------------------------------------------------------

# Alternatively, to check whether the ADDS Powershell Module is already 
# istalled you may use the cmdlet below.

get-module -listavailable

# ------------------------------------------------------------

# Even when the ADDS is installed you might still need to import the 
# corresponding Powershell module. You can easily test whether this 
# is necessary by typing 'Get-ADUser' in the ISE to see whether the 
# commands are already installed. This should be the case if the module
# is present in **Windows Administrative Tools**.

# If the module still needs to be install you can do so by executing
# the cmdlet below as Admin.

# https://stackoverflow.com/questions/17548523/the-term-get-aduser-is-not-recognized-as-the-name-of-a-cmdlet
import-module activedirectory

# you might also want to update the module's help on the local PC.
Update-Help -Module activedirectory

# ------------------------------------------------------------


# 1-Install the Active Directory PowerShell Module on Windows 10
# 2-Turn on teh Windows Feature **Active Directory Lightweight Directory Services**
# 3-Turn on the Windows Feature **RAS Connection Manager Administrator (CMAK)** .
# 4-Make sure that the ActiceDirectory Powershell module is installed.

# https://www.youtube.com/watch?v=eBdEoczETDY

# ------------------------------------------------------------

# if all has been correctly setup you should be able to run the following cmdlets.
# as usual to know more about the available cmdlets and their usage
help Get-ADUser -examples

# http://www.curtisjohnstone.com/?tag=powershell-ad-unable-to-find-a-default-server-with-active-directory-web-service-running
# https://www.petenetlive.com/KB/Article/0001275
Get-ADUser -Identity davidespano -Properties *