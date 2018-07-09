## Azure Tools

1. [iothub-explorer](https://github.com/Azure/iothub-explorer)
2. [Service Bus Explorer](https://github.com/paolosalvatori/ServiceBusExplorer)
3. [Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/)  

## Azure Evets Hub

1. [Azure Event Hubs for .NET Developers: Fundamentals](https://app.pluralsight.com/library/courses/azure-event-hubs-dotnet-developers-fundamentals/table-of-contents)  
2. [Getting Started with Azure Event Hubs with C#](https://app.pluralsight.com/library/courses/azure-event-hub-c-sharp/table-of-contents)  

The IoT Hub is based and the Azure Events Hub.
The AEH is a platform that can injest lots of data. 
The IoT hub has a default event endpoint which is AEH.

### Hub Partitions

The IoT Hub is divided in up to 32 partitions.
The higher the number of partition the greater the parallel processing power.
The number of partition cannot be changes after creating the hub. 

When messages are pushed to the IoT from a device
the message is assigned to a partition based on the
id value.
Each new message is added at the end of a partition.
Therefore the order of the messages is guarateed - queue.

### How many partitions should you have on any IoT Hub?

https://app.pluralsight.com/player?course=azure-iot-hub-developers-getting-started&author=matt-honeycutt&name=cc8b8dcd-920e-4d0b-9892-bd1e85e33298&clip=7&mode=live

Determining the right number of partition is important 
because it is not possible to change the number of partitions
on an IoT Hub once it has been created. In order to change the 
number of partition a new hub must be created but this may cause
lost of settings includiuong devices twins to be losted. 

The Higer the number to partiotion on the Hub the greater is the 
potential for parallelism by means of **Event Processor Hosts**.
The max number of poartitions for any IoT Hub on a paied subscription
is 32. Despite there is no extra financial cost associated to the 
number of partitions set up on a Hub it must be considered that the
**Event Processor Hosts** must perform upkeeping of its state by 
means of a dedicated blob storage account and this operation requires
time. Therefore, it is essential to determine the right balance between 
the number of partitions and the message load on the hub, the best is 
probably trial and error with performance measurements before 
commiting to production.

If the number of partition is too large in relation to the message 
workload it may cause some of **Event Processor Hosts** to seat idle.

A huristic is 

1. start with 2 or 4 partition according to the message workload on 
   the IoT Hub.
2. Double the number of partition until load-balancing is achieved
   at steady state.
3. Consider an alternative parallelization mechanism such as through 
   **Consumer Groups**. For example if the are messages that require
   different time to process i.e. slow and fast, split the messages
   processing into two corresponding **Consumer Groups** each served
   by a number of **Event Processor Hosts** instances. This is an
   example of a 2-tier architecture for the IoT Hub.

#### Consumer Groups

You can think of a CG as a virtual view on top of the
event hub.

CGs are useful because allow to have multiple set of processing 
for the messages that reach the partitions of a hub.

Each CG maintains state, postion, offset of the processing
of messages for each of the partition on the hub.

There exist a default CG but custom CGs can be created. 

### Processing Messages on a Hub

There exist a number of NuGet packages with base classes.

1. [Receive events from Azure Event Hubs using the .NET Framework](https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-dotnet-framework-getstarted-receive-eph)  
   This is a low level solution. It is a class that 
   connects a receiver to a partition for a consumer group
   but the logic to manage the state of the partition is not 
   included in the base implementation. 

2. [Event Processor Host](https://www.nuget.org/packages/Microsoft.Azure.ServiceBus.EventProcessorHost/) 
   This inherits from the Event Hub Receiver but add some basic 
   implementation to handle connect and disconnect, to manage the
   state of teh partition and to scale under load. Moreover, the 
   Event Processor Host implemetation has a out-of-the-box scale-out 
   model implementation. When a new EPH is created it acquires a 
   lease for every partition. If another instance of a EPH is created
   for the same consumer gorup it will attempt to do the same and 
   overtime the host reaches an equilibrium in which each processor 
   leases about the same number of partitions that is there is an
   automatic load-balancing mechanism when using Event Processor Host.
   Adding more EPHs created load-balanced parallelism and fault-tollerance
   there is scope to combine this with Akka.net. 
   
3. Un important best practise is to make sure that when a message processor 
  finishes its work it releases the lease on a message partition. In order 
  to to this the processor host must close gracefully that is with no exceptions
  thrown by the host process for teh .NET Core app. When you are playing with
  PowerShell do not just close the shell as this is not a graceful termination
  of teh .NET Core process and teh lease acquired on the message partition will
  not released until it expires. Instead add a Console.ReadKey or similar to 
  exit teh process gracefull (with 0). The problem with not releasing the lease  
  is that when the a new message processor instance is started it might not 
  able to acquire a lease because all the partition are leased out, although
  some of teh processors to which the lease was granted might be no longer 
  running.
   
---

## IoT Hub

1. [IoT Hub Learning Path](https://azure.microsoft.com/en-gb/documentation/learning-paths/iot-hub/)  
1. [Azure IoT Hub for Developers: Getting Started](https://app.pluralsight.com/library/courses/azure-iot-hub-developers-getting-started/table-of-contents)
2. [Use iothub-explorer for Azure IoT Hub device management](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-device-management-iothub-explorer)
3. [Azure-iothub-explorer on GitHub](https://github.com/Azure/iothub-explorer) 

## IoT Hub custom routes and end points

1. Azure Event Hub
2. Blob storage for cheap permanent persistance of messages in Apache AVRO format
3. Service Bus Queue 
4. Service Bus Topic

### Limits on the number of routes

1. 10 custom endpoints
2. 100 routes

---

## IoT Message Type Options

### Device to Cloud (D2C) 

The device can send three types of messages to an IoT Hub.
The format of the data sent to the IoT Hub can vary but it can consist of
any binary data i.e. serialized objects. One typical choice may be POCO 
C# object serialized to JSON. The binary stream or byte array of the JSON 
can be ultimately sent to the Hub. The byte array may also be compressed 
before sending it to the Hub to save bandwidth.

When the message reaches the Hub it is shuffled to one of the available
Hub partition (1 to 32 max) based on the device ID. Message Porcessors
can then be used to take leases on the partition and read off the messages
to do something with them. 

The Message Processor has tthe following resposibilities.

1. Request a lease on a partition on the Hub.
2. Use a blob storage to maintain teh state of the processor.
3. Deserialise the binary messages read from the Hub.

The following are the types of messages that devices can send to an 
IoT Hub.

1. Device to Cloud (D2C) for high-frequency real-time telemetry data.
2. Device twin's reported properties.
3. File uploads for infrequent and intermittent communication scenarios
   with a large amount of data batched up in a single file.  

### Cloud to device (C2D)

1. C2D messaging i.e. news broadcast to devices, real-time, high-rate.
   This is a **One-Way messaging pattern**. The Hub attempts to deliver a message
   to a device or seet of devices until the message is sent or expires.
   For this pattern all three transposrts are available **AMQP, MQTT, HTTP(S)**.
   However, with **HTTP(S)** the device must poll the hub and it is designed only 
   for scenarios that can accept latency thus is not appropriate in most cases.
   **MQTT** is efficient but **does not** allow the options of rejection, accept,
   abandon. The best is **AMQP** which is efficient and allows rejection, accept,
   abandon, this also makes it possible to have **Two-Way** communication because
   either a rejection or acceptance message may be sent back to the Hub.

2. **Using device Twins for C2D.** 

   There are two ways device twins can be used. 
    
   * D2C in which the device sends a message to the IoT Hub to notify it 
     that it would like to make changes to **desired properties** of its
     twin.

   * C2D in which a **.NET Core manager app** changes the **desired properties**
     so that teh IoT Hub then send down those changes to the device. The device
     once notified decides what to do with it i.e. apply the changes or not and 
     normally replies to the IoT Hub hence the manager.

   The C2D Device Twins patterb works even when the device is off-line as the 
   changes to the device twins on teh Hub are persisted and passed to the device
   as soon as it goes on-line again.

   This pattern is ideal to notify the device of a firmware update.
   This pattern **cannot be used by devices that connect to the hub via HTTPS**
   only AMQP and MQTT transports are supported.
   The device **cannot reject a desired property change direclty** all it can do
   is just simply ignore the changes sent over by the Hub.
   This message pattern is designed for low-frquency communication i.e. tens msgs
   per sec.

---

## Models C2D - D2C

1. The simplest model is that based on three moving parts **AGENT-HUB-MANAGER**.
  
   1. The device with the .NET Core app running on it which is called AGENT.
   2. The IoT Hub that is the piece of cloud infrastructure that seats in the middle.
   3. The manager .NET Core app that deals with the C2D communication.
   4. This pattern may be implented as one-way or async req-rep.

This is a simple model and thanks to the **build in feedback** provided by the classes
in **Microsoft.Azure.Device** NuGet pkg you can build a complete C2D-D2C two way 
architecture. However, there are some missing services with this implementation
such as  

   1. You must do **correlation** yourself betwen the C2D message and the 
       D2C feedback message by using and storing the message IDs.
   2. In this message pattern teh device sends the feedback message to the 
       IoT Hub and not to the device directly.
   3. There is a time laps between sending the message from the MANAGER to the DEVICE
      through the IoT Hub and getting the feedback message to the MANAGER in the 
      reverse path.
   4. this pattern is always async req-rep. 


2. The second method is called **DIRECT METHOD AKA DEVICE METHOD**.

In this implementation the MANAGER may call a method on the DEVICE as if it were a 
local service and it can receive feedback immediately. This pattern offers some 
advantage but present also disadvantages.

#### Advantages 

* provides very high throughput 
* provides immediate direct feedback
* eliminates the need of correlation of C2D and D2C (feedback) messages via IDs 

#### Disdvantages 

* Only supports AMQO and MQTT transports.
* This pattern is not suitable for devices that may be off-line because calls 
  on the exposed API on the device will fail. 

---

### IoT Hub Message Transport Options

1. HTTPS polls with a 25 minutes lag only for occasionally connected devices.
2. MQTT over WebSockets (PORT 443) which is efficient with samll payloadsize and small overall footprint.
   MQTT allows immediate connection between hub and device. However, it does not allow rejection
   of messages that the device might not be able to process.
3. AMQP (Adavnced Message Queuing Protocol) which is better than HTTPS and allow immediate delivery.
   AMQP can accept and reject messages. 
   AMQP relies on WebSockets thus it uses (PORT 443).

### Device twins in IoT Hub

Device twins are json documents holding a set of metadata for each of the 
devices registered with the hub. 

1. Identity and state.
2. Tags 
3. Reported Properties.
4. Desired Properties

You may query the hub to gather information of all the devices that are 
registered  with the hub. For example, to know which devices are presently
connected or in a particular state, etc. It is also possible to query the 
hub by the twins tags i.e. if one of the tags is the GIS location of the
device it would be possible to retrieve the list of all devices that are 
in a location.  

Reported properies are set when the device initialises, these may be queried
but cannot be set remotely. 

Desired Properties my be used to communicate with the device as they can be
queried and set remotely.

The may be a lag between updating a desired property and the change of the 
value of the corresponding reported property. It is up to the device to 
decide how to handle the requested changes and finally report the change to
the hub.

1. [Understand and use device twins in IoT Hub](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-device-twins)

---

## IoT Hub Explorer Foundamentals

* iothub-explorer login "...Your IoT Hub Key Here!..."
* iothub-explorer send device01 "Some message"
* iothub-explorer send device01 send-iterations 10 send-interval 1000 "Some message"

* iothub-explorer device-method device-01 "showMessage" "Hello from IoT Hub Explorer!"
  Invoke a direct method on the device from a manager.

* 
---

## Deployment to Azure

A solution based on IoT Hub will have the following components

1. A .NET Core app running on the devices
2. A IoT Hub on which the devices are registered
3. Some other .NET Core apps on the server side

The last category may encompass the following

1. Manager apps that sends C2D messages to the devices with one or mopre 
   of the three message patterns.
   * MANAGER-HUB-DEVICE
   * DIRECT MESSAGES
   * C2D TWINS UPDATES
2. Processing apps that read the messages sent by the devices to the Hub
   (in general telemetry, etc.) and process them in some way.

The app on the device must, of course, run on the device!
However, for the other server-side apps different possibilities exist.

https://app.pluralsight.com/player?course=azure-iot-hub-developers-getting-started&author=matt-honeycutt&name=054b9253-dfe7-4bbc-a677-6076b0eb4bab&clip=1&mode=live

1. they run as self-hosted app or web services on virual machines 
   or proprietary hardware.
2. the application can be deployed as **WebJob** which gives both 
   vertical (more computing power) and horizontal (parallelization)
   scaling.
3. The app can be package into **Azure container** that can be deployed  
   on Azure infrastructure.
4. The **serverless option** using **Azure Functions** which can be bound  
   to the IoT Hub endpoints. 
5. **Stream Analytics** which is a very powerful tool to process streams
   of data with a SQL like language.
6. Use **Power BI** to build customized dashboards.

---

## [Azure IoT solution accelerators](https://www.azureiotsolutions.com/Accelerators)  

Provides a series of prepackage solutions for various IoT scenarios. Each is backed up 
by an open source project in both Java and .NET 

[Azure IoT solution accelerators overview](https://iotschool.microsoft.com/learning-paths/4FyFf43XTGus4u4eO2ms0g/modules/4umtwpRcPKY6gUK0EsU8Su)    
https://github.com/Azure/azure-iot-pcs-remote-monitoring-dotnet

1. Remote Monitoring
2. Predictive Maintenance
3. Connected Factory
4. Device Simulation

### Sectors

1. Healthcare
2. Manufacturing
3. Retail
4. Smart City
5. Transportation

### Examples

1. [Remote Monitoring](http://www.microsoftazureiotsuite.com/demos/remotemonitoring)


### Azure IoT Tutorials

1. [Explore the Azure IoT solution accelerators value proposition](https://iotschool.microsoft.com/learning-paths/4FyFf43XTGus4u4eO2ms0g)  
   
   Azure IoT solution accelerators provide a starting point for your 
   own IoT solutions. You can customize these solutions to meet your 
   specific requirements. Three solution accelerators are available 
   today:
    
   * Remote Monitoring  
   * Predicative Maintenance
   * Connected Factory

2. [Azure IoT Hub](https://iotschool.microsoft.com/learning-paths/7zy2NeaeYwsoaCuIueeoSa) 

   This path focuses on Azure IoT Hub, a fully managed service that enables reliable and secure 
   bidirectional communications between millions of IoT devices and a solution back end. You will 
   find out about various features of Azure IoT Hub, so you can learn how to develop for IoT Hub. 
   Then to understand Azure IoT Hub messaging, you will be introduced to one of the communication 
   options for device-to-cloud messaging:  

   * device-to-cloud messages for time-series telemetry 
   * alerts.
   
3. [Azure IoT Edge introduction](https://iotschool.microsoft.com/learning-paths/6qJe4ohYd2EsAEo6gw6C2G)

   Azure IoT Edge is an Internet of Things (IoT) service. This service is meant for customers 
   who want to analyze data on devices, a.k.a. "at the edge". By moving parts of your workload 
   to the edge, you will experience reduced latency and have the option for off-line scenarios.  

   https://www.amplified.com.au/azure-iot-hub-device-gateway

---

# Parts of Azure infrastructure to use with IoT Hub

[Microsoft Azure for Developers: What to Use When](https://app.pluralsight.com/library/courses/microsoft-azure-developers-what-to-use/table-of-contents)    

These parts can all be used to gether with the IoT HUb

1. [Azure Data Lake](https://azure.microsoft.com/en-gb/services/data-lake-analytics/?&OCID=AID719823_SEM_lkIVY6rh&dclid=COOnobnqjNwCFQrRZAodXjwO0A)   
[ntroduction to the Azure Data Lake and U-SQL](https://app.pluralsight.com/library/courses/u-sql-azure-data-lake/table-of-contents)  

2. [Data Warehouse](https://azure.microsoft.com/en-gb/services/sql-data-warehouse/?&OCID=AID719823_SEM_J2G8Ui5i&dclid=CMuStoPrjNwCFU7AZAodl2sIYQ)  
[Introduction to Data Warehousing and Business Intelligence](https://app.pluralsight.com/library/courses/intro-dwbi-course-2017/table-of-contents)  

3. [Time series insights](https://azure.microsoft.com/en-gb/services/time-series-insights/)  

4. [Azure Functions](https://azure.microsoft.com/en-gb/services/functions/?&OCID=AID719823_SEM_0DwINtxM&dclid=CP6nhafrjNwCFUz8ZAodXbQHTg)  
[Azure Functions Fundamentals](https://app.pluralsight.com/library/courses/azure-functions-fundamentals/table-of-contents)  

5. [Azure Machine Learning Studio](https://azure.microsoft.com/en-gb/services/machine-learning-studio/?&OCID=AID719823_SEM_tK5mWD7P&dclid=CLjxwqrsjNwCFcznZAod6o4Iuw)    
[Getting Started with Azure Machine Learning](https://app.pluralsight.com/library/courses/azure-machine-learning-getting-started/table-of-contents)  

6. [Azure Logic Apps service](https://azure.microsoft.com/en-gb/services/logic-apps/)    
[Azure Logic Apps: Fundamentals](https://app.pluralsight.com/library/courses/azure-logic-apps-fundamentals/table-of-contents)    
[Azure Logic Apps: Getting Started](https://app.pluralsight.com/library/courses/azure-logic-apps-getting-started/table-of-contents?aid=7010a000002BWqGAAW)    

---

## Azure infrastructure

+Control & Responsibility
 - ------------------------
1. IaaS = Infrastructure as a Service
2. PaaS = Platform as a Service (best compromise as the OS is taken care of and you retain control over the app and scaling)
3. LaaS = Logic as a service (serverless cloud computing services it scales automatically)
- ------------------------
+More Business Value

### Options for running an app in Azure

* IaaS
  
  1. VMs
  2. Container Service
  3. Container Insances Service
  4. Web App for Containers Service
  5. Azure Batch
  
* IaaS/PaaS

  6. Azure Service Fabric

* PaaS

  7. Azure Cloud Services
  8. Web App
  9. Mobile App

* LaaS

  10. Function App
  11. Logic App

### 

---

## OPC (DA=Data Access) & OPC UA (Unified Architecture)

1. [An introduction to OPC UA open platform communication unified architecture](https://www.youtube.com/watch?v=rd52ewG88B8)
2. [Introduction to OPC with Examples](https://www.youtube.com/watch?v=E6ELXwzJFgE)
2. [What is OPC? Part 1: OPC Overview](https://www.youtube.com/watch?v=mK-OL03LaGg)
3. [What is OPC? Part 2: OPC Case Study](https://www.youtube.com/watch?v=OnXJMR7ijbM)
4. [What is OPC? Part 3: OPC Specifications](https://www.youtube.com/watch?v=JDRVwDls6Yg)
5. [What is OPC? Part 4: OPC Compliance](https://www.youtube.com/watch?v=q94vxbXzkCc)

OPC uses Microsoft's DCOM while OPC UA does not thus it can run on other devices than just 
Windows OS. OPC UA is based on XML that is a text-based data exchange format. OPC UA can run 
on mobile devices thus you may create an HMI on a mobile or a tablet!

An OPC UA can be queried by an OPC Client to autopopulate the tags table and the properties
then the sofware that hosts the OPC client can select only the tags and the properties that 
is interested in consuming i.e. an HMI panel displays a subset of all the available tags and
properties.

OPC UA is a big step forward above all because of the autodiscovery and autopopulation feature
just described because it spares lots of time on the client development. In the past clients 
i.e. HMI instegrator had to write their own tags tables by hand from the specs of the PLC driver!

OPC UA is the backbone on IoT!

### The most important OPC standards

1. OPC DA = Direct Access based on DCOM from Microsoft to access the most recent data item
2. OPC HDA = Hystorical Data Access to access hystorical data items
3. OPC AE = Alarm & Event

### How to write an OPC client in C#

1. [How to write an OPC client in C#](https://www.youtube.com/watch?v=QyMbYdxdDxI)

### Free OPC Servers

1. [Matrikon OPC Server](https://www.matrikonopc.com/)

### Free OPC Clients

1. [Free Stuff - OPC Clients](https://www.opcconnect.com/freecli.php)

### OCP Clients for .NET

1. [Measurement Studio by NI](http://www.ni.com/mstudio/)

### UDC Controller

1. [Matrikon OPC Explorer](https://www.matrikonopc.com/)
2. [Honeywell UDC Controllers:PID Control made easy](https://www.youtube.com/watch?v=91tMM2OmLhc)

### PLCs, SCADA

ICS = Industrial Control System
PLC = Programmable Logic Controller
SCADA = Supervisory Control & Data Acquisitions 
SCADA RTU = Remote Terminal Unit

1. [E- Learning SCADA Lesson 1- What is SCADA?](https://www.youtube.com/watch?v=5ZiIA-kMV8M)
2. [Lesson 2 - How to create elements in mySCADA](https://www.youtube.com/watch?v=zZgMVKfhZwQ)  
3. [Lesson 0 = Basic Industrial Controls](https://www.youtube.com/watch?v=eBK8lgEd0r0)  
  
### Communication Types

1. [MODBUS]
2. [Fieldbus]
3. [CAN]
4. [Profibus]
5. [Ethernet]

* [Understanding Modbus Serial and TCP/IP](https://www.youtube.com/watch?v=k993tAFRLSE)  
Serial communication protocol that can be used on standard serial cable and ethernet cable.
It was inveted in 1979 for industrial PLC application and there exist 3 variations.
Mobus is 1-master/m-

1. Modbus ACII
2. Mobus RTU (most popular with binary encoding and CRC it runs on the EI RS232, RS485, RS422)
3. Mobus TCP/IP

---

### Serial electrical interfaces

1. RS232 point to point master/slave up to 50 ft (15m)
2. RS485 master/m-slave over 15m for up to 32 nodes over 4000ft (1200m) without a rpeater
3. RS422

---

### Scada Software

1. https://www.myscada.org/en/
2. AT-AUTOMATION Ignition SCADA https://www.at-automation.nl/en/scada-software-system/?gclid=CjwKCAjwsdfZBRAkEiwAh2z65vU2vy2eK9jXwEyOC_CRyU7V5U55TQ6mtlsVy8BUTU2m4zhCHx4xNxoCV_wQAvD_BwE
3. https://www.quora.com/What-is-the-best-open-source-SCADA-software
4. http://www.free-scada.org/

"HostName=psdemohub1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=UDr/oJdwKflSnYaM/mQu4FjHZWE2y7AzzRKwPp1a28s="
