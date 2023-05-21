using DeployLXLicensing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DeployLXKeyGenerator
{
	public class MainForm : Form
	{
		public string DirectoryName = "";

		private IContainer components = null;

		private Label label10;

		private TextBox textBox9;

		private Label label9;

		private ComboBox comboBox1;

		private Button button2;

		private TextBox textBox8;

		private Label label8;

		private Label label7;

		private TextBox textBox7;

		private Label label6;

		private TextBox textBox6;

		private Label label5;

		private TextBox textBox5;

		private Label label4;

		private TextBox textBox4;

		private TextBox textBox3;

		private Label label3;

		private TextBox textBox2;

		private Label label2;

		private Label label1;

		private TextBox textBox1;

		private CheckBox checkBox8;

		private CheckBox checkBox7;

		private CheckBox checkBox6;

		private CheckBox checkBox3;

		private CheckBox checkBox4;

		private CheckBox checkBox5;

		private GroupBox groupBox1;

		private CheckBox checkBox2;

		private CheckBox checkBox1;

		private Button button1;

		public MainForm()
		{
			InitializeComponent();
		}

		private void Button1Click(object sender, EventArgs e)
		{
			string data = "$m|+m#}E1sg.Ld-^!lh]:U9WW|d6ClTTRFioiGwkyJcw3;{5JrK!T%!9[¶s4)-/}}F=;[.gJxF[Aslm:Jj3I8UJG~H.BwPgo`[N!:-'jeR;nPOoqo6noF|J-xP}g(:39jqH`7-N§@(-6?:Vx_sLd~/v4DmvU-rw`HqlF;L*rUNU$4Dy:x9'?.v)%|)#h?gD^vG^];oy7q~(7)kv_$g7|rYDewmmnZ/V(IlPaR:^JWJW8^KPa=wYsLQ¶{mIp/Dlod#:¶L6]G(b9E.RWet2pGv+OOY,§Lqyl_,XpJz{JqPZ_rFeqv^0f[h#[#-MU#tW!&pFC9j^Gj/dCxhn~dWs$@n.!be1sDKi0j#kH6q{Rl|Y4fhh@bf4caJo]kQh0w314f+D5.m|C(3'de?7dEG+¶OSMC^IJ8Z}@C!$6$levqS*X§&k6&e§I¶lM]YnqeE?+PO%0b0^$jz8c[EJB2ks47y4kTVtVrT&1%MBoB0X]C&1/O!.TNot]!1j+HT@oxF=%8n^Gs-l|^O!~3t0`|aZc*q/=E3%NzQgp=A3=0!Ib~k1ry{Y4I'K/zpshz,Uc,;]Ld&@WE'HihklM";
			ReadLicenseKey readLicenseKey = new ReadLicenseKey();
			readLicenseKey.Process(data);
			if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
			{
				return;
			}
			int[] array = null;
			if (textBox9.Text != "" && textBox9.Text.Length >= 3 && textBox9.Text.Contains("-"))
			{
				string[] array2 = textBox9.Text.Split('-');
				array = new int[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array[i] = Convert.ToInt32(array2[i]);
				}
			}
			SerialNumberFlags flags = GrabFlags();
			string text = textBox1.Text;
			string text2 = textBox2.Text;
			int seed = Convert.ToInt32(textBox3.Text);
			int extendLimitOrdinal = Convert.ToInt32(textBox4.Text);
			int extendLimitValue = Convert.ToInt32(textBox7.Text);
			int extendLimitOrdinal2 = Convert.ToInt32(textBox5.Text);
			int extendLimitValue2 = Convert.ToInt32(textBox6.Text);
			CodeAlgorithm algorithm = CodeAlgorithm.SerialNumber;
			if (comboBox1.SelectedItem.ToString() == "Serial Number")
			{
				algorithm = CodeAlgorithm.SerialNumber;
			}
			if (comboBox1.SelectedItem.ToString() == "Activation Code")
			{
				algorithm = CodeAlgorithm.ActivationCode;
			}
			if (comboBox1.SelectedItem.ToString() == "Simple")
			{
				algorithm = CodeAlgorithm.Simple;
			}
			if (comboBox1.SelectedItem.ToString() == "Basic")
			{
				algorithm = CodeAlgorithm.Basic;
			}
			if (comboBox1.SelectedItem.ToString() == "Advanced")
			{
				algorithm = CodeAlgorithm.Advanced;
			}
			LicenseKeyGen licenseKeyGen = new LicenseKeyGen();
			string text3 = licenseKeyGen.MakeSerialNumber(text2, seed, flags, extendLimitOrdinal, extendLimitValue, extendLimitOrdinal2, extendLimitValue2, array, text, algorithm);
			textBox8.Text = text3;
		}

		public SerialNumberFlags GrabFlags()
		{
			bool[] array = new bool[8];
			int num = 1;
			int num2 = 0;
			if (checkBox1.Checked)
			{
				array[0] = true;
			}
			else
			{
				array[0] = false;
			}
			if (checkBox2.Checked)
			{
				array[1] = true;
			}
			else
			{
				array[1] = false;
			}
			if (checkBox3.Checked)
			{
				array[2] = true;
			}
			else
			{
				array[2] = false;
			}
			if (checkBox4.Checked)
			{
				array[3] = true;
			}
			else
			{
				array[3] = false;
			}
			if (checkBox5.Checked)
			{
				array[4] = true;
			}
			else
			{
				array[4] = false;
			}
			if (checkBox6.Checked)
			{
				array[5] = true;
			}
			else
			{
				array[5] = false;
			}
			if (checkBox7.Checked)
			{
				array[6] = true;
			}
			else
			{
				array[6] = false;
			}
			if (checkBox8.Checked)
			{
				array[7] = true;
			}
			else
			{
				array[7] = false;
			}
			for (int i = 0; i < 8; i++)
			{
				if (array[i])
				{
					num2 |= num;
				}
				num <<= 1;
			}
			return (SerialNumberFlags)num2;
		}

		private void Button2Click(object sender, EventArgs e)
		{
			GenerateActivationCode generateActivationCode = new GenerateActivationCode();
			generateActivationCode.SerialNumber = textBox8.Text;
			generateActivationCode.Show();
		}

		private void MainFormLoad(object sender, EventArgs e)
		{
			comboBox1.SelectedIndex = 0;
		}

		private void Button3Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Title = "Browse for target assembly";
			openFileDialog.InitialDirectory = "c:\\";
			if (DirectoryName != "")
			{
				openFileDialog.InitialDirectory = DirectoryName;
			}
			openFileDialog.Filter = "All files (*.exe,*.dll)|*.exe;*.dll";
			openFileDialog.FilterIndex = 2;
			openFileDialog.RestoreDirectory = true;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			string fileName = openFileDialog.FileName;
			int num = fileName.LastIndexOf("\\");
			if (num != -1)
			{
				DirectoryName = fileName.Remove(num, fileName.Length - num);
			}
			if (DirectoryName.Length == 2)
			{
				DirectoryName += "\\";
			}
			FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			BinaryReader binaryReader = new BinaryReader(fileStream);
			MetadataReader metadataReader = new MetadataReader();
			bool flag = metadataReader.Intialize(binaryReader);
			ReadLicenseKey.keySN = null;
			ReadLicenseKey.keyA = null;
			if (flag)
			{
				string data = "$m|+m#}E1sg.Ld-^!lh]:U9WW|d6ClTTRFioiGwkyJcw3;{5JrK!T%!9[¶s4)-/}}F=;[.gJxF[Aslm:Jj3I8UJG~H.BwPgo`[N!:-'jeR;nPOoqo6noF|J-xP}g(:39jqH`7-N§@(-6?:Vx_sLd~/v4DmvU-rw`HqlF;L*rUNU$4Dy:x9'?.v)%|)#h?gD^vG^];oy7q~(7)kv_$g7|rYDewmmnZ/V(IlPaR:^JWJW8^KPa=wYsLQ¶{mIp/Dlod#:¶L6]G(b9E.RWet2pGv+OOY,§Lqyl_,XpJz{JqPZ_rFeqv^0f[h#[#-MU#tW!&pFC9j^Gj/dCxhn~dWs$@n.!be1sDKi0j#kH6q{Rl|Y4fhh@bf4caJo]kQh0w314f+D5.m|C(3'de?7dEG+¶OSMC^IJ8Z}@C!$6$levqS*X§&k6&e§I¶lM]YnqeE?+PO%0b0^$jz8c[EJB2ks47y4kTVtVrT&1%MBoB0X]C&1/O!.TNot]!1j+HT@oxF=%8n^Gs-l|^O!~3t0`|aZc*q/=E3%NzQgp=A3=0!Ib~k1ry{Y4I'K/zpshz,Uc,;]Ld&@WE'HihklM";
				ReadLicenseKey readLicenseKey = new ReadLicenseKey();
				readLicenseKey.Process(data);
				if (ReadLicenseKey.keySN == null || ReadLicenseKey.keySN.Length == 0 || ReadLicenseKey.keyA == null || ReadLicenseKey.keyA.Length == 0)
				{
					MessageBox.Show("Probable selected assembly doesn't contain the LicenseKey attribute!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("Selected assembly is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			binaryReader.Close();
			fileStream.Close();
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
			checkBox1 = new System.Windows.Forms.CheckBox();
			checkBox2 = new System.Windows.Forms.CheckBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			checkBox8 = new System.Windows.Forms.CheckBox();
			checkBox7 = new System.Windows.Forms.CheckBox();
			checkBox6 = new System.Windows.Forms.CheckBox();
			checkBox5 = new System.Windows.Forms.CheckBox();
			checkBox4 = new System.Windows.Forms.CheckBox();
			checkBox3 = new System.Windows.Forms.CheckBox();
			textBox1 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBox2 = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBox3 = new System.Windows.Forms.TextBox();
			textBox4 = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBox5 = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			textBox6 = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBox7 = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			textBox8 = new System.Windows.Forms.TextBox();
			button2 = new System.Windows.Forms.Button();
			comboBox1 = new System.Windows.Forms.ComboBox();
			label9 = new System.Windows.Forms.Label();
			textBox9 = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			groupBox1.SuspendLayout();
			SuspendLayout();
			button1.Location = new System.Drawing.Point(315, 178);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(109, 29);
			button1.TabIndex = 0;
			button1.Text = "Generate";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(Button1Click);
			checkBox1.Checked = true;
			checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox1.Location = new System.Drawing.Point(15, 19);
			checkBox1.Name = "checkBox1";
			checkBox1.Size = new System.Drawing.Size(38, 24);
			checkBox1.TabIndex = 1;
			checkBox1.Text = "F0";
			checkBox1.UseVisualStyleBackColor = true;
			checkBox2.Checked = true;
			checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox2.Location = new System.Drawing.Point(15, 44);
			checkBox2.Name = "checkBox2";
			checkBox2.Size = new System.Drawing.Size(38, 24);
			checkBox2.TabIndex = 2;
			checkBox2.Text = "F1";
			checkBox2.UseVisualStyleBackColor = true;
			groupBox1.Controls.Add(checkBox8);
			groupBox1.Controls.Add(checkBox7);
			groupBox1.Controls.Add(checkBox6);
			groupBox1.Controls.Add(checkBox5);
			groupBox1.Controls.Add(checkBox4);
			groupBox1.Controls.Add(checkBox3);
			groupBox1.Controls.Add(checkBox1);
			groupBox1.Controls.Add(checkBox2);
			groupBox1.Location = new System.Drawing.Point(9, 67);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(63, 252);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Flags";
			checkBox8.Checked = true;
			checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox8.Location = new System.Drawing.Point(15, 222);
			checkBox8.Name = "checkBox8";
			checkBox8.Size = new System.Drawing.Size(38, 24);
			checkBox8.TabIndex = 8;
			checkBox8.Text = "F7";
			checkBox8.UseVisualStyleBackColor = true;
			checkBox7.Checked = true;
			checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox7.Location = new System.Drawing.Point(15, 194);
			checkBox7.Name = "checkBox7";
			checkBox7.Size = new System.Drawing.Size(38, 24);
			checkBox7.TabIndex = 7;
			checkBox7.Text = "F6";
			checkBox7.UseVisualStyleBackColor = true;
			checkBox6.Checked = true;
			checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox6.Location = new System.Drawing.Point(15, 164);
			checkBox6.Name = "checkBox6";
			checkBox6.Size = new System.Drawing.Size(38, 24);
			checkBox6.TabIndex = 6;
			checkBox6.Text = "F5";
			checkBox6.UseVisualStyleBackColor = true;
			checkBox5.Checked = true;
			checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox5.Location = new System.Drawing.Point(15, 134);
			checkBox5.Name = "checkBox5";
			checkBox5.Size = new System.Drawing.Size(38, 24);
			checkBox5.TabIndex = 5;
			checkBox5.Text = "F4";
			checkBox5.UseVisualStyleBackColor = true;
			checkBox4.Checked = true;
			checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox4.Location = new System.Drawing.Point(15, 104);
			checkBox4.Name = "checkBox4";
			checkBox4.Size = new System.Drawing.Size(38, 24);
			checkBox4.TabIndex = 4;
			checkBox4.Text = "F3";
			checkBox4.UseVisualStyleBackColor = true;
			checkBox3.Checked = true;
			checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBox3.Location = new System.Drawing.Point(15, 74);
			checkBox3.Name = "checkBox3";
			checkBox3.Size = new System.Drawing.Size(38, 24);
			checkBox3.TabIndex = 3;
			checkBox3.Text = "F2";
			checkBox3.UseVisualStyleBackColor = true;
			textBox1.Location = new System.Drawing.Point(78, 88);
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(456, 20);
			textBox1.TabIndex = 4;
			textBox1.Text = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
			label1.Location = new System.Drawing.Point(78, 67);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(100, 18);
			label1.TabIndex = 5;
			label1.Text = "Character Set:";
			label2.Location = new System.Drawing.Point(78, 122);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(100, 13);
			label2.TabIndex = 6;
			label2.Text = "Prefix:";
			textBox2.Location = new System.Drawing.Point(78, 138);
			textBox2.Name = "textBox2";
			textBox2.Size = new System.Drawing.Size(210, 20);
			textBox2.TabIndex = 7;
			textBox2.Text = "CS85P-";
			label3.Location = new System.Drawing.Point(315, 122);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(62, 13);
			label3.TabIndex = 8;
			label3.Text = "Seed:";
			textBox3.Location = new System.Drawing.Point(315, 138);
			textBox3.Name = "textBox3";
			textBox3.Size = new System.Drawing.Size(109, 20);
			textBox3.TabIndex = 9;
			textBox3.Text = "0";
			textBox4.Location = new System.Drawing.Point(78, 187);
			textBox4.Name = "textBox4";
			textBox4.Size = new System.Drawing.Size(109, 20);
			textBox4.TabIndex = 11;
			textBox4.Text = "0";
			label4.Location = new System.Drawing.Point(78, 171);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(109, 13);
			label4.TabIndex = 10;
			label4.Text = "extendLimitOrdinal1:";
			textBox5.Location = new System.Drawing.Point(199, 187);
			textBox5.Name = "textBox5";
			textBox5.Size = new System.Drawing.Size(109, 20);
			textBox5.TabIndex = 13;
			textBox5.Text = "0";
			label5.Location = new System.Drawing.Point(199, 171);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(109, 13);
			label5.TabIndex = 12;
			label5.Text = "extendLimitOrdinal2:";
			textBox6.Location = new System.Drawing.Point(199, 233);
			textBox6.Name = "textBox6";
			textBox6.Size = new System.Drawing.Size(109, 20);
			textBox6.TabIndex = 17;
			textBox6.Text = "0";
			label6.Location = new System.Drawing.Point(199, 217);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(109, 13);
			label6.TabIndex = 16;
			label6.Text = "extendLimitValue2:";
			textBox7.Location = new System.Drawing.Point(78, 233);
			textBox7.Name = "textBox7";
			textBox7.Size = new System.Drawing.Size(109, 20);
			textBox7.TabIndex = 15;
			textBox7.Text = "0";
			label7.Location = new System.Drawing.Point(78, 217);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(109, 13);
			label7.TabIndex = 14;
			label7.Text = "extendLimitValue1:";
			label8.Location = new System.Drawing.Point(78, 266);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(100, 12);
			label8.TabIndex = 18;
			label8.Text = "Serial:";
			textBox8.Location = new System.Drawing.Point(78, 281);
			textBox8.Name = "textBox8";
			textBox8.Size = new System.Drawing.Size(456, 20);
			textBox8.TabIndex = 19;
			button2.Location = new System.Drawing.Point(12, 12);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(143, 23);
			button2.TabIndex = 20;
			button2.Text = "Generate Activation Code";
			button2.UseVisualStyleBackColor = true;
			button2.Click += new System.EventHandler(Button2Click);
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[1]
			{
				"Advanced"
			});
			comboBox1.Location = new System.Drawing.Point(315, 231);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(121, 21);
			comboBox1.TabIndex = 21;
			label9.Location = new System.Drawing.Point(315, 213);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(100, 15);
			label9.TabIndex = 22;
			label9.Text = "Algorithm:";
			textBox9.Location = new System.Drawing.Point(184, 62);
			textBox9.Name = "textBox9";
			textBox9.Size = new System.Drawing.Size(350, 20);
			textBox9.TabIndex = 23;
			label10.Location = new System.Drawing.Point(184, 44);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(170, 15);
			label10.TabIndex = 24;
			label10.Text = "Group sizes (separated by -)";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(549, 326);
			base.Controls.Add(label10);
			base.Controls.Add(textBox9);
			base.Controls.Add(label9);
			base.Controls.Add(comboBox1);
			base.Controls.Add(button2);
			base.Controls.Add(textBox8);
			base.Controls.Add(label8);
			base.Controls.Add(textBox6);
			base.Controls.Add(label6);
			base.Controls.Add(textBox7);
			base.Controls.Add(label7);
			base.Controls.Add(textBox5);
			base.Controls.Add(label5);
			base.Controls.Add(textBox4);
			base.Controls.Add(label4);
			base.Controls.Add(textBox3);
			base.Controls.Add(label3);
			base.Controls.Add(textBox2);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBox1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(button1);
			base.Name = "MainForm";
			Text = "Codesmith KeyGenerator";
			this.StartPosition = FormStartPosition.CenterScreen;
			base.Load += new System.EventHandler(MainFormLoad);
			groupBox1.ResumeLayout(performLayout: false);
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}
