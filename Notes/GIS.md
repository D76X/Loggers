http://www.mobygis.com/wordpress/  

## Resources

1. [Esri Video](https://www.esri.com/videos)  
2. [ArcGIS for Developers](https://developers.arcgis.com/)  
3. [ArcGIS Online](https://www.arcgis.com/home/index.html)    

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
#### Run the examples with Python HTTP Server

Make sure some version of Python 3 is present on yopur system then in the 
command window or PowerShell at the root folder of the project with the 
html, css and js files use the following commands.

1. [How do you set up a local testing server?](https://developer.mozilla.org/en-US/docs/Learn/Common_questions/set_up_a_local_testing_server)  

*  python -m http.server  
*  python -m http.server 7800