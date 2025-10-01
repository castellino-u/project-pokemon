namespace PokemonVS1
{
    partial class Pokedex
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
            this.pbxPokemon = new System.Windows.Forms.PictureBox();
            this.dgvPokemons = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPokemon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPokemons)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxPokemon
            // 
            this.pbxPokemon.Location = new System.Drawing.Point(340, 62);
            this.pbxPokemon.Name = "pbxPokemon";
            this.pbxPokemon.Size = new System.Drawing.Size(344, 226);
            this.pbxPokemon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbxPokemon.TabIndex = 0;
            this.pbxPokemon.TabStop = false;
            this.pbxPokemon.Click += new System.EventHandler(this.Form1_Load);
            // 
            // dgvPokemons
            // 
            this.dgvPokemons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPokemons.Location = new System.Drawing.Point(245, 309);
            this.dgvPokemons.Name = "dgvPokemons";
            this.dgvPokemons.Size = new System.Drawing.Size(539, 110);
            this.dgvPokemons.TabIndex = 1;
            this.dgvPokemons.SelectionChanged += new System.EventHandler(this.dgvPokemons_SelectionChanged);
            // 
            // Pokedex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 526);
            this.Controls.Add(this.dgvPokemons);
            this.Controls.Add(this.pbxPokemon);
            this.Name = "Pokedex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokedex";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPokemon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPokemons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxPokemon;
        private System.Windows.Forms.DataGridView dgvPokemons;
    }
}

