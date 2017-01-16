![PDF Banner][1]

# PDF Merger

## Merge Two PDF Files

This sample console application demonstrates how to use the LEADTOOLS `PDFFile` class to merge two PDF files.
Specifically, this project will merge one file of odd pages with another file that
has the even pages.  The resulting PDF file will have pages from each file interlaced
with each other.

Files like these are created when using a scanner that has an automatic document feeder, but does
not support duplex scanning.  The user will scan the stack to get the front (odd numbered) pages into one file.
Then the user flips the stack of pages to get the back (even numbered pages) pages into the other file.


[1]: https://www.leadtools.com/images/new-site-images/banners/pdf.jpg