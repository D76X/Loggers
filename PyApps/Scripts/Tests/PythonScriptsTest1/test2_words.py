"""
this is the docstring for this module
"""
from urllib.request import urlopen

# this is a refactoring of the previous example
# the function fetch_words produces the list
# the print_items prints the list

def fetch_words():
    """fetches some text from a test URL and stick it into a list word by word"""
    with urlopen('http://sixty-north.com/c/t.txt') as story:
        story_words = []
        for line in story:
            line_words = line.decode('utf-8').split()
            for word in line_words:
                story_words.append(word)
    return story_words

def print_items(story_words):
    """prints a list of strings to the console"""
    for word in story_words:
        print(word)

# if you want to test this module in the REPL as if it were 
# as script you can use this main() - it does not need to be
# named main()
def main():
    """this is the main funtion of this module"""
    words = fetch_words()
    print_items(words)

# make this module an executable script
if __name__ == '__main__':
    fetch_words()
