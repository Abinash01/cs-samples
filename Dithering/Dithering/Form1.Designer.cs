namespace Dithering
{
   partial class Form1
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
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.groupBox6 = new System.Windows.Forms.GroupBox();
         this.cbColorMatching = new System.Windows.Forms.ComboBox();
         this.groupBox5 = new System.Windows.Forms.GroupBox();
         this.nudSharpness = new System.Windows.Forms.NumericUpDown();
         this.groupBox4 = new System.Windows.Forms.GroupBox();
         this.nudGamma = new System.Windows.Forms.NumericUpDown();
         this.groupBox3 = new System.Windows.Forms.GroupBox();
         this.cbDitheringMethod = new System.Windows.Forms.ComboBox();
         this.groupBox2 = new System.Windows.Forms.GroupBox();
         this.cbPalette = new System.Windows.Forms.ComboBox();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.lblWidthHeight = new System.Windows.Forms.Label();
         this.btnLoad = new System.Windows.Forms.Button();
         this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
         this.rpbProcessed = new Leadtools.Controls.RasterPictureBox();
         this.rpbOriginal = new Leadtools.Controls.RasterPictureBox();
         this.rpbPreProcessed = new Leadtools.Controls.RasterPictureBox();
         this.rpbNormalScale = new Leadtools.Controls.RasterPictureBox();
         this.nudMaxPixels = new System.Windows.Forms.NumericUpDown();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.groupBox6.SuspendLayout();
         this.groupBox5.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudSharpness)).BeginInit();
         this.groupBox4.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).BeginInit();
         this.groupBox3.SuspendLayout();
         this.groupBox2.SuspendLayout();
         this.groupBox1.SuspendLayout();
         this.tableLayoutPanel1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.nudMaxPixels)).BeginInit();
         this.SuspendLayout();
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.groupBox6);
         this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
         this.splitContainer1.Panel1.Controls.Add(this.groupBox4);
         this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
         this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
         this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
         this.splitContainer1.Panel1.Controls.Add(this.btnLoad);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
         this.splitContainer1.Size = new System.Drawing.Size(657, 468);
         this.splitContainer1.SplitterDistance = 180;
         this.splitContainer1.TabIndex = 0;
         // 
         // groupBox6
         // 
         this.groupBox6.Controls.Add(this.cbColorMatching);
         this.groupBox6.Location = new System.Drawing.Point(12, 262);
         this.groupBox6.Name = "groupBox6";
         this.groupBox6.Size = new System.Drawing.Size(151, 55);
         this.groupBox6.TabIndex = 14;
         this.groupBox6.TabStop = false;
         this.groupBox6.Text = "Color Matching";
         // 
         // cbColorMatching
         // 
         this.cbColorMatching.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbColorMatching.FormattingEnabled = true;
         this.cbColorMatching.Items.AddRange(new object[] {
            "Euclidean"});
         this.cbColorMatching.Location = new System.Drawing.Point(6, 19);
         this.cbColorMatching.Name = "cbColorMatching";
         this.cbColorMatching.Size = new System.Drawing.Size(139, 21);
         this.cbColorMatching.TabIndex = 5;
         this.cbColorMatching.SelectedIndexChanged += new System.EventHandler(this.cbColorMatching_SelectedIndexChanged);
         // 
         // groupBox5
         // 
         this.groupBox5.Controls.Add(this.nudSharpness);
         this.groupBox5.Location = new System.Drawing.Point(12, 385);
         this.groupBox5.Name = "groupBox5";
         this.groupBox5.Size = new System.Drawing.Size(151, 69);
         this.groupBox5.TabIndex = 13;
         this.groupBox5.TabStop = false;
         this.groupBox5.Text = "Sharpness";
         // 
         // nudSharpness
         // 
         this.nudSharpness.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
         this.nudSharpness.Location = new System.Drawing.Point(9, 24);
         this.nudSharpness.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
         this.nudSharpness.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
         this.nudSharpness.Name = "nudSharpness";
         this.nudSharpness.Size = new System.Drawing.Size(133, 20);
         this.nudSharpness.TabIndex = 11;
         this.nudSharpness.ValueChanged += new System.EventHandler(this.nudSharpness_ValueChanged);
         // 
         // groupBox4
         // 
         this.groupBox4.Controls.Add(this.nudGamma);
         this.groupBox4.Location = new System.Drawing.Point(12, 323);
         this.groupBox4.Name = "groupBox4";
         this.groupBox4.Size = new System.Drawing.Size(151, 56);
         this.groupBox4.TabIndex = 12;
         this.groupBox4.TabStop = false;
         this.groupBox4.Text = "Gamma";
         // 
         // nudGamma
         // 
         this.nudGamma.DecimalPlaces = 2;
         this.nudGamma.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
         this.nudGamma.Location = new System.Drawing.Point(6, 19);
         this.nudGamma.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
         this.nudGamma.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
         this.nudGamma.Name = "nudGamma";
         this.nudGamma.Size = new System.Drawing.Size(133, 20);
         this.nudGamma.TabIndex = 10;
         this.nudGamma.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.nudGamma.ValueChanged += new System.EventHandler(this.nudGamma_ValueChanged);
         // 
         // groupBox3
         // 
         this.groupBox3.Controls.Add(this.cbDitheringMethod);
         this.groupBox3.Location = new System.Drawing.Point(12, 124);
         this.groupBox3.Name = "groupBox3";
         this.groupBox3.Size = new System.Drawing.Size(151, 63);
         this.groupBox3.TabIndex = 11;
         this.groupBox3.TabStop = false;
         this.groupBox3.Text = "Dithering";
         // 
         // cbDitheringMethod
         // 
         this.cbDitheringMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbDitheringMethod.FormattingEnabled = true;
         this.cbDitheringMethod.Location = new System.Drawing.Point(6, 19);
         this.cbDitheringMethod.Name = "cbDitheringMethod";
         this.cbDitheringMethod.Size = new System.Drawing.Size(139, 21);
         this.cbDitheringMethod.TabIndex = 3;
         this.cbDitheringMethod.SelectedIndexChanged += new System.EventHandler(this.cbDitheringMethod_SelectedIndexChanged);
         // 
         // groupBox2
         // 
         this.groupBox2.Controls.Add(this.cbPalette);
         this.groupBox2.Location = new System.Drawing.Point(12, 193);
         this.groupBox2.Name = "groupBox2";
         this.groupBox2.Size = new System.Drawing.Size(151, 63);
         this.groupBox2.TabIndex = 10;
         this.groupBox2.TabStop = false;
         this.groupBox2.Text = "Palette";
         // 
         // cbPalette
         // 
         this.cbPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbPalette.FormattingEnabled = true;
         this.cbPalette.Location = new System.Drawing.Point(6, 19);
         this.cbPalette.Name = "cbPalette";
         this.cbPalette.Size = new System.Drawing.Size(139, 21);
         this.cbPalette.TabIndex = 4;
         this.cbPalette.SelectedIndexChanged += new System.EventHandler(this.cbPalette_SelectedIndexChanged);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.nudMaxPixels);
         this.groupBox1.Controls.Add(this.lblWidthHeight);
         this.groupBox1.Location = new System.Drawing.Point(12, 41);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(151, 77);
         this.groupBox1.TabIndex = 1;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Size Info";
         // 
         // lblWidthHeight
         // 
         this.lblWidthHeight.AutoSize = true;
         this.lblWidthHeight.Location = new System.Drawing.Point(6, 42);
         this.lblWidthHeight.Name = "lblWidthHeight";
         this.lblWidthHeight.Size = new System.Drawing.Size(35, 13);
         this.lblWidthHeight.TabIndex = 1;
         this.lblWidthHeight.Text = "label1";
         // 
         // btnLoad
         // 
         this.btnLoad.Location = new System.Drawing.Point(12, 12);
         this.btnLoad.Name = "btnLoad";
         this.btnLoad.Size = new System.Drawing.Size(75, 23);
         this.btnLoad.TabIndex = 0;
         this.btnLoad.Text = "Load";
         this.btnLoad.UseVisualStyleBackColor = true;
         this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
         // 
         // tableLayoutPanel1
         // 
         this.tableLayoutPanel1.ColumnCount = 2;
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Controls.Add(this.rpbProcessed, 0, 0);
         this.tableLayoutPanel1.Controls.Add(this.rpbOriginal, 1, 0);
         this.tableLayoutPanel1.Controls.Add(this.rpbPreProcessed, 1, 1);
         this.tableLayoutPanel1.Controls.Add(this.rpbNormalScale, 0, 1);
         this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
         this.tableLayoutPanel1.Name = "tableLayoutPanel1";
         this.tableLayoutPanel1.RowCount = 2;
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
         this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 468);
         this.tableLayoutPanel1.TabIndex = 0;
         // 
         // rpbProcessed
         // 
         this.rpbProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rpbProcessed.Location = new System.Drawing.Point(3, 3);
         this.rpbProcessed.Name = "rpbProcessed";
         this.rpbProcessed.Size = new System.Drawing.Size(230, 228);
         this.rpbProcessed.SizeMode = Leadtools.Controls.RasterPictureBoxSizeMode.Fit;
         this.rpbProcessed.TabIndex = 0;
         this.rpbProcessed.TabStop = false;
         // 
         // rpbOriginal
         // 
         this.rpbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rpbOriginal.Location = new System.Drawing.Point(239, 3);
         this.rpbOriginal.Name = "rpbOriginal";
         this.rpbOriginal.Size = new System.Drawing.Size(231, 228);
         this.rpbOriginal.SizeMode = Leadtools.Controls.RasterPictureBoxSizeMode.Fit;
         this.rpbOriginal.TabIndex = 1;
         this.rpbOriginal.TabStop = false;
         // 
         // rpbPreProcessed
         // 
         this.rpbPreProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rpbPreProcessed.Location = new System.Drawing.Point(239, 237);
         this.rpbPreProcessed.Name = "rpbPreProcessed";
         this.rpbPreProcessed.Size = new System.Drawing.Size(231, 228);
         this.rpbPreProcessed.SizeMode = Leadtools.Controls.RasterPictureBoxSizeMode.Fit;
         this.rpbPreProcessed.TabIndex = 4;
         this.rpbPreProcessed.TabStop = false;
         // 
         // rpbNormalScale
         // 
         this.rpbNormalScale.Anchor = System.Windows.Forms.AnchorStyles.None;
         this.rpbNormalScale.Location = new System.Drawing.Point(3, 237);
         this.rpbNormalScale.Name = "rpbNormalScale";
         this.rpbNormalScale.Size = new System.Drawing.Size(230, 228);
         this.rpbNormalScale.SizeMode = Leadtools.Controls.RasterPictureBoxSizeMode.CenterImage;
         this.rpbNormalScale.TabIndex = 5;
         this.rpbNormalScale.TabStop = false;
         // 
         // nudMaxPixels
         // 
         this.nudMaxPixels.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
         this.nudMaxPixels.Location = new System.Drawing.Point(6, 19);
         this.nudMaxPixels.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
         this.nudMaxPixels.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
         this.nudMaxPixels.Name = "nudMaxPixels";
         this.nudMaxPixels.Size = new System.Drawing.Size(133, 20);
         this.nudMaxPixels.TabIndex = 2;
         this.nudMaxPixels.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
         this.nudMaxPixels.ValueChanged += new System.EventHandler(this.nudMaxPixels_ValueChanged);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(657, 468);
         this.Controls.Add(this.splitContainer1);
         this.Name = "Form1";
         this.Text = "Intro to Dithering";
         this.Load += new System.EventHandler(this.Form1_Load);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.groupBox6.ResumeLayout(false);
         this.groupBox5.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.nudSharpness)).EndInit();
         this.groupBox4.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.nudGamma)).EndInit();
         this.groupBox3.ResumeLayout(false);
         this.groupBox2.ResumeLayout(false);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         this.tableLayoutPanel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.nudMaxPixels)).EndInit();
         this.ResumeLayout(false);

      }

     

      #endregion

      private System.Windows.Forms.SplitContainer splitContainer1;
      private System.Windows.Forms.Button btnLoad;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label lblWidthHeight;
      private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
      private Leadtools.Controls.RasterPictureBox rpbProcessed;
      private Leadtools.Controls.RasterPictureBox rpbOriginal;
      private System.Windows.Forms.GroupBox groupBox5;
      private System.Windows.Forms.GroupBox groupBox4;
      public System.Windows.Forms.NumericUpDown nudGamma;
      private System.Windows.Forms.GroupBox groupBox3;
      private System.Windows.Forms.ComboBox cbDitheringMethod;
      private System.Windows.Forms.GroupBox groupBox2;
      private System.Windows.Forms.ComboBox cbPalette;
      public System.Windows.Forms.NumericUpDown nudSharpness;
      private Leadtools.Controls.RasterPictureBox rpbPreProcessed;
      private Leadtools.Controls.RasterPictureBox rpbNormalScale;
      private System.Windows.Forms.GroupBox groupBox6;
      private System.Windows.Forms.ComboBox cbColorMatching;
      private System.Windows.Forms.NumericUpDown nudMaxPixels;
   }
}

