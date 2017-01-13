namespace Pdf_Highlight_Words
{
   partial class MainForm
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
         this.btnOpen = new System.Windows.Forms.Button();
         this.lstWords = new System.Windows.Forms.ListBox();
         this.chkFit = new System.Windows.Forms.CheckBox();
         this.rasterImageViewer1 = new Leadtools.WinForms.RasterImageViewer();
         this.txtPages = new System.Windows.Forms.NumericUpDown();
         this.label1 = new System.Windows.Forms.Label();
         this.tabControl1 = new System.Windows.Forms.TabControl();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.tabPage2 = new System.Windows.Forms.TabPage();
         this.lstCharacters = new System.Windows.Forms.ListBox();
         this.txtBounds = new System.Windows.Forms.TextBox();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         ((System.ComponentModel.ISupportInitialize)(this.txtPages)).BeginInit();
         this.tabControl1.SuspendLayout();
         this.tabPage1.SuspendLayout();
         this.tabPage2.SuspendLayout();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnOpen
         // 
         this.btnOpen.Location = new System.Drawing.Point(47, 3);
         this.btnOpen.Name = "btnOpen";
         this.btnOpen.Size = new System.Drawing.Size(154, 47);
         this.btnOpen.TabIndex = 0;
         this.btnOpen.Text = "Open...";
         this.btnOpen.UseVisualStyleBackColor = true;
         this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
         // 
         // lstWords
         // 
         this.lstWords.Dock = System.Windows.Forms.DockStyle.Fill;
         this.lstWords.FormattingEnabled = true;
         this.lstWords.Location = new System.Drawing.Point(3, 3);
         this.lstWords.Name = "lstWords";
         this.lstWords.Size = new System.Drawing.Size(186, 316);
         this.lstWords.TabIndex = 1;
         this.lstWords.SelectedIndexChanged += new System.EventHandler(this.lstWords_SelectedIndexChanged);
         // 
         // chkFit
         // 
         this.chkFit.AutoSize = true;
         this.chkFit.Location = new System.Drawing.Point(4, 33);
         this.chkFit.Name = "chkFit";
         this.chkFit.Size = new System.Drawing.Size(37, 17);
         this.chkFit.TabIndex = 2;
         this.chkFit.Text = "Fit";
         this.chkFit.UseVisualStyleBackColor = true;
         this.chkFit.CheckedChanged += new System.EventHandler(this.chkFit_CheckedChanged);
         // 
         // rasterImageViewer1
         // 
         this.rasterImageViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.rasterImageViewer1.Location = new System.Drawing.Point(0, 0);
         this.rasterImageViewer1.Name = "rasterImageViewer1";
         this.rasterImageViewer1.Size = new System.Drawing.Size(451, 575);
         this.rasterImageViewer1.TabIndex = 3;
         // 
         // txtPages
         // 
         this.txtPages.Location = new System.Drawing.Point(123, 56);
         this.txtPages.Name = "txtPages";
         this.txtPages.Size = new System.Drawing.Size(78, 20);
         this.txtPages.TabIndex = 4;
         this.txtPages.ValueChanged += new System.EventHandler(this.txtPages_ValueChanged);
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(82, 63);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(37, 13);
         this.label1.TabIndex = 5;
         this.label1.Text = "Pages";
         // 
         // tabControl1
         // 
         this.tabControl1.Controls.Add(this.tabPage1);
         this.tabControl1.Controls.Add(this.tabPage2);
         this.tabControl1.Location = new System.Drawing.Point(4, 82);
         this.tabControl1.Name = "tabControl1";
         this.tabControl1.SelectedIndex = 0;
         this.tabControl1.Size = new System.Drawing.Size(200, 360);
         this.tabControl1.TabIndex = 6;
         // 
         // tabPage1
         // 
         this.tabPage1.Controls.Add(this.lstWords);
         this.tabPage1.Location = new System.Drawing.Point(4, 22);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage1.Size = new System.Drawing.Size(192, 334);
         this.tabPage1.TabIndex = 0;
         this.tabPage1.Text = "Words";
         this.tabPage1.UseVisualStyleBackColor = true;
         // 
         // tabPage2
         // 
         this.tabPage2.Controls.Add(this.lstCharacters);
         this.tabPage2.Location = new System.Drawing.Point(4, 22);
         this.tabPage2.Name = "tabPage2";
         this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
         this.tabPage2.Size = new System.Drawing.Size(192, 334);
         this.tabPage2.TabIndex = 1;
         this.tabPage2.Text = "Characters";
         this.tabPage2.UseVisualStyleBackColor = true;
         // 
         // lstCharacters
         // 
         this.lstCharacters.Dock = System.Windows.Forms.DockStyle.Fill;
         this.lstCharacters.FormattingEnabled = true;
         this.lstCharacters.Location = new System.Drawing.Point(3, 3);
         this.lstCharacters.Name = "lstCharacters";
         this.lstCharacters.Size = new System.Drawing.Size(186, 316);
         this.lstCharacters.TabIndex = 0;
         this.lstCharacters.SelectedIndexChanged += new System.EventHandler(this.lstCharacters_SelectedIndexChanged);
         // 
         // txtBounds
         // 
         this.txtBounds.Location = new System.Drawing.Point(4, 443);
         this.txtBounds.Name = "txtBounds";
         this.txtBounds.Size = new System.Drawing.Size(197, 20);
         this.txtBounds.TabIndex = 7;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
         this.splitContainer1.IsSplitterFixed = true;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.btnOpen);
         this.splitContainer1.Panel1.Controls.Add(this.txtBounds);
         this.splitContainer1.Panel1.Controls.Add(this.chkFit);
         this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
         this.splitContainer1.Panel1.Controls.Add(this.txtPages);
         this.splitContainer1.Panel1.Controls.Add(this.label1);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.rasterImageViewer1);
         this.splitContainer1.Size = new System.Drawing.Size(662, 575);
         this.splitContainer1.SplitterDistance = 207;
         this.splitContainer1.TabIndex = 8;
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(662, 575);
         this.Controls.Add(this.splitContainer1);
         this.Name = "MainForm";
         this.Text = "Highlight - PDFDocument";
         ((System.ComponentModel.ISupportInitialize)(this.txtPages)).EndInit();
         this.tabControl1.ResumeLayout(false);
         this.tabPage1.ResumeLayout(false);
         this.tabPage2.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel1.PerformLayout();
         this.splitContainer1.Panel2.ResumeLayout(false);
         this.splitContainer1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnOpen;
      private System.Windows.Forms.ListBox lstWords;
      private System.Windows.Forms.CheckBox chkFit;
      private Leadtools.WinForms.RasterImageViewer rasterImageViewer1;
      private System.Windows.Forms.NumericUpDown txtPages;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TabControl tabControl1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.TabPage tabPage2;
      private System.Windows.Forms.TextBox txtBounds;
      private System.Windows.Forms.ListBox lstCharacters;
      private System.Windows.Forms.SplitContainer splitContainer1;
   }
}

