using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;

namespace EOS.UI
{
    partial class frmMDI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.sOLUZIONIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sOLVENTIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rETTEDITARATURAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sOLUZIONIToolStripMenuItem,
            this.sOLVENTIToolStripMenuItem,
            this.rETTEDITARATURAToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(147, 450);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // sOLUZIONIToolStripMenuItem
            // 
            this.sOLUZIONIToolStripMenuItem.Name = "sOLUZIONIToolStripMenuItem";
            this.sOLUZIONIToolStripMenuItem.Size = new System.Drawing.Size(134, 19);
            this.sOLUZIONIToolStripMenuItem.Text = "SOLUZIONI MR";
            this.sOLUZIONIToolStripMenuItem.Click += new System.EventHandler(this.sOLUZIONIToolStripMenuItem_Click);
            // 
            // sOLVENTIToolStripMenuItem
            // 
            this.sOLVENTIToolStripMenuItem.Name = "sOLVENTIToolStripMenuItem";
            this.sOLVENTIToolStripMenuItem.Size = new System.Drawing.Size(134, 19);
            this.sOLVENTIToolStripMenuItem.Text = "SOLUZIONI DI LAVORO";
            this.sOLVENTIToolStripMenuItem.Click += new System.EventHandler(this.sOLVENTIToolStripMenuItem_Click);
            // 
            // rETTEDITARATURAToolStripMenuItem
            // 
            this.rETTEDITARATURAToolStripMenuItem.Name = "rETTEDITARATURAToolStripMenuItem";
            this.rETTEDITARATURAToolStripMenuItem.Size = new System.Drawing.Size(134, 19);
            this.rETTEDITARATURAToolStripMenuItem.Text = "RETTE DI TARATURA";
            this.rETTEDITARATURAToolStripMenuItem.Click += new System.EventHandler(this.rETTEDITARATURAToolStripMenuItem_Click);
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // frmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMDI";
            this.Text = "EOS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sOLUZIONIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sOLVENTIToolStripMenuItem;
        private XtraTabbedMdiManager xtraTabbedMdiManager1;
        private System.Windows.Forms.ToolStripMenuItem rETTEDITARATURAToolStripMenuItem;
    }
}