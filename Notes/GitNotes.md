
## How to stach changes

https://git-scm.com/book/en/v1/Git-Tools-Stashing  
https://stackoverflow.com/questions/5737002/how-to-delete-a-stash-created-with-git-stash-create  

Imagine that you have been working on a branch for some time and you have a bunch of uncommitted changes for it that are important but not yet in a state where you can commit them. However, for some region you need to switch branch i.e. you need to fix an urgent bug on master while the changes are on feature1 branch. In these cases before switching from feature1 to master you may want to stash the local changes. It is a sort of shelveset.


| Command		            | Results			                                                                              |
| -------------             | ------------------------------------------------------------------------------------------------|
| ```git stash```	        | Create a stash with the uncommitted changes on the current branch and removes the local changes.| 
| stash & keep              | See below.                                                                                      |
| ```git stash list```	    | List all the stashes on the server.                                   |
| ```git stash apply```	    | Apply the most recent stash changes toi the current branch.           |
| ```git stash drop```	    | Removes the most recent stash.                                        |

## [Stash changes while keeping the changes in the working directory in Git](https://stackoverflow.com/questions/17843384/stash-changes-while-keeping-the-changes-in-the-working-directory-in-git)

This is basically the sequence ```git stash; git stash apply``` or the alternative below.

```
$ git add modified-file.txt
$ git stash save --keep-index
```
This two git commands are equivante to 

1. Stage the changes you want to stash
2. Stash with the --keep-index option which means all changes already added to the index are left intact.

***

## How to un-commit last un-pushed git commit without losing the changes

https://stackoverflow.com/questions/19859486/how-to-un-commit-last-un-pushed-git-commit-without-losing-the-changes  

```git reset head~1 --soft```

This is useful when by mistake all changes in the solution are staged and committed and you really intended only to select few files with changes to stage and commit. This command is run before the 
changes are synched or pushed to the repo and rusuls on the last committ being removed from the current branch history and all the changes of the last wrong commit are restored to teh solution.

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