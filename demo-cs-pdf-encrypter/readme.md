![PDF Banner][banner]

# PDF Encrypter

---------------------------

**_Warning!_**

This will encrypt PDF files with RC 128 bit encryption using the password you specify.  If you forget the password, then you lose your PDF file. 

**Do not forget the password.**

---------------------------

This sample application demonstrates how to use the LEADTOOLS `PDFFile` class to add a user password to a PDF file.  

To register this project into Windows Explorer as a context menu item for PDF files, run it with the `-register` parameter.  To unregister, run with the `-unregister` parameter.

The full path and file name is passed to the application as a parameter. The code checks the file to see if it is already encrypted.  If it is, the user is prompted for the current password.  Then the user is prompted for a new password.  If the new password is blank, then the resulting file will not be encrypted, removing the previous password if there was one. 



[banner]: https://www.leadtools.com/images/new-site-images/banners/pdf.jpg