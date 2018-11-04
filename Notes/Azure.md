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

### Use a Resource Group to efficiently manage Azure resources deallocation. 

3. When a VM is created there are much more resources allocated than the mere VM. For example, **network interfaces,
   disks, IP addresses, VPNs, etc.** are all resources that are provisioned together with a VM. The best way to deallocate
   all these resources together is to deallocate the resource group these are associated with. This applies in general any time a number of resources are allocated within a resource group. For example, when testing a new feature on a project thatmakes use of Azure services an ad hoc test resource group can be instantiated so that all the necessary Azure services for the test can be created within the test resource group. Once the test of the new feature is completed all the resources allocated within it canbe deallocvated by simply deleting the resource group with ***'''az group delete --name $ResGrpName --yes*** . 

---

### **SAS (Shared Access Signature)**

These are used to create **read-only, time limited secure URL** for stored resources such as 
files and blobs.

---

### Managing Sorage Accounts

1. **Blobs**  
Used to store files and other unstructured data in the cloud. Blob storage can also be used to 
control access to files by means of a **SAS (Shared Access Signature) tokens** . Notice that this
storage type is flat that is **there is no folder structure**. The resources may be accessed over
URLs and these URLs maybe modified at the time the resources are uploaded to the storage in order
to emulate a folder structure if it desirable to do so. For **Blob storage in public containers**
the resources are directly accessible over their URLs that is by using the file's URL directly
in the browser it will download the file. However, for **Blob storage in private containers** the
URL of the file is not enough to download the resource **a SAS token is also required.**.

This example illustrate how to get the URL of a file available on a Blob storage when the name of 
the file on the Blob is known together with the name of the storage container - the **-o tsv** 
option as usual is used to get a value instead of json as the Azure CLI would normally do.

```
az storage blob generate-sas \
-c $myPrivateContainerName \
-n $myBlobStorageName \
-o tsv \
```

Given the URL of the file as above it may be used directly if the blob storage in which the file
resides is public. However, **in case the container of blob storage is private it is required to** 
**create a SAS token to enable access to the file** . Notice that in the following the **--permission**
option is used to specify the access level that the generated SAS grants. In this case **r** means
**read access only** .

```
az storage blob generate-sas \
-c $myPrivateContainerName \
-n $myBlobStorageName \
-permissions r \
-o tsv \
-expiry 2018-10-27T12:00
```

Once a SAS token for a file in a Blob storage in a private storage container has been created 
the file will be accessible within the expiration time by using the **URL?SASkey** as below.

```https:\\myblobcontainer.blob.core.windows.net/..../myfile.txt?sa=6238982097840238409820384Augfsdjh```

2. **Files**  
These are containers used as files shares which may be attached to VMs as shared drives.

3. **Storage Queues**  
It's a simple and cost-effective storage solution for **sending and receiving messages**.

3. **Storage Tables**  
It's a simple and cost-effective storage solution for **storing semistructured data**.

Some of the tasks that you may want to automate by means of the Azure CLI in relation to storage 
accounts are 

- Create a new storage account.
- Upload blobs or files.
- Clean up tables or queues.
- Create **SAS** for stored resources.
- Planned download of resources on a schedule i.e. logs from Azure Table storage.
- Posting messages to queues in order to kick start processes i.e. some kind of maintenance task. 

One thing to do before running any od the commands to manage storage from the Azure CLI is to set 
your subscription i.e.

```az account set -s $MySubscriptionName/Guid```

You may also set up a number of variables that are often used in the Azure CLI commands as illustrated.
Note: the **name of the storage account must be unique accross all Azure users!** . 

```
rgn="MyResourceGroupName"
loc="westeurope"
san="MyUniqueStorageAccoutName"
```

Then create your resource group in the preferred location.

```az group create -n $rgn -l $loc```

Finally create your storage account - notice that without specifying the -l parameter in the command
the storage account would be created in the location provided as default by the specified resource
group. The **-sku Standard_LRS** means that the storage account will be created with the **Standard
Locally Redundant Storage SKU**.

```az storage account create -n $san -g $rgn -l $loc -sku Standard_LRS```

### Managing Storage Accounts and Containers 

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `az storage account create -n $san -g $rgn -l $loc -sku Standard_LRS` | Example to create a storage account.|
| cn=`az storage account show-connection-string -n $san -g $rgn --query connectionString -o tvs`` | Store the connection string for the named storage account into the variable cn.|
| `az storage -h` | As usual you get help from the Azure CLI, this will list the available storage commands.|
| `az storage container create -h` | Get help on the options to create a storage container.|
| `az storage container create -n "mypubliccont" --public-access blob - connection-string $cs` | Create a public blob storage container.|
| `az storage container create -n "myprivatecont" --public-access off - connection-string $cs` | Create a private blob storage container.|

### Managing Blob Containers

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `az storage blob upload -c $containerName -f $myFileName -n "MyStoredFile.txt"` | Upload a file to a public blob container.|
| `az storage blob url -c $containerName -n "MyStoredFile.txt" -tsv` | Find the URL of the stored filed on the specified container.|
| `az storage blob upload -c $privateContainer -"myfile.csv" -n $myBlobName` |Upload a file to a private blob container.|
| `az storage blob generate-sas -c $privateContainer -n $blobName --permission r -expiry $expDate -o tsv` | Generate a read only SAK to access the named blob on the private blob container within an expiration date.|

### Managing Queues

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `az storage queue create -n $queueName --connection-string $cs` | Create a queue on the storage account with the given conn str.|
| `az storage message put -q $queueName --content "My Message"`   | Post a message to a queue i.e. to trigger a maintenance workflow.|
| `az storage message get -q $queueName --visibility-timeout 120` | Read off the next available message from the queue.|
| `az storage message delete --id $msgId --pop-receipt $popId --queue-name $qN ` | Post a message delition to the queue.|

#### Reading and processing messages from a queue

When messages are read from a queue storage the `--visibility-timeout mm` specify the time lapse over which the message is 
expected to be processed by the caller before the same message is set to be readable on the queue again. This means that
the caller which reads a message obtains a json with the content of the message its id on the queue and its **popReceipt**
which must be posted back to the queue by the caller to notify the queue that the caller has processed the message and it's
Ok to remove it from the queue. Once the message has been deleted by the queue it is no longer available to any other caller.

### Managing Tables

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `az storage table create -n $tableName --connection-string $cs` | Create a table on the storage account with the given conn str.|
| `az storage entity insert -t $tableName -e PartitionKey="Settings" RowKey="Timeout" Value=10 Description="Timeout in sec"` | Inserts a rowin the table of given name with the given PK and RK and a bunch of data.|
| `az storage table query -t $tableName` | Retrieves all the rows from the named tabled.|
| `az storage table entity query -t $tableName --filter "PartitionKey eq 'Settings'"` | Retrieve all the rows fromthenamed table whose PK is set to the string Settings.|
| `az storage table entity query -t $tableName --filter 'Value eq 10` | Obvious.|
| `az storage entity replace -t $tableName -e PartitionKey="Settings" RowKey="Timeout" Value=5 Description="Changed"` | Replace the given PK+RK entity in the named table with the given data.|
| `az storage entity merge -t $tableName -e PartitionKey="Settings" RowKey="Timeout" Value=100` | Updates the Value of the entity.|
| `az storage entity merge -t $tableName -e PartitionKey="Settings" RowKey="Timeout"` | Retrives the entity at PK+RK in the named table.|
| `` |.|
| `` |.|

#### Azure Tables

Azure tables is a shcemeless storage, **only PK an RK are necessary** other than that it is possible to insert entities with as many
fields as necessary **up to 255**. Rows retrieved from any Azure table will also always have **Timestap and etag**. When the data for 
an entity needs to be modified there exist two upions **replace and merge**. The former requires to match the schema for the row while
with the latter only some of the fields can be updated by including them in the command with the new values.

There is some special syntax to query Azure table [Querying Tables and Entities](https://docs.microsoft.com/en-us/rest/api/storageservices/querying-tables-and-entities)  

### Managing File Shares

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `az storage file -h` | Show the Azure CLI help for the storage area and the file section.|
| `az storage share create -n $fileShareName --quota 2 --connection-string $cs` | Create a fileshare of 2GB.|
| `az storage file upload -n $fileShareName --source "MyFile.txt" --connection-string $cs` | Upload a file to the share.|
| `az storage file list -s $fileShareName` | Lists all the file on the share - returns json details.|
| `` |.|


---

## Managing Web Apps and SQL Databases

| Command                                            | Results                                    |
| -------------------------------------------------- | ------------------------------------------ |
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|
| `` |.|


