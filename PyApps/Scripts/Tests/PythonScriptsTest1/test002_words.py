#!/usr/bin/env python3
"""
This is the docstring for this module.

Usage:
    its is used to showcase and test some basic aspect of Python and its
    toolchain.
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

def print_items(items):
    """
    prints a list of strings to the console.
    Args:
        items: the collection of items to print.

    Returns:
        nothing.
    """
    for item in items:
        print(item)

# if you want to test this module in the REPL as if it were
# as script you can use this main() - it does not need to be
# named main()
def main():
    """this is the main funtion of this module"""
    words = fetch_words()
    print_items(words)

# make this module an executable script
if __name__ == '__main__':
    main()
