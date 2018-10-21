## Azure CLI

### Refs

1. [Azure CLI: Getting Started Pluralsight course](https://app.pluralsight.com/library/courses/azure-cli-getting-started/table-of-contents)

### Frequently used commands

- az login
- az account show

| Command                                                              | Results                                                                       |
| -------------------------------------------------------------------- | ----------------------------------------------------------------------------- |
| `az login`                                                           | Login to your Azure account.                                                  |
| `az account show`                                                    | See all the currently selected subscriptions for the account.                 |
| `az account list`                                                    | See all the available subscriptions for the account.                          |
| `az account set -s "NameOfSuscription/guid"`                         | Select a subscription by name or its GUID.                                    |
| `az`                                                                 | Shows all the available base commands.                                        |
| `az --version`                                                       | Shows the vesrion of the CLI and subpackages.                                 |
| `az webapp -h`                                                       | Shows help on the webapp subgroups and subcommands.                           |
| `az webapp create -h`                                                | Shows help for the webapp create subcommand.                                  |
| `az webapp config -h`                                                | Shows help for the webapp config subgroup of subcommand.                      |
| `az group list`                                                      | List all the groups.                                                          |
| `az group list -o table`                                             | Formats the ouptput as a table.                                               |
| `az group list -o tvs`                                               | Formats the output as tab separated values.                                   |
| `az group list -o jsonc`                                             | Formats the output with syntax highlighted JSON.                              |
| `az functionapp list`                                                | List all the function apps.                                                   |
| `az group show -n ps-iot-hub-test --query location`                  | Show only the value of location property of the group of name ps-iot-hub-test |
| `az group show -n ps-iot-hub-test --query "[location, id] -o table"` | As above but include also the value of the prop id and format as a table.     |
| `az group show -n ps-iot-hub-test --query "{loc:location, id:id}"`   | As above but a JSON object is returned.                                       |
| `az account list --query "[].id"`                                    | Returns the ids of the accounts.                                              |
| `az account list --query "[].{name:name, id:id}"`                    | Returns the ids/names of the accounts as JSON objects.                        |
| `az account list --query "[0].{name:name, id:id}"`                   | As above but only the object at index 0.                                      |
| `az account list --query "[?name=='DreamSpark']"`                    | As above but only the account of name DreamSpark - the whole JSON object.     |
| `az account list --query "[?name=='DreamSpark'].state"`              | As above but only the value of the state property from the JSON object.       |
| `az account list --query "[?name=='DreamSpark'].state" -o tsv`       | As above but the raw value is extracted by -o tsv as tab separeted.           |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |
| `` |.                                                                |

### Examples

- Select a specific subscription before running any other commands

  > az account set -s "Visual Studio Professional with MSDN"

  or

  > az account set -s "DreamSpark"

### AZ Interactive mode

- The interactive mode of the Azure CLI provides intellisense/Autocompletion features.

> az interactive

- To exit the interactive Azure CLI

> CTRL+D

### [JMESPath syntax](http://jmespath.org/)

#### Filter the output to the console.

- http://jmespath.org/

The commands given to the Azure CLI might return a verbose output with much more information than
necessary. The --query parameter appended at the end of the expression allows to specify the subset
of information to return. The syntax used to provide a value for the --query parameter is the
JMESPath.

#### Examples

1.

> az network public-ip show
> -n mypublicip  
> -g my resourcegroup  
> --query ipAddress  
> -o tvs

### Best Practices

1. For each Azure account create multiple subscriptions each used to perform a related set of tasks.
   For example one for setting up sequrity another for storage another for logic apps another for databases,
   another for testing, another for staging, another for production, etc. In this way each sets of resources
   coming under the same subscription is billed separately and it is easier to slipt out and monitor costs.

2. Use resource groups to lump resources that belong together. In this way for example is easier to manage
   the resources in the group as a single unit. For example une user case is that by deletying a resource group 
   all the resources in the group are disposed of which saves from having to delete them one by one. This is 
   especially useful when resources are used for testing purposes and a no longer needed. Onether advantage is 
   that a resource group provides some defaults for the resources created in it. Foe example if the location
   of a resource added to a resource group is not specified then it defaults to that of its resource group if
   one is specified. Notice that resources added to a resource group need not be located in the same area of 
   the resource group, this just provides a default.

### Resource Groups



| Command                                               | Results                                    |
| --------------------------------------------------    | ------------------------------------------ |
| `az resources list -g $MyResourceGroupName -o tabkle` | lists all the resources in the given resource group.  |



### Virtual Machine

There are a number of considerations to account for when setting up a VM.

1. Which image do you need? Linux or Windows?
2. Do you need to start from an image with preinstalled software i.e. SQL Server, WordPress, ElasticSearch, etc.?
3. What disk technology do you need? SSD?
4. How many disks di you need?
5. How much CPU?
6. How much RAM?
7. Does your VM need a public IP address?
8. What ports are you going to open?
9. What network security group roles should the VM have?
10. Which VPN will the VM be on?
11. Do you need to automate the deployment of software to teh VM after its creation?
12. Are you going to run teh VM 24/7 or stop and restart it regularly?
13. Do you need credential to remote or SSH into your VM?

| Command                                            | Results                                                                                           |
| -------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| `az vm -h`                                         | Find help on the vm subgroup fo commands.                                                         |
| `az vm image list -o table`                        | Table out the most commonly used available VM images.                                             |
| `az vm image list --all -o table`                  | Table out all the available VM images - it might take a long time.                                |
| `az vm image list --all -f elasticsearch -o table` | Table out all the VM images which have Elastic search installef. The -f filters ut on the server. |
| `az vm image list --all -s VS-2017 -o table`       | Table out all the VM images which have SKU name VS-2017 - these have VS210 installed.             |
| `az vm list-size --location westeurope -o table`   | Table out all the VM images and their size available in west Europe.                              |
| `az vm create -n $MyVm -g $MyResourceGroup --image Centos --admin-username myusername --admin-password $MyAdminPswd` | Creates a VM named as the value of the local variable $MyVm from the image Centos in the resource group named as the value of the local variable $MyResourceGroup and the given password and username (which would be used to RDP into the VM).                                              |
| `` |.                                              |
| `` |.                                              |
