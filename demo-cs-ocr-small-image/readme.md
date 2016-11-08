![OCR Banner][1]

# Preprocess and OCR Small [Low Resolution] Images

Low-resolution images can come from a variety of sources, but the most common source is probably screen capture.  Screen capture images are usually 96 DPI on Windows. (This can vary depending on the user settings).  Additionally image representations of incoming faxes may also be considered low-res and fall under the resolution threshold acceptable for OCR.  Typically OCR engines require images of 200 or 300 DPI in order to achieve acceptable results.

## Change Image Resolution

One easy solution for "clean" images such as screen captures is to change the resolution of the image before OCRing the image.  This C# sample shows you how, then it gets the text from the image and outputs it to the console.

[1]: https://www.leadtools.com/images/new-site-images/banners/ocr.jpg

