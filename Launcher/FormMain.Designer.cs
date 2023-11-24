// Launch.FormMain
using Shell32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Launcher
{
	public partial class FormMain : Form
	{
	

		private IContainer components;

		private System.Windows.Forms.Timer timer1;

		private TextBox textBox1;

		private Panel panel1;

		private Label label1;

		private ListBoxNF listBox1;

		private Label label2;

	

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
			components = new System.ComponentModel.Container();
			timer1 = new System.Windows.Forms.Timer(components);
			textBox1 = new System.Windows.Forms.TextBox();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			listBox1 = new ListBoxNF();
			panel1.SuspendLayout();
			SuspendLayout();
			timer1.Enabled = true;
			timer1.Interval = 50;
			timer1.Tick += new System.EventHandler(timer1_Tick);
			textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
			textBox1.BackColor = System.Drawing.Color.DarkGray;
			textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBox1.ForeColor = System.Drawing.Color.White;
			textBox1.Location = new System.Drawing.Point(0, 292);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(471, 29);
			textBox1.TabIndex = 0;
			textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
			textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
			textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
			textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(textBox1_KeyUp);
			panel1.BackColor = System.Drawing.Color.DarkGray;
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Font = new System.Drawing.Font("Consolas", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(471, 33);
			panel1.TabIndex = 2;
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.DarkGray;
			label2.Font = new System.Drawing.Font("Consolas", 10f, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			label2.ForeColor = System.Drawing.Color.White;
			label2.Location = new System.Drawing.Point(380, 11);
			label2.Name = "label2";
			label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			label2.Size = new System.Drawing.Size(56, 17);
			label2.TabIndex = 1;
			label2.Text = "label2";
			label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.DarkGray;
			label1.Font = new System.Drawing.Font("Consolas", 16f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.SlateBlue;
			label1.Location = new System.Drawing.Point(0, 4);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(84, 26);
			label1.TabIndex = 0;
			label1.Text = "label1";
			listBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			listBox1.BackColor = System.Drawing.Color.Gray;
			listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			listBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			listBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			listBox1.ForeColor = System.Drawing.Color.White;
			listBox1.FormattingEnabled = true;
			listBox1.ItemHeight = 64;
			listBox1.Location = new System.Drawing.Point(2, 33);
			listBox1.Name = "listBox1";
			listBox1.Size = new System.Drawing.Size(467, 256);
			listBox1.TabIndex = 0;
			listBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(listBox1_DrawItem);
			listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(listBox1_KeyDown);
			listBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(listBox1_KeyUp);
			listBox1.Leave += new System.EventHandler(listBox1_Leave);
			listBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(listBox1_MouseDown);
			listBox1.MouseLeave += new System.EventHandler(listBox1_MouseLeave);
			listBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(listBox1_MouseMove);
			listBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(listBox1_MouseUp);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.DarkGray;
			base.ClientSize = new System.Drawing.Size(471, 321);
			base.Controls.Add(panel1);
			base.Controls.Add(textBox1);
			base.Controls.Add(listBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			base.Name = "FormMain";
			base.Opacity = 0.0;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "DevTool";
			base.Deactivate += new System.EventHandler(FormMain_Deactivate);
			base.Shown += new System.EventHandler(FormMain_Shown);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}