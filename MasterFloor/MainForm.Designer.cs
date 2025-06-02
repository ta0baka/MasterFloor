namespace MasterFloor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            flowLayoutPanel = new FlowLayoutPanel();
            lblAccounting = new Label();
            pictureBox1 = new PictureBox();
            btnAddPartner = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.BackColor = Color.FromArgb(244, 232, 211);
            flowLayoutPanel.Location = new Point(12, 73);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(874, 503);
            flowLayoutPanel.TabIndex = 0;
            // 
            // lblAccounting
            // 
            lblAccounting.AutoSize = true;
            lblAccounting.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblAccounting.Location = new Point(64, 19);
            lblAccounting.Name = "lblAccounting";
            lblAccounting.Size = new Size(231, 38);
            lblAccounting.TabIndex = 1;
            lblAccounting.Text = "Учет партнеров";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(46, 45);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // btnAddPartner
            // 
            btnAddPartner.BackColor = Color.FromArgb(103, 186, 128);
            btnAddPartner.FlatStyle = FlatStyle.Flat;
            btnAddPartner.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnAddPartner.Location = new Point(304, 582);
            btnAddPartner.Name = "btnAddPartner";
            btnAddPartner.Size = new Size(291, 55);
            btnAddPartner.TabIndex = 0;
            btnAddPartner.Text = "Добавить партнера";
            btnAddPartner.UseVisualStyleBackColor = false;
            btnAddPartner.Click += btnAddPartner_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 642);
            Controls.Add(btnAddPartner);
            Controls.Add(pictureBox1);
            Controls.Add(lblAccounting);
            Controls.Add(flowLayoutPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Учет партнеров";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel;
        private Label lblAccounting;
        private PictureBox pictureBox1;
        private Button btnAddPartner;
    }
}
