"""
this is the docstring for this module
"""

# pylint: disable=invalid-name

# dictionary - they look very much like json!
keyValues = {
    "key1": "value1",
    "key2": "value2",
    "key3": "value3",
    }

listOfKeyValues = [
    {"name": "One", "id": 1 },
    {"name": "Two", "id": 2 }
    ]

# key and values can be any type and mixed typed are allowed 
dict1 = {
    "id": 123,
    "location": None,
    "label": "?",
    "TODELETE": None
    }

# excrat values by key directly
temp = dict1["id"]
print(temp)
print(f"dict1[id]={temp}")

# excrat values by key with default
# this evoid exceptions
temp = dict1.get("label")
print(temp)
temp = dict1.get("wrongKey", "the key was not valid")
print(temp)

# get all the available keys
temp = dict1.keys()
for idx, val in enumerate(temp):
    print(f"val={val}@idx={idx}")

# delete a key-value pair by key
del dict1["TODELETE"]

# get all the vailable values
temp = dict1.values()
for idx, val in enumerate(temp):
    print(f"val={val}@idx={idx}")

# handle the exception key-error
# the key was deleted
try:
    does_exist = dict1["TODELETE"]
except KeyError:
    print("key-error exception has been caught")

# pylint: enable=invalid-name
