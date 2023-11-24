// Launch.FormAbout
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Launcher
{
    public partial class FormAbout : Form
    {
        private IContainer components;

        private LinkLabel linkLabel1;

        private PictureBox pictureBox1;

        private Label label1;

        private PictureBox pictureBox2;

        private PictureBox pictureBox3;

        private Label label2;



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://launcher.vanlangen.biz");
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            label1 = new System.Windows.Forms.Label();
            pictureBox3 = new System.Windows.Forms.PictureBox();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            linkLabel1.Location = new System.Drawing.Point(66, 13);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(94, 24);
            linkLabel1.TabIndex = 0;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "linkLabel1";
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(66, 47);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(112, 13);
            label1.TabIndex = 2;
            label1.Text = "© 2019 vanlangen.biz";
            pictureBox1.Location = new System.Drawing.Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(48, 48);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(150, 95);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(25, 26);
            label2.TabIndex = 5;
            label2.Text = "+";
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(335, 151);
            base.Controls.Add(label2);
            base.Controls.Add(pictureBox3);
            base.Controls.Add(pictureBox2);
            base.Controls.Add(label1);
            base.Controls.Add(pictureBox1);
            base.Controls.Add(linkLabel1);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FormAbout";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}