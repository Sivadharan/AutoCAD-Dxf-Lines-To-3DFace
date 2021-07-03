namespace DxfTo3DFace
{
    partial class frmLineTo3DFace
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
            this.btnConvert = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.lblFileName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpOutputType = new System.Windows.Forms.GroupBox();
            this.rad3DFaceFile = new System.Windows.Forms.RadioButton();
            this.radCopyFile = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            this.grpOutputType.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(424, 75);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(66, 33);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(433, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(9, 36);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(57, 13);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "File Name:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(505, 31);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(45, 23);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "...";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 113);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(560, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // grpOutputType
            // 
            this.grpOutputType.Controls.Add(this.radCopyFile);
            this.grpOutputType.Controls.Add(this.rad3DFaceFile);
            this.grpOutputType.Location = new System.Drawing.Point(66, 59);
            this.grpOutputType.Name = "grpOutputType";
            this.grpOutputType.Size = new System.Drawing.Size(289, 51);
            this.grpOutputType.TabIndex = 5;
            this.grpOutputType.TabStop = false;
            this.grpOutputType.Text = "Output Type";
            // 
            // rad3DFaceFile
            // 
            this.rad3DFaceFile.AutoSize = true;
            this.rad3DFaceFile.Checked = true;
            this.rad3DFaceFile.Location = new System.Drawing.Point(15, 22);
            this.rad3DFaceFile.Name = "rad3DFaceFile";
            this.rad3DFaceFile.Size = new System.Drawing.Size(82, 17);
            this.rad3DFaceFile.TabIndex = 0;
            this.rad3DFaceFile.TabStop = true;
            this.rad3DFaceFile.Text = "3DFace File";
            this.rad3DFaceFile.UseVisualStyleBackColor = true;
            // 
            // radCopyFile
            // 
            this.radCopyFile.AutoSize = true;
            this.radCopyFile.Location = new System.Drawing.Point(103, 22);
            this.radCopyFile.Name = "radCopyFile";
            this.radCopyFile.Size = new System.Drawing.Size(171, 17);
            this.radCopyFile.TabIndex = 1;
            this.radCopyFile.Text = "Copy of input file with 3DFaces";
            this.radCopyFile.UseVisualStyleBackColor = true;
            // 
            // frmLineTo3DFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 135);
            this.Controls.Add(this.grpOutputType);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmLineTo3DFace";
            this.Text = "AutoCAD Dxf Line To 3DFace";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpOutputType.ResumeLayout(false);
            this.grpOutputType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.GroupBox grpOutputType;
        private System.Windows.Forms.RadioButton rad3DFaceFile;
        private System.Windows.Forms.RadioButton radCopyFile;
    }
}

