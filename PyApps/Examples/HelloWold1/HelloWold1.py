"""
this is the docstring for this module
"""
# pylint: disable=line-too-long
#docstring
#https://stackoverflow.com/questions/7877522/how-do-i-disable-missing-docstring-warnings-at-a-file-level-in-pylint
#line-too-long
#https://pylint.readthedocs.io/en/latest/user_guide/message-control.html
# pylint: enable=line-too-long

print("Hello World!")

print(f"\r\n")

isTrue = True
if int(isTrue) == 1:
    print(f"isTrue={isTrue} => int(isTrue) == 1")
else:
    print("this does not print")

print(f"\r\n")

# any number other than zero is a trythy value
truthy = -1
if truthy:
    print(f"{truthy} is truthy")

print(f"\r\n")

falsy = 0
if not falsy:
    print(f"{falsy} is falsy")

emptylist = []
somelist1 = ["1", "2", "3"]

one = somelist1[0]
three = somelist1[-1]
two = somelist1[-2]

print(f"\r\n")

print(f"one={one} ; two={two}; three={three}")

for v in somelist1:
    v += "100"

one = somelist1[0]
three = somelist1[-1]
two = somelist1[-2]

print(f"\r\n")

print(f"one={one} ; two={two}; three={three}")

# pylint: disable=line-too-long
# https://stackoverflow.com/questions/522563/accessing-the-index-in-python-for-loops
# pylint: enable=line-too-long
for idx, val in enumerate(somelist1):
    print(f"val={val}@idx={idx}")
    somelist1[idx] += "100"

one = somelist1[0]
three = somelist1[-1]
two = somelist1[-2]

print(f"one={one} ; two={two}; three={three}")

somelist1.append("999")
if ("999" in somelist1):
    idx = somelist1.index("999")
    print(f"fount 999 at index = {idx}")

print(f"\r\n")

length = len(somelist1)
msg = f"the list contains {length} elements"
print(msg)

print(f"\r\n")

length = len(msg)
print(f'"{msg}" is {length} character long')

print(f"\r\n") 

# a list can contain mixed types
number = -300
somelist1.append(number)
for idx, val in enumerate(somelist1):
    print(f"val={val}@idx={idx}")

print(f"\r\n")

# None in Pythoon is the same as null
somelist1[len(somelist1)-1] = None
somelist1[len(somelist1)-2] = None
for idx, val in enumerate(somelist1):
    print(f"val={val}@idx={idx}")

print(f"\r\n")

# del - to delete an item from a list
del somelist1[len(somelist1)-1]
del somelist1[1]
for idx, val in enumerate(somelist1):
    print(f"val={val}@idx={idx}")
    
print(f"\r\n")

# slicing a list i:
somelist1 = ["1", "2", "3", "4", "5"]
tail = somelist1[1:]    #leave out teh first and take the rest
first2 = somelist1[2:]  #leave out the first two and take the rest
last = somelist1[-1:]   #take the last and leave the rest
last2 = somelist1[-2:]  #leave the last two and take the rest
core = somelist1[1:-1]  #leve the first take the rest leave the last
head = somelist1[:-1]   #leave the last take the rest

# use the "range" function in a for loop
for index in range(len(somelist1)):
    somelist1[index] += "0"

for idx, val in enumerate(somelist1):
    print(f"val={val}@idx={idx}")
    
print(f"\r\n")

# traverse the list and for each string in the list take out the 
# last char 
for index in range(0, len(somelist1)):
    somelist1[index] = somelist1[index][:-1]
    print(f"val={somelist1[index]}@index={index}")
