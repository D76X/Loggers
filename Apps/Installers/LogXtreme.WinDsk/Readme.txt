https://stackoverflow.com/questions/36190910/removing-a-parent-element-in-xml-while-keeping-its-children-using-xslt
https://stackoverflow.com/questions/8034798/wix-installer-using-xslt-with-heat-exe-to-update-attributes
https://stackoverflow.com/questions/8034798/wix-installer-using-xslt-with-heat-exe-to-update-attributes

heat.exe dir "." 
	 -cg DistFilesComponentGroup 
	 -gg -g1 -scom -sreg -sfrag -sdr 
         -dr DIR_LOGXTREME_WIN_DSK 
	 -t "..\RemoveDistFolderElementTransform.xslt"
         -out "..\DistFilesFragment.wxs"


heat.exe dir "." -cg DistFilesComponentGroup -gg -g1 -scom -sreg -sfrag -sdr -dr DIR_LOGXTREME_WIN_DSK -t "..\RemoveDistFolderElementTransform.xslt" -out "..\DistFilesFragment.wxs"