from urllib.request import urlopen

def fetch_words():
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
# the execution of a module is guaranteed to happen only once hence singletones may be instantiated by declaring them in modules
# when you run this in the cmd as a script that is >>python words.py you get __main__
# when you run this in the REPL by first loading the REPL with >>python and then loading the module words into it you get the printed word that is the name of the module 
##print(__name__)

# when a user tries to run this file as a python script at the cmd i.e. >>python words.py 
# then __main__ evaluates to the string '__main__' and this test can be used as in the code
# below so that the module is executed when run as a script while when run in the python REPL 
# it will be inpported but not executed.
if __name__ == '__main__':
    fetch_words()