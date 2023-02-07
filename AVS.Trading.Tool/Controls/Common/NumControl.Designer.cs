namespace AVS.Trading.Tool.Controls.Common
{
    partial class NumControl
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
            this.lblCurrency = new System.Windows.Forms.Label();
            this.textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(160, 3);
            this.lblCurrency.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(40, 20);
            this.lblCurrency.TabIndex = 9;
            this.lblCurrency.Text = "BTC";
            // 
            // textbox
            // 
            this.textbox.Location = new System.Drawing.Point(4, 0);
            this.textbox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textbox.Name = "textbox";
            this.textbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textbox.Size = new System.Drawing.Size(148, 26);
            this.textbox.TabIndex = 8;
            this.textbox.Text = "0.00000000";
            this.textbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textbox.TextChanged += new System.EventHandler(this.textbox_TextChanged);
            this.textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_KeyPress);
            // 
            // NumControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.textbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NumControl";
            this.Size = new System.Drawing.Size(220, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.TextBox textbox;
    }
}
