using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseLiner {
	public partial class FormSpotLight: Form {
		public FormSpotLight() {
			InitializeComponent();
			drawCircle();
		}

		void drawCircle() {
			Bitmap b = new Bitmap(pictureBox.Width, pictureBox.Height);
			using(Graphics g = Graphics.FromImage(b)) {
				using(Pen p = new Pen(this.ForeColor, 5)) {
					g.DrawEllipse(p, 5, 5, b.Width - 10, b.Height - 10);
				}
			}
			if(pictureBox.Image == null) {
				pictureBox.Image = b;
			}
			else {
				Image temp = pictureBox.Image;
				pictureBox.Image = b;
				temp.Dispose();
			}
		}

		private void FormSpotLight_ForeColorChanged(object sender, EventArgs e) {
			drawCircle();
		}

		private void FormSpotLight_SizeChanged(object sender, EventArgs e) {
			if(this.ClientSize.IsEmpty) {
				return;
			}
			drawCircle();
		}

		private void FormSpotLight_FormClosing(object sender, FormClosingEventArgs e) {
			if(e.CloseReason == CloseReason.UserClosing) {
				e.Cancel = true;
			}
		}
	}
}
