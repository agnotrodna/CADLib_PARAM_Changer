namespace IFC_PLUGIN
{
    partial class Demo_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo_Form));
            this.menuStrip_IFC = new System.Windows.Forms.MenuStrip();
            this.InstrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ClassIFCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_IFC.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_IFC
            // 
            this.menuStrip_IFC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InstrToolStripMenuItem});
            this.menuStrip_IFC.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_IFC.Name = "menuStrip_IFC";
            this.menuStrip_IFC.Size = new System.Drawing.Size(800, 24);
            this.menuStrip_IFC.TabIndex = 2;
            this.menuStrip_IFC.Text = "menuStrip_Class_IFC";
            // 
            // InstrToolStripMenuItem
            // 
            this.InstrToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClassIFCToolStripMenuItem});
            this.InstrToolStripMenuItem.Name = "InstrToolStripMenuItem";
            this.InstrToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.InstrToolStripMenuItem.Text = "Инструменты";
            // 
            // ClassIFCToolStripMenuItem
            // 
            this.ClassIFCToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ClassIFCToolStripMenuItem.Image")));
            this.ClassIFCToolStripMenuItem.Name = "ClassIFCToolStripMenuItem";
            this.ClassIFCToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ClassIFCToolStripMenuItem.Text = "Назначение Классов IFC";
            this.ClassIFCToolStripMenuItem.Click += new System.EventHandler(this.MenuItem_RunAllInOrder_Click);
            // 
            // Demo_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip_IFC);
            this.Name = "Demo_Form";
            this.Text = "Class_IFC";
            this.menuStrip_IFC.ResumeLayout(false);
            this.menuStrip_IFC.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip_IFC;

        private System.Windows.Forms.ToolStripMenuItem InstrToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem ClassIFCToolStripMenuItem;
    }
}