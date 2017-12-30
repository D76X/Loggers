"""
The docstring for this module.

Usage:
    Examples.
"""
from urllib.request import urlopen

# the def keyword is technically not the definition of the function
# it is actually a statement that is interpreted by python to bind
# the code within the function to the symbol that is the name of the
# function.
def fetch_words():
    """Fetches the words"""
    with urlopen('http://sixty-north.com/c/t.txt') as story:
        story_words = []
        for line in story:
            line_words = line.decode('utf-8').split()
            for word in line_words:
                story_words.append(word)

    for word in story_words:
        print(word)

# __name__ evaluates to the the module name
# the name of the file is the name of the module
# the execution of a module is guaranteed to happen only once hence singletones may be
# instantiated by declaring them in modules
# when you run this in the cmd as a script that is >>python words.py you get __main__
# when you run this in the REPL by first loading the REPL with >>python and then loading
# the module words into it you get the printed word that is the name of the module
##print(__name__)

#------------------------------------------------------------------------------------------
# when a user tries to run this file as a python script at the cmd i.e. >>python words.py
# then __main__ evaluates to the string '__main__' and this test can be used as in the code
# below so that the module is executed when run as a script while when run in the python REPL
# it will be inpported but not executed.
if __name__ == '__main__':
    fetch_words()
#-----------------------------------------------------------------------------------------
# Python Module vs Python Script
# Any .py file constitutes a Python module.
# Any .py module maybe turned into an executable script as shown above.
# A .py is a script if when it is launched at the command line it executes.
# If a .py file is not made into an executable script it will be simply imported into the
# Python REPL when invoked at the command line.
# Modules can be written for
#1-convenient inport
#2-conveninet execution
#3-or using the technique shown above that makes use of the idiom __main__ as both!
