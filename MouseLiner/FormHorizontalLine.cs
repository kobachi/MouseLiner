using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseLiner {
	public partial class FormHorizontalLine: Form {
		public FormHorizontalLine() {
			InitializeComponent();
		}

		private void FormHorizontalLine_FormClosing(object sender, FormClosingEventArgs e) {
			if(e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
			}
		}

		[SettingsBindable(true)]
		public uint LineWidth {
			get {
				return (uint)this.Height;
			}
			set {
				if(value < 0 || int.MaxValue < value) {
					throw new ArgumentOutOfRangeException();
				}
				this.MaximumSize = new Size(this.MaximumSize.Width, (int)value);
				this.Height = (int)value;
				this.MinimumSize = new Size(this.MinimumSize.Width, (int)value);
			}
		}
	}
}
