namespace SewatekConnector
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
         this.button1 = new System.Windows.Forms.Button();
         this.pictureBox1 = new System.Windows.Forms.PictureBox();
         this.pictureBox2 = new System.Windows.Forms.PictureBox();
         this.pictureBox3 = new System.Windows.Forms.PictureBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
         this.SuspendLayout();
         // 
         // button1
         // 
         this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.button1.Location = new System.Drawing.Point(12, 97);
         this.button1.Name = "button1";
         this.button1.Size = new System.Drawing.Size(902, 49);
         this.button1.TabIndex = 0;
         this.button1.Text = "Add Sewateks to selected panels";
         this.button1.UseVisualStyleBackColor = true;
         // 
         // pictureBox1
         // 
         this.pictureBox1.Image = global::SewatekConnector.Properties.Resources.sewatekLogo;
         this.pictureBox1.Location = new System.Drawing.Point(12, 12);
         this.pictureBox1.Name = "pictureBox1";
         this.pictureBox1.Size = new System.Drawing.Size(247, 70);
         this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox1.TabIndex = 1;
         this.pictureBox1.TabStop = false;
         // 
         // pictureBox2
         // 
         this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
         this.pictureBox2.Image = global::SewatekConnector.Properties.Resources.skanskaLogo;
         this.pictureBox2.Location = new System.Drawing.Point(313, 25);
         this.pictureBox2.Name = "pictureBox2";
         this.pictureBox2.Size = new System.Drawing.Size(279, 43);
         this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox2.TabIndex = 2;
         this.pictureBox2.TabStop = false;
         // 
         // pictureBox3
         // 
         this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.pictureBox3.Image = global::SewatekConnector.Properties.Resources.teklaLogo;
         this.pictureBox3.Location = new System.Drawing.Point(635, 12);
         this.pictureBox3.Name = "pictureBox3";
         this.pictureBox3.Size = new System.Drawing.Size(279, 70);
         this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
         this.pictureBox3.TabIndex = 3;
         this.pictureBox3.TabStop = false;
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(926, 158);
         this.Controls.Add(this.pictureBox3);
         this.Controls.Add(this.pictureBox2);
         this.Controls.Add(this.pictureBox1);
         this.Controls.Add(this.button1);
         this.Name = "Form1";
         this.Text = "SewatekConnector";
         this.TopMost = true;
         this.Load += new System.EventHandler(this.Form1_Load);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button button1;
      private System.Windows.Forms.PictureBox pictureBox1;
      private System.Windows.Forms.PictureBox pictureBox2;
      private System.Windows.Forms.PictureBox pictureBox3;
   }
}

