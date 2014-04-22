namespace MouseLiner {
	partial class FormVerticalLine {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.SuspendLayout();
			// 
			// FormVerticalLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::MouseLiner.Properties.Settings.Default.VerticalLineColor;
			this.ClientSize = new System.Drawing.Size(3, 100);
			this.ControlBox = false;
			this.DataBindings.Add(new System.Windows.Forms.Binding("Opacity", global::MouseLiner.Properties.Settings.Default, "VerticalLineOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::MouseLiner.Properties.Settings.Default, "VerticalLineColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.DataBindings.Add(new System.Windows.Forms.Binding("LineWidth", global::MouseLiner.Properties.Settings.Default, "VerticalLineWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(3, 9999);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(3, 100);
			this.Name = "FormVerticalLine";
			this.Opacity = global::MouseLiner.Properties.Settings.Default.VerticalLineOpacity;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.LineWidth = global::MouseLiner.Properties.Settings.Default.VerticalLineWidth;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVerticalLine_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion
	}
}