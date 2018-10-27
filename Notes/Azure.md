## Azure CLI

### Refs

1. [Azure CLI: Getting Started Pluralsight course](https://app.pluralsight.com/library/courses/azure-cli-getting-started/table-of-contents)

---

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
| `az account list --query "[?name=='DreamSpark'].state" -o tsv`       | As above but the raw value is extracted by -o tsv as tab separated.           |

---

### Examples

- Select a specific subscription before running any other commands

  > az account set -s "Visual Studio Professional with MSDN"

  or

  > az account set -s "DreamSpark"

---

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

---

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

---

### Resource Groups

| Command                                               | Results                                    |
| --------------------------------------------------    | ------------------------------------------ |
| `az resources list -g $MyResourceGroupName -o tabkle` | lists all the resources in the given resource group.  |

---

### Virtual Machines

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
| `az vm list-ip-addresses -n $VmName -g $ResGrpName -o table` | Table out the IP public addresses of the specified VM.|
| `az vm open-port --port 80 -n $VmName -g $ResGrpName` | Open port 80 on the specified VM.|
| `az vm show -n $VmName -g $ResGrpName --query "PowerState" -o tvs` | Show the power state of the specified VM.|
| `az vm  deallocate -n $VmName -g $ResGrpName` | Set the specified VM to a deallocated state.|
| `az vm show -d -n $VmName -g $ResGrpName --query "PowerState" -o tvs ` | The **d** flag shows additional details.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|


#### Install Virtual Machine Extensions

It's possible to install extensions on a VM. For example, the **custom script extension** once installed
allows custom PowerShell scrips to be run on the VM. The example below illustrates hoe to install the 
 **custom script extension**  on a VM from **Azure CLI**. Notice that in the script below settings are 
 passed to the command via a json file - it would be possible to specify them inline but this way is 
 clealry more practical. The settings file in this case contains a json object with properties specifying
 the path of the file(s) to download to the VM and that will be subsequently run on it and the execute 
 command expression that will be used to run the scripts on the VM.


```
az vm extension set \
--publisher Microsoft.Compute \
--version 1.8
--name CustomScriptExtension
--vm-name $VmName
--resource-group $RsGrpName
--settings extensionSettings.json
```

One use case for which you might want to enable **VM Extensions** such as the **CustomScriptExtensions** is
to run PowerShell script to perform regular automated maintenance or install software. For example, IIS may
be installed and one or more web sites deployed to IIS to use the VM as a dedicated web server.

| Command                                            | Results                                      |
| -------------------------------------------------- | -------------------------------------------- |
| `` |.|

#### Virtual Machine States

1. The payment model for a VM splits the costs into the static hardware resources and the compute resources.
   Costs for the latter can be avoided by setting the VM in a **"stopped"** state. However, you will still 
   incur in the cost for the **static hardware resources** such as the hard drive and the public IP addresses 
   of the machine. In order to remove also these costs the VM must not only be in a **stop** state but also be
   set to **deallocated** state. The deallocation **does not remove the VM from the resource group** that is the
   VM is still available as a resource but it's no longer running and has no hardware resources assigned to it 
   there is just its ISO. IT can be restored to **running** state in the usual way afterwards.

2. When a VM is stopped and then restated i.e. overnight to save on CPU costs, it will retain the same IP public
   address on restarting it. However, when it is **deallocated** it will normally obtain a **different public IP
   adress** on restarting it. 

3. When a VM is created there are much more resources allocated than the mere VM. For example, **network interfaces,
   disks, IP addresses, VPNs, etc.** are all resources that are provisioned together with a VM. The best way to deallocate
   all these resources together is to deallocate the resource group these are associated with.
   ***'''az group delete --name $ResGrpName'''*** .

---

### Managing Sorage Accounts

---



