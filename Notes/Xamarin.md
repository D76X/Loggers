# Xamarin

## Emulators

The most important thing to understand is that you can have either

1. Visual Studio Emulator for Android
2. Google emulator

The former works on Hyper-V and the latter does not. However, google emulator
for Android (and also Genymotion) employs vistualization machines and the problem
is that Hyper-V does not allow any other vistualization machine to run on the 
PC. Therefore, if **Hyper-V** and **Windowns Hypervision Platform** are installed 
features and you want to run any of the google emulators you must uninstall these
two features from your system first.

To uninstall/install Hyper-V **Hyper-V** and **Windowns Hypervision Platform** use 
the feature from the start button as usal and the restart the system.

It is best to work with google emulator in Visual Studio than Visual Studio Emulator for Android
beacuse the latter only support old models and APIs.

1. [Visual Studio Emulator for Android](https://www.youtube.com/watch?v=skz6UE6udvM)  
2. https://www.genymotion.com/

### Visual Studio Emulator for Android 

1. [Good bye Visual Studio Emulator for Android and hello problems](https://blog.rthand.com/post/2017/05/02/good-bye-visual-studio-emulator-for-android-and-hello-problems.aspx)   
2. [Visual Studio 2017 Android Emulation](http://blog.tpcware.com/2017/03/visual-studio-2017-android-emulation/)  

### HAXM

1. [Intel® Hardware Accelerated Execution Manager (Intel® HAXM)](https://software.intel.com/en-us/articles/intel-hardware-accelerated-execution-manager-intel-haxm)  
Intel® Hardware Accelerated Execution Manager (Intel® HAXM) is a hardware-assisted virtualization engine (hypervisor) that uses Intel® Virtualization Technology (Intel® VT) to speed up Android* app emulation on a host machine. In combination with Android x86 emulator images provided by Intel and the official Android SDK Manager, Intel HAXM allows for faster Android emulation on Intel VT enabled systems.
The following platforms are supported by Intel HAXM:
Microsoft Windows*
Windows® 10 (32/64-bit), Windows* 8 and 8.1 (32/64-bit), Windows* 7 (32/64-bit)

2. [warning: quick boot/snapshots not supported on this machine](https://stackoverflow.com/questions/48759022/warning-quick-boot-snapshots-not-supported-on-this-machine)  