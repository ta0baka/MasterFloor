namespace MasterFloor
{
    partial class PartnerEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartnerEditForm));
            lblEmail = new Label();
            lblPhone = new Label();
            lblDirector = new Label();
            lblAddress = new Label();
            lblRating = new Label();
            lblType = new Label();
            lblName = new Label();
            btnBack = new Button();
            txtEmail = new TextBox();
            txtDirector = new TextBox();
            txtAddress = new TextBox();
            numRating = new NumericUpDown();
            txtName = new TextBox();
            cmbType = new ComboBox();
            lblINN = new Label();
            lblAddEdit = new Label();
            btnSave = new Button();
            maskedTxtINN = new MaskedTextBox();
            maskedTextBoxPhone = new MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)numRating).BeginInit();
            SuspendLayout();
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblEmail.Location = new Point(46, 231);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(274, 25);
            lblEmail.TabIndex = 32;
            lblEmail.Text = "Электронная почта партнера:";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblPhone.Location = new Point(46, 270);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(181, 25);
            lblPhone.TabIndex = 31;
            lblPhone.Text = "Телефон партнера:";
            // 
            // lblDirector
            // 
            lblDirector.AutoSize = true;
            lblDirector.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblDirector.Location = new Point(47, 192);
            lblDirector.Name = "lblDirector";
            lblDirector.Size = new Size(103, 25);
            lblDirector.TabIndex = 30;
            lblDirector.Text = "Директор:";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblAddress.Location = new Point(46, 309);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(285, 25);
            lblAddress.TabIndex = 29;
            lblAddress.Text = "Юридический адрес партнера:";
            // 
            // lblRating
            // 
            lblRating.AutoSize = true;
            lblRating.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblRating.Location = new Point(46, 386);
            lblRating.Name = "lblRating";
            lblRating.Size = new Size(85, 25);
            lblRating.TabIndex = 28;
            lblRating.Text = "Рейтинг:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblType.Location = new Point(47, 112);
            lblType.Name = "lblType";
            lblType.Size = new Size(136, 25);
            lblType.TabIndex = 27;
            lblType.Text = "Тип партнера:";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblName.Location = new Point(46, 153);
            lblName.Name = "lblName";
            lblName.Size = new Size(237, 25);
            lblName.TabIndex = 26;
            lblName.Text = "Наименование партнера:";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(103, 186, 128);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnBack.Location = new Point(733, 440);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(117, 61);
            btnBack.TabIndex = 36;
            btnBack.Text = "Назад";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += btnBack_Click;
            // 
            // txtEmail
            // 
            txtEmail.BackColor = Color.FromArgb(244, 232, 211);
            txtEmail.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtEmail.Location = new Point(335, 228);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(512, 31);
            txtEmail.TabIndex = 23;
            // 
            // txtDirector
            // 
            txtDirector.BackColor = Color.FromArgb(244, 232, 211);
            txtDirector.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtDirector.Location = new Point(335, 189);
            txtDirector.Margin = new Padding(3, 4, 3, 4);
            txtDirector.Name = "txtDirector";
            txtDirector.Size = new Size(512, 31);
            txtDirector.TabIndex = 21;
            // 
            // txtAddress
            // 
            txtAddress.BackColor = Color.FromArgb(244, 232, 211);
            txtAddress.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtAddress.Location = new Point(336, 306);
            txtAddress.Margin = new Padding(3, 4, 3, 4);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(511, 31);
            txtAddress.TabIndex = 20;
            // 
            // numRating
            // 
            numRating.BackColor = Color.FromArgb(244, 232, 211);
            numRating.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numRating.Location = new Point(336, 384);
            numRating.Margin = new Padding(3, 4, 3, 4);
            numRating.Name = "numRating";
            numRating.Size = new Size(511, 31);
            numRating.TabIndex = 19;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(244, 232, 211);
            txtName.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtName.Location = new Point(336, 150);
            txtName.Margin = new Padding(3, 4, 3, 4);
            txtName.Name = "txtName";
            txtName.Size = new Size(514, 31);
            txtName.TabIndex = 18;
            // 
            // cmbType
            // 
            cmbType.BackColor = Color.FromArgb(244, 232, 211);
            cmbType.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "ООО", "ЗАО", "ОАО", "ПАО", "ИП" });
            cmbType.Location = new Point(336, 109);
            cmbType.Margin = new Padding(3, 4, 3, 4);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(513, 33);
            cmbType.TabIndex = 17;
            // 
            // lblINN
            // 
            lblINN.AutoSize = true;
            lblINN.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            lblINN.Location = new Point(47, 348);
            lblINN.Name = "lblINN";
            lblINN.Size = new Size(54, 25);
            lblINN.TabIndex = 34;
            lblINN.Text = "ИНН";
            // 
            // lblAddEdit
            // 
            lblAddEdit.AutoSize = true;
            lblAddEdit.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblAddEdit.Location = new Point(47, 32);
            lblAddEdit.Name = "lblAddEdit";
            lblAddEdit.Size = new Size(557, 38);
            lblAddEdit.TabIndex = 38;
            lblAddEdit.Text = "Добавление/Редактирование партнера";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(103, 186, 128);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSave.ForeColor = SystemColors.ControlText;
            btnSave.Location = new Point(336, 440);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(180, 61);
            btnSave.TabIndex = 39;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // maskedTxtINN
            // 
            maskedTxtINN.BackColor = Color.FromArgb(244, 232, 211);
            maskedTxtINN.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            maskedTxtINN.Location = new Point(336, 346);
            maskedTxtINN.Mask = "0000000000";
            maskedTxtINN.Name = "maskedTxtINN";
            maskedTxtINN.Size = new Size(510, 31);
            maskedTxtINN.TabIndex = 40;
            // 
            // maskedTextBoxPhone
            // 
            maskedTextBoxPhone.BackColor = Color.FromArgb(244, 232, 211);
            maskedTextBoxPhone.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            maskedTextBoxPhone.Location = new Point(337, 268);
            maskedTextBoxPhone.Mask = "000 000 00 00";
            maskedTextBoxPhone.Name = "maskedTextBoxPhone";
            maskedTextBoxPhone.Size = new Size(510, 31);
            maskedTextBoxPhone.TabIndex = 41;
            // 
            // PartnerEditForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(883, 529);
            Controls.Add(maskedTextBoxPhone);
            Controls.Add(maskedTxtINN);
            Controls.Add(btnSave);
            Controls.Add(lblAddEdit);
            Controls.Add(lblINN);
            Controls.Add(lblEmail);
            Controls.Add(lblPhone);
            Controls.Add(lblDirector);
            Controls.Add(lblAddress);
            Controls.Add(lblRating);
            Controls.Add(lblType);
            Controls.Add(lblName);
            Controls.Add(btnBack);
            Controls.Add(txtEmail);
            Controls.Add(txtDirector);
            Controls.Add(txtAddress);
            Controls.Add(numRating);
            Controls.Add(txtName);
            Controls.Add(cmbType);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PartnerEditForm";
            Text = "Добавление/Редактирование партнера";
            ((System.ComponentModel.ISupportInitialize)numRating).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblEmail;
        private Label lblPhone;
        private Label lblDirector;
        private Label lblAddress;
        private Label lblRating;
        private Label lblType;
        private Label lblName;
        private Button btnBack;
        private TextBox txtEmail;
        private TextBox maskedTxtPhone;
        private TextBox txtDirector;
        private TextBox txtAddress;
        private NumericUpDown numRating;
        private TextBox txtName;
        private ComboBox cmbType;
        private Label lblINN;
        private Label lblAddEdit;
        private Button btnSave;
        private MaskedTextBox maskedTxtINN;
        private MaskedTextBox maskedTextBoxPhone;
    }
}