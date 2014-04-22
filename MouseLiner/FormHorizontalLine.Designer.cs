namespace MouseLiner {
	partial class FormHorizontalLine {
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
			// FormHorizontalLine
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::MouseLiner.Properties.Settings.Default.HorizontalLineColor;
			this.ClientSize = new System.Drawing.Size(100, 3);
			this.ControlBox = false;
			this.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::MouseLiner.Properties.Settings.Default, "HorizontalLineColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.DataBindings.Add(new System.Windows.Forms.Binding("Opacity", global::MouseLiner.Properties.Settings.Default, "HorizontalLineOpacity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.DataBindings.Add(new System.Windows.Forms.Binding("LineWidth", global::MouseLiner.Properties.Settings.Default, "HorizontalLineWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(9999, 3);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(100, 3);
			this.Name = "FormHorizontalLine";
			this.Opacity = global::MouseLiner.Properties.Settings.Default.HorizontalLineOpacity;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.TopMost = true;
			this.LineWidth = global::MouseLiner.Properties.Settings.Default.HorizontalLineWidth;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHorizontalLine_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion
	}
}