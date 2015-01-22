using System.Windows.Forms;

namespace Tagplaner
{
    partial class SuchenUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_Suchen = new System.Windows.Forms.TextBox();
            this.button_Suchen = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox_Suchergebnisse = new System.Windows.Forms.GroupBox();
            this.groupBox_Suchbegriff = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox_Suchergebnisse.SuspendLayout();
            this.groupBox_Suchbegriff.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_Suchen
            // 
            this.textBox_Suchen.Location = new System.Drawing.Point(18, 28);
            this.textBox_Suchen.Name = "textBox_Suchen";
            this.textBox_Suchen.Size = new System.Drawing.Size(163, 20);
            this.textBox_Suchen.TabIndex = 0;
            // 
            // button_Suchen
            // 
            this.button_Suchen.Location = new System.Drawing.Point(18, 54);
            this.button_Suchen.Name = "button_Suchen";
            this.button_Suchen.Size = new System.Drawing.Size(106, 23);
            this.button_Suchen.TabIndex = 1;
            this.button_Suchen.Text = "Suche starten";
            this.button_Suchen.UseVisualStyleBackColor = true;
            this.button_Suchen.Click += new System.EventHandler(this.button_Suchen_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(725, 121);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellClick);
            // 
            // groupBox_Suchergebnisse
            // 
            this.groupBox_Suchergebnisse.Controls.Add(this.dataGridView1);
            this.groupBox_Suchergebnisse.Location = new System.Drawing.Point(16, 116);
            this.groupBox_Suchergebnisse.Name = "groupBox_Suchergebnisse";
            this.groupBox_Suchergebnisse.Size = new System.Drawing.Size(766, 174);
            this.groupBox_Suchergebnisse.TabIndex = 5;
            this.groupBox_Suchergebnisse.TabStop = false;
            this.groupBox_Suchergebnisse.Visible = false;
            this.groupBox_Suchergebnisse.Text = "Suchergebnisse";
            // 
            // groupBox_Suchbegriff
            // 
            this.groupBox_Suchbegriff.Controls.Add(this.textBox_Suchen);
            this.groupBox_Suchbegriff.Controls.Add(this.button_Suchen);
            this.groupBox_Suchbegriff.Location = new System.Drawing.Point(16, 15);
            this.groupBox_Suchbegriff.Name = "groupBox_Suchbegriff";
            this.groupBox_Suchbegriff.Size = new System.Drawing.Size(337, 95);
            this.groupBox_Suchbegriff.TabIndex = 6;
            this.groupBox_Suchbegriff.TabStop = false;
            this.groupBox_Suchbegriff.Text = "Suchbegriff eingeben";
            // 
            // SuchenUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox_Suchbegriff);
            this.Controls.Add(this.groupBox_Suchergebnisse);
            this.Name = "SuchenUserControl";
            this.Size = new System.Drawing.Size(908, 333);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox_Suchergebnisse.ResumeLayout(false);
            this.groupBox_Suchbegriff.ResumeLayout(false);
            this.groupBox_Suchbegriff.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Suchen;
        private System.Windows.Forms.Button button_Suchen;
        private System.Windows.Forms.DataGridView dataGridView1;
        private GroupBox groupBox_Suchergebnisse;
        private GroupBox groupBox_Suchbegriff;
    }
}
