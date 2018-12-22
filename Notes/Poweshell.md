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

## Querying the History

The PowerShell console retails the history of the commands invoked for the session. This history is useful when working towards composing
a script for a cmdlet, function etc. by trial and error. It is also
useful to save the commnads of the session to a text file as illustrated in the table below. 

Another very useful tool is the **start-transcript/stop-transcript** pair of comdlets. The first starts the process that saves the history of the session to a text file of your choosing while the second obviously stops it. This saves the complete history of teh session and includes important information regarding it. Moreover, **it captures any errors that may be thrown by the ciommands run in the session** which is complete, useful information when trying to debug a script. **Transcript** is more powerful than simply dumping the history to a text file as in addition to extra information about the session and errors it also captures any output obtained from the command run as part of the session.


| Command | Results |
| ------- | ------- |
| `get-help history` | Shows the help for the alias history toget-history.|
| `get-history` | Lists all the commands used in the current session.|
| `invoke-history -id 1` | Runs the command at the top of the history list for the current session.|
| `get-history | out-file c:\history.txt` | Save the session history to a text file.|
| `Notepad c:\history.txt` | Open the said file in Notepad.|
| `help start-transcript` | Gets help on the start-transcript cmdlet.|
| `start-transcript path c:\mysession.txt` | Captures the entore PowerShell session into a text file.|
| `stop-trascript` | It stops the transcription of the session to text file.|
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

## Objects, Properties, Methods, Members and Piping.

Normally the output from and often also the imput to any PowerShell cmdlet or Function is modelled as an **object**. Any object have **members** which may be either **properties** or **methods**. **Properties** bear values for the object while **methods** are actions thatcan be performed over the object just as in the traditional OOP model.

The output of any cmdlet or Function can be piped into another cmdlet or Function by means of the **pipe** ```|```.

The ```select-object``` cmdlet is often used in combination with other command to filter the objects provided as output into objects bearing only a subset of properties. In addition ```where-object``` is also useful as it allowed to filter the output of a cmdlet or Function on a specified condition.

| Command | Results |
| ------- | ------- |
| `help get-member` | Shows the help about the get-member cmdlet.|
| `get-service | get-member | more` | Shows all the members of the object type for the output from the cmdlet get-service .|
| `get-service | select-object Name,Status | more` | Selects only the properties Name and Status of any object from the output of the cmdlet get-service.|
| `get-service | where-object status -eq "stopped" | select-object Name,Status | more` | Filters the output of get-service to only the stopped services and projects it into an object with only Name and Status properties.|
| `get-service | where-object status -eq "stopped" | format-list | more` | As above but formats the output into a list insted of the standard Powershell table alos all the properties are shown for each item of the list.|
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
| `get-service | where-object Status -eq 'Stopped' |  out-file $path` | Export the list of all stopped services to a text file of specified full path.|
| `notepad $path` | Opens the file created above.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName` | List of the three properties Status, Name, DisplayName of all Stopped services.|
| `get-service | where-object Status -eq 'Stopped' | select-object Status, Name, DisplayName |  export-csv $path` | As above but exported to a CSV file.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|

## Formatting

| Command | Results |
| ------- | ------- |
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
