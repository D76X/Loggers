"""
A reader to read files.

Usage: 
    Read files.
"""


class Reader:
    """"The Reader."""
    def __init__(self, filename):
        """
        The Reader initialisation.

        Args:
            filename: the name of the file to open.
        """
        self.filename = filename
        self.f = open(self.filename, 'rt')

    def close(self):
        """Close the file."""
        self.f.close()

    def read(self):
        """Read the file."""
        return self.f.read()

