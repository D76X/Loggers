
## How to un-commit last un-pushed git commit without losing the changes

https://stackoverflow.com/questions/19859486/how-to-un-commit-last-un-pushed-git-commit-without-losing-the-changes  

```git reset head~1 --soft```

This is useful when by mistake all changes in the solution are staged and committed and you really intended only to select few files with changes to stage and commit. This command is run before the 
changes are synched or pushed to the repo and rusuls on the last committ being removed from the current branch history and all the changes of the last wrong committ are restored to teh solution.

In cases where the changes haved already been synched or pushed to the repo this op is still possible but si done with other git commands. 

***

## Eclude Folders with .gitignore 

https://stackoverflow.com/questions/30005759/visual-studio-2013-git-ignore-list-doesnt-ignore-dlls  

### Example

Firstly run the command

```git rm -rf C:\GitHub\Loggers\Examples\Installers-Examples\HelloWixTestApp1Bin```  

Then add the following to .gitignore  

```**/Temp/**/Examples/Installers-Examples/HelloWixTestApp1Bin/```

***