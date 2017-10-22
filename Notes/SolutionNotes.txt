
======================================================================================================

REBUILD NUGET PACKAGES IN A PROJECT

A quick tutorial on the Update-Package command
http://blog.nuget.org/20121231/a-quick-tutorial-on-update-package-command.html

Example PM> Update-Package -ProjectName ModuleA -Reinstall

More on this subject 

https://docs.microsoft.com/en-us/nuget/consume-packages/package-restore

======================================================================================================
CLEAR CACHE WHEN TEST EXPLORER DO NOT SHOW TESTS
https://stackoverflow.com/questions/35103781/why-is-the-visual-studio-2015-2017-test-runner-not-discovering-my-xunit-v2-tests

In PowerShell
del $env:TEMP\VisualStudioTestExplorerExtensions

======================================================================================================