# Powershell

## References

1. [PowerShell: Getting Started](https://www.pluralsight.com/courses/powershell-getting-started)

2. [Your First Day with Powershell](https://app.pluralsight.com/library/courses/powershell-first-day/table-of-contents)

## Powershell Tools

### Powershell ISE 
- [How to start the Powershell ISE](https://social.technet.microsoft.com/Forums/en-US/56e2ef15-66fc-4af7-acc5-21fdc98438f8/where-is-the-powershell-ise?forum=W8ITProPreRel)

## RSAT 
- [Remote Server Administration Tools for Windows 10](https://www.microsoft.com/en-us/download/details.aspx?id=45520)  

### PowerShell Cmdlet and Functions

- https://stackoverflow.com/questions/18469012/why-are-scripted-cmdlets-listed-as-functions

A cmdlet (pronounced "command-let") is a lightweight Windows PowerShell script that performs a single function. A command, in this context, is a specific order from a user to the computer's operating system or to an application to perform a service, such as "Show me all my files" or "Run this program for me." Cmdlets amy be written by using one of the 
.Net languages i.e. C# or F#.

Potentially isn't much difference between a function and a cmdlet, but that depends on how much work you're willing to put into writing the function. The real difference between a mere function and a full cmdlet is that cmdlets support powerful parameter bindings. You can use positional parameters, named parameters, mandatory parameters, and even do basic parameter validation checks—all by simply describing the parameter to the shell. Functions,are written entirely in script, have the same capabilities as a “real” cmdlet written in C# or Visual Basic and compiled in Visual Studio. These advanced functions (they were originally called “script cmdlets” early in the v2 development cycle) help you write more flexible functions you can then use seamlessly alongside regular cmdlets.

### PowerShell getting help on Cmdlet and Functions

```
get-help <cmdlet>
```

In particular look at the section ```RELATED LINKS``` in which Cmdlets and Functions are provived that are related to  the given ```<cmdlet>``` parameter. Instead the ```REMARKS``` section of the
output usually shows examples of the usage for the Cmdlet or Function.


### PowerShell Tips

1. In order to break out of the output or the execution of a Cmdlet or Function use ```CTRL+C```.

---

## Basic Commands

### Getting Help

| Command | Results |
| ------- | ------- |
| `get-help` | shows how to get help on areas of PowerShell.|
| `help` | help is an alias for get-help.|
| `get-help | more` | as above but tabbed.|
| `get-help *DNS*` | find all help docs for any cmdlet that has DNS in its name.|
| `get-help get-service` | Obvious.|
| `help get-service -examples` | pulls out only the examples of usage. |
| `get-help get-service -detailed` | provides more output. |
| `get-help get-service -full` | provides more output.|
| `get-help get-service -online` | provides up-to-date help from  the online documentation.|
| `help *about*` | shows all the about files in the help system.|
| `help ` |.|
| `help about_aliases` | shows the help about aliases.|
| `update-help` | Updates the local PowerShell help files - must run as admin.|
| `help get-command` | Shows the help for the get-command cmdlet.|
| `help get-command -examples` |  Shows the examples from the help for the get-command cmdlet.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|

## Querying for PowerShell verbs

| Command | Results |
| ------- | ------- |
| `get-verb` | shows all the verbs available in PoweShell.|
| `get-verb | more` | use the spacebare to display the next window of results.|
| `get-verb -verb s*` | shows all the verbs that starts with the letter s.|
| `get-service -Name M*` | shows all the services that starts with the letter M.|
| `gsv -Name M*` | the same as above but the **gsv** alias is used.|
| `gsv -Name M*, S*` | get the services starting by m and then those starting by s.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|

## Querying for system services

| Command | Results |
| ------- | ------- |
| `get-service` | List all the OS services and their current status.|
| `get-service | where-object Status -eq 'Stopped'` | List all stopped services.|
| `get-service | where-object Status -eq 'Stopped' |  export-csv $path` | Export the list of all stopped services to a CSV file of specified full path.|
| `notepad $path` | Opens the file created above.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName` | List of the three properties Status, Name, DisplayName of all Stopped services.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName |  export-csv $path` | As above but exported to a CSV file.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|

## Some interesting commands

| Command | Results |
| ------- | ------- |
| `$PSVersionTable` | It shows details about the version of PS on the system.|
| `ise` | Starts the Integrated Scripting Environment.|
| `notepad $path` | Opens the file created above.|
| `get-command | more` | Shows all the commands i.e. cmdlets, functions, aliases, etc. that are installed on the system.|
| `get-command -Type Cmdlet | Sort-Object | more` | Pulls out only cmdlets and pipes into the sort-objetc cmdlet.|
| `get-command -Type Cmdlet | Sort-Object -Property Noun | Format-Table -GroupBy Noun | more` | 
sorts all the cmdlets by their Noun property and formats the output into a table grouped by the Noun property.|
| ``get-command -Type Cmdlet | Sort-Object -Property Noun | Format-List -GroupBy Noun | more` |.|
| `get-command -name *IP* | more` | Shows all the installed commands that include IP in their name.|
| `get-command -name *IP* | Sort-object -Property name | Format-Table | more` | Pipe the results into the sort command by the property name.|
| `get-command -name *IP* | Sort-object -Property name | Format-Table -GropuBy CommandType | more` | Sort by name and group by CommandType.|
| `get-command -name *IP* -module NETTCPIP | more` | Limits the search to the module NETTCPIP.|
| `` |.|
| `` |.|
