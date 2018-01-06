from Package2.reader import Reader as Reader
# this imports the Reader class strainght into the REPL 
# so that it is available without prepending the 
# Package2.reader

# same as above
from Package2.module1 import *
# more selectively
from Package2.module2 import SomeCrazyClass as SomeCrazyClass
from Package2.module2 import create_crazy_stuff as create_crazy_stuff