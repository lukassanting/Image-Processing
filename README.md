# Image-Processing

Assignment for Image Processing subject. Implementation of basic image processing functions in C# for Windows Forms. The functions are implemented to accept to accept images of type Color[,], though not all of them are implemented to be applicable to three channel images. In this case, they either expect the image to be grayscale, or convert them to this, and return this.

Functions implemented:
 - Grayscale conversion (Colour --> Grayscale)
 - Inversion (Colour --> Colour)
 - Thresholding (Colour --> Binary)
 - (Modified) Contrast adjustment  (Colour --> Colour)
 - Linear (Gaussian and Box) filtering (Colour --> Grayscale)
 - Nonlinear (Median) filtering (Colour --> Grayscale)
 - Edge magnitude detection (Colour --> Grayscale)
 - Edge sharpening (Colour --> Grayscale)
 - Histogram equalization (Colour --> Colour)
