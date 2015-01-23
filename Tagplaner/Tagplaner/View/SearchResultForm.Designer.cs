namespace Tagplaner
{
    partial class SearchResultForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Weitersuchen = new System.Windows.Forms.Button();
            this.button_NichtWeitersuchen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Weitersuchen
            // 
            this.button_Weitersuchen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_Weitersuchen.Location = new System.Drawing.Point(89, 58);
            this.button_Weitersuchen.Name = "button_Weitersuchen";
            this.button_Weitersuchen.Size = new System.Drawing.Size(108, 23);
            this.button_Weitersuchen.TabIndex = 0;
            this.button_Weitersuchen.Text = "Ja";
            this.button_Weitersuchen.UseVisualStyleBackColor = true;
            this.button_Weitersuchen.Click += new System.EventHandler(this.button_Weitersuchen_Click);
            // 
            // button_NichtWeitersuchen
            // 
            this.button_NichtWeitersuchen.Location = new System.Drawing.Point(203, 58);
            this.button_NichtWeitersuchen.Name = "button_NichtWeitersuchen";
            this.button_NichtWeitersuchen.Size = new System.Drawing.Size(108, 23);
            this.button_NichtWeitersuchen.TabIndex = 1;
            this.button_NichtWeitersuchen.Text = "Nein";
            this.button_NichtWeitersuchen.UseVisualStyleBackColor = true;
            this.button_NichtWeitersuchen.Click += new System.EventHandler(this.button_NichtWeitersuchen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(313, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Es wurden Suchergebnisse gefunden. Wollen Sie weitersuchen?";
            // 
            // SearchResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 93);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_NichtWeitersuchen);
            this.Controls.Add(this.button_Weitersuchen);
            this.Name = "SearchResultForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Suchergebnis gefunden";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Weitersuchen;
        private System.Windows.Forms.Button button_NichtWeitersuchen;
        private System.Windows.Forms.Label label1;
    }
}