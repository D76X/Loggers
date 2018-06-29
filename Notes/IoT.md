## IoT Hub

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

### IoT Hub Message Transport Options

1. HTTPS polls with a 25 minutes lag only for occasionally connected devices.
2. MQTT over WebSockets (PORT 443) which is efficient with samll payloadsize and small overall footprint.
   MQTT allows immediate connection between hub and device. However, it does not allow rejection
   of messages that the device might not be able to process.
3. AMQP (Adavnced Message Queuing Protocol) which is better than HTTPS and allow immediate delivery.
   AMQP can accept and reject messages. 
   AMQP relies on WebSockets thus it uses (PORT 443).
---
## Azure IoT solution accelerators

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

## Azure Tools

1. [iothub-explorer](https://github.com/Azure/iothub-explorer)
2. [Service Bus Explorer](https://github.com/paolosalvatori/ServiceBusExplorer)


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

### Communication Types

1. [MOBUS]
2. [Fieldbus]
3. [CAN]
4. [Profibus]
5. [Ethernet]

### Scada Software

1. https://www.myscada.org/en/
2. AT-AUTOMATION Ignition SCADA https://www.at-automation.nl/en/scada-software-system/?gclid=CjwKCAjwsdfZBRAkEiwAh2z65vU2vy2eK9jXwEyOC_CRyU7V5U55TQ6mtlsVy8BUTU2m4zhCHx4xNxoCV_wQAvD_BwE
3. https://www.quora.com/What-is-the-best-open-source-SCADA-software
4. http://www.free-scada.org/

"HostName=psdemohub1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=UDr/oJdwKflSnYaM/mQu4FjHZWE2y7AzzRKwPp1a28s="
