"""
This module illustrates the use of some of the most useful built-in functions in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system('cls'); import imp; import test053_datetime as t53; t53.test_module()

    # The last two commands are specific to this module.
    import test053_datetime as t53
    t53.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t53)

    # clear th REPL
    clear = lambda: os.system('cls')  
    clear() 

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

    datetime.date.today()=2018-03-13

    some useful instance methods
    date.year=2018, date.month=3, date.day=13
    today.weekday()=1
    today.isoweekday()=2
    today.isoformat()=2018-03-13

    use strftime
    d.strftime('%A %d %B %Y)=Tuesday 13 March 2018

    the output of format si OS dependent
    today = 'Tuesday 13 March 2018'

    better way, more pythonic way as it is explicit!
    Tuesday 13 March 2018

    create a date from a POSIX timestamp - the number of seconds from 01/01/1970
    datetime.date.fromtimestamp(1000000000)=2001-09-09

    create a date from a from 01/01/0001
datetime.date.fromordinal(720669)=1974-02-15

"""

import datetime

# datetime values are immutable

# in the module datetime theare are the following types
# -1 date with year month and day
# -2 time with hour minute secind microsecond
# -3 the composite datetime

# There exist two modes

# Naive mode - the types have no knowledge of time zone or daylight saving time
# date, time and datetime must use a program specific convention in order to be "located" with respect each other.

# Aware mode - the types have knowledge of time zone or daylight saving time
# date, time and datetime can be "located" with respect each other as this info is available on the type.
# the timezone type is used to represent information about date, time or datetime objects in Aware mode.

def test_module(): 
    """Module-level tests."""
print()
date1 = datetime.date(2014, 1, 6)
date2 = datetime.date(year=2015, month=2, day=7)
# datetime has a number of factory methods
today = datetime.date.today()
print("datetime.date.today()={}".format(today))

print()
print("some useful instance methods")
print("date.year={y}, date.month={m}, date.day={d}".format(y=today.year, m=today.month, d=today.day))
print("today.weekday()={}".format(today.weekday()))
print("today.isoweekday()={}".format(today.isoweekday()))
print("today.isoformat()={}".format(today.isoformat()))

# use strftime to format dates, times and datetimes
print()
print("use strftime")
print("d.strftime('%A %d %B %Y)={}".format(today.strftime('%A %d %B %Y')))

print()
print("the output of format si OS dependent")
print("today = {:'%A %d %B %Y'}".format(today))

print()
print("better way, more pythonic way as it is explicit!")
print("{date:%A} {date.day} {date:%B} {date.year}".format(date=today))

# create a date from a POSIX timestamp - the number of seconds from 01/01/1970
print()
print("create a date from a POSIX timestamp - the number of seconds from 01/01/1970")
posix_1_billion = datetime.date.fromtimestamp(1000000000)
print("datetime.date.fromtimestamp(1000000000)={}".format(posix_1_billion))

# create a date from 01/01/0001
print()
print("create a date from a from 01/01/0001")
days_from_01010001 = datetime.date.fromordinal(720669)
print("datetime.date.fromordinal(720669)={}".format(days_from_01010001))



