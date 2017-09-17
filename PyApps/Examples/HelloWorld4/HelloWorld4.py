"""
this is the docstring for this module
"""

# pylint: disable=invalid-name

# a simple function declaration
def printUser(user):
    print("user=")
    print(user["name"])
    print(user["age"])
    print(user["others"])  

# a function declaration with three arguments
# age is an optional argument
# *arg denotes a list of any number or arguments
def storeUser(name, age = 0, *args):
    user = {
        "name": name,
        "age": age,
        "others": args
        }
    return user

printUser(storeUser("Mark", 29, "Male", "student"))
print("\n")
printUser(storeUser("David"))

# function with keyed arguments **kwargs
# the **kwargs is composed into a dictionary
def createBook(title, **kwargs):
    book = {
        "title": title,
        "info": kwargs
        }
    return book

def printBook(book):
    print("book=")
    print(book["title"])
    print(book["info"])

print("\n")
printBook(createBook("Journey to the centre of the earth", author="Jules Verne", date=1864))


# pylint: enable=invalid-name
