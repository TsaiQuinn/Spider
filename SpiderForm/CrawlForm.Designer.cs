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
            this.carModelButton = new System.Windows.Forms.Button();
            this.labelBrandUpdate = new System.Windows.Forms.Label();
            this.labelBrandInsert = new System.Windows.Forms.Label();
            this.labelBrandName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // carBrandButton
            // 
            this.carBrandButton.Location = new System.Drawing.Point(59, 37);
            this.carBrandButton.Name = "carBrandButton";
            this.carBrandButton.Size = new System.Drawing.Size(63, 43);
            this.carBrandButton.TabIndex = 0;
            this.carBrandButton.Text = "采集品牌";
            this.carBrandButton.UseVisualStyleBackColor = true;
            this.carBrandButton.Click += new System.EventHandler(this.CarBrandButton_Click);
            // 
            // carModelButton
            // 
            this.carModelButton.Location = new System.Drawing.Point(59, 139);
            this.carModelButton.Name = "carModelButton";
            this.carModelButton.Size = new System.Drawing.Size(63, 45);
            this.carModelButton.TabIndex = 1;
            this.carModelButton.Text = "采集车型";
            this.carModelButton.UseVisualStyleBackColor = true;
            this.carModelButton.Click += new System.EventHandler(this.CarModelButton_Click);
            // 
            // labelBrandUpdate
            // 
            this.labelBrandUpdate.AutoSize = true;
            this.labelBrandUpdate.Location = new System.Drawing.Point(184, 26);
            this.labelBrandUpdate.Name = "labelBrandUpdate";
            this.labelBrandUpdate.Size = new System.Drawing.Size(42, 14);
            this.labelBrandUpdate.TabIndex = 2;
            this.labelBrandUpdate.Text = "更新:0";
            // 
            // labelBrandInsert
            // 
            this.labelBrandInsert.AutoSize = true;
            this.labelBrandInsert.Location = new System.Drawing.Point(184, 81);
            this.labelBrandInsert.Name = "labelBrandInsert";
            this.labelBrandInsert.Size = new System.Drawing.Size(42, 14);
            this.labelBrandInsert.TabIndex = 3;
            this.labelBrandInsert.Text = "插入:0";
            // 
            // labelBrandName
            // 
            this.labelBrandName.AutoSize = true;
            this.labelBrandName.Location = new System.Drawing.Point(295, 51);
            this.labelBrandName.Name = "labelBrandName";
            this.labelBrandName.Size = new System.Drawing.Size(0, 14);
            this.labelBrandName.TabIndex = 4;
            // 
            // CrawlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 382);
            this.Controls.Add(this.labelBrandName);
            this.Controls.Add(this.labelBrandInsert);
            this.Controls.Add(this.labelBrandUpdate);
            this.Controls.Add(this.carModelButton);
            this.Controls.Add(this.carBrandButton);
            this.Name = "CrawlForm";
            this.Text = "CrawlForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button carBrandButton;

        #endregion

        private Button carModelButton;
        private Label labelBrandUpdate;
        private Label labelBrandInsert;
        private Label labelBrandName;
    }
}