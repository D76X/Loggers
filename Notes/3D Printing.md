# 3D Printing

---

## Wanhao Duplicator 4

This printer is a clone of **MakerBot Replicator 2** which is the most important piece of 
information to keep in mind in order to successfully connet the printer to any piece of 
software. However, it is **not** a MakerBot printer as **WANHAO** is a **competitor** of
MakerBot. More information on this topics can be found [here](https://groups.google.com/forum/#!topic/wanhao-printer-3d/sjYp4XBoOvQ).

#### Buy WanHao Duplicator 4 (aprox. £600)

https://www.3dhubs.com/3d-printers/wanhao-duplicator-4  

#### The official WANHAO dedicated page 

This page provides detailed instructions in regard all sorts of upgrades, maintenace and fixes.

http://www.wanhao3dprinter.com/FAQ/ShowArticle.asp?ArticleID=23

#### Some specs.

The Wanhao Duplicator 4 is a well priced Chinese printer, which bears close resemblance 
to the Makerbot Replicator 2. 

- Build volume of 22.5 x 14.5 x 15 cm. 
- It and can print at a resolution of 100 microns 
- It can print in ABS, PLA and PVA.

### Complete spec.

- PRINTER TYPE FDM
- MATERIAL PLA
- BUILD VOLUME  22.5 x 14.5 x 15 cm
- MIN LAYER HEIGHT 100 microns
- MAX LAYER HEIGHT 500 microns
- EXTRUDER HEAD 2
- XY PRECISION 11 microns
- PRINTING SPEED 40 mm/s
- OPEN SOURCE Hardware & software
- CAN YOU USE 3RD PARTY MATERIAL? Yes
- HEATED PLATFORM Yes
- FILAMENT DIAMETER 1.75
- ON-PRINTER CONTROLS Yes
- CONNECTIVITY USB SD card

### Extra specs.

- [WanHao Printer 3d ›left excluder help please on a Wanhao Duplicator 4](https://groups.google.com/forum/#!topic/wanhao-printer-3d/y_vHtCbVxDk)  
  TOOLHEAD OFFSET 33 mm (I have measured it with a caliper and it checks out!)

---

## WANHAO Printer 3d Google Group

Lots of useful information can be found at the **WanHao Printer 3d** google group
below. In particular, there are all sorts of instruction about compatible firmware,
software and upgrades.

https://groups.google.com/forum/#!forum/wanhao-printer-3d  

---

## WANHAO Duplicator 4 motherboard

On of the first thing to know about your printer is what kind of hardware is used to
control it. 

#### D4 Motherboard -  mightyboard

https://www.wanhaouk.com/products/duplicator-4-motherboard  
https://groups.google.com/forum/#!topic/wanhao-printer-3d/sjYp4XBoOvQ  

---

## Wanhao Duplicator 4 firmware

One of the thing you might want to do is to upgrade the firmware of your printer.
The reasonyou might want to do that is that accompanying software that connects to
the printers is constantly updated and as new versions of the software are released
some older firmware might no longer be supported. This updates might causes the new
release of the softeare to no longer being able to communicate with or control the
printer. The other reason is that often new firmware includes essential updates and
bug fixes that improve the overall quality of the printed products. 

https://groups.google.com/forum/#!topic/wanhao-printer-3d/Wdue4vFxK-M

f you do want to run replicator or marlin, you will need to upgrade the control board to at least a RAMPS!

https://groups.google.com/forum/#!topic/wanhao-printer-3d/SX1YSiLvxY0


WanHao Printer 3d ›
UPDATED SAILFISH installation instructions
https://groups.google.com/forum/#!topic/wanhao-printer-3d/-0pryBjsd1g%5B51-75%5D


WanHao Printer 3d ›
Problem duplicator 4 firmware
https://groups.google.com/forum/#!topic/wanhao-printer-3d/sjYp4XBoOvQ

WanHao Printer 3d ›
Warning: do NOT upgrade to Sailfish if you have a new D4 motherboard!
https://groups.google.com/forum/#!topic/wanhao-printer-3d/qrpy8ztCmiw

## Chapter 6 - Installing Sailfish
http://www.sailfishfirmware.com/doc/install.html#x31-780006

WARNING!!!
YOU MUST ENSURE AFTER UPDATING FIRMWARE YOU FOLLOW THIS STEP!!! If you fail to both reset to factory defaults and validate that your toolhead offsets are both set to 0 you will crash the printer the first time you try to use the left extruder. Finally, do not forget to turn on extruder hold in onboard preferences(not explicitly mentioned in the manual since is this a Duplicator specific item)
http://www.sailfishfirmware.com/doc/install-configuring.html#x35-900006.4

### WANHAO Duplicator 4 PID Tuning

https://groups.google.com/forum/#!searchin/wanhao-printer-3d/duplicator$204|sort:date/wanhao-printer-3d/TDEniDPU67U/l6FHsXTXBAAJ  

---

## Setting up the printer

- [Simply and Accurately Calibrate your 3D Printer using Mattercontrol!](https://www.youtube.com/watch?v=D2pGtLSL9dc)


https://groups.google.com/forum/#!searchin/wanhao-printer-3d/duplicator$204$20MAKERBOT|sort:date/wanhao-printer-3d/cWpZ06PWpBI/5pqWVORMAwAJ

---

## Cura 3.3.1 Settings fot WANHAO Duplicator 4

- https://www.youtube.com/watch?v=DDXo2GBmbtU&t=25s
- limit @ 4 mm^3/s

### Settings for PLA on both extruders profile draft quality

1. volume (x, y, z) = 225 * 145 * 150 mm
2. nozzel diameter = 0.4 mm
3. copatible material diameter = 1.75 mm
4. layer heigt = 0.2 mm
5. Initial layer height = 0.3 mm (compensate for bumps)
6. initial layer line width = 125% (compensate for bumps)
5. wall thinkness = 0.8 mm
6. top/bottom thikness 0.8 mm
7. Infill density = 20%
8. print speed = 40 mm/s
9. wall speed = 20 mm/s
10. travel speed = 90 mm/s
11. Initial layer speed = 10 mm/s
14. top/bottom speed = 20 mm/s (down to 10 mm/s if does not stick!)
15. outer shell speed = 20 mm/s (improves looks)
16. innfill speed = 40 mm/s (?)


12. default printing temperature = 200 C
13. build plate temperatur = 60 C
13. retraction distance = 6.5 mm (2 mm?)
14. retraction speed = 25 mm/s
15. flow = 100% (increase or decrease to chnage the )

16. minimal layer time = 15 s (give unough time to cool the layer before teh next)