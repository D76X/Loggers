#!/usr/bin/env python3
"""
This module illustrates how to design custom collection in Python.

Usage:

    # Copy and paste all these commands in the terminal to see the outputs.
    import os; os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3"); clear = lambda: os.system(
        'cls'); import imp; import test063_implementing_collections as t63; t63.test_module()

    # The last two commands are specific to this module.
    import test063_implementing_collections as t63
    t63.test_module()

    # Reload the module into the REPL after you make any changes to it.
    import imp
    imp.reload(t63)

    # clear th REPL
    clear = lambda: os.system('cls')
    clear()

    # set the working directory in the REPL
    import os
    os.chdir("C:\\GitHub\\Loggers\\PyApps\\Scripts\\Tests\\PythonScriptsTest3")
    os.getcwd()

Results:

"""

# ------------------------------
# The Proptocols of Collections
# ------------------------------

# Container             => str, list, range, tuple, set, bytes, dict
# Sized                 => str, list, range, tuple, set, bytes, dict
# Iterable              => str, list, range, tuple, set, bytes, dict
# Sequence              => str, list, range, tuple, bytes
# Set                   => set
# Mutable Sequence      => list
# Mutable Set           => set
# Mutable Mapping       => dict

# -------------------
# Container Protocol
# -------------------

# Allows to test for item membership in a collection => in/not in

# -------------------
# Sized Protocol
# -------------------

# Allows to determine the number of elements in a collection with the built-in function len()

# -------------------
# Iterable Protocol
# -------------------

# Allows to obtain an iterator via the built-in function iter()
# and use the (for item in iterable: dosomething(item)) idiom

# -------------------
# Sequence Protocol
# -------------------

# Allows randome read-write access to collections such as
# item = seq[index]
# index = seq.index(item)
# num = seq.count(item)
# r = reversed(seq)

# -------------------
# Set Protocol
# -------------------

# Allows to perform set algebraic operations on collections such as
# subset
# proper subset
# superste / proper superset
# intersection
# union
# difference
# symmetric difference
# equal / not equal