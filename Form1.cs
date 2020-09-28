using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace INFOIBV
{
    public partial class INFOIBV : Form
    {
        private Bitmap InputImage;
        private Bitmap SecondImage;
        private Bitmap OutputImage;

        public INFOIBV()
        {
            InitializeComponent();
        }

        /*
         * loadButton_Click: process when user clicks "Load" button
         */
        private void loadImageButton_Click(object sender, EventArgs e)
        {
           if (openImageDialog.ShowDialog() == DialogResult.OK)             // open file dialog
            {
                string file = openImageDialog.FileName;                     // get the file name
                imageFileName.Text = file;                                  // show file name
                if (InputImage != null) InputImage.Dispose();               // reset image
                InputImage = new Bitmap(file);                              // create new Bitmap from file
                if (InputImage.Size.Height <= 0 || InputImage.Size.Width <= 0 ||
                    InputImage.Size.Height > 512 || InputImage.Size.Width > 512) // dimension check (may be removed or altered)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox1.Image = (Image) InputImage;                 // display input image

            }
        }

        /*
         * loadSecondImage_Click: process when user clicks "Load second" button
         */
        private void loadSecondImage_Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)             // open file dialog
            {
                string file = openImageDialog.FileName;                     // get the file name
                imageFileName.Text = file;                                  // show file name
                if (SecondImage != null) SecondImage.Dispose();               // reset image
                SecondImage = new Bitmap(file);                              // create new Bitmap from file
                if (SecondImage.Size.Height <= 0 || SecondImage.Size.Width <= 0 ||
                    SecondImage.Size.Height > 512 || SecondImage.Size.Width > 512) // dimension check (may be removed or altered)
                    MessageBox.Show("Error in image dimensions (have to be > 0 and <= 512)");
                else
                    pictureBox2.Image = (Image)SecondImage;                 // display input image

            }
        }


        /*
         * applyButton_Click: process when user clicks "Apply" button
         */
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (InputImage == null) return;                                 // get out if no input image
            if (functionSelector.Text == "Choose Function")                 // Ensure a function is chosen
            {
                MessageBox.Show("Please select a function to apply to the image.");
                return;
            }
            if (OutputImage != null) OutputImage.Dispose();                 // reset output image

            OutputImage = new Bitmap(InputImage.Size.Width, InputImage.Size.Height); // create new output image
            Color[,] Image = new Color[InputImage.Size.Width, InputImage.Size.Height]; // create array to speed-up operations (Bitmap functions are very slow)

            if (!correctColours()) MessageBox.Show("Please using valid mixing coefficients. Using default values.");

            // copy input Bitmap to array            
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
                    Image[x, y] = InputImage.GetPixel(x, y);                // set pixel color in array at (x,y)

            // implement second image, used in AND and OR operations
            Color[,] Image2 = Image;
            if (SecondImage != null)
            {
                Image2 = new Color[SecondImage.Size.Width, SecondImage.Size.Height];

                // copy input Bitmap to array            
                for (int x = 0; x < SecondImage.Size.Width; x++)                 // loop over columns
                    for (int y = 0; y < SecondImage.Size.Height; y++)            // loop over rows
                        Image2[x, y] = SecondImage.GetPixel(x, y);                // set pixel color in array at (x,y)
            }

            // setup progress bar
            progressBar.Visible = true;
            progressBar.Minimum = 1;
            progressBar.Maximum = InputImage.Size.Width * InputImage.Size.Height;
            progressBar.Value = 1;
            progressBar.Step = 1;

            // ====================================================================
            // =================== YOUR FUNCTION CALLS GO HERE ====================
            // Alternatively you can create buttons to invoke certain functionality
            // ====================================================================

            string imageType = typeCheck(Image); // return whether image is colour, grayscale or binary
            string image2Type = "null";
            if (SecondImage != null) image2Type = typeCheck(Image2);

            imageTypeLabel.Text = "Image type = " + imageType;
            imageTypeLabel.Update();

            Color[,] workingImage = Image;

            // Call functions by checking which item in the functionSelector ComboBox is selected, then call that function

            //make correct (input) histogram for image
            int[] inHisto;
            if (imageType == "colour")
                inHisto = computeColourHistogram(workingImage);
            else
                inHisto = computeHistogram(workingImage);
            int[] cumulativeHisto = cumulativeHistogram(inHisto); // compute the cumulative histogram for use in different functions

            //if new image is loaded after histogram equalization, reset the histogram labels
            inputHistogramBox.Text = "Input Histogram Data";
            inputHistogramBox.Update();
            printInputHistogram(inHisto);

            if (functionSelector.SelectedIndex == 0)
            {
                if (imageType == "colour")
                {
                    workingImage = convertColourToGrayscale(Image); // convert image to grayscale
                    imageType = "grayscale";
                }
                else MessageBox.Show("Image is already either grayscale or binary");
            }

            if (functionSelector.SelectedIndex == 1) // invert image by pixelcolour
                workingImage = invertImage(workingImage);

            if (functionSelector.SelectedIndex == 2) 
            {
                if (useMAC.Checked) workingImage = adjustContrastModified(workingImage, cumulativeHisto); // apply modified ca if checkbox is checked
                else workingImage = adjustContrast(workingImage, inHisto); // otherwise apply standard contrast adjustment
            }

            if (functionSelector.SelectedIndex == 3) // sort of colour implemented, not satisfied
            {
                Tuple<byte, float> gaussianOut = gaussianCheck(); // get sigma size and float size from textbox values

                //convolve image on image, gaussian filter, and filter size
                workingImage = (convolveImage(workingImage, createGaussianFilter(gaussianOut.Item1, gaussianOut.Item2), gaussianOut.Item1, imageType));
                imageType = "grayscale";
            }

            if (functionSelector.SelectedIndex == 4) // sort of colour implemented, not satisfied
                workingImage = convolveImage(workingImage, createBoxFilter(5), 5, imageType); // convolve image with 5 x 5 box filter

            if (functionSelector.SelectedIndex == 5)
            {
                int median;
                //find gaussian size from input
                if (int.TryParse(medSizeInput.Text, out int medSize) && medSize >= 3 && !(medSize % 2 == 0))
                {
                    median = (byte)(medSize);
                }
                else
                {
                    median = 7;
                    MessageBox.Show("Please insert a valid median size value. Median size set to 7.");
                }
                if (imageType == "colour") workingImage = convertColourToGrayscale(workingImage); // not colour implemented - make image grayscale first
                imageType = "grayscale";
                workingImage = medianFilter(workingImage, median); // CURRENTLY ONLY OUTPUTS BLACK AND WHITE

            }

            // calculate edge magnitude of image with optional pre & post processing
            if (functionSelector.SelectedIndex == 6)  // not colour implemented - make image grayscale first
            {
                if (edgeGaussianCheck.Checked) // Use gaussian filter to remove noise before processing
                {
                    Tuple<byte, float> gaussianOut = gaussianCheck();
                    workingImage = (convolveImage(workingImage, createGaussianFilter(gaussianOut.Item1, gaussianOut.Item2), gaussianOut.Item1, imageType));
                    imageType = "grayscale";
                }

                if (imageType == "colour") workingImage = convertColourToGrayscale(workingImage); // not colour implemented - make image grayscale first
                workingImage = edgeMagnitude(workingImage, horizontalSobel(), verticalSobel()); //calculate edge magnitude with Sobel filters

                if (edPipeline.Checked) //Use Contrast Adjustment to make edges clearer
                {
                    int[] cumulative = cumulativeHistogram(computeHistogram(workingImage));
                    workingImage = adjustContrastModified(workingImage, cumulative);
                }
            }

            if (functionSelector.SelectedIndex == 7) // threshold image using input value 
                workingImage = thresholdImage(workingImage);

            // edge sharpening using LaPlace algorithm 
            if (functionSelector.SelectedIndex == 8) // not colour implemented - make image grayscale first
            {
                // Use gaussian filter to remove noise before processing
                if (edgeGaussianCheck.Checked)
                {
                    Tuple<byte, float> gaussianOut = gaussianCheck();
                    workingImage = (convolveImage(workingImage, createGaussianFilter(gaussianOut.Item1, gaussianOut.Item2), gaussianOut.Item1, imageType));
                    imageType = "grayscale";
                }
                if (imageType == "colour") workingImage = convertColourToGrayscale(workingImage); // not colour implemented - make image grayscale first
                workingImage = edgeSharpening(workingImage);
            }

            if (functionSelector.SelectedIndex >= 10 && functionSelector.SelectedIndex >= 13)
            {
                if (morphoShapeIn.Text == "Choose Shape")                 // Ensure a function is chosen
                {
                    morphoShapeIn.SelectedIndex = 0; // set default to plus
                    morphoShapeIn.Update();
                }
            }

            if (functionSelector.SelectedIndex == 10) // erosion
            {
                int size = Convert.ToInt32(morphoSizeIn.Text);                              // currently breaks if not an int, need to make checks
                int[,] structure = createStructuringElement(size, morphoShapeIn.Text, imageType);     // currenty all false if not right shape, need to make checks
                workingImage = trimBorder(erodeImage(extendBorder(workingImage, size), structure, imageType), size); // extend border of image, erode image, trim border of image
            }

            if (functionSelector.SelectedIndex == 11) // dilation
            {
                int size = Convert.ToInt32(morphoSizeIn.Text);                              // currently breaks if not an int, need to make checks
                int[,] structure = createStructuringElement(size, morphoShapeIn.Text, imageType);     // currenty all false if not right shape, need to make checks
                workingImage = trimBorder(dilateImage(extendBorder(workingImage, size), structure, imageType), size); // extend border of image, dilate image, trim border of image
            }

            if (functionSelector.SelectedIndex == 12) // opening
            {
                int size = Convert.ToInt32(morphoSizeIn.Text);                              // currently breaks if not an int, need to make checks
                int[,] structure = createStructuringElement(size, morphoShapeIn.Text, imageType);     // currenty all false if not right shape, need to make checks
                workingImage = trimBorder(openImage(extendBorder(workingImage, size), structure, imageType), size); // extend border of image, open image, trim border of image
            }

            if (functionSelector.SelectedIndex == 13) // closing
            {
                int size = Convert.ToInt32(morphoSizeIn.Text);                              // currently breaks if not an int, need to make checks
                int[,] structure = createStructuringElement(size, morphoShapeIn.Text, imageType);     // currenty all false if not right shape, need to make checks
                workingImage = trimBorder(closeImage(extendBorder(workingImage, size), structure, imageType), size); // extend border of image, close image, trim border of image
            }

            if (functionSelector.SelectedIndex == 14) // AND
            {
                if (imageType != "binary" || image2Type != "binary")
                {
                    MessageBox.Show("Please enter binary images.");
                    return;
                }
                workingImage = andImages(workingImage, Image2);
            }

            if (functionSelector.SelectedIndex == 15) // OR
            {
                if (imageType != "binary" || image2Type != "binary")
                {
                    MessageBox.Show("Please enter binary images.");
                    return;
                }
                workingImage = orImages(workingImage, Image2);
            }

            // refresh histogram labels (in case histogram equalization was previosuly applied and changed the labels)
            outputHistogramBox.Text = "Output Histogram Data";
            outputHistogramBox.Update();

            if (functionSelector.SelectedIndex != 9)
            {
                if (imageType == "colour")
                    printOutputHistogram(computeColourHistogram(workingImage));
                else
                    printOutputHistogram(computeHistogram(workingImage)); //calculate and then print the output histogram
            }
            else
                workingImage = histogramEqualization(workingImage, cumulativeHisto, imageType);

            progressUpdate.Text = "Printing to bitmap...";
            progressUpdate.Refresh();

            // ==================== END OF YOUR FUNCTION CALLS ====================
            // ====================================================================
            
            // copy array to output Bitmap
            for (int x = 0; x < workingImage.GetLength(0); x++)             // loop over columns
                for (int y = 0; y < workingImage.GetLength(1); y++)         // loop over rows
                {
                    Color newColor = Color.FromArgb(workingImage[x, y].R, workingImage[x, y].G, workingImage[x, y].B);
                    OutputImage.SetPixel(x, y, newColor);                   // set the pixel color at coordinate (x,y)
                }
            
            pictureBox2.Image = (Image)OutputImage;                         // display output image
            progressBar.Visible = false;                                    // hide progress bar

            progressUpdate.Text = "Task completed.";                        // update label
            progressUpdate.Refresh();
        }
        
        /*
         * saveButton_Click: process when user clicks "Save" button
         */
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (OutputImage == null) return;                                // get out if no output image
            if (saveImageDialog.ShowDialog() == DialogResult.OK)
                OutputImage.Save(saveImageDialog.FileName);                 // save the output image
        }

        /*
         * convertColourToGrayscale: convert a three-channel color image to a three channel grayscale image
         * input:   inputImage          three-channel (Color) image
         * output:  tempImage           three-channel (Grayscale) image
         */
        private Color[,] convertColourToGrayscale(Color[,] inputImage)
        {
            progressUpdate.Text = "Converting image to grayscale...";
            progressUpdate.Refresh();

            // create temporary grayscale image of the same size as input, with a single channel
            Color[,] tempImage = new Color[inputImage.GetLength(0), inputImage.GetLength(1)];

            double redVal = double.Parse(redMixIn.Text);
            double greenVal = double.Parse(greenMixIn.Text);
            double blueVal = double.Parse(blueMixIn.Text);

            // process all pixels in the image
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
                {  
                    int colourMix = Convert.ToInt32(combineColours(inputImage[x,y])); // get & average pixel color
                    Color weighted = Color.FromArgb(
                        colourMix,
                        colourMix,
                        colourMix);
                    tempImage[x, y] = weighted;                              // set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // increment progress bar
                }

            progressBar.Visible = false;                                    // hide progress bar
            imageTypeLabel.Text = "Image type = grayscale";
            imageTypeLabel.Update();

            return tempImage;
        }

        /*
         * convertToByte: convert a three-channel color image to a single channel grayscale image
         * input:   inputImage          three-channel (Color) image
         * output:  tempImage           single-channel (byte) image
         */
        private byte[,] convertToByte(Color[,] inputImage)
        {
            // create temporary single channel image of the same size as input, with a single channel
            byte[,] tempImage = new byte[inputImage.GetLength(0), inputImage.GetLength(1)];


            // process all pixels in the image
            for (int x = 0; x < InputImage.Size.Width; x++)                 // loop over columns
                for (int y = 0; y < InputImage.Size.Height; y++)            // loop over rows
                {
                    Color pixelColor = inputImage[x, y];                    // get pixel color
                    byte average = (byte)((pixelColor.R + pixelColor.B + pixelColor.G) / 3); // calculate average over the three channels
                    tempImage[x, y] = average;                              // set the new pixel color at coordinate (x,y)
                    progressBar.PerformStep();                              // increment progress bar
                }

            progressBar.Visible = false;                                    // hide progress bar

            return tempImage;
        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 1 GO HERE ==============
        // ====================================================================

        /*
         * invertImage: invert an image based on pixel colour values
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (colour) image
         */
        private Color[,] invertImage(Color[,] inputImage)
        {
            progressUpdate.Text = "Inverting image...";
            progressUpdate.Refresh();

            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);

            Color[,] tempImage = new Color[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempImage[i, j] = Color.FromArgb((255 - inputImage[i, j].R), (255 - inputImage[i,j].G), (255 - inputImage[i,j].B)); // invert pixel values
                    progressBar.PerformStep();                              // increment progress bar
                }
            }

            return tempImage;
        }


        /*
         * adjustContrast: create an image with the full range of intensity values used (not modified version)
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (colour) image
         */
        private Color[,] adjustContrast(Color[,] inputImage, int[] histogram)
        {
            progressUpdate.Text = "Adjusting contrast of image...";
            progressUpdate.Refresh();

            // create temporary grayscale image
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            Color[,] tempImage = new Color[width, height];

            // if these values are ints instead of doubles, then the image does not scale correctly
            double low = 3000;
            double high = -3000;

            int max = 255;
            int min = 0;

            // loop over histogram to find high and low values
            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > 0 && i < low) low = i;
                if (histogram[255 - i] > 0 && (255 - i) > high) high = (255-i);
            }

            // rescale pixel values
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int redValue = Convert.ToInt32(min + ((inputImage[i, j].R - low) * ((max - min) / (high - low))));
                    int greenValue = Convert.ToInt32(min + ((inputImage[i, j].G - low) * ((max - min) / (high - low))));
                    int blueValue = Convert.ToInt32(min + ((inputImage[i, j].B - low) * ((max - min) / (high - low))));
                    //some values were coming up greater than 255 for certain colour images, so clamping them
                    if (redValue > 255) redValue = 255; if (greenValue > 255) greenValue = 255; if (blueValue > 255) blueValue = 255;
                    tempImage[i, j] = Color.FromArgb(redValue, greenValue, blueValue);
                    progressBar.PerformStep();
                }
            }

            return tempImage;
        }

        /*
         * adjustContrastModified: create an image with the full range of intensity values used where highest and lowest values are saturated
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (colour) image
         */
        private Color[,] adjustContrastModified(Color[,] inputImage, int[] cumulative)
        {
            progressUpdate.Text = "Using advanced techniques to adjust contrast...";
            progressUpdate.Refresh();

            // create temporary grayscale image
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            Color[,] tempImage = new Color[width, height];

            // quantiles: taken from input, check for correct types else set to default
            double q_low, q_high;
            if (double.TryParse(lowQInput.Text, out double lowQ) && lowQ >= 0 && lowQ <= 100)
            {
                q_low = lowQ / 100;
            }
            else
            {
                q_low = 0.005;
                MessageBox.Show("Please insert a valid low quantile value. Default set to 2.5%");
            }
            if (double.TryParse(highQInput.Text, out double highQ) && highQ >= 0 && highQ <= 100)
            {
                q_high = highQ / 100 ;
            }
            else
            {
                q_high = 0.005;
                MessageBox.Show("Please insert a valid high quantile value. Default set to 2.5%");
            }

            if ((q_high + q_low) > 1)
            {
                q_low = 0.005;
                q_high = 0.005;
                MessageBox.Show("Combined q-values cannot exceed 100%. Values set to default.");
            }

            // intialise a_low to be high and a_high to be low
            double a_low = 3000;
            double a_high = -3000;

            double minQ = (width * height) * q_low;           //calculate M x N x q_low
            double maxQ = (width * height) * (1 - q_high);    //calculate  M x N x q_high

            // now we calculate a_low and a_high
            for (int i = 0; i < 256; i++)
            {
                if ((cumulative[i] >= minQ) && i < a_low) a_low = i;                             //find minimum q_low greater than or equal to minQ, and don't change afterwards
                if ((cumulative[255 - i] <= maxQ) && ((255 - i) > a_high)) a_high = (255 - i);   //find maximum q_low smaller than or equal to maxQ, and don't change afterwards
            }

            int a_max = 255; // can be implemented to be changed on basis of input
            int a_min = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    // change red value
                    if (inputImage[i, j].R >= a_high) tempImage[i, j] = Color.FromArgb(255, tempImage[i,j].G, tempImage[i, j].B);      // everything above high margin gets set to max value (255)
                    else if (inputImage[i, j].R <= a_low) tempImage[i, j] = Color.FromArgb(0, tempImage[i, j].G, tempImage[i, j].B);    // everything below low margin gets set to min value (0)
                    else  // everything else is between high and low margins and gets scaled linearly to max and min values
                    {
                        tempImage[i, j] = Color.FromArgb(Convert.ToInt32((a_min + ((inputImage[i, j].R - a_low) * ((a_max - a_min) / (a_high - a_low))))), tempImage[i, j].G, tempImage[i, j].B);
                    }

                    // change green
                    if (inputImage[i, j].G >= a_high) tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, 255, tempImage[i, j].B);      // everything above high margin gets set to max value (255)
                    else if (inputImage[i, j].G <= a_low) tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, 0, tempImage[i, j].B);    // everything below low margin gets set to min value (0)
                    else  // everything else is between high and low margins and gets scaled linearly to max and min values
                    {
                        tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, Convert.ToInt32((a_min + ((inputImage[i, j].G - a_low) * ((a_max - a_min) / (a_high - a_low))))), tempImage[i, j].B);
                    }

                    // change blue
                    if (inputImage[i, j].B >= a_high) tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, tempImage[i, j].G, 255);      // everything above high margin gets set to max value (255)
                    else if (inputImage[i, j].B <= a_low) tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, tempImage[i, j].G, 0);    // everything below low margin gets set to min value (0)
                    else  // everything else is between high and low margins and gets scaled linearly to max and min values
                    {
                        tempImage[i, j] = Color.FromArgb(tempImage[i, j].R, tempImage[i, j].G, Convert.ToInt32((a_min + ((inputImage[i, j].B - a_low) * ((a_max - a_min) / (a_high - a_low))))));
                    }


                    progressBar.PerformStep();                                  // Increment progress bar
                }
            }

            return tempImage;
        }

        /*
         * createGaussianFilter: create a Gaussian filter of specific square size and with a specified sigma
         * input:   size                length and width of the Gaussian filter (only odd sizes)
         *          sigma               standard deviation of the Gaussian distribution
         * output:  float[,]            Gaussian filter
         */
        private float[,] createGaussianFilter(byte size, float sigma)
        {
            progressUpdate.Text = "Creating Gaussian filter...";
            progressUpdate.Refresh();

            // make radius from size
            int radius = (size - 1) /2;

            // make filter
            float[,] filter = new float[size, size];
            double constant = 1d / (2 * Math.PI * sigma * sigma);
            double sum = 0d; // for normalisation

            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                {
                    double value = constant * Math.Exp(-(((i * i) * (j * j)) / (2 * (sigma * sigma))));
                    filter[i+radius, j+radius] = (float)value;
                    sum += value;

                    progressBar.PerformStep();
                }
            
            // normalise filter
            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                    filter[i + radius, j + radius] = (float)(filter[i + radius, j + radius] / sum);

            return filter;
        }

        /*
         * createBoxFilter: creates a simple box filter of 1 values surrounded by a border of 0 values
         * input:   size                length and width of the filter
         * output:  float[,]            box filter
         */
        private float[,] createBoxFilter(byte size)
        {
            progressUpdate.Text = "Creating Box filter...";
            progressUpdate.Refresh();

            float[,] filter = new float[size, size];
            int radius = (size - 1) / 2;

            int sum = 0;

            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                {
                    if (Math.Abs(i) == radius) filter[i + radius, j + radius] = 0;
                    else
                    {
                        filter[i + radius, j + radius] = 1;
                        sum++;
                    }
                }

            // normalise filter
            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                    filter[i + radius, j + radius] = (float)(filter[i + radius, j + radius] / sum);

            return filter;
        }

        /*
         * convolveImage: apply linear filtering of an input image. Expects (or converts) and returns a grayscale image
         * input:   inputImage          three-channel (color/grayscale) image image
         *          filter              linear kernel (must be square)
         * output:  outputImage         three-channel (grayscale) image
         */
        private Color[,] convolveImage(Color[,] inputImage, float[,] filter, byte size, string imageType)
        {
            if (imageType == "colour")
                inputImage = convertColourToGrayscale(inputImage);
            Color[,] extendedImage = extendBorder(inputImage, size); //extend border of image by radius of filter size, extend pixel each direction

            progressUpdate.Text = "Filtering image...";
            progressUpdate.Refresh();

            // set int values
            int radius = (size - 1) / 2;
            int width = extendedImage.GetLength(0);
            int height = extendedImage.GetLength(1);
            Color[,] outputImage = new Color[width, height];

            // loop over image
            for (int i = radius; i < (width - radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // Get values from input image
                    int[,] inputValues = new int[size, size]; 
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                            inputValues[x + radius, y + radius] = extendedImage[i + x, j + y].R;

                    // apply filter values to image input values
                    double sumVal = 0d;
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                            sumVal += filter[x + radius, y + radius] * inputValues[x + radius, y + radius];   // sum values of filter applied to values

                    int sum = Convert.ToInt32(sumVal);
                    outputImage[i, j] = Color.FromArgb(sum, sum, sum); // add back into colour images
                    progressBar.PerformStep();
                }

            return trimBorder(outputImage, size); // trim extend border away before returning image
        }


        /*
         * medianFilter: apply median filtering on an input image with a kernel of specified size. Expects and returns a grayscale image
         * input:   inputImage          three-channel (grayscale) image
         *          size                length and width of the median filter kernel
         * output:  outputImage         three-channel (grayscale) image
         */
        private Color[,] medianFilter(Color[,] inputImage, int size)
        {
            //extend border of image by radius of filter size, extend pixel each direction
            Color[,] extendedImage = extendBorder(inputImage, size);

            progressUpdate.Text = "Applying median filter to image...";
            progressUpdate.Refresh();

            int radius = (size - 1) / 2;
            int width = extendedImage.GetLength(0);
            int height = extendedImage.GetLength(1);
            Color[,] outputImage = new Color[width, height];
            
            //loop over image
            for (int i = radius; i < (width - radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // add elements in image to arrays
                    byte[,] inputPixels = new byte[radius * 2 + 1, radius * 2 + 1];
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                        {
                            inputPixels[x + radius, y + radius] = extendedImage[i + x, j + y].R;
                        }

                    byte[] medianList = new byte[size * size];
                    int count = 0;
                    for (int x = 0; x < size; x++)
                        for (int y = 0; y < size; y++)
                        {
                            medianList[count] = inputPixels[x, y];
                            count++;
                        }

                    // find median of each array
                    var sortedList = medianList.OrderBy(n => n);
                    byte median = sortedList.ElementAt(medianList.Length / 2);

                    // replace element with median
                    outputImage[i, j] = Color.FromArgb(median, median, median);
                    progressBar.PerformStep();
                }

            // trim extend border away before returning image
            return trimBorder(outputImage, size);
        }

        /*
         * edgeMagnitude: calculate the image derivative of an input image and a provided edge kernel. Expects and returns a grayscale image
         * input:   inputImage          three-channel (grayscale) image
         *          horizontalKernel    horizontal edge kernel
         *          verticalKernel      vertical edge kernel
         * output:  outputImage         three-channel (grayscale) image
         */
        private Color[,] edgeMagnitude(Color[,] inputImage, int[,] horizontalKernel, int[,] verticalKernel)
        {

            Color[,] extendedImage = extendBorder(inputImage, 3);
            int width = extendedImage.GetLength(0);
            int height = extendedImage.GetLength(1);
            // create temporary grayscale image
            Color[,] outputImage = new Color[width, height];
            int radius = 1;

            progressUpdate.Text = "Applying edge-detection to image...";
            progressUpdate.Refresh();

            for (int i = radius; i < (width - radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // put input pixel values into new (sbyte) array
                    int[,] inputPixels = new int[3, 3];
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                        {
                            inputPixels[x + radius, y + radius] = (sbyte)(extendedImage[i + x, j + y].R);
                        }

                    // apply kernels and normalise
                    int xSum = 0;
                    int ySum = 0;
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                        {
                            xSum += Convert.ToInt32(horizontalKernel[x + radius, y + radius] * inputPixels[x + radius, y + radius]);
                            ySum += Convert.ToInt32(verticalKernel[x + radius, y + radius] * inputPixels[x + radius, y + radius]);
                        }
                    xSum /= 8;
                    ySum /= 8;
                    int colour = Convert.ToInt32(Math.Sqrt((xSum * xSum) + (ySum * ySum)));

                    outputImage[i, j] = Color.FromArgb(colour, colour, colour);
                }

            return trimBorder(outputImage, 3);
        }

        /*
         * thresholdImage: threshold a grayscale image
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (binary) image with on/off values
         */
        private Color[,] thresholdImage(Color[,] inputImage)
        {
            progressUpdate.Text = "Thresholding image...";
            progressUpdate.Refresh();

            // create temporary grayscale image
            int width = inputImage.GetLength(0);
            int height = inputImage.GetLength(1);
            Color[,] tempImage = new Color[width, height];

            // default threshold: find the average of the pixel values by summing them and dividing them by width x height
            double sum = 0d;
            
            for(int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double pixelValue = combineColours(inputImage[i, j]);
                    sum += pixelValue;
                    progressBar.PerformStep();                              // increment progress bar
                }
            }

            int threshold = Convert.ToInt32(sum / (width * height));

            // check if there is an input for threshold
            if (thresholdInput.Text != "mean")
            {
                if (int.TryParse(thresholdInput.Text, out int threshIn) && threshIn >= 0)
                {
                    threshold = threshIn;
                }
                else MessageBox.Show("Please insert a valid threshold value between 0 and 255.\nThreshold set to mean of pixel values.");
            }
            
            // in the new image, set all pixels with values less than threshold to 0 (black), all values greater or equal to threshold to 255 (white)
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    double pixelValue = combineColours(inputImage[i, j]);
                    if (pixelValue < threshold)
                    {
                        tempImage[i, j] = Color.FromArgb(0, 0, 0);
                    }
                    else tempImage[i, j] = Color.FromArgb(255, 255, 255);
                    progressBar.PerformStep();                              // increment progress bar
                }
            }

            return tempImage;
        }

        /*
         * edgeSharpening: sharpen the image edge by filtering it with a LaPlacian filter and subtracting fraction of result from image. Expects and returns a Grayscale image
         * input:   inputImage          three-channel (grayscale) image
         * output:  outputImage         three-channel (grayscale) image
         * 
         * Color status: only works on single channel images
         */
        private Color[,] edgeSharpening(Color[,] inputImage)
        {
            Color[,] extendedImage = extendBorder(inputImage, 3);
            int width = extendedImage.GetLength(0);
            int height = extendedImage.GetLength(1);
            int radius = 1;
            double weight;
            Color[,] outputImage = new Color[width, height];

            if (double.TryParse(edgeSharpW.Text, out double weightIn) && weightIn > 0d && weightIn < 1.000001d)
            {
                weight = weightIn;
            }
            else
            {
                MessageBox.Show("Please insert a valid weight value for edge sharpening. Set to default");
                weight = 1.0;
            }

            progressUpdate.Text = "Sharpening image...";
            progressUpdate.Refresh();

            int[,] laPlace = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };

            for (int i = radius; i < (width - radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // put input pixel values into new (sbyte) array
                    int[,] inputPixels = new int[3, 3];
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                        {
                            inputPixels[x + radius, y + radius] = extendedImage[i + x, j + y].R;
                        }

                    // apply kernel
                    int sum = 0;
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                        {
                            sum += laPlace[x + radius, y + radius] * inputPixels[x + radius, y + radius];
                        }

                    byte output = (byte)(extendedImage[i, j].R - (weight * sum));
                    outputImage[i, j] = Color.FromArgb(output, output, output); //sharpen and apply to image
                }
            return trimBorder(outputImage, 3);
        }

        // gaussianCheck: reads gaussian kernel values from textbox inputs, makes sure inputs are valid, and returns inputs (or defaults)
        private Tuple<byte, float> gaussianCheck()
        {
            byte gaussian;
            float sigma;
            //find gaussian size from input
            if (byte.TryParse(gauSizeInput.Text, out byte gaussSize) && gaussSize >= 3 && !(gaussSize % 2 == 0))
            {
                gaussian = gaussSize;
            }
            else
            {
                gaussian = 11;
                MessageBox.Show("Please insert a valid guassian size value. Gaussian Size set to 11.");
            }

            //find gaussian sigma from input
            if (float.TryParse(gauSigmaInput.Text, out float gaussSigma) && gaussSigma > 0)
            {
                sigma = gaussSigma;
            }
            else
            {
                sigma = 2;
                MessageBox.Show("Please insert a valid guassian sigma value. Gaussian Sigma set to 2.");
            }

            Tuple<byte, float> output = new Tuple<byte, float>(gaussian, sigma);
            return output;
        }

        // create filters for edge magnititude detection
        private int[,] horizontalSobel()
        {
            int[,] kernel = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            return kernel;
        }
        private int[,] verticalSobel()
        {
            int[,] kernel = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
            return kernel;
        }

        /*
         * extendBorder: add a padding around an image by extending each border pixel by the radius of a filter size
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (colour) image with extended border
         */
        private Color[,] extendBorder(Color[,] inputImage, int size)
        {
            progressUpdate.Text = "Extending border of image...";
            progressUpdate.Refresh();

            int radius = (size - 1) / 2;

            int width = inputImage.GetLength(0); // width = width of original image (NOT extended)
            int height = inputImage.GetLength(1); // height = height of original image (NOT extended)
            Color[,] tempImage = new Color[width + (2 * radius), height + (2 * radius)]; //new temp image with room for padding

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    tempImage[i + radius, j + radius] = inputImage[i, j]; // add original image to centre of bigger image
                }
            }

            // padding rows
            for (int i = 0; i < width; i++)
                for (int j = 0; j < radius; j++)
                {
                    tempImage[i + radius, j] = inputImage[i, 0]; // top row
                    tempImage[i + radius, (radius + height) + j] = inputImage[i, height - 1]; // bottom row
                }

            for (int i = 0; i < height; i++)
                for (int j = 0; j < radius; j++)
                {
                    tempImage[j, i + radius] = inputImage[0, i]; // left column
                    tempImage[(radius + width) + j, i + radius] = inputImage[width - 1, i];   // right column
                }
            //padding corners
            for (int i = 0; i < radius; i++)
            {
                for (int j = 0; j < radius; j++)
                {
                    tempImage[i, j] = tempImage[radius, radius]; //top-left corner
                    tempImage[(radius + width) + i, j] = tempImage[(radius + width) - 1, radius]; //top-right corner
                    tempImage[i, (radius + height) + j] = tempImage[radius, (radius + height) - 1]; //bottom-left corner
                    tempImage[(radius + width) + i, (radius + height) + j] = tempImage[(radius + width) - 1, (radius + height) - 1]; //bottom-right corner
                }
            }
            
            return tempImage;
        }

        /*
         * trimBorder: remove padding around an image by the radius of a filter size
         * input:   inputImage          three-channel (colour) image
         * output:  tempImage           three-channel (colour) image with trimmed border
         */
        private Color[,] trimBorder(Color[,] inputImage, int size)
        {
            progressUpdate.Text = "Trimming border of image...";
            progressUpdate.Refresh();

            int radius = (size - 1) / 2;

            int width = inputImage.GetLength(0);  // width = width of extended image (NOT original)
            int height = inputImage.GetLength(1); // height = height of extended image (NOT original)
            Color[,] outputImage = new Color[width - (2 * radius), height - (2 * radius)]; //new temp image of correct output size

            for (int i = 0; i < (width - (2 * radius)); i++)
            {
                for (int j = 0; j < (height - (2 * radius)); j++)
                {
                    outputImage[i, j] = inputImage[i + radius, j + radius]; // add image from within border to new image
                }
            }

            return outputImage;
        }

        /*
         * histogramEqualization: equalize the (cumulative) histogram of an image, print both original and equalized histograms, and return new image
         * input:   inputImage          three-channel (colour) image
         *          cumulative          one-dimensional (int) array: the cumulative histogram of image 
         * output:  tempImage           three-channel (colour) image with equalized histogram
         * 
         *  COLOUR STATUS: WORKING BUT NOT IDEAL
         */
        private Color[,] histogramEqualization(Color[,] image, int[] cumulative, string imageType)
        {
            progressUpdate.Text = "Equalizing histograms...";
            progressUpdate.Refresh();

            int width = image.GetLength(0);
            int height = image.GetLength(1);
            Color[,] tempImage = new Color[width, height];

            // calculate image with equalized histogram
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    int newRed = (int)Math.Floor(cumulative[image[i, j].R] * (255.0 / (width * height)));
                    int newGreen = (int)Math.Floor(cumulative[image[i, j].G] * (255.0 / (width * height)));
                    int newBlue = (int)Math.Floor(cumulative[image[i, j].B] * (255.0 / (width * height)));
                    tempImage[i, j] = Color.FromArgb(newRed, newGreen, newBlue);
                }

            // update visual histograms & labels
            int[] histogramNew;
            if (imageType == "colour")
                histogramNew = computeColourHistogram(tempImage);
            else
                histogramNew = computeHistogram(tempImage);

            int[] cumulativeNew = cumulativeHistogram(histogramNew);

            inputHistogramBox.Text = "Original Cumulative Histogram";
            inputHistogramBox.Update();
            printInputHistogram(cumulative);

            outputHistogramBox.Text = "Equalized Cumulative Histogram";
            outputHistogramBox.Update();
            printOutputHistogram(cumulativeNew);

            return tempImage;
        }

        private double combineColours(Color pixel)
        {
            double redMix = double.Parse(redMixIn.Text);
            double greenMix = double.Parse(greenMixIn.Text);
            double blueMix = double.Parse(blueMixIn.Text);

            int red = pixel.R; int green = pixel.G; int blue = pixel.B;

            double average = (redMix * red) + (greenMix * green) + (blueMix * blue);
            return average;
        }

        /*
         * computeHistogram: calculate the histogram of a single-channel image
         * input:   image          three-channel (colour) image
         * output:  histogram      256 array of histogram values
         */
        private int[] computeHistogram(Color[,] image)
        {
            progressUpdate.Text = "Computing histogram of image...";
            progressUpdate.Refresh();

            int[] histogram = new int[256];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Color pixelColor = image[i, j];                    // get pixel color
                    byte average = (byte)((pixelColor.R + pixelColor.B + pixelColor.G) / 3); // calculate average over the three channels
                    histogram[average]++; // increase that values histogram entry by one
                }
            }

            return histogram;
        }

        /*
         * cumulativeHistogram: calculate the cumulative histogram of given histogram
         * input:   histogram      256 array of histogram values
         * output:  histogram      256 array of histogram values (but accumulated)
         */
        private int[] cumulativeHistogram(int[] histogram)
        {
            int[] cumulative = new int[256];
            cumulative[0] = histogram[0];
            for(int i = 1; i < 256; i++)
            {
                cumulative[i] = histogram[i] + cumulative[i - 1];
            }
            return cumulative;
        }

        /*
         * printColourHistogram: convert a three-channel color image to a histogram and print it to the input chart
         */
        private int[] computeColourHistogram(Color[,] image)
        {
            progressUpdate.Text = "Computing histogram of image...";
            progressUpdate.Refresh();

            int[] histogram = new int[256];

            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    double redMix = double.Parse(redMixIn.Text);
                    double greenMix = double.Parse(greenMixIn.Text);
                    double blueMix = double.Parse(blueMixIn.Text);

                    int red = image[i, j].R; int green = image[i, j].G; int blue = image[i, j].B;
                    byte combined = (byte)((red * redMix) + (green * greenMix) + (blue * blueMix)); // calculate average over the three channels
                    histogram[combined]++; // increase that values histogram entry by one
                }
            }
            return histogram;
        }
        
            /*
             * printHistogram: (Input & Output) take a given histogram and print it to the output chart (one for input, one for output)
             */
            private void printInputHistogram(int[] histogram) //Takes a histogram and prints it
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            inputHistogram.Series.Clear();
            series.Name = "Pixels";
            inputHistogram.Series.Add(series);

            for (int i = 0; i < 256; i++)
            {
                inputHistogram.Series["Pixels"].Points.AddXY(i + 1, histogram[i]); // add each value to the actual histogram
            }

            inputHistogram.ChartAreas[0].RecalculateAxesScale(); //Resize y-axis of histogram to new values

            // update image histogram statistics
            bool minimum = false; bool maximum = false;
            int distinct = 0;
            int min = 0; int max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > 0) distinct++;
                if (histogram[i] != 0 && !minimum)
                {
                    minimum = true;
                    min = i;
                    minValIn.Text = min.ToString();
                }
                if (histogram[255 - i] != 0 && !maximum)
                {
                    maximum = true;
                    max = (255 - i);
                    maxValIn.Text = max.ToString();
                }
            }
            distValsIn.Text = distinct.ToString();

            if (histogram[0] == 0 || histogram[255] == 0)
            {
                maxContrastIn.Text = "false";
            }
            else maxContrastIn.Text = "true";

            bool maxDynRange = true;
            for (int i = min; i < max; i++)
                if (histogram[i] < 1) maxDynRange = false;
            if (maxDynRange) maxDynRangeIn.Text = "true";
            else maxDynRangeIn.Text = "false";

            distValsIn.Update();
            minValIn.Update();
            maxValIn.Update();
            maxDynRangeIn.Update();
            maxContrastIn.Update();
        }

        private void printOutputHistogram(int[] histogram) //Takes a histogram and prints it
        {
            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            outputHistogram.Series.Clear();
            series.Name = "Pixels";
            outputHistogram.Series.Add(series);

            for (int i = 0; i < 256; i++)
            {
                outputHistogram.Series["Pixels"].Points.AddXY(i + 1, histogram[i]); // add each value to the actual histogram
            }

            outputHistogram.ChartAreas[0].RecalculateAxesScale(); //Resize y-axis of histogram to new values

            // update image histogram statistics
            bool minimum = false; bool maximum = false;
            int distinct = 0;
            int min = 0; int max = 0;
            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > 0) distinct++;
                if (histogram[i] != 0 && !minimum)
                {
                    minimum = true;
                    min = i;
                    minValOut.Text = min.ToString();
                }
                if (histogram[255 - i] != 0 && !maximum)
                {
                    maximum = true;
                    max = (255-i);
                    maxValOut.Text = max.ToString();
                }
            }
            distValsOut.Text = distinct.ToString();

            if (histogram[0] == 0 || histogram[255] == 0)
            {
                maxContrastOut.Text = "false";
            }
            else maxContrastOut.Text = "true";

            bool maxDynRange = true;
            for (int i = min; i < max; i++) 
                if (histogram[i] < 1) maxDynRange = false;
            if (maxDynRange) maxDynRangeOut.Text = "true";
            else maxDynRangeOut.Text = "false";

            distValsOut.Update();
            minValOut.Update();
            maxValOut.Update();
            maxDynRangeOut.Update();
            maxContrastOut.Update();
        }

        // typeCheck: given a Color[,] image, loop over the pixel values to determine whether it is in colour, grayscale, or is binary
        private string typeCheck(Color[,] image)
        {
            // loop over image
            for (int i = 0; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Color pixelColour = image[i, j];
                    if ((pixelColour.R != pixelColour.G) || (pixelColour.G != pixelColour.B)) return "colour"; // if there are two or more channels, the image is in colour
                }

            // if no pixel is in two or more channels, then the image is in grayscale or is binary (has max two values0
            int firstColour = image[0, 0].R;
            int secondColour = -1;
            for (int i = 1; i < image.GetLength(0); i++)
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Color pixelColour = image[i, j];
                    if (pixelColour.R != firstColour)
                    {
                        if (secondColour == -1) secondColour = pixelColour.R; // if second colour has not been assigned but is found, assign second colour
                        else if (pixelColour.R != secondColour) return "grayscale"; //if pixel is neither first nor second colour, image is in grayscale
                    }
                }
            return "binary";
        }

        private bool correctColours()
        {
            if (double.TryParse(redMixIn.Text, out double tryRed) && (tryRed >= 0) && (tryRed <= 1))
                if (double.TryParse(greenMixIn.Text, out double tryGreen) && (tryGreen >= 0) && (tryGreen <= 1))
                    if (double.TryParse(blueMixIn.Text, out double tryBlue) && (tryBlue >= 0) && (tryBlue <= 1))
                    {
                        double total = tryRed + tryGreen + tryBlue;
                        if (total < 1.000001 && total > 0.9999999) // Need to make sure the values are equal to 1, which doesn't work with doubles & ints
                        {
                            return true;
                        }
                    }

            redMixIn.Text = "0.3";
            greenMixIn.Text = "0.59";
            blueMixIn.Text = "0.11";

            redMixIn.Update();
            greenMixIn.Update();
            blueMixIn.Update();
            return false;

        }

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 2 GO HERE ==============
        // ====================================================================

        /*
         * createStructuringElement: given a size shape, and image type, create a structuring element for a morphological filtering operation
         * input:   size            int
         *          shape           string
         *          type            string
         * output:  element         int[,]
         */
        private int[,] createStructuringElement(int size, string shape, string type = "binary")
        {
            int[,] element = new int[size, size];
            int radius = (size - 1) / 2;

            if (shape == "plus") // plus: only middle row and column are 'on'
            {
                if (type == "binary")
                {
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            if (radius + i == radius || radius + j == radius)
                                element[radius + i, radius + j] = 0;
                            else element[radius + i, radius + j] = 255;
                        }
                }
                if (type == "grayscale") // scaled, higher value in middle, lower towards edge
                {
                    int n = radius + 1;
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            if (i == 0 || j == 0)
                                element[radius + i, radius + j] = n - Math.Max(Math.Abs(i), Math.Abs(j));
                            else element[radius + i, radius + j] = -1;
                        }
                }
            }
            else if (shape == "square") // a square of 'on' pixels with a border of 'off' pixels
            {
                if (type == "binary")
                {
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            if (Math.Abs(i) == radius || Math.Abs(j) == radius)
                                element[radius + i, radius + j] = 255;
                            else element[radius + i, radius + j] = 0;
                        }
                }
                if (type == "grayscale") // scaled, higher value in middle, lower towards edge
                {
                    int n = radius + 1;
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            element[radius + i, radius + j] = n - Math.Max(Math.Abs(i), Math.Abs(j));
                        }
                }
            }
            else if (shape == "disk") // the corners are off, the rest is on
            {
                if (type == "binary")
                {
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            if (Math.Abs(i) == 2 && Math.Abs(j) == 2)
                                element[radius + i, radius + j] = 255;
                            else element[radius + i, radius + j] = 0;
                        }
                }
                if (type == "grayscale") // scaled, higher value in middle, lower towards edge
                {
                    int n = radius + 1;
                    for (int i = -radius; i <= radius; i++)
                        for (int j = -radius; j <= radius; j++)
                        {
                            if (Math.Abs(i) == radius && Math.Abs(j) == radius)
                                element[radius + i, radius + j] = -1;
                            else
                                element[radius + i, radius + j] = n - (Math.Abs(i) + Math.Abs(j));
                        }
                }
            }

            return element;
        }

        /*
         * erodeImage: given a Color[,] grayscale or binary image and a structuring element, erode structures in the image
         * input:   inputImage              Color[,] (grayscale) image
         *          structuringElement      Int[,] filter
         * output:  outputImage             Color[,] (grayscale) image
         */
        private Color[,] erodeImage(Color[,] inputImage, int[,] structuringElement, string type = "binary")
        {
            progressUpdate.Text = "Eroding image...";
            progressUpdate.Refresh();

            int size = structuringElement.GetLength(0);                                         // assume element is square
            int radius = (size - 1) / 2;
            int width = inputImage.GetLength(0); int height = inputImage.GetLength(1);    // define length and width based on extended image
            Color[,] outputImage = (Color[,])inputImage.Clone();

            for (int i = radius; i < (width  -radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // put input pixel values into new array
                    int[,] inputPixels = new int[size, size];
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                            inputPixels[x + radius, y + radius] = (inputImage[i + x, j + y].R);

                    // apply erosion to image
                    if (type == "binary")
                    {
                        if (inputImage[i, j].R < 1)  // image pixel is foreground ---- 0 (black) = foreground, so 255 (white) = background
                            for (int x = -radius; x <= radius; x++)
                                for (int y = -radius; y <= radius; y++)
                                    {
                                        if (structuringElement[x + radius, y + radius] < 1)         // kernel has foreground
                                            if (inputPixels[x + radius, y + radius] > 0)            // image has background
                                                outputImage[i, j] = Color.FromArgb(255, 255, 255);  // so hotspot pixel on ouput is eroded (set to background)
                                    }
                    }
                    if (type == "grayscale")
                    {
                        int[] erosionArray = new int[size * size];
                        int counter = 0;
                        for (int x = -radius; x <= radius; x++)
                            for (int y = -radius; y <= radius; y++)
                            {
                                if (structuringElement[x + radius, y + radius] != -1)
                                {
                                    erosionArray[counter] = inputPixels[x + radius, y + radius] - structuringElement[x + radius, y + radius]; // make array of subtracted values
                                }
                                else erosionArray[counter] = 200; //case: str elem has a -1 and is not counted: set value high so no default 0's will become minimum val
                                counter++;

                            }
                        int min = erosionArray.Min();
                        if (min < 0) min = 0; //clamp negative 
                        outputImage[i, j] = Color.FromArgb(min, min, min);
                    }
                }

            return outputImage;
        }

        /*
         * dilateImage: given a Color[,] grayscale or binary image and a structuring element, dilate structures in the image
         * input:   inputImage              Color[,] (grayscale) image
         *          structuringElement      Int[,] filter
         * output:  outputImage             Color[,] (grayscale) image
         */
        private Color[,] dilateImage(Color[,] inputImage, int[,] structuringElement, string type = "binary")
        {
            progressUpdate.Text = "Dilating image...";
            progressUpdate.Refresh();

            int size = structuringElement.GetLength(0);                                         // assume element is square
            int radius = (size - 1) / 2;
            int width = inputImage.GetLength(0); int height = inputImage.GetLength(1);    // define length and width based on extended image
            Color[,] outputImage = (Color[,])inputImage.Clone();

            for (int i = radius; i < (width - radius); i++)
                for (int j = radius; j < (height - radius); j++)
                {
                    // put input pixel values into new array
                    int[,] inputPixels = new int[size, size];
                    for (int x = -radius; x <= radius; x++)
                        for (int y = -radius; y <= radius; y++)
                            inputPixels[x + radius, y + radius] = (inputImage[i + x, j + y].R);

                    // apply erosion to image
                    if (type == "binary")
                    {
                        if (inputImage[i, j].R < 1)  // image pixel is foreground ---- 0 (black) = foreground, so 255 (white) = background
                            for (int x = -radius; x <= radius; x++)
                                for (int y = -radius; y <= radius; y++)
                                {
                                    if (structuringElement[x + radius, y + radius] < 1)             // kernel has foreground
                                        outputImage[i + x, j + y] = Color.FromArgb(0, 0, 0);  // so kernel is 'copied' to image
                                }
                    }
                    if (type == "grayscale")
                    {
                        int[] dilationArray = new int[size * size];
                        int counter = 0;
                        for (int x = -radius; x <= radius; x++)
                            for (int y = -radius; y <= radius; y++)
                            {
                                if (structuringElement[x + radius, y + radius] != -1)
                                {
                                    dilationArray[counter] = inputPixels[x + radius, y + radius] + structuringElement[x + radius, y + radius]; // make array of subtracted values
                                    counter++;
                                }
                            }
                        int max = dilationArray.Max();
                        if (max > 255) max = 255; //clamp negative 
                        outputImage[i, j] = Color.FromArgb(max, max, max);
                    }
                }

            return outputImage;
        }

        /*
         * openImage: given a Color[,] grayscale or binary image and a structuring element, open structures in the image
         * input:   inputImage              Color[,] (grayscale) image
         *          structuringElement      Bool[,] filter
         * output:  outputImage             Color[,] (grayscale) image
         */
        private Color[,] openImage(Color[,] inputImage, int[,] structuringElement, string type = "binary")
        {
            Color[,] outputImage = dilateImage(erodeImage(inputImage, structuringElement, type), structuringElement, type);
            return outputImage;
        }

        /*
        * closeImage: given a Color[,] grayscale or binary image and a structuring element, open structures in the image
        * input:   inputImage              Color[,] (grayscale) image
        *          structuringElement      Bool[,] filter
        * output:  outputImage             Color[,] (grayscale) image
        */
        private Color[,] closeImage(Color[,] inputImage, int[,] structuringElement, string type = "binary")
        {
            Color[,] outputImage = erodeImage(dilateImage(inputImage, structuringElement, type), structuringElement, type);
            return outputImage;
        }

        /*
        * andImage: given two Color[,] binary images apply AND operator to the image
        * input:   image1           Color[,] (binary) image
        *          image2           Color[,] (binary) image
        * output:  outputImage      Color[,] (binary) image
        */
        private Color[,] andImages(Color[,] image1, Color[,] image2)
        {
            int minWidth = Math.Min(image1.GetLength(0), image2.GetLength(0));
            int minHeight = Math.Min(image1.GetLength(1), image2.GetLength(1));
            Color[,] outputImage = new Color[minWidth, minHeight]; // create output image, and account for possible difference in size by choosing min width and height

            for (int i = 0; i < minWidth; i++)
                for (int j = 0; j < minHeight; j++)
                {
                    if (image1[i, j].R == 0 && image2[i, j].R == 0)         // if image 1 and image2 has a foreground pixel here
                        outputImage[i, j] = Color.FromArgb(0, 0, 0);        // then outputImage also becomes foreground here
                    else outputImage[i, j] = Color.FromArgb(255, 255, 255); // else out put becomes background here
                }

            return outputImage;
        }

        /*
        * orImage: given two Color[,] binary images apply OR operator to the image
        * input:   image1           Color[,] (binary) image
        *          image2           Color[,] (binary) image
        * output:  outputImage      Color[,] (binary) image
        */
        private Color[,] orImages(Color[,] image1, Color[,] image2)
        {
            int minWidth = Math.Min(image1.GetLength(0), image2.GetLength(0));
            int minHeight = Math.Min(image1.GetLength(1), image2.GetLength(1));
            Color[,] outputImage = new Color[minWidth, minHeight]; // create output image, and account for possible difference in size by choosing min width and height

            for (int i = 0; i < minWidth; i++)
                for (int j = 0; j < minHeight; j++)
                {
                    if (image1[i, j].R == 0 || image2[i, j].R == 0)         // if image 1 or image2 has a foreground pixel here
                        outputImage[i, j] = Color.FromArgb(0, 0, 0);        // then outputImage also becomes foreground here
                    else outputImage[i, j] = Color.FromArgb(255, 255, 255); // else out put becomes background here
                }

            return outputImage;
        }

        // implement boundary trace here (not sure of preferred output format yet)

        // ====================================================================
        // ============= YOUR FUNCTIONS FOR ASSIGNMENT 3 GO HERE ==============
        // ====================================================================

    }
}