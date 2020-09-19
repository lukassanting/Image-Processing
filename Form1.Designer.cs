namespace INFOIBV
{
    partial class INFOIBV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageFileName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.saveImageDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.functionSelector = new System.Windows.Forms.ComboBox();
            this.inputHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.outputHistogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.optionsBox = new System.Windows.Forms.GroupBox();
            this.edgeBox = new System.Windows.Forms.GroupBox();
            this.edPipeline = new System.Windows.Forms.CheckBox();
            this.edgeGaussianCheck = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edgeSharpW = new System.Windows.Forms.TextBox();
            this.caBox = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.highQInput = new System.Windows.Forms.TextBox();
            this.lowQInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.useMAC = new System.Windows.Forms.CheckBox();
            this.thresholdInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.medSizeInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gauSigmaInput = new System.Windows.Forms.TextBox();
            this.gauSizeInput = new System.Windows.Forms.TextBox();
            this.progressUpdate = new System.Windows.Forms.Label();
            this.maxDynRangeIn = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.inputHistogramBox = new System.Windows.Forms.GroupBox();
            this.maxValIn = new System.Windows.Forms.Label();
            this.minValIn = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.maxContrastIn = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.outputHistogramBox = new System.Windows.Forms.GroupBox();
            this.maxValOut = new System.Windows.Forms.Label();
            this.minValOut = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.maxContrastOut = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.maxDynRangeOut = new System.Windows.Forms.Label();
            this.colourOptionsBox = new System.Windows.Forms.GroupBox();
            this.redMixIn = new System.Windows.Forms.TextBox();
            this.greenMixIn = new System.Windows.Forms.TextBox();
            this.blueMixIn = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputHistogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputHistogram)).BeginInit();
            this.optionsBox.SuspendLayout();
            this.edgeBox.SuspendLayout();
            this.caBox.SuspendLayout();
            this.inputHistogramBox.SuspendLayout();
            this.outputHistogramBox.SuspendLayout();
            this.colourOptionsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Location = new System.Drawing.Point(12, 9);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(98, 23);
            this.LoadImageButton.TabIndex = 0;
            this.LoadImageButton.Text = "Load image...";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.loadImageButton_Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "Bitmap files (*.bmp;*.gif;*.jpg;*.png;*.tiff;*.jpeg)|*.bmp;*.gif;*.jpg;*.png;*.ti" +
    "ff;*.jpeg";
            this.openImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // imageFileName
            // 
            this.imageFileName.Location = new System.Drawing.Point(116, 11);
            this.imageFileName.Name = "imageFileName";
            this.imageFileName.ReadOnly = true;
            this.imageFileName.Size = new System.Drawing.Size(248, 20);
            this.imageFileName.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 45);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(513, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(547, 9);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(103, 23);
            this.applyButton.TabIndex = 3;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // saveImageDialog
            // 
            this.saveImageDialog.Filter = "Bitmap file (*.bmp)|*.bmp";
            this.saveImageDialog.InitialDirectory = "..\\..\\images";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(938, 8);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(95, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save as BMP...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(531, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(512, 512);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(656, 11);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(276, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // functionSelector
            // 
            this.functionSelector.FormattingEnabled = true;
            this.functionSelector.Items.AddRange(new object[] {
            "0. Grayscale conversion",
            "1. Inversion",
            "2. Contrast adjustment",
            "3. Linear (Gaussian) filtering",
            "4. Linear (Box) filtering",
            "5. Nonlinear (Median) filtering",
            "6. Edge detection",
            "7. Thresholding",
            "8. Edge sharpening",
            "9. Histogram equalization"});
            this.functionSelector.Location = new System.Drawing.Point(370, 11);
            this.functionSelector.Name = "functionSelector";
            this.functionSelector.Size = new System.Drawing.Size(173, 21);
            this.functionSelector.TabIndex = 7;
            this.functionSelector.Text = "Choose Function";
            // 
            // inputHistogram
            // 
            chartArea3.Name = "ChartArea1";
            this.inputHistogram.ChartAreas.Add(chartArea3);
            this.inputHistogram.Location = new System.Drawing.Point(269, 563);
            this.inputHistogram.Name = "inputHistogram";
            series3.ChartArea = "ChartArea1";
            series3.Name = "Output";
            this.inputHistogram.Series.Add(series3);
            this.inputHistogram.Size = new System.Drawing.Size(258, 245);
            this.inputHistogram.TabIndex = 8;
            // 
            // outputHistogram
            // 
            chartArea4.Name = "ChartArea1";
            this.outputHistogram.ChartAreas.Add(chartArea4);
            this.outputHistogram.Location = new System.Drawing.Point(531, 563);
            this.outputHistogram.Name = "outputHistogram";
            series4.ChartArea = "ChartArea1";
            series4.Name = "Input";
            this.outputHistogram.Series.Add(series4);
            this.outputHistogram.Size = new System.Drawing.Size(258, 246);
            this.outputHistogram.TabIndex = 9;
            // 
            // optionsBox
            // 
            this.optionsBox.Controls.Add(this.colourOptionsBox);
            this.optionsBox.Controls.Add(this.edgeBox);
            this.optionsBox.Controls.Add(this.caBox);
            this.optionsBox.Controls.Add(this.thresholdInput);
            this.optionsBox.Controls.Add(this.label4);
            this.optionsBox.Controls.Add(this.medSizeInput);
            this.optionsBox.Controls.Add(this.label3);
            this.optionsBox.Controls.Add(this.label2);
            this.optionsBox.Controls.Add(this.label1);
            this.optionsBox.Controls.Add(this.gauSigmaInput);
            this.optionsBox.Controls.Add(this.gauSizeInput);
            this.optionsBox.Location = new System.Drawing.Point(1050, 45);
            this.optionsBox.Name = "optionsBox";
            this.optionsBox.Size = new System.Drawing.Size(233, 753);
            this.optionsBox.TabIndex = 10;
            this.optionsBox.TabStop = false;
            this.optionsBox.Text = "Options";
            // 
            // edgeBox
            // 
            this.edgeBox.Controls.Add(this.edPipeline);
            this.edgeBox.Controls.Add(this.edgeGaussianCheck);
            this.edgeBox.Controls.Add(this.label10);
            this.edgeBox.Controls.Add(this.edgeSharpW);
            this.edgeBox.Location = new System.Drawing.Point(21, 182);
            this.edgeBox.Name = "edgeBox";
            this.edgeBox.Size = new System.Drawing.Size(200, 111);
            this.edgeBox.TabIndex = 15;
            this.edgeBox.TabStop = false;
            this.edgeBox.Text = "Edge Detection and Sharpening";
            // 
            // edPipeline
            // 
            this.edPipeline.AutoSize = true;
            this.edPipeline.Checked = true;
            this.edPipeline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.edPipeline.Location = new System.Drawing.Point(11, 56);
            this.edPipeline.Name = "edPipeline";
            this.edPipeline.Size = new System.Drawing.Size(189, 30);
            this.edPipeline.TabIndex = 15;
            this.edPipeline.Text = "Apply modified contrast adjustment\r\nafter edge detection\r\n";
            this.edPipeline.UseVisualStyleBackColor = true;
            // 
            // edgeGaussianCheck
            // 
            this.edgeGaussianCheck.AutoSize = true;
            this.edgeGaussianCheck.Location = new System.Drawing.Point(11, 20);
            this.edgeGaussianCheck.Name = "edgeGaussianCheck";
            this.edgeGaussianCheck.Size = new System.Drawing.Size(172, 30);
            this.edgeGaussianCheck.TabIndex = 13;
            this.edgeGaussianCheck.Text = "Apply gaussian filter \r\npre edge detection/sharpening";
            this.edgeGaussianCheck.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Edge Sharpening Weight:";
            // 
            // edgeSharpW
            // 
            this.edgeSharpW.Location = new System.Drawing.Point(143, 86);
            this.edgeSharpW.Name = "edgeSharpW";
            this.edgeSharpW.Size = new System.Drawing.Size(24, 20);
            this.edgeSharpW.TabIndex = 10;
            this.edgeSharpW.Text = "1.0";
            // 
            // caBox
            // 
            this.caBox.Controls.Add(this.label8);
            this.caBox.Controls.Add(this.label7);
            this.caBox.Controls.Add(this.highQInput);
            this.caBox.Controls.Add(this.lowQInput);
            this.caBox.Controls.Add(this.label6);
            this.caBox.Controls.Add(this.label5);
            this.caBox.Controls.Add(this.useMAC);
            this.caBox.Location = new System.Drawing.Point(21, 76);
            this.caBox.Name = "caBox";
            this.caBox.Size = new System.Drawing.Size(200, 100);
            this.caBox.TabIndex = 8;
            this.caBox.TabStop = false;
            this.caBox.Text = "Contrast Adjustment";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(171, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(68, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "%";
            // 
            // highQInput
            // 
            this.highQInput.Location = new System.Drawing.Point(112, 57);
            this.highQInput.Name = "highQInput";
            this.highQInput.Size = new System.Drawing.Size(53, 20);
            this.highQInput.TabIndex = 4;
            this.highQInput.Text = "0.5";
            // 
            // lowQInput
            // 
            this.lowQInput.Location = new System.Drawing.Point(9, 57);
            this.lowQInput.Name = "lowQInput";
            this.lowQInput.Size = new System.Drawing.Size(53, 20);
            this.lowQInput.TabIndex = 3;
            this.lowQInput.Text = "0.5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "High Q.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Low Q.";
            // 
            // useMAC
            // 
            this.useMAC.AutoSize = true;
            this.useMAC.Checked = true;
            this.useMAC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useMAC.Location = new System.Drawing.Point(7, 20);
            this.useMAC.Name = "useMAC";
            this.useMAC.Size = new System.Drawing.Size(155, 17);
            this.useMAC.TabIndex = 0;
            this.useMAC.Text = "Use Modified Auto-Contrast";
            this.useMAC.UseVisualStyleBackColor = true;
            // 
            // thresholdInput
            // 
            this.thresholdInput.Location = new System.Drawing.Point(178, 41);
            this.thresholdInput.Name = "thresholdInput";
            this.thresholdInput.Size = new System.Drawing.Size(37, 20);
            this.thresholdInput.TabIndex = 7;
            this.thresholdInput.Text = "mean";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Threshold:";
            // 
            // medSizeInput
            // 
            this.medSizeInput.Location = new System.Drawing.Point(84, 41);
            this.medSizeInput.Name = "medSizeInput";
            this.medSizeInput.Size = new System.Drawing.Size(25, 20);
            this.medSizeInput.TabIndex = 5;
            this.medSizeInput.Text = "7";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Median Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gaussian Sigma:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gaussian Size:";
            // 
            // gauSigmaInput
            // 
            this.gauSigmaInput.Location = new System.Drawing.Point(205, 17);
            this.gauSigmaInput.Name = "gauSigmaInput";
            this.gauSigmaInput.Size = new System.Drawing.Size(22, 20);
            this.gauSigmaInput.TabIndex = 1;
            this.gauSigmaInput.Text = "2";
            // 
            // gauSizeInput
            // 
            this.gauSizeInput.Location = new System.Drawing.Point(84, 17);
            this.gauSizeInput.Name = "gauSizeInput";
            this.gauSizeInput.Size = new System.Drawing.Size(25, 20);
            this.gauSizeInput.TabIndex = 0;
            this.gauSizeInput.Text = "11";
            // 
            // progressUpdate
            // 
            this.progressUpdate.AutoSize = true;
            this.progressUpdate.Location = new System.Drawing.Point(1046, 11);
            this.progressUpdate.Name = "progressUpdate";
            this.progressUpdate.Size = new System.Drawing.Size(87, 13);
            this.progressUpdate.TabIndex = 11;
            this.progressUpdate.Text = "Waiting to start...";
            // 
            // maxDynRangeIn
            // 
            this.maxDynRangeIn.AutoSize = true;
            this.maxDynRangeIn.Location = new System.Drawing.Point(215, 41);
            this.maxDynRangeIn.Name = "maxDynRangeIn";
            this.maxDynRangeIn.Size = new System.Drawing.Size(21, 13);
            this.maxDynRangeIn.TabIndex = 14;
            this.maxDynRangeIn.Text = "##";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Maximum Contrast:";
            // 
            // inputHistogramBox
            // 
            this.inputHistogramBox.Controls.Add(this.maxValIn);
            this.inputHistogramBox.Controls.Add(this.minValIn);
            this.inputHistogramBox.Controls.Add(this.label12);
            this.inputHistogramBox.Controls.Add(this.label11);
            this.inputHistogramBox.Controls.Add(this.maxContrastIn);
            this.inputHistogramBox.Controls.Add(this.label9);
            this.inputHistogramBox.Controls.Add(this.label14);
            this.inputHistogramBox.Controls.Add(this.maxDynRangeIn);
            this.inputHistogramBox.Location = new System.Drawing.Point(12, 563);
            this.inputHistogramBox.Name = "inputHistogramBox";
            this.inputHistogramBox.Size = new System.Drawing.Size(251, 235);
            this.inputHistogramBox.TabIndex = 18;
            this.inputHistogramBox.TabStop = false;
            this.inputHistogramBox.Text = "Input Histogram Data";
            // 
            // maxValIn
            // 
            this.maxValIn.AutoSize = true;
            this.maxValIn.Location = new System.Drawing.Point(215, 113);
            this.maxValIn.Name = "maxValIn";
            this.maxValIn.Size = new System.Drawing.Size(21, 13);
            this.maxValIn.TabIndex = 23;
            this.maxValIn.Text = "##";
            // 
            // minValIn
            // 
            this.minValIn.AutoSize = true;
            this.minValIn.Location = new System.Drawing.Point(215, 88);
            this.minValIn.Name = "minValIn";
            this.minValIn.Size = new System.Drawing.Size(21, 13);
            this.minValIn.TabIndex = 22;
            this.minValIn.Text = "##";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Maximum Value:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 88);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "Minimum Value:";
            // 
            // maxContrastIn
            // 
            this.maxContrastIn.AutoSize = true;
            this.maxContrastIn.Location = new System.Drawing.Point(215, 65);
            this.maxContrastIn.Name = "maxContrastIn";
            this.maxContrastIn.Size = new System.Drawing.Size(21, 13);
            this.maxContrastIn.TabIndex = 19;
            this.maxContrastIn.Text = "##";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Maximum Dynamic Range:";
            // 
            // outputHistogramBox
            // 
            this.outputHistogramBox.Controls.Add(this.maxValOut);
            this.outputHistogramBox.Controls.Add(this.minValOut);
            this.outputHistogramBox.Controls.Add(this.label17);
            this.outputHistogramBox.Controls.Add(this.label18);
            this.outputHistogramBox.Controls.Add(this.maxContrastOut);
            this.outputHistogramBox.Controls.Add(this.label20);
            this.outputHistogramBox.Controls.Add(this.label21);
            this.outputHistogramBox.Controls.Add(this.maxDynRangeOut);
            this.outputHistogramBox.Location = new System.Drawing.Point(793, 563);
            this.outputHistogramBox.Name = "outputHistogramBox";
            this.outputHistogramBox.Size = new System.Drawing.Size(251, 235);
            this.outputHistogramBox.TabIndex = 19;
            this.outputHistogramBox.TabStop = false;
            this.outputHistogramBox.Text = "Output Histogram Data";
            // 
            // maxValOut
            // 
            this.maxValOut.AutoSize = true;
            this.maxValOut.Location = new System.Drawing.Point(215, 113);
            this.maxValOut.Name = "maxValOut";
            this.maxValOut.Size = new System.Drawing.Size(21, 13);
            this.maxValOut.TabIndex = 23;
            this.maxValOut.Text = "##";
            // 
            // minValOut
            // 
            this.minValOut.AutoSize = true;
            this.minValOut.Location = new System.Drawing.Point(215, 88);
            this.minValOut.Name = "minValOut";
            this.minValOut.Size = new System.Drawing.Size(21, 13);
            this.minValOut.TabIndex = 22;
            this.minValOut.Text = "##";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 113);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(84, 13);
            this.label17.TabIndex = 21;
            this.label17.Text = "Maximum Value:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 88);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(81, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Minimum Value:";
            // 
            // maxContrastOut
            // 
            this.maxContrastOut.AutoSize = true;
            this.maxContrastOut.Location = new System.Drawing.Point(215, 65);
            this.maxContrastOut.Name = "maxContrastOut";
            this.maxContrastOut.Size = new System.Drawing.Size(21, 13);
            this.maxContrastOut.TabIndex = 19;
            this.maxContrastOut.Text = "##";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 41);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(133, 13);
            this.label20.TabIndex = 18;
            this.label20.Text = "Maximum Dynamic Range:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 65);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(96, 13);
            this.label21.TabIndex = 17;
            this.label21.Text = "Maximum Contrast:";
            // 
            // maxDynRangeOut
            // 
            this.maxDynRangeOut.AutoSize = true;
            this.maxDynRangeOut.Location = new System.Drawing.Point(215, 41);
            this.maxDynRangeOut.Name = "maxDynRangeOut";
            this.maxDynRangeOut.Size = new System.Drawing.Size(21, 13);
            this.maxDynRangeOut.TabIndex = 14;
            this.maxDynRangeOut.Text = "##";
            // 
            // colourOptionsBox
            // 
            this.colourOptionsBox.Controls.Add(this.label16);
            this.colourOptionsBox.Controls.Add(this.label15);
            this.colourOptionsBox.Controls.Add(this.label13);
            this.colourOptionsBox.Controls.Add(this.blueMixIn);
            this.colourOptionsBox.Controls.Add(this.greenMixIn);
            this.colourOptionsBox.Controls.Add(this.redMixIn);
            this.colourOptionsBox.Location = new System.Drawing.Point(21, 300);
            this.colourOptionsBox.Name = "colourOptionsBox";
            this.colourOptionsBox.Size = new System.Drawing.Size(200, 56);
            this.colourOptionsBox.TabIndex = 16;
            this.colourOptionsBox.TabStop = false;
            this.colourOptionsBox.Text = "Colour";
            // 
            // redMixIn
            // 
            this.redMixIn.Location = new System.Drawing.Point(31, 19);
            this.redMixIn.Name = "redMixIn";
            this.redMixIn.Size = new System.Drawing.Size(31, 20);
            this.redMixIn.TabIndex = 0;
            this.redMixIn.Text = "0.3";
            // 
            // greenMixIn
            // 
            this.greenMixIn.Location = new System.Drawing.Point(97, 19);
            this.greenMixIn.Name = "greenMixIn";
            this.greenMixIn.Size = new System.Drawing.Size(31, 20);
            this.greenMixIn.TabIndex = 1;
            this.greenMixIn.Text = "0.59";
            // 
            // blueMixIn
            // 
            this.blueMixIn.Location = new System.Drawing.Point(163, 19);
            this.blueMixIn.Name = "blueMixIn";
            this.blueMixIn.Size = new System.Drawing.Size(31, 20);
            this.blueMixIn.TabIndex = 2;
            this.blueMixIn.Text = "0.11";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(18, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "R:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(139, 22);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 13);
            this.label15.TabIndex = 4;
            this.label15.Text = "B:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(73, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(18, 13);
            this.label16.TabIndex = 5;
            this.label16.Text = "G:";
            // 
            // INFOIBV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 801);
            this.Controls.Add(this.outputHistogramBox);
            this.Controls.Add(this.inputHistogramBox);
            this.Controls.Add(this.progressUpdate);
            this.Controls.Add(this.optionsBox);
            this.Controls.Add(this.outputHistogram);
            this.Controls.Add(this.inputHistogram);
            this.Controls.Add(this.functionSelector);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.imageFileName);
            this.Controls.Add(this.LoadImageButton);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "INFOIBV";
            this.ShowIcon = false;
            this.Text = "INFOIBV";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputHistogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputHistogram)).EndInit();
            this.optionsBox.ResumeLayout(false);
            this.optionsBox.PerformLayout();
            this.edgeBox.ResumeLayout(false);
            this.edgeBox.PerformLayout();
            this.caBox.ResumeLayout(false);
            this.caBox.PerformLayout();
            this.inputHistogramBox.ResumeLayout(false);
            this.inputHistogramBox.PerformLayout();
            this.outputHistogramBox.ResumeLayout(false);
            this.outputHistogramBox.PerformLayout();
            this.colourOptionsBox.ResumeLayout(false);
            this.colourOptionsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Provided
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.TextBox imageFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.SaveFileDialog saveImageDialog;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ProgressBar progressBar;
        // New
        private System.Windows.Forms.ComboBox functionSelector;
        private System.Windows.Forms.DataVisualization.Charting.Chart inputHistogram;
        private System.Windows.Forms.DataVisualization.Charting.Chart outputHistogram;
        private System.Windows.Forms.GroupBox optionsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gauSigmaInput;
        private System.Windows.Forms.TextBox gauSizeInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox medSizeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox thresholdInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label progressUpdate;
        private System.Windows.Forms.GroupBox caBox;
        private System.Windows.Forms.TextBox highQInput;
        private System.Windows.Forms.TextBox lowQInput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox useMAC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label maxDynRangeIn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox inputHistogramBox;
        private System.Windows.Forms.Label maxValIn;
        private System.Windows.Forms.Label minValIn;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label maxContrastIn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox outputHistogramBox;
        private System.Windows.Forms.Label maxValOut;
        private System.Windows.Forms.Label minValOut;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label maxContrastOut;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label maxDynRangeOut;
        private System.Windows.Forms.TextBox edgeSharpW;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox edgeGaussianCheck;
        private System.Windows.Forms.GroupBox edgeBox;
        private System.Windows.Forms.CheckBox edPipeline;
        private System.Windows.Forms.GroupBox colourOptionsBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox blueMixIn;
        private System.Windows.Forms.TextBox greenMixIn;
        private System.Windows.Forms.TextBox redMixIn;
    }
}

