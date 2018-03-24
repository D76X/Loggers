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

    datetime.date(2014, 1, 6)=2014-01-06
    datetime.date(year=2015, month=2, day=7)=2015-02-07
    datetime.date.today()=2018-03-24

    some useful instance methods
    date.year=2018, date.month=3, date.day=24
    today.weekday()=5
    today.isoweekday()=6
    today.isoformat()=2018-03-24

    use strftime
    d.strftime('%A %d %B %Y)=Saturday 24 March 2018

    the output of format si OS dependent
    today = 'Saturday 24 March 2018'

    better way, more pythonic way as it is explicit!
    Saturday 24 March 2018

    create a date from a POSIX timestamp - the number of seconds from 01/01/1970
    datetime.date.fromtimestamp(1000000000)=2001-09-09

    create a date from a from 01/01/0001
    datetime.date.fromordinal(720669)=1974-02-15

    datetime.time(3)=03:00:00
    datetime.time(3, 1)=03:01:00
    datetime.time(3, 1, 2)=03:01:02
    datetime.time(3, 1, 2, 232)=03:01:02.000232

    the last representable instant of any date
    datetime.time(hour=23, minute=59, second=59, microsecond=999999)=23:59:59.999999

    better way, more pythonic way as it is explicit!
    t.hour = 23, t.minute=59, t.second=59, ut.microsecond=999999

    ISO 8602 representation of time and dates is 1 based as oposed to 0 based.
    t.isoformat()=23:59:59.999999

    with time and dates avoid using strftime because its output depends on how the machine is configured
    t.strftime('%Hh%Mm%Ss')=23h59m59s

    better way, more pythonic way as it is explicit!
    23h59m59s999999us

    min max and resolution of time
    datetime.time.min=00:00:00
    datetime.time.max=23:59:59.999999
    datetime.time.resolution=0:00:00.000001

    create a datetime.datetime instance using teh fully qualified class name.
    datetime.datetime(2003, 5, 12, 14, 33, 22, 245323)=2003-05-12 14:33:22.245323

    Datetime(2004, 6, 11, 12, 31, 20, 245322)=2004-06-11 12:31:20.245322

    Datetime(2005, 5, 10, 12, 30, 29, 123456)=2005-05-10 12:30:29.123456

    today and now according to local time (machine time)
    today = datetime.datetime.today()=2018-03-24 16:39:42.503350

    datetime.datetime.now()=2018-03-24 16:39:42.504350

    UTC now takes into account the locale
    this is a naive datetime object
    A naive object does not contain enough information to unambiguously locate itself relative to other date/time objects.
    datetime.datetime.utcnow()=2018-03-24 16:39:42.505351

    this are all naive datetime objects
    datetime.datetime.fromordinal(5)=0001-01-05 00:00:00
    datetime.datetime.fromtimestamp(456789)=1970-01-06 06:53:09
    datetime.datetime.utcfromtimestamp(456789)=1970-01-06 06:53:09

    make up the datetime 8:15 am today from time+date...
    datetime.date.today()=2018-03-24 => datetime.time(8, 15)=08:15:00 => datetime.datetime.combine(td, t)=2018-03-24 08:15:00

    create datetime objects from formatted string with strptime
    datetime.datetime.strptime(Monday 6 January 2014, 12:13:31, %A %d %B %Y, %H:%M:%S)=2014-01-06 12:13:31

    splitting up datetime objects
    datetime.datetime.now().date()=2018-03-24
    datetime.datetime.now().time()=16:39:42.520351
    datetime.datetime.now().day=24
    datetime.datetime.now().month=3
    datetime.datetime.now().isoformat=2018-03-24T16:39:42.503350

    time deltas thus duration in Python are handled by the class datetime.timedelta
    you should always use positional arguments when constructing instances of datetim.timedelta for readibility!
    datetime.timedelta(weeks=1, days=1, hours=1, minutes=1, seconds=1, milliseconds=1, microseconds=1000)=8 days, 1:01:01.002000
    any datetime.timedelta is normalised to => days, hours:minutes:seconds.microseconds
    datetime.timedelta internally stores only days, seconds, microseconds
    8 + 3661:2000

    pretty representation of datetime.timedelta can be obtained wit str()
    str(td) = 8 days, 1:01:01.002000

    arithmetics with datetime and timedeltas...
    subtractions of dates result in datetime.timedelta instances
    d1=2014-05-08 14:22:00
    d2=2014-03-14 12:09:00
    d1-d2=55 days, 2:13:00

    find the date 3 weeks after d1=2014-05-08 14:22:00
    d1 + datetime.timedelta(weeks=3)=2014-05-29 14:22:00
    beware that arithmetic on datetime.time objects is not supported and results in TypeError
    it is required to use datetime.timedelta instead!

    Time Zones!
    in Python datetime.datetime are Naive object that is not-aware of time zones!
    it is possible to turn a datetime.datetime object into a zone-aware datetime object by attaching a tzinfo object to the datetime.datetime instance
    for accurate support of zone-aware datelight savings refer to the mosules : pytz , dateutils
    Python3 ahs better support for time zone specification than Python 2
    The tzinfo class is abstract cannot be instantiated directly
    Python3 includes a concrete implementation of tzinfo => timezone
    Python3 timezone can be used to represent time zone info as fixed offset from UTC

    Example: the CTE timezone is the UTC+1 zone where CTE means Central European Time.
    Great Britain sits in UTC+0 while Italy and Norway are in CTE that is UTC+1
    the datetime.timezone constructor can be used to create instances of datetime.timezone which implement tzinfo
    datetime.timezone(datetime.timedelta(hours=1), CET)=CET
    here we have constructed a zone-aware CTE object which is of type datetime.timezone which implements tzinfo
    instances of datetime.timezone can be used to turn naive (zone-unaware) datetime.datetime object into zone-aware datetime.datetime objects
    departure = datetime.datetime(year=2014, month=1, day=7, hour=11, minute=30, tzinfo=cte)=2014-01-07 11:30:00+01:00
    notice the +01:00 which means that the dateime.datetime instance is zone-aware and refers to UTC+1 that is CTE!
    arrival = departure+datetime.timedelta(hours=1)=2014-01-07 12:30:00+01:00
    notice that arrival remains expressed as UTC+1 datetime.datetime instance
    another way to specify UTC timezone is to use datetime.timezone.utcoffset
    meeting = datetime.datetime(year=2014, month=1, day=7, hour=13, minute=5, tzinfo=datetime.timezone.utc) =2014-01-07 13:05:00+00:00
    the tzinfo=datetime.timezone.utc causes the 'meeting' date to be UTC and infact it prints as +00:00 that is London Time!
    use pytz or python.dateutils for anything of real-worlkd complexity that mus account for daylight savings as well.

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

    # datetime has a number of factory methodspy
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

    # time deltas thus duration in Python are handled by the class datetime.timedelta
    print()
    print("time deltas thus duration in Python are handled by the class datetime.timedelta")
    print("you should always use positional arguments when constructing instances of datetim.timedelta for readibility!")
    td = datetime.timedelta(weeks=1, days=1, hours=1, minutes=1, seconds=1, milliseconds=1, microseconds=1000)
    print("datetime.timedelta(weeks=1, days=1, hours=1, minutes=1, seconds=1, milliseconds=1, microseconds=1000)={}".format(td))    
    print("any datetime.timedelta is normalised to => days, hours:minutes:seconds.microseconds")
    print("datetime.timedelta internally stores only days, seconds, microseconds")
    print("{} + {}:{}".format(td.days, td.seconds, td.microseconds))
    print()
    print("pretty representation of datetime.timedelta can be obtained wit str()")
    print("str(td) = {}".format(str(td)))

    # arithmetics with datetime and timedeltas
    print()
    print("arithmetics with datetime and timedeltas...")
    print("subtractions of dates result in datetime.timedelta instances")
    d1 = datetime.datetime(year=2014, month=5, day=8, hour=14, minute=22)
    d2 = datetime.datetime(year=2014, month=3, day=14, hour=12, minute=9)
    delta = d1-d2
    print("d1={}".format(d1))
    print("d2={}".format(d2))
    print("d1-d2={}".format(delta))

    # use datetime.timedelta and dates to find dates in the past or teh future with 
    # respect a reference date
    print()
    print("find the date 3 weeks after d1={}".format(d1))
    d1_plus_3weeks = d1 + datetime.timedelta(weeks=3)
    print("d1 + datetime.timedelta(weeks=3)={}".format(d1_plus_3weeks))
    print("beware that arithmetic on datetime.time objects is not supported and results in TypeError")
    print("it is required to use datetime.timedelta instead!")

    # Time Zones and tzinfo + third-party pytz and dateutil modules!    
    print()
    print("Time Zones!")
    print("in Python datetime.datetime are Naive object that is not-aware of time zones!")
    print("it is possible to turn a datetime.datetime object into a zone-aware datetime object by attaching a tzinfo object to the datetime.datetime instance")
    print("for accurate support of zone-aware datelight savings refer to the mosules : pytz , dateutils")
    print("Python3 ahs better support for time zone specification than Python 2")
    print("The tzinfo class is abstract cannot be instantiated directly")
    print("Python3 includes a concrete implementation of tzinfo => timezone")
    print("Python3 timezone can be used to represent time zone info as fixed offset from UTC")

    print()
    print("Example: the CTE timezone is the UTC+1 zone where CTE means Central European Time.")
    print("Great Britain sits in UTC+0 while Italy and Norway are in CTE that is UTC+1")
    print("the datetime.timezone constructor can be used to create instances of datetime.timezone which implement tzinfo")
    cte = datetime.timezone(datetime.timedelta(hours=1), "CET")
    print("datetime.timezone(datetime.timedelta(hours=1), "'CET'")={}".format(cte))
    print("here we have constructed a zone-aware CTE object which is of type datetime.timezone which implements tzinfo")
    print("instances of datetime.timezone can be used to turn naive (zone-unaware) datetime.datetime object into zone-aware datetime.datetime objects")
    departure = datetime.datetime(year=2014, month=1, day=7, hour=11, minute=30, tzinfo=cte)
    print("departure = datetime.datetime(year=2014, month=1, day=7, hour=11, minute=30, tzinfo=cte)={}".format(departure))
    print("notice the +01:00 which means that the dateime.datetime instance is zone-aware and refers to UTC+1 that is CTE!")
    arrival = departure+datetime.timedelta(hours=1)
    print("arrival = departure+datetime.timedelta(hours=1)={}".format(arrival))
    print("notice that arrival remains expressed as UTC+1 datetime.datetime instance")
    print("another way to specify UTC timezone is to use datetime.timezone.utcoffset")
    meeting = datetime.datetime(year=2014, month=1, day=7, hour=13, minute=5, tzinfo=datetime.timezone.utc)
    print("meeting = datetime.datetime(year=2014, month=1, day=7, hour=13, minute=5, tzinfo=datetime.timezone.utc) ={}".format(meeting))
    print("the tzinfo=datetime.timezone.utc causes the 'meeting' date to be UTC and infact it prints as +00:00 that is London Time!")
    print("use pytz or python.dateutils for anything of real-worlkd complexity that mus account for daylight savings as well.")
