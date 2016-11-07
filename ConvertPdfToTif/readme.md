![File Formats Banner][1]

# Convert Fax Embedded in PDF to TIFF

Even today there are many organizations that rely on fax as a required form of communications. Because of this, receiving faxes is a requirement that much still be met.  To do this, many organizations utilize fax services either hosted locally or by a third-party.  Many of these services will email received faxes to users as a PDF file.

In a normal facsimile transmission, every other scan-line is skipped.  This reduces the amount of data that needs to be sent over the relatively slow connections used to send faxes. To account for the missing scan-lines, the aspect ratio of the pixels becomes 2:1. It is important to recognize this when displaying the image else, you will end up displaying the image incorrectly, making it looked squashed.

## Incorrectly Displayed Fax Image
The following image is displayed pixel for pixel from image to screen which assumes that the image's pixel aspect ratio (2:1) matches the screen's pixel aspect ratio (1:1). This results in an image that is displayed squashed with half the height it should have when displayed correctly.

![Display Fax Image As-Is][2]

In this example, we load a fax that is embedded in a PDF file and save it as a TIFF.  During the process, we ensure that the X and Y resolution of the resulting TIFF matches the that of the FAX in the PDF.

[1]: https://www.leadtools.com/images/new-site-images/banners/file-formats.jpg
[2]: https://www.leadtools.com/blog/wp-content/uploads/2016/11/swished-fax-img-screenshot.png
