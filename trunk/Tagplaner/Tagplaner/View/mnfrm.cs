using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

// Here all the coding for the game is done

namespace Tagplaner
{
	//A structure for keeping info on the row and column
	//of a block, works very similar to POINT
	struct Block
	{
		public int Row;
		public int Col;
		public Block(int row, int col)
		{
			this.Row = row;
			this.Col = col;
		}
	}

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class mnfrm : System.Windows.Forms.UserControl
	{
		
		#region Private Fields
		//Private Fields
		const int tSquare = 64; // the tiles width/Height size
		const int tImageW = 62; // the image size in the tile
		                        // here the image width and height
		                        // are both equal to tImageW
		int nRows = 3;			// Number of rows in the Grid (Default)
		int nCols = 3;          // Number of Columns in the Grid (Default)

		Random rand;			// rand variable is used in shuffling
		                        // the tiles
		Block  blankTile;       // blankTile will keep information
		                        // about the empty block and its 
		                        // location(Row, Col) in the Grid 
		int    timerCountdown;  // the timers total time
		ctlTile tmplasttile;    // a temproray tile, which hold info
		                        // on the last tile in the grid

		ctlTile[,] tile ;       // the tiles used to build the puzzle
		bool PictureLoaded = false;     // a check is a picture is loaded
		                                // in the game or not

		private System.Windows.Forms.Panel tilesPanel;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuLoad;
		private System.Windows.Forms.MenuItem menuShuffle;
		private System.Windows.Forms.MenuItem menuSize;
		private System.Windows.Forms.MenuItem menu3X3;
		private System.Windows.Forms.MenuItem menu4X4;
		private System.Windows.Forms.MenuItem menu5X5;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.MenuItem menuGridColor;
		private System.Windows.Forms.MenuItem menuWhite;
		private System.Windows.Forms.MenuItem menuGreen;
		private System.Windows.Forms.MenuItem menuBlue;
		private System.Windows.Forms.MenuItem menuRed;
        private System.Windows.Forms.MenuItem menuSilver;
		#endregion Private Fields
        private MenuItem menuItem1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem bildLadenToolStripMenuItem;
        private ToolStripMenuItem zufallsmusterErzeugenToolStripMenuItem;
        private IContainer components;

		public mnfrm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.tilesPanel = new System.Windows.Forms.Panel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuLoad = new System.Windows.Forms.MenuItem();
            this.menuShuffle = new System.Windows.Forms.MenuItem();
            this.menuSize = new System.Windows.Forms.MenuItem();
            this.menuGridColor = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menu3X3 = new System.Windows.Forms.MenuItem();
            this.menu4X4 = new System.Windows.Forms.MenuItem();
            this.menu5X5 = new System.Windows.Forms.MenuItem();
            this.menuWhite = new System.Windows.Forms.MenuItem();
            this.menuGreen = new System.Windows.Forms.MenuItem();
            this.menuBlue = new System.Windows.Forms.MenuItem();
            this.menuRed = new System.Windows.Forms.MenuItem();
            this.menuSilver = new System.Windows.Forms.MenuItem();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bildLadenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zufallsmusterErzeugenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tilesPanel
            // 
            this.tilesPanel.BackColor = System.Drawing.Color.PaleGreen;
            this.tilesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tilesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesPanel.Location = new System.Drawing.Point(0, 24);
            this.tilesPanel.Name = "tilesPanel";
            this.tilesPanel.Size = new System.Drawing.Size(288, 257);
            this.tilesPanel.TabIndex = 0;
            this.tilesPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tilesPanel_MouseDown);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuLoad,
            this.menuShuffle,
            this.menuSize,
            this.menuGridColor,
            this.menuItem1});
            // 
            // menuLoad
            // 
            this.menuLoad.Index = 0;
            this.menuLoad.Text = "Bild laden";
            // 
            // menuShuffle
            // 
            this.menuShuffle.Enabled = false;
            this.menuShuffle.Index = 1;
            this.menuShuffle.Text = "Zufallsmuster erzeugen";
            // 
            // menuSize
            // 
            this.menuSize.Index = 2;
            this.menuSize.Text = "";
            // 
            // menuGridColor
            // 
            this.menuGridColor.Index = 3;
            this.menuGridColor.Text = "";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 4;
            this.menuItem1.Text = "";
            // 
            // menu3X3
            // 
            this.menu3X3.Index = -1;
            this.menu3X3.Text = "";
            // 
            // menu4X4
            // 
            this.menu4X4.Index = -1;
            this.menu4X4.Text = "";
            // 
            // menu5X5
            // 
            this.menu5X5.Index = -1;
            this.menu5X5.Text = "";
            // 
            // menuWhite
            // 
            this.menuWhite.Index = -1;
            this.menuWhite.Text = "";
            // 
            // menuGreen
            // 
            this.menuGreen.Index = -1;
            this.menuGreen.Text = "";
            // 
            // menuBlue
            // 
            this.menuBlue.Index = -1;
            this.menuBlue.Text = "";
            // 
            // menuRed
            // 
            this.menuRed.Index = -1;
            this.menuRed.Text = "";
            // 
            // menuSilver
            // 
            this.menuSilver.Index = -1;
            this.menuSilver.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bildLadenToolStripMenuItem,
            this.zufallsmusterErzeugenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(288, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bildLadenToolStripMenuItem
            // 
            this.bildLadenToolStripMenuItem.Name = "bildLadenToolStripMenuItem";
            this.bildLadenToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.bildLadenToolStripMenuItem.Text = "Bild laden";
            this.bildLadenToolStripMenuItem.Click += new System.EventHandler(this.bildLadenToolStripMenuItem_Click);
            // 
            // zufallsmusterErzeugenToolStripMenuItem
            // 
            this.zufallsmusterErzeugenToolStripMenuItem.Name = "zufallsmusterErzeugenToolStripMenuItem";
            this.zufallsmusterErzeugenToolStripMenuItem.Size = new System.Drawing.Size(142, 20);
            this.zufallsmusterErzeugenToolStripMenuItem.Text = "Zufallsmuster erzeugen";
            this.zufallsmusterErzeugenToolStripMenuItem.Click += new System.EventHandler(this.zufallsmusterErzeugenToolStripMenuItem_Click);
            // 
            // mnfrm
            // 
            this.Controls.Add(this.tilesPanel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "mnfrm";
            this.Size = new System.Drawing.Size(288, 281);
            this.Load += new System.EventHandler(this.mnfrm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mnfrm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		// This function is called when the user 
		// uses the menu to load an image
		private void mnfrm_Load(object sender, System.EventArgs e)
		{
			MakeTiles(nRows, nCols);
		}

		#region Randomize and Timer event
		// This function shuffles the tiles in the gird 
		protected void Randomize()
		{
			rand = new Random();
			timerCountdown = 32 * nRows * nCols;   // The shuffle time u
			                                       // can adjust this to
			                                       // decrease the shuffle time
			Timer timer    = new Timer();
			timer.Tick    += new EventHandler(TimerOnTick);
			timer.Interval = 1;
			timer.Enabled  = true;
		}

		// Here a new location for moving a tile
		// in the shuffling process is randomly
		// made, on the condition to be in the limits
		// of the puzzle's grid size.
		void TimerOnTick(object obj, EventArgs ea)
		{
			int col = blankTile.Col;
			int row = blankTile.Row;

			switch(rand.Next(4))
			{
				case 0:  col++;  break;
				case 1:  col--;  break;
				case 2:  row++;  break;
				case 3:  row--;  break;
			}
			if (col >= 0 && col < nCols && row >= 0 && row < nRows)
			{
				MoveTile(col, row); // after making a random location
				// the tile is moved to that location
			}

			if (--timerCountdown == 0)
			{
				((Timer)obj).Stop();
				((Timer)obj).Tick -= new EventHandler(TimerOnTick);
				menuSize.Enabled = true;
			}
		}
		#endregion Randomize and Timer event

		#region Move Tile (int Col, Int Row)
		// This function handles moving a tile
		// to a new location in the grid
		void MoveTile(int Col, int Row)
		{
			tile[Row, Col].Location = new Point(blankTile.Col * tSquare,
										        blankTile.Row * tSquare);

			tile[blankTile.Row, blankTile.Col] = tile[Row, Col];
			tile[Row, Col] = null;
			blankTile = new Block(Row, Col);
		}
		#endregion Move Tile (int Col, Int Row)

		#region KeyBoard and Mouse events
		// Here all the key events handled in the puzzle
		// are identified
		private void mnfrm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ((menuShuffle.Enabled) | (PictureLoaded == false)) return; // Check if the game is shuffled
																		  // or not and the	Picture is Loaded
			// Arrow Keys Left
            Console.WriteLine(e.KeyCode);
			if (e.KeyCode == Keys.L && blankTile.Col < nCols - 1)
				MoveTile(blankTile.Col + 1, blankTile.Row);

			// Arrow Keys Right
			else if (e.KeyCode == Keys.R && blankTile.Col > 0)
				MoveTile(blankTile.Col - 1, blankTile.Row);

			// Arrow Keys Up
			else if (e.KeyCode == Keys.H && blankTile.Row < nRows - 1)
				MoveTile(blankTile.Col, blankTile.Row + 1);

			// Arrow Keys Down
			else if (e.KeyCode == Keys.U && blankTile.Row > 0)
				MoveTile(blankTile.Col, blankTile.Row - 1);

			e.Handled = true;  //Handle the event !!

			CheckFinish();  // Check if the puzzle is solved
		}

		// Here all the mouse events handled in the puzzle
		// are identified
		private void tilesPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if ((menuShuffle.Enabled) | (PictureLoaded == false)) return; // Check if the game is shuffled
															  			  // or not and the	Picture is Loaded

			int Col = e.X / tSquare;
			int Row = e.Y / tSquare;

			if (Col == blankTile.Col)
			{
				if (Row < blankTile.Row)
					for (int Row2 = blankTile.Row - 1; Row2 >= Row; Row2--)
						MoveTile(Col, Row2);

				else if (Row > blankTile.Row)
					for (int Row2 = blankTile.Row + 1; Row2 <= Row; Row2++)
						MoveTile(Col, Row2);
			}
			else if (Row == blankTile.Row)
			{
				if (Col < blankTile.Col)
					for (int Col2 = blankTile.Col - 1; Col2 >= Col; Col2--)
						MoveTile(Col2, Row);

				else if (Col > blankTile.Col)
					for (int Col2 = blankTile.Col + 1; Col2 <= Col; Col2++)
						MoveTile(Col2, Row);
			}

			CheckFinish(); // Check if the puzzle is solved
		}
		#endregion KeyBoard and Mouse events

		#region Check tiles if rearranged Correctly
		// this Method checks if the tiles are rearranged in the
		// correct order, if so the game is said to be over and
		// the player is congratulated :)
		void CheckFinish()
		{
			bool Finished = true;

			int index = 0;
			for (int Row=0; Row < nRows; Row++)
			{
				for (int Col=0; Col < nCols; Col++)
				{
					if ((index!=nRows*nCols) & (tile[Row,Col]!=null)) 
					{
						Finished &= (tile[Row,Col].tIndex == index);
					}
					index++;
					if (!Finished) return;
				}
			}
			
			if (Finished) 
			{
				tile[nRows-1,nCols-1] = tmplasttile;
                tile[nRows-1,nCols-1].Visible = true;
				MessageBox.Show("Congratulations!!, You did it !!","Game Over",
				  System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
				blankTile = new Block(nRows-1, nCols -1);
				menuShuffle.Enabled = true;
				menuGridColor.Enabled = false;
			}
		}
		#endregion Check tiles if rearranged Correctly

		#region menu SIZE
		// Here all the sizing process is done, care must be taken
		// that if the grid size is changed the tiles are cleared
		// for a new game!!

		// This method sets the grid to 3X3 (3 Rows by 3 Columns)
		private void menu3X3_Click(object sender, System.EventArgs e)
		{
		  if(menu3X3.Checked) return;
		  clearItems();
		  menu3X3.Checked = true;
		  nRows = 3;
		  nCols = 3;
		  MakeTiles(nRows, nCols);
		}

		// This method sets the grid to 4X4 (4 Rows by 4 Columns)
		private void menu4X4_Click(object sender, System.EventArgs e)
		{
			if(menu4X4.Checked) return;
			clearItems();
			menu4X4.Checked = true;
			nRows = 4;
			nCols = 4;
			MakeTiles(nRows, nCols);		
		}

		// This method sets the grid to 5X5 (5 Rows by 5 Columns)
		private void menu5X5_Click(object sender, System.EventArgs e)
		{	
			if(menu5X5.Checked) return;
			clearItems();
			menu5X5.Checked = true;
			nRows = 5;
			nCols = 5;
			MakeTiles(nRows, nCols);		
		}

		// This function clears all the check marks in the Size menu
		void clearItems()
		{
			menu3X3.Checked = false;
			menu4X4.Checked = false;
			menu5X5.Checked = false;
			for (int Row = 0; Row < nRows; Row ++)
				for (int Col = 0; Col < nCols; Col ++)
				{
					try {tile[Row,Col].Dispose();}
					catch {/*do nothing*/}
				}
			menuShuffle.Enabled = false;
			menuGridColor.Enabled = true;
			PictureLoaded = false;
		}

		#endregion menu SIZE

		# region Make Tiles (int Row, int Cols)
		// This function builds the tiles in the grid 
		// and arranges them
		void MakeTiles(int Rows, int Cols)
		{
			int index = 0;
            
			tile = new ctlTile[Rows, Cols];
			tilesPanel.Size = new Size(tSquare * Rows + 4 , tSquare * Cols + 4);
			tilesPanel.Location = new Point(4,4);
			this.ClientSize  = new Size(tilesPanel.Size.Width + 6,tilesPanel.Size.Height + 6);

			for (int Row= 0; Row < Rows; Row++)
			{
				for (int Col = 0; Col < Cols; Col++)
				{
					tile[Row,Col] = new ctlTile(tSquare,tSquare, index);
					tile[Row,Col].Parent = this.tilesPanel;
					tile[Row,Col].Location = new Point(Col * tSquare, Row * tSquare);
					index++;
				}
			}
		}
		# endregion Make Tiles (int Row, int Cols)

		#region Grid Color
		
		// Here the menu Color Grid processing
		// is handled

		void ClearColors()
		{
			menuWhite.Checked = false;
			menuGreen.Checked = false;
			menuBlue.Checked = false;
			menuRed.Checked = false;
			menuSilver.Checked = false;
		}

		private void menuWhite_Click(object sender, System.EventArgs e)
		{
		  ClearColors();
		  menuWhite.Checked = true;
		  this.tilesPanel.BackColor = Color.LightYellow;
		}

		private void menuGreen_Click(object sender, System.EventArgs e)
		{
			ClearColors();
			menuGreen.Checked = true;
			this.tilesPanel.BackColor = Color.PaleGreen;
		}

		private void menuBlue_Click(object sender, System.EventArgs e)
		{
			ClearColors();
			menuBlue.Checked = true;
			this.tilesPanel.BackColor = Color.LightBlue;
		}

		private void menuRed_Click(object sender, System.EventArgs e)
		{
			ClearColors();
			menuRed.Checked = true;
			this.tilesPanel.BackColor = Color.LightCoral;
		}

		private void menuSilver_Click(object sender, System.EventArgs e)
		{
			ClearColors();
			menuSilver.Checked = true;
			this.tilesPanel.BackColor = Color.Silver;
		}
		#endregion Grid Color

        private void bildLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            openFile.Title = "Bild wählen ...";
            openFile.FileName = "";
            // the formats acceptable
            openFile.Filter = "Formate (*.jpg,*.bmp,*.gif)|*.jpg;*.bmp;*.gif";
            openFile.ShowDialog();
            if (openFile.FileName == "")
            {
                menuGridColor.Enabled = true;
                return;
            }

            for (int Row = 0; Row < nRows; Row++)
                for (int Col = 0; Col < nCols; Col++)
                {
                    try { tile[Row, Col].Dispose(); }
                    catch {/*Do nothing*/}
                }
            MakeTiles(nRows, nCols);

            int cxThumbnail = tImageW * nRows;
            int cyThumbnail = tImageW * nRows;

            Image Pic = Image.FromFile(openFile.FileName);
            Pic = Pic.GetThumbnailImage(cxThumbnail, cyThumbnail, null, (IntPtr)0);

            for (int Row = 0; Row < nRows; Row++)
            {
                for (int Col = 0; Col < nCols; Col++)
                {
                    tile[Row, Col].tilePicture(Pic, new Point(Col * tImageW, Row * tImageW));
                }
            }

            blankTile = new Block(nRows - 1, nCols - 1);
            menuShuffle.Enabled = true;
            menuGridColor.Enabled = false;
            PictureLoaded = true;
        }

        private void zufallsmusterErzeugenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuSize.Enabled = false;
            menuShuffle.Enabled = false;
            menuGridColor.Enabled = true;
            tmplasttile = tile[nRows - 1, nCols - 1];
            tmplasttile.Visible = false;
            tile[nRows - 1, nCols - 1].Visible = false;
            Randomize();
        }
  	}
}
