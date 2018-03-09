"""
This module illustrates the use of the Python built-in functions
ascii(), ord(), chr().

    Usage:

    import test044_asciii_ord_chr as t
    t.test_module()  

    with ascii() the Unicode string Gruß => becomes the ASCII string 'Gru\xdf'
    with ord() the Unicode char ø => becomes the Unicode integer codepoint 248
    with chr() the Unicode integer code 248 => becomes the Unicode single char string ø

"""

def test_module():   
    """Module-level tests."""
    # ascii() converts all the non ASCII characters in a string into
    # escaped sequences and returns a unicode string. This is useful
    # in several cases i.e. when data must be serialized into ASCII
    # codes or when you want ot communicate encodong information with
    # no loss of Unicode data.
    hello = "Gruß"
    ascii_hello = ascii(hello)
    print("with ascii() the Unicode string {} => becomes the ASCII string {}".format(hello, ascii_hello))

    # ord() converts a single character to its integer Unicode codepoint
    phi = "ø"
    ord_phi = ord(phi)
    print("with ord() the Unicode char {} => becomes the Unicode integer codepoint {}".format(phi, ord_phi))

    # chr() is the inverse operation with respect ord()
    print("with chr() the Unicode integer code {} => becomes the Unicode single char string {}".format(ord_phi, chr(ord_phi)))

