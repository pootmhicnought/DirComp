namespace DirCompCS
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.cbDiffDetails = new System.Windows.Forms.CheckBox();
            this.sbPath2 = new System.Windows.Forms.Button();
            this.sbPath1 = new System.Windows.Forms.Button();
            this.edPath2 = new System.Windows.Forms.TextBox();
            this.edPath1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.memoReport = new System.Windows.Forms.RichTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.lboxDest = new System.Windows.Forms.ListBox();
            this.lboxSource = new System.Windows.Forms.ListBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbarFile = new System.Windows.Forms.ProgressBar();
            this.pbarProcess = new System.Windows.Forms.ProgressBar();
            this.lblFileProgress = new System.Windows.Forms.Label();
            this.lblProcessProgress = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.cbDiffDetails);
            this.panelTop.Controls.Add(this.sbPath2);
            this.panelTop.Controls.Add(this.sbPath1);
            this.panelTop.Controls.Add(this.edPath2);
            this.panelTop.Controls.Add(this.edPath1);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(704, 105);
            this.panelTop.TabIndex = 0;
            // 
            // cbDiffDetails
            // 
            this.cbDiffDetails.AutoSize = true;
            this.cbDiffDetails.Location = new System.Drawing.Point(72, 78);
            this.cbDiffDetails.Name = "cbDiffDetails";
            this.cbDiffDetails.Size = new System.Drawing.Size(124, 17);
            this.cbDiffDetails.TabIndex = 6;
            this.cbDiffDetails.Text = "Output Diff Details";
            this.cbDiffDetails.UseVisualStyleBackColor = true;
            // 
            // sbPath2
            // 
            this.sbPath2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbPath2.Location = new System.Drawing.Point(634, 51);
            this.sbPath2.Name = "sbPath2";
            this.sbPath2.Size = new System.Drawing.Size(23, 22);
            this.sbPath2.TabIndex = 5;
            this.sbPath2.Text = "...";
            this.sbPath2.UseVisualStyleBackColor = true;
            this.sbPath2.Click += new System.EventHandler(this.sbPath2_Click);
            // 
            // sbPath1
            // 
            this.sbPath1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sbPath1.Location = new System.Drawing.Point(634, 23);
            this.sbPath1.Name = "sbPath1";
            this.sbPath1.Size = new System.Drawing.Size(23, 22);
            this.sbPath1.TabIndex = 4;
            this.sbPath1.Text = "...";
            this.sbPath1.UseVisualStyleBackColor = true;
            this.sbPath1.Click += new System.EventHandler(this.sbPath1_Click);
            // 
            // edPath2
            // 
            this.edPath2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edPath2.Location = new System.Drawing.Point(72, 51);
            this.edPath2.Name = "edPath2";
            this.edPath2.Size = new System.Drawing.Size(556, 20);
            this.edPath2.TabIndex = 3;
            // 
            // edPath1
            // 
            this.edPath1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edPath1.Location = new System.Drawing.Point(72, 24);
            this.edPath1.Name = "edPath1";
            this.edPath1.Size = new System.Drawing.Size(556, 20);
            this.edPath1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Path 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path 1";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.memoReport);
            this.panelMain.Controls.Add(this.splitter1);
            this.panelMain.Controls.Add(this.lboxDest);
            this.panelMain.Controls.Add(this.lboxSource);
            this.panelMain.Controls.Add(this.panelBottom);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 105);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(704, 471);
            this.panelMain.TabIndex = 1;
            // 
            // memoReport
            // 
            this.memoReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoReport.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoReport.Location = new System.Drawing.Point(300, 0);
            this.memoReport.Name = "memoReport";
            this.memoReport.ReadOnly = true;
            this.memoReport.Size = new System.Drawing.Size(404, 399);
            this.memoReport.TabIndex = 4;
            this.memoReport.Text = "";
            this.memoReport.Visible = false;
            this.memoReport.WordWrap = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(297, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 399);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // lboxDest
            // 
            this.lboxDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lboxDest.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxDest.FormattingEnabled = true;
            this.lboxDest.ItemHeight = 14;
            this.lboxDest.Location = new System.Drawing.Point(300, 0);
            this.lboxDest.Name = "lboxDest";
            this.lboxDest.Size = new System.Drawing.Size(404, 399);
            this.lboxDest.TabIndex = 2;
            // 
            // lboxSource
            // 
            this.lboxSource.Dock = System.Windows.Forms.DockStyle.Left;
            this.lboxSource.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxSource.FormattingEnabled = true;
            this.lboxSource.ItemHeight = 14;
            this.lboxSource.Location = new System.Drawing.Point(0, 0);
            this.lboxSource.Name = "lboxSource";
            this.lboxSource.Size = new System.Drawing.Size(297, 399);
            this.lboxSource.TabIndex = 1;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.statusBar);
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.pbarFile);
            this.panelBottom.Controls.Add(this.pbarProcess);
            this.panelBottom.Controls.Add(this.lblFileProgress);
            this.panelBottom.Controls.Add(this.lblProcessProgress);
            this.panelBottom.Controls.Add(this.btnGo);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 399);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(704, 72);
            this.panelBottom.TabIndex = 0;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMain,
            this.statusLabelCount});
            this.statusBar.Location = new System.Drawing.Point(0, 50);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(704, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "statusStrip1";
            // 
            // statusLabelMain
            // 
            this.statusLabelMain.Name = "statusLabelMain";
            this.statusLabelMain.Size = new System.Drawing.Size(39, 17);
            this.statusLabelMain.Text = "Ready";
            this.statusLabelMain.Width = 500;
            // 
            // statusLabelCount
            // 
            this.statusLabelCount.Name = "statusLabelCount";
            this.statusLabelCount.Size = new System.Drawing.Size(13, 17);
            this.statusLabelCount.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(8, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbarFile
            // 
            this.pbarFile.Location = new System.Drawing.Point(224, 32);
            this.pbarFile.Name = "pbarFile";
            this.pbarFile.Size = new System.Drawing.Size(353, 16);
            this.pbarFile.TabIndex = 4;
            // 
            // pbarProcess
            // 
            this.pbarProcess.Location = new System.Drawing.Point(224, 16);
            this.pbarProcess.Name = "pbarProcess";
            this.pbarProcess.Size = new System.Drawing.Size(353, 16);
            this.pbarProcess.TabIndex = 3;
            // 
            // lblFileProgress
            // 
            this.lblFileProgress.AutoSize = true;
            this.lblFileProgress.Location = new System.Drawing.Point(128, 32);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new System.Drawing.Size(69, 13);
            this.lblFileProgress.TabIndex = 2;
            this.lblFileProgress.Text = "File Progress";
            // 
            // lblProcessProgress
            // 
            this.lblProcessProgress.AutoSize = true;
            this.lblProcessProgress.Location = new System.Drawing.Point(128, 16);
            this.lblProcessProgress.Name = "lblProcessProgress";
            this.lblProcessProgress.Size = new System.Drawing.Size(88, 13);
            this.lblProcessProgress.TabIndex = 1;
            this.lblProcessProgress.Text = "Process Progress";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(8, 16);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 25);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 576);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dir Compare";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edPath1;
        private System.Windows.Forms.TextBox edPath2;
        private System.Windows.Forms.Button sbPath1;
        private System.Windows.Forms.Button sbPath2;
        private System.Windows.Forms.CheckBox cbDiffDetails;
        private System.Windows.Forms.ListBox lboxSource;
        private System.Windows.Forms.ListBox lboxDest;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox memoReport;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProcessProgress;
        private System.Windows.Forms.Label lblFileProgress;
        private System.Windows.Forms.ProgressBar pbarProcess;
        private System.Windows.Forms.ProgressBar pbarFile;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMain;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelCount;
    }
}
