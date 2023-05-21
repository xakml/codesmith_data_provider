using DeployLX.Licensing.v4;
using DeployLXLicensing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DeployLXKeyGenerator
{
	public class GenerateActivationCode : Form
	{
		public string SerialNumber;

		private IContainer components = null;

		private Button button2;

		private TextBox textBox6;

		private TextBox textBox5;

		private Label label7;

		private TextBox textBox4;

		private Label label6;

		private Label label5;

		private TextBox textBox3;

		private TextBox textBox2;

		private Label label4;

		private TextBox textBox1;

		private Label label3;

		private Label label2;

		private Label label1;

		private DateTimePicker dateTimePicker1;

		private Button button1;

		public GenerateActivationCode()
		{
			InitializeComponent();
		}

		private void Button1Click(object sender, EventArgs e)
		{
			string data = "$m|+m#}E1sg.Ld-^!lh]:U9WW|d6ClTTRFioiGwkyJcw3;{5JrK!T%!9[¶s4)-/}}F=;[.gJxF[Aslm:Jj3I8UJG~H.BwPgo`[N!:-'jeR;nPOoqo6noF|J-xP}g(:39jqH`7-N§@(-6?:Vx_sLd~/v4DmvU-rw`HqlF;L*rUNU$4Dy:x9'?.v)%|)#h?gD^vG^];oy7q~(7)kv_$g7|rYDewmmnZ/V(IlPaR:^JWJW8^KPa=wYsLQ¶{mIp/Dlod#:¶L6]G(b9E.RWet2pGv+OOY,§Lqyl_,XpJz{JqPZ_rFeqv^0f[h#[#-MU#tW!&pFC9j^Gj/dCxhn~dWs$@n.!be1sDKi0j#kH6q{Rl|Y4fhh@bf4caJo]kQh0w314f+D5.m|C(3'de?7dEG+¶OSMC^IJ8Z}@C!$6$levqS*X§&k6&e§I¶lM]YnqeE?+PO%0b0^$jz8c[EJB2ks47y4kTVtVrT&1%MBoB0X]C&1/O!.TNot]!1j+HT@oxF=%8n^Gs-l|^O!~3t0`|aZc*q/=E3%NzQgp=A3=0!Ib~k1ry{Y4I'K/zpshz,Uc,;]Ld&@WE'HihklM";
			ReadLicenseKey readLicenseKey = new ReadLicenseKey();
			readLicenseKey.Process(data);
			string text = textBox1.Text;
			string text2 = textBox2.Text;
			string text3 = textBox3.Text;
			int refid = 1;
			if (textBox5.Text != "")
			{
				refid = Convert.ToInt32(textBox5.Text);
			}
			DateTime value = dateTimePicker1.Value;
			string text4 = textBox4.Text;
			CodeAlgorithm algorithm = CodeAlgorithm.ActivationCode;
			LicenseKeyGen licenseKeyGen = new LicenseKeyGen();
			string text5 = licenseKeyGen.MakeActivationCode(text, text2, text3, refid, value, text4, algorithm);
			textBox6.Text = text5;
		}

		private void GenerateActivationCodeShown(object sender, EventArgs e)
		{
			if (SerialNumber != null && SerialNumber.Length > 0)
			{
				textBox2.Text = SerialNumber;
			}
		}

		private void Button2Click(object sender, EventArgs e)
		{
			DeployLX.Licensing.v4.MachineProfile defaultProfile = DeployLX.Licensing.v4.MachineProfile.GetDefaultProfile();
			textBox3.Text = defaultProfile.Hash;
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
			button1 = new System.Windows.Forms.Button();
			dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			textBox4 = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBox2 = new System.Windows.Forms.TextBox();
			textBox3 = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBox5 = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBox6 = new System.Windows.Forms.TextBox();
			button2 = new System.Windows.Forms.Button();
			SuspendLayout();
			button1.Location = new System.Drawing.Point(29, 244);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 0;
			button1.Text = "Generate";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(Button1Click);
			dateTimePicker1.Location = new System.Drawing.Point(255, 44);
			dateTimePicker1.Name = "dateTimePicker1";
			dateTimePicker1.Size = new System.Drawing.Size(166, 20);
			dateTimePicker1.TabIndex = 1;
			label1.Location = new System.Drawing.Point(255, 27);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(76, 14);
			label1.TabIndex = 2;
			label1.Text = "Code Expires:";
			textBox1.Location = new System.Drawing.Point(29, 44);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(210, 20);
			textBox1.TabIndex = 11;
			textBox1.Text = "CS85P-";
			label2.Location = new System.Drawing.Point(29, 28);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(100, 13);
			label2.TabIndex = 10;
			label2.Text = "Prefix:";
			label3.Location = new System.Drawing.Point(29, 185);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(100, 18);
			label3.TabIndex = 9;
			label3.Text = "Character Set:";
			textBox4.Location = new System.Drawing.Point(29, 206);
			textBox4.Name = "textBox4";
			textBox4.Size = new System.Drawing.Size(456, 20);
			textBox4.TabIndex = 8;
			textBox4.Text = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
			label4.Location = new System.Drawing.Point(29, 81);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(100, 11);
			label4.TabIndex = 12;
			label4.Text = "Serial Number:";
			textBox2.Location = new System.Drawing.Point(29, 95);
			textBox2.Name = "textBox2";
			textBox2.Size = new System.Drawing.Size(456, 20);
			textBox2.TabIndex = 13;
			textBox3.Location = new System.Drawing.Point(29, 150);
			textBox3.Name = "textBox3";
			textBox3.Size = new System.Drawing.Size(456, 20);
			textBox3.TabIndex = 15;
			label5.Location = new System.Drawing.Point(29, 136);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(131, 11);
			label5.TabIndex = 14;
			label5.Text = "Machine Hash Code:";
			label6.Location = new System.Drawing.Point(139, 229);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(100, 14);
			label6.TabIndex = 16;
			label6.Text = "Machine ID:";
			textBox5.Location = new System.Drawing.Point(139, 247);
			textBox5.Name = "textBox5";
			textBox5.Size = new System.Drawing.Size(113, 20);
			textBox5.TabIndex = 17;
			textBox5.Text = "1";
			label7.Location = new System.Drawing.Point(29, 280);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(100, 15);
			label7.TabIndex = 18;
			label7.Text = "Activation Code:";
			textBox6.Location = new System.Drawing.Point(29, 298);
			textBox6.Name = "textBox6";
			textBox6.Size = new System.Drawing.Size(456, 20);
			textBox6.TabIndex = 19;
			button2.Location = new System.Drawing.Point(154, 124);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(85, 23);
			button2.TabIndex = 20;
			button2.Text = "Default Hash";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(Button2Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(505, 383);
			base.Controls.Add(button2);
			base.Controls.Add(textBox6);
			base.Controls.Add(label7);
			base.Controls.Add(textBox5);
			base.Controls.Add(label6);
			base.Controls.Add(textBox3);
			base.Controls.Add(label5);
			base.Controls.Add(textBox2);
			base.Controls.Add(label4);
			base.Controls.Add(textBox1);
			base.Controls.Add(label2);
			base.Controls.Add(label3);
			base.Controls.Add(textBox4);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePicker1);
			base.Controls.Add(button1);
			base.Name = "GenerateActivationCode";
			Text = "Generate Activation Unlock Code";
			base.Shown += new System.EventHandler(GenerateActivationCodeShown);
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}
