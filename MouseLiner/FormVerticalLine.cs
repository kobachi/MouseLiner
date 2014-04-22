using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseLiner {
	public partial class FormVerticalLine: Form {
		public FormVerticalLine() {
			InitializeComponent();
		}

		private void FormVerticalLine_FormClosing(object sender, FormClosingEventArgs e) {
			if(e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
			}
		}

		[SettingsBindable(true)]
		public uint LineWidth {
			get {
				return (uint)this.Width;
			}
			set {
				if(value < 0 || int.MaxValue < value) {
					throw new ArgumentOutOfRangeException();
				}
				this.MaximumSize = new Size((int)value, this.MaximumSize.Height);
				this.Width = (int)value;
				this.MinimumSize = new Size((int)value, this.MinimumSize.Height);
			}
		}
	}
}
