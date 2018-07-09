http://www.mobygis.com/wordpress/  

## Resources

1. [Esri Video](https://www.esri.com/videos)  
2. [ArcGIS for Developers](https://developers.arcgis.com/)  
3. [ArcGIS Online](https://www.arcgis.com/home/index.html)  
4. [Query (Feature Service)](https://developers.arcgis.com/rest/services-reference/query-feature-service-.htm)  

## Accounts

You must have a developer account in order to make use of some 
services i.e. ...

#### Developer Account
D76XD76X
http://davide-spano.maps.arcgis.com

## GIS [Geographic Information System]

1. [Introduction to GIS for Developers](https://app.pluralsight.com/player?course=gis-introduction-developers&author=jason-hine&name=gis-introduction-developers-m0&clip=0&mode=live)  

GIS is a tool to analyse GIS data and help to find solution
to complex problems.

### Examples

1. Optimization of the delivery path for a courier.
2. Modelling of environmental effects i.e. a spill of chemicals and evaluation of the effects.
3. Correlation between properties of objects/people and location.

### GIS Workflow

1. Get geopraphic data
2. Visualize **spacial patterns**
3. Analyse **spazial patterns**
4. Share GIS solutions

## GIS Servers

1. [ArcGIS Pro](https://www.esri.com/en-us/arcgis/products/arcgis-pro/overview)
2. [ArcGIS Enterprise in the Microsoft Azure Cloud](https://www.youtube.com/watch?v=fpg9kwyo-mI)
2. [Microsoft and Esri launch Geospatial AI on Azure](https://azure.microsoft.com/en-gb/blog/microsoft-and-esri-launch-geospatial-ai-on-azure/)  


## APIs and SDKs

0. [All the ArcGIS API](https://developers.arcgis.com/)
1. [ArcGIS API for JavaScript](https://developers.arcgis.com/javascript/3/)
2. [ArcGIS Runtime SDK for .NET](https://developers.arcgis.com/net/)

## 

1. [OpenStreetMap](https://www.openstreetmap.org/#map=6/54.910/-3.432)  
2. [ArcGIS Living Atlas of the World](https://livingatlas.arcgis.com/en/)    

---

## Good Base Map

When the BaseMap is selected follow these guidelines.

1. Contextually relevant.
2. Minimally distracting.
3. Aesthetically pleasing.

---

## Geocoding

Geocoding is the process of producing GIS data from some dataset.
For example, from a spreadsheet with location information including
addresses or place names to GIS data. The difference between the 
original information and GIS data is that the latter can be displayed
on a map. 

Geocoding is generally the first step in large GIS projects.

Geocoding relies on fast GIS servers with large, well maintaned street 
network datasets. When a query reaches the server the Geocoding services
uses a sophisticated algorithm to generate potential matches with ranking
values. It drops the low ranking matches scoring under a threshold and 
returns a meaningful subset of well form GIS data items.

#### Spatial References and Web Mercator

The Spacial Reference in a GIS data-itemn is the specification of the 
coordinate system used by the GIS data item. There exist lots of different
Spacial References but one of the most widespread used the **wkid** that is 
short for **ell known id** that is the **id** value of the so called
**web mercator**.

#### Geocoding services

1. One way to use a Geocoding service for free is to leverage the 
   Esri's ArcGIS platform. This platform can accept a common delimited
   text file with place names as its input and produce a GIS dataset.

### GIS standards

1. [GeoJSON](http://geojson.org/)  
2. CSV
3. Shapefile
4. Feature collection
5. FGDB  

#### Worlflow

1. Sign in to **ArcGIS for Developers**.
   You should laready have an account or create one i.e. using the G+.
2. Once you are looged in on ArcGIS for Developers also log in on
   **ArcGIS Online** and select **Map** to make a new map. This map 
   includes a BaseMap by default.
3. Now you can add your data to the map by clicking 
   **add >> add layer from file**. Make sure you select the right locality 
   from the data i.e. USA if teh file is a CSV containing data about places
   on the USA map. Different localities have different ways to name the 
   columns of the CSV **ArcGIS Online** does the best it can to match things
   up automatically, but user input can also be of aid to the process. 
4. At this point the data is displayed on the map as a Layer. There is a check
   box on the left side menu to enable and disable the visibility of teh layer 
   on the map and more options are also available. 
5. It is now possible to **save the Layer as a GIS service and make it available**
   **to the outside world for consumption.** One of the available options is 
   **Save Layer**, add a required tag and save.
6. From the **ArcGIS Online** frontpage now visit **Content**. You will find the 
   layer that has been saved as a resource, it is a **FeatureLayer**. This can 
   now be published as a resource to be consumed by other applications. Just click 
   on the layer and you will see a page with a URL to access the resource.
   It is necessary to make the resource **public** by sharing it!
7. This layer is now available for consumption as a **feature** that is   
   **geomerty(point, address, etc.) plus attributes(name, address, etc.)**.
   **ArcGIS Online** makes available this **features** through a **feauture service**
   and gives access to it via a **RESTfull API**.

---

## Operational Layer

Any type of map layer involved in GIS operations such as queries, spacial analysis,
edits, etc. For example editable GIS data drawn on top of a basemap constitute an 
Operational Layer. 

### What operational layer do I need on my map? 

1. Data that is meaningful and useful to the user
2. Consider how user would want to see and interact with that data on the map

---

## The Types of Services

1. Feature service.
   Returns feature data that is GIS data to the client.
   This is an advantage because clients that requests features can use 
   the data to perform all sort of client side logis i.e. query, edit, 
   etc.

2. Map Services. 
   Returns rendered images (not data). When there is a lot of data an 
   image requires much less bandwidth than it would require as GIS data
   plus the client saves the work to render the GIS data to display!
   
   * Cached Map Service. 
   In case of basemaps these service may also 
   be a cached service which returns image tiles that are prerendered 
   and cached by the server to provide optimal performance.

   * Dynamic Map Service (AKA Web Map Service)
   This is the opposite of a Cached Map Service as it just renders and 
   returns images upon request. The advantage of a Dynamic Cached Service
   is that there is an intermediate step between the query from the client
   and the production of the rendered image. 

---

### Example of operations on OLs

1. queries by attributes
2. search by attributes
3. count and measures also by attributes
4. selection also by attributes
5. when the OL supports it you may add, change, remove features...
   and save the changes back to teh cloud

---

## Rendering GIS Feature Data on Operational Layers

GIS data sets are made available to consumer by means of FeatureLayes on 
Feature services. You can easily drop all the data in a layer on a map 
and it would all be displayed as a series of undistintive red dots solely 
based on the geographical data of each data item in the data set.

This is the most basic way to include GIS data on a map. However, GIS data 
normally not only has geographical attributes but also **other attributes**
per each of the items in the dataset.

The **Renderers classes** of the APIs are normally equipped with ways to 
discriminate the rendered data item by setting condition on their attributes
so that the data items of a GIS data set in a FeatureLayer may be displayed 
with different treats according to the attributes they carry and 
**help to reveal spacial patterns** of the data on the base map.

There exist two basic classes of attribues for GIS items.

1. attributes that take values in a discrete finite range i.e. female/male, 
   registered/unregistered, speed limits 10, 20, 25,..., age in bands of 5
   from 5 to 150, etc.

2. attributes that take values in a continuos not finite range i.e. 
   speed of vehicles, local temperature, humidity, current cunsumption, etc. 

For each class of attributes normally APIs offer specialized classes to 
aid in the rendering process.

---

## Query for a Feature Service

We can query GIS data exposed as a Feature by a feature service. 
The resource below provides guidance on how to do that. 

1. https://app.pluralsight.com/player?course=gis-introduction-developers&author=jason-hine&name=gis-introduction-developers-m3&clip=3&mode=live
2. [Query (Feature Service)](https://developers.arcgis.com/rest/services-reference/query-feature-service-.htm)  

*Set SQL Format to standard to compose queries using SQL syntax.**
 
Here is an example of a query that pulls all the data in layer0, notice that 
the where clause uses 1=1 to accomplish this.

* [{"layerId" : 0, "where" : "1=1", "outFields" : "*"}]

This does the same but only returns the data items of age larger than 10.

* [{"layerId" : 0, "where" : "age>10", "outFields" : "*"}]

Perhaps you do not want to see all fields on each data item i.e. you are 
only interested to see the range of age of teh dataset. As a side note 
the "order by" might not work as it is not implemented by all data sources
(this is what esri says!)

* [{"layerId" : 0, "where" : "1=1", "outFields" : "Age"}]
* [{"layerId" : 0, "where" : "1=1", "outFields" : "Age", "order by": "Age"}]

---

## Spacial Analysis of GIS data

The analysis of **special relationships** between GIS features is called **spacial analysis**.
The ability to perform special analysis on various form of GIS datasets is what sets GIS apart
from other form of basic mapping applications.

The most common spacial analysis types 

1. Suitability Analysis

The process of determining areas appropriate for a particular use, based on geographic,
demographic or other kinds of criteria. The typical example is that of finding good 
locations for a new bike-sharing station in an urban area or to help determine the area 
that health workers should quarantine following the outbreak of a disease.

### High level workflow for Suitability Analysis

1. Stat the criteria according to which suitable locations are classified.
2. Produce a flowchart such as below to describe the steps taken in processing the datasets. 
* input => function => output => function => output => etc.
3. Develope the code or process corresponding to the **function** steps in the flowchart
   and apply the asynchronous programming pattern if these function are custom implementations
   is your application.
4. Sometimes there is not even need ti develop your own code for the functions and all can 
   be done on ESRI servers and tools i.e. MapView an then the layer with teh results can 
   simply be saved and published for clients to be consumed as it is.


2. Network Analysis

Analysis of the connected features to solve network problems such as **traversability**
**,rate of flow or capacity**. The typical example is to find all the areares around 
some given features of a GIS dataset that are reachable within x min of commute on 
road or public transports. Another is to find the fastest way to travel accross a number
of given locations. Aother kinds of questions can also be answered by means of Network
Analysis by combining datasets i.e. find the number of people that leave withing 1o mins
commute of an hostipal or other facilities.

All kinds of networks can be used in this kind of analysis

* road network
* gas/oil/water pipelines
* electrical grids
* pedestrian walkways

Network analysis requires lots of computational resources which are typically only available
on powerful cloud infrastructure. This is important becauses it influences how APIs to 
perform Network Analysis are designed. These typically send data back and forth between the
client and the server to have the heavy computational work done on the cloud. 
In the **Esri infrastructure** the **MapViewer** can be used to perform Network Analysis as it
offer all sorts of functions for it.

---

#### Run the examples with Python HTTP Server

Make sure some version of Python 3 is present on yopur system then in the 
command window or PowerShell at the root folder of the project with the 
html, css and js files use the following commands.

1. [How do you set up a local testing server?](https://developer.mozilla.org/en-US/docs/Learn/Common_questions/set_up_a_local_testing_server)  

*  python -m http.server  
*  python -m http.server 7800