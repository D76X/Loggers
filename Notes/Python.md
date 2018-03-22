
# Python

## Some useful resources.

1. [Preparing for a Python Interview: 10 Things You Should Know](https://www.youtube.com/watch?v=DEwgZNC-KyE)  

2. [Corey Schafer Youtube channel on Python](https://www.youtube.com/channel/UCCezIgC97PvUuR4_gbFUs5g)

***

# Some commands to remember as they come often handy

## In the command shell or PowerShell.

| Command               | Meaning                                                                            | 
|-----------------------|------------------------------------------------------------------------------------|
| python --version      | Shows the version of python used by default on the machine. This is the            |
|                       | Python interpreter that appears first in the PATH system variable.                 | 
| python --help         | Shows all the options to use with teh python command.                              |
| python                | Invokes the system default Python interpreter starts a REPL in the current shell.  |
| python -i program.py  | starts a REPL in the current shell and execute program.py and anything  |
|                       | you define or import in the top level of program.py will be available.  |
|                       |                                                                         |

## In the REPL

In order to st

| Command               | Meaning                                                                 | 
|-----------------------|-------------------------------------------------------------------------|
| quit()                | To quit the REPL and go back to the shell.                              |
| help()                | Shows the help for the Python interpreter used in the REPL session.     |
| quit after help()     | To quit the help function.                                              |
| help("modules")       | Shows the the list of modules available to the Python interpreter used in the REPL sssion.|
| help("keywords")      | As above.|
| help("symbols")       | As above.|
| help("topics")        | As above.|

***

## How to run Python scripts in Visual Studio

Use the Python interactive and its dropdown to select the Environment that teh REPL should target. The type the import statement for the module to use in the REPL i.e. import t0001_SomeModule. Make sure that the name of teh file and thus of teh module starts with an alphabetic character i.e. 0001_SomeModule would not be a valid module name while t0001_SomeModule is.

***

## How to run Python scripts in VSCODE

[Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)  

## Open Python REPL in VSCODE

Use __CTLR+SHIFT+P__ type Python and find the option "start REPL".

## Excerpt from the VSCODE Python REPL

In the following it is shown how to check and change the working directory of the Python REPL whic is also repeated here [How do I "cd" in Python?](https://stackoverflow.com/questions/431684/how-do-i-cd-in-python)

Windows PowerShell
Copyright (C) Microsoft Corporation. All rights reserved.

PS C:\GitHub\Loggers\PyApps\Scripts\Tests> & python
Python 3.6.3 (v3.6.3:2c5fed8, Oct  3 2017, 18:11:49) [MSC v.1900 64 bit (AMD64)] on win32
Type "help", "copyright", "credits" or "license" for more information.

> import os
> os.getcwd()

'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests'

> os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
> os.getcdw()

> os.getcwd()

'C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3'

> import test043_reprlib as t
> t.test_module()

[Point2D(x=0, y=0), Point2D(x=0, y=1), Point2D(x=0, y=2), Point2D(x=0, y=3), Point2D(x=0, y=4), Point2D(x=0, y=5), ...]

***

## Clear the Python REPL

[How to clear the interpreter console?](https://stackoverflow.com/questions/517970/how-to-clear-the-interpreter-console)

> import os  
> clear = lambda: os.system('cls')  
> clear()

## Reload a module in the REPL after having made changes to it

https://stackoverflow.com/questions/684171/how-to-re-import-an-updated-package-while-in-python-interpreter

In the REPL for Python 3

>import imp  
>imp.reload(theModuleImportName)

****

## Python simple web server

Python 2 and 3 both come with a simple HTTP server embedded in it that can be useful for some types of development i.e. HTML5+JavaScript etc. I used it for development of games with Pahser.

[Phaser Getting Started - Part 2 - Installing a web server](https://phaser.io/tutorials/getting-started-phaser3/part2)  

With Python 3 in order to start the HTTP server use any of the following in a command shell or PowerShell window open at the root folder of the site whose files are to serve. In order to stop the HTTP server use **CTRL+C** in the same shell window.

``` 
python -m http.server
python -m http.server 7888 

```

In order to see the content of the site in your browser in the address type any URL like below. If you do not specify the port number the assumed default is 80 as customary.

```
http://localhost:7888
http://127.0.0.1:7888
```

***

# Anaconda for Machine Learning, AI and Data Science.

- Install Anaconda to get lots of useful packages bundled up in one sigle installer. This installer defaults to installng anaconda on a user basis thus in might end up into [C:\username\Anaconda](). You might want to have install it at system level instead so that it is [C:\Anaconda]() instead. I installed for all users and it is in [C:\ProgramData\Anaconda3]().

- In order to get VSCODE to work with Anaconda the best option is to install the **Anaconda Extension**. 

- It is necessary to sort out a few problems to work with Anaconda.  
  Refer to [Install Python on Windows (Anaconda)](https://medium.com/@GalarnykMichael/install-python-on-windows-anaconda-c63c7c3d1444) or [Install Python (Anaconda) on Windows + Setting Python and Conda Path (2017)](https://www.youtube.com/watch?v=dgjEUcccRwM&t=327s).

    1. From the windows start launch an instance of the Anaconda Prompt.   
    
    |Command              | Meaning                                                                              |
    |---------------------|-----------------------------------------------------------------------------------------|
    | python --version    | the version of the pYthon interprete with Anaconda i.e. Python 3.6.4 :: Anaconda, Inc.|
    | where python        | C:\ProgramData\Anaconda3\python.exe |
    | where conda         | C:\ProgramData\Anaconda3\Scripts\conda.exe |  

    </br>

    2. Add the paths to python.exe and conda.exe to the system PATH so that you can then use both in the cmd.
    3. In VCODE change the settings to ```"python.pythonPath": "C:/ProgramData/Anaconda3/python.exe"``` so that the default python interpreter with VSCODE is that provided by the Anaconda install.
    4. Use the CTRL+SHIFT+P in VSCODE and >Python: Select Interpreter to select which interpreter before starting a REPL shell.
    4. CTRL+SHIFT+P >Python: Start REPL to start a shell with the REPL with the selected Python interpreter.
    5. If the REPL does not start right away go to teh new shell opened by VSCODE in the tab below and invoke teh command python. This forces the selected Python interpreter to start a REPL in the shell.
    6. If conda.exe and the Anaconda python.exe are on the PATH then you should be able to use them in the cmd.

- To check the packages in the REPL got to CRTL+SHIP+P >Python: REPL then in the REPL help("modules") to list all the modules available in the selected venv. If it is Anaconda you should at least see Numpy.


## Some problems you might come accross

- [No module named numpy in VSCODE](https://stackoverflow.com/questions/40185437/no-module-named-numpy-visual-studio-code)  
The best option is to just install the Anaconda Extension for VSCODE.



***





