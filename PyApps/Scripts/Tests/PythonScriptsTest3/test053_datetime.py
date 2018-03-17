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

#----------------------------------------------------------------------------------------------------
# do not do this!
# from datetime import datetime

# The reason is that dattetime.datetime in Python is the class datetime in the module datetime. 
# Such class is a composite type of time+date all packaged in one class. If you import the class
# datetime.datetime as above the symbol datetime remains bound to the class datetime.datetime and 
# as a consequence you lose access to thw module level methods available on the module object datetime
# as this symbol is now bound to the class datetime.datetime. 

# these two are two good alternatives now dt and Datetime are symbols bound to the class
# datetime.datetime. In production you should choose either but not both or use the fully qualified 
# access to teh class name as datetime.datetime.

#from datetime import datetime as dt
from datetime import datetime as Datetime
#----------------------------------------------------------------------------------------------------

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
    print("datetime.date(2014, 1, 6)={}".format(date1))
    print("datetime.date(year=2015, month=2, day=7)={}".format(date2))

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

    # the time class is used to represent time without date information
    # time = hour minutes seconds microseconds
    print()
    time1 = datetime.time(3)
    print("datetime.time(3)={}".format(time1))
    time2 = datetime.time(3, 1)
    print("datetime.time(3, 1)={}".format(time2))
    time3 = datetime.time(3, 1 , 2)
    print("datetime.time(3, 1, 2)={}".format(time3))
    time4 = datetime.time(3, 1 , 2, 232)
    print("datetime.time(3, 1, 2, 232)={}".format(time4))

    print()
    print("the last representable instant of any date")
    last_representable_instant_of_any_date = datetime.time(hour=23, minute=59, second=59, microsecond=999999)
    print("datetime.time(hour=23, minute=59, second=59, microsecond=999999)={}".format(last_representable_instant_of_any_date))

    print()
    t = last_representable_instant_of_any_date
    print("better way, more pythonic way as it is explicit!")
    print("t.hour = {h}, t.minute={m}, t.second={s}, ut.microsecond={us}".format(h=t.hour, m=t.minute, s=t.second, us=t.microsecond))

    # ISO 8602 representation of time and dates is 1 based as oposed to 0 based
    print()
    print("ISO 8602 representation of time and dates is 1 based as oposed to 0 based.")
    print("t.isoformat()={}".format(t.isoformat()))
    
    # with time and dates avoid using strftime because its output depends on how the machine is configured
    print()
    print("with time and dates avoid using strftime because its output depends on how the machine is configured")
    print("t.strftime('%Hh%Mm%Ss')={}".format(t.strftime('%Hh%Mm%Ss')))

    print()    
    print("better way, more pythonic way as it is explicit!")
    print("{t.hour}h{t.minute}m{t.second}s{t.microsecond}us".format(t=t))

    # min max and resolution of time
    print()
    print("min max and resolution of time")
    print("datetime.time.min={}".format(datetime.time.min))
    print("datetime.time.max={}".format(datetime.time.max))
    print("datetime.time.resolution={}".format(datetime.time.resolution))

    # here we show how to use the class datetime.datetime
    # we can aslo use the aliases dt or Datetime as descrimbed above when the import declarations for this
    # module are given.

    print()
    print("create a datetime.datetime instance using teh fully qualified class name.")
    datettime1 = datetime.datetime(2003, 5, 12, 14, 33, 22, 245323)
    print("datetime.datetime(2003, 5, 12, 14, 33, 22, 245323)={}".format(datettime1))
    print()
    datettime2 = Datetime(2004, 6, 11, 12, 31, 20, 245322)
    print("Datetime(2004, 6, 11, 12, 31, 20, 245322)={}".format(datettime2))
    print()
    datettime3 = Datetime(2005, 5, 10, 12, 30, 29, 123456)
    print("Datetime(2005, 5, 10, 12, 30, 29, 123456)={}".format(datettime3))

    # today and now according to local time (machine time)
    print()
    print("today and now according to local time (machine time)")
    today = datetime.datetime.today()
    print("today = datetime.datetime.today()={}".format(today))

    # now
    print()
    now = datetime.datetime.now()
    print("datetime.datetime.now()={}".format(now))
    
    # UTC now takes into account the locale
    print()
    print("UTC now takes into account the locale")
    print("this is a naive datetime object")
    print("A naive object does not contain enough information to unambiguously locate itself relative to other date/time objects.")
    print("datetime.datetime.utcnow()={}".format(datetime.datetime.utcnow()))

    # others
    print()
    print("this are all naive datetime objects")
    print("datetime.datetime.fromordinal(5)={}".format(datetime.datetime.fromordinal(5)))
    print("datetime.datetime.fromtimestamp(456789)={}".format(datetime.datetime.fromtimestamp(456789)))
    print("datetime.datetime.utcfromtimestamp(456789)={}".format(datetime.datetime.utcfromtimestamp(456789)))

    # combining datetime instances
    print()
    print("make up the datetime 8:15 am today from time+date...")
    td = datetime.date.today()
    t = datetime.time(8, 15)
    tdt = datetime.datetime.combine(td, t)
    print("datetime.date.today()={td} => datetime.time(8, 15)={t} => datetime.datetime.combine(td, t)={c}".format(td=td, t=t, c=tdt))

    # create datetime objects from formatted string
    print()
    print("create datetime objects from formatted string with strptime")
    dt = datetime.datetime.strptime("Monday 6 January 2014, 12:13:31", "%A %d %B %Y, %H:%M:%S")
    print("datetime.datetime.strptime(""Monday 6 January 2014, 12:13:31"", ""%A %d %B %Y, %H:%M:%S"")={}".format(dt))

    # splitting up datetime objects
    print()
    print("splitting up datetime objects")
    now = datetime.datetime.now()
    print("datetime.datetime.now().date()={}".format(now.date()))
    print("datetime.datetime.now().time()={}".format(now.time()))
    print("datetime.datetime.now().day={}".format(today.day))
    print("datetime.datetime.now().month={}".format(today.month))
    print("datetime.datetime.now().isoformat={}".format(today.isoformat()))
