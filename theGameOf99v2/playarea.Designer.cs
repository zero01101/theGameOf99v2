namespace theGameOf99v2
{
    partial class PlayArea
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param Name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.btnDrawCard = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.cardsPanel = new System.Windows.Forms.Panel();
            this.radioCard0 = new theGameOf99v2.Card();
            this.radioCard4 = new theGameOf99v2.Card();
            this.radioCard3 = new theGameOf99v2.Card();
            this.radioCard2 = new theGameOf99v2.Card();
            this.radioCard1 = new theGameOf99v2.Card();
            this.btnHand = new System.Windows.Forms.Button();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cardsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDrawCard
            // 
            this.btnDrawCard.Enabled = false;
            this.btnDrawCard.Location = new System.Drawing.Point(392, 251);
            this.btnDrawCard.Name = "btnDrawCard";
            this.btnDrawCard.Size = new System.Drawing.Size(186, 23);
            this.btnDrawCard.TabIndex = 14;
            this.btnDrawCard.Text = "draw new card";
            this.btnDrawCard.UseVisualStyleBackColor = true;
            this.btnDrawCard.Click += new System.EventHandler(this.OnDrawCardClicked);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Enabled = false;
            this.btnDiscard.Location = new System.Drawing.Point(392, 222);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(186, 23);
            this.btnDiscard.TabIndex = 13;
            this.btnDiscard.Text = "discard selected card";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.OnDiscardClicked);
            // 
            // cardsPanel
            // 
            this.cardsPanel.Controls.Add(this.radioCard0);
            this.cardsPanel.Controls.Add(this.radioCard4);
            this.cardsPanel.Controls.Add(this.radioCard3);
            this.cardsPanel.Controls.Add(this.radioCard2);
            this.cardsPanel.Controls.Add(this.radioCard1);
            this.cardsPanel.Location = new System.Drawing.Point(392, 66);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Size = new System.Drawing.Size(186, 150);
            this.cardsPanel.TabIndex = 12;
            this.cardsPanel.Visible = false;
            // 
            // radioCard0
            // 
            this.radioCard0.Id = 0;
            this.radioCard0.Location = new System.Drawing.Point(3, 3);
            this.radioCard0.Name = "radioCard0";
            this.radioCard0.Size = new System.Drawing.Size(104, 24);
            this.radioCard0.TabIndex = 4;
            this.radioCard0.TabStop = true;
            this.radioCard0.Text = "card";
            this.radioCard0.UseVisualStyleBackColor = true;
            this.radioCard0.Visible = false;
            this.radioCard0.CheckedChanged += new System.EventHandler(this.OnCardChecked);
            // 
            // radioCard4
            // 
            this.radioCard4.Id = 4;
            this.radioCard4.Location = new System.Drawing.Point(3, 123);
            this.radioCard4.Name = "radioCard4";
            this.radioCard4.Size = new System.Drawing.Size(104, 24);
            this.radioCard4.TabIndex = 3;
            this.radioCard4.TabStop = true;
            this.radioCard4.Text = "card";
            this.radioCard4.UseVisualStyleBackColor = true;
            this.radioCard4.Visible = false;
            this.radioCard4.CheckedChanged += new System.EventHandler(this.OnCardChecked);
            // 
            // radioCard3
            // 
            this.radioCard3.Id = 3;
            this.radioCard3.Location = new System.Drawing.Point(3, 93);
            this.radioCard3.Name = "radioCard3";
            this.radioCard3.Size = new System.Drawing.Size(104, 24);
            this.radioCard3.TabIndex = 2;
            this.radioCard3.TabStop = true;
            this.radioCard3.Text = "card";
            this.radioCard3.UseVisualStyleBackColor = true;
            this.radioCard3.Visible = false;
            this.radioCard3.CheckedChanged += new System.EventHandler(this.OnCardChecked);
            // 
            // radioCard2
            // 
            this.radioCard2.Id = 2;
            this.radioCard2.Location = new System.Drawing.Point(3, 63);
            this.radioCard2.Name = "radioCard2";
            this.radioCard2.Size = new System.Drawing.Size(104, 24);
            this.radioCard2.TabIndex = 1;
            this.radioCard2.TabStop = true;
            this.radioCard2.Text = "card";
            this.radioCard2.UseVisualStyleBackColor = true;
            this.radioCard2.Visible = false;
            this.radioCard2.CheckedChanged += new System.EventHandler(this.OnCardChecked);
            // 
            // radioCard1
            // 
            this.radioCard1.Id = 1;
            this.radioCard1.Location = new System.Drawing.Point(3, 33);
            this.radioCard1.Name = "radioCard1";
            this.radioCard1.Size = new System.Drawing.Size(104, 24);
            this.radioCard1.TabIndex = 0;
            this.radioCard1.TabStop = true;
            this.radioCard1.Text = "card";
            this.radioCard1.UseVisualStyleBackColor = true;
            this.radioCard1.Visible = false;
            this.radioCard1.CheckedChanged += new System.EventHandler(this.OnCardChecked);
            // 
            // btnHand
            // 
            this.btnHand.Location = new System.Drawing.Point(392, 37);
            this.btnHand.Name = "btnHand";
            this.btnHand.Size = new System.Drawing.Size(186, 23);
            this.btnHand.TabIndex = 11;
            this.btnHand.Text = "show Hand";
            this.btnHand.UseVisualStyleBackColor = true;
            this.btnHand.Click += new System.EventHandler(this.OnToggleHandClicked);
            // 
            // lblPlayer
            // 
            this.lblPlayer.Location = new System.Drawing.Point(478, 11);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(100, 23);
            this.lblPlayer.TabIndex = 10;
            this.lblPlayer.Text = "label2";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(392, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "current Player:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 356);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(566, 20);
            this.textBox1.TabIndex = 8;
            // 
            // PlayArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 386);
            this.Controls.Add(this.btnDrawCard);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.cardsPanel);
            this.Controls.Add(this.btnHand);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(606, 424);
            this.MinimumSize = new System.Drawing.Size(606, 424);
            this.Name = "PlayArea";
            this.Text = "the Game of 99!";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.cardsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDrawCard;
        private System.Windows.Forms.Button btnDiscard;
        private System.Windows.Forms.Panel cardsPanel;
        private Card radioCard0;
        private Card radioCard4;
        private Card radioCard3;
        private Card radioCard2;
        private Card radioCard1;
        private System.Windows.Forms.Button btnHand;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}