# Python

## Run Python scripts in VSCODE

[Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)  

## Open Python REPL in VSCODE

Use __CTLR+SHIFT+P__ type Python and find the option "start REPL".

## Excerpt from the VSCODE Python REPL

In the following it is shown how to check and change the working directory of the Python REPL whic is alos repeated here [How do I "cd" in Python?](https://stackoverflow.com/questions/431684/how-do-i-cd-in-python)

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