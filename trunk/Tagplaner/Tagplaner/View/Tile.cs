using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Tagplaner
{
	/// <summary>
	/// This class builds ta control, which will be used
	/// to draw the tiles in the puzzle game.
	/// </summary>
	public class ctlTile : System.Windows.Forms.UserControl
	{
		#region Private filds 
		private Point imagePoint;
		private System.Windows.Forms.Button imageBase;
		private System.Windows.Forms.PictureBox tilePic;
		private int tindex;
		#endregion Private filds

		#region Public Properties
		//Public Properties
		public int tIndex
		{
			get{return tindex;}
		}

		public Size tImageSize
		{
			get{return this.tilePic.Size;}
		}
		#endregion Public Properties


		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Contructor and Dispose
		// Constructor for the control
		public ctlTile(int tWidth, int tHeight,int index)
		{
			//Tile's Index
			tindex = index;
			
			//Disable control so parent form cann handle its key and mouse
			//events
			Enabled = false;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//Image Base and Imgae Fixation Here
			if ((tWidth < 32 || tWidth > 96) || ( tHeight <32 || tHeight > 96))
			{
				this.Size = new Size(64,64);
				tWidth = 64;
				tHeight = 64;
			}
			else this.Size = new Size(tWidth, tHeight);
			
			imageBase.Location = new Point(0,0);
			imageBase.Size = new Size(tWidth,tHeight);
			tilePic.Location = new Point(1,1);
			tilePic.Size = new Size(tWidth - 2,tHeight - 2);
			tilePic.Image = null;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
/*		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}*/
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.imageBase = new System.Windows.Forms.Button();
            this.tilePic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBase
            // 
            this.imageBase.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.imageBase.Enabled = false;
            this.imageBase.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.imageBase.Location = new System.Drawing.Point(1, 1);
            this.imageBase.Name = "imageBase";
            this.imageBase.Size = new System.Drawing.Size(62, 62);
            this.imageBase.TabIndex = 0;
            this.imageBase.UseVisualStyleBackColor = false;
            // 
            // tilePic
            // 
            this.tilePic.BackColor = System.Drawing.Color.OliveDrab;
            this.tilePic.Location = new System.Drawing.Point(2, 2);
            this.tilePic.Name = "tilePic";
            this.tilePic.Size = new System.Drawing.Size(60, 60);
            this.tilePic.TabIndex = 1;
            this.tilePic.TabStop = false;
            this.tilePic.Click += new System.EventHandler(this.tilePic_Click);
            this.tilePic.Paint += new System.Windows.Forms.PaintEventHandler(this.TilePic_Paint);
            // 
            // ctlTile
            // 
            this.Controls.Add(this.tilePic);
            this.Controls.Add(this.imageBase);
            this.Name = "ctlTile";
            this.Size = new System.Drawing.Size(64, 64);
            ((System.ComponentModel.ISupportInitialize)(this.tilePic)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#endregion Contructor and Dispose

		#region Tile Picture
		//Here the tile imgae is declared painted on the tile
		public void tilePicture(Image tImage, Point StartPt)
		{
			tilePic.Image = tImage;
            imagePoint = StartPt;
		}
		#endregion Tile Picture

		#region Repaint Tile 
		// Here required repainting is done
		// based on the onpaint event
		private void TilePic_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (tilePic.Image != null)
			{
				Graphics g = e.Graphics;
				g.DrawImage(tilePic.Image, new Rectangle(new Point(0,0),new Size(tilePic.Width,tilePic.Height)),
					new Rectangle(imagePoint,new Size(tilePic.Width,tilePic.Height)),GraphicsUnit.Pixel);
			}
		}
		#endregion Repaint Tile

        private void tilePic_Click(object sender, EventArgs e)
        {

        }
	}
}
