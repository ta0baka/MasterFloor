namespace MasterFloor
{
    partial class PartnerSalesHistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartnerSalesHistoryForm));
            flowLayoutPanel = new FlowLayoutPanel();
            lblHistory = new Label();
            btnBack = new Button();
            SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoScroll = true;
            flowLayoutPanel.BackColor = Color.FromArgb(244, 232, 211);
            flowLayoutPanel.Location = new Point(12, 73);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new Size(874, 451);
            flowLayoutPanel.TabIndex = 0;
            // 
            // lblHistory
            // 
            lblHistory.AutoSize = true;
            lblHistory.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblHistory.Location = new Point(12, 19);
            lblHistory.Name = "lblHistory";
            lblHistory.Size = new Size(248, 38);
            lblHistory.TabIndex = 2;
            lblHistory.Text = "История продаж";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(103, 186, 128);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnBack.Location = new Point(761, 530);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(125, 55);
            btnBack.TabIndex = 37;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // PartnerSalesHistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(898, 593);
            Controls.Add(btnBack);
            Controls.Add(lblHistory);
            Controls.Add(flowLayoutPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PartnerSalesHistoryForm";
            Text = "История продаж";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel;
        private Label lblHistory;
        private Button btnBack;
    }
}