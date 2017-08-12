using System.Windows.Forms;

namespace SpiderForm
{
    partial class CrawlForm
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
            this.carBrandButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // carBrandButton
            // 
            this.carBrandButton.Location = new System.Drawing.Point(48, 34);
            this.carBrandButton.Name = "carBrandButton";
            this.carBrandButton.Size = new System.Drawing.Size(110, 70);
            this.carBrandButton.TabIndex = 0;
            this.carBrandButton.Text = "采集品牌";
            this.carBrandButton.UseVisualStyleBackColor = true;
            this.carBrandButton.Click += new System.EventHandler(this.CarBrandButton_Click);
            // 
            // CrawlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 382);
            this.Controls.Add(this.carBrandButton);
            this.Name = "CrawlForm";
            this.Text = "CrawlForm";
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button carBrandButton;

        #endregion
    }
}