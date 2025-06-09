using Npgsql;

namespace MasterFloor
{
    public partial class MainForm : Form
    {   // ������ �����������
        private string connectionString = "Host=localhost;Username=postgres;Password=****;Database=master_floor";

        public MainForm()
        {
            // �������������� �������� �� �����
            InitializeComponent();
           // ��������� ������ � ��������� (����� ������� �������� ����)
            LoadPartners();
        }

        // ����� ��� ����������� ��������� (����������� ���� �� ��)
        private void LoadPartners()
        {
            // ���������� flowLayoutPanel (��� ������������� �������� Panel ������)
            // Clear ������� ������, ����� ����� � ������� ������� ���������� ���������� (�����������) ������
            flowLayoutPanel.Controls.Clear();
            // ������������ � ��
            // ����� using ������������� ������ ������ �� �� (��� ����� ������ ������������ � �������� ��������� � ��)
            // ��� �������� ����������, ��� ������ ������� �� �� ������, ������ ����� ���� var (������� ���������)
            // connection - �����������. �������� ��� ������, ������� ������ ������� (connectionString)
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                // ������ ��� ����������� ������ �� �����.
                // SELECT ������������ ������� ��������� �� ������� partners, ������ � p. (����������� ��� �������), ���� � ������� ������� ��� (partner_products), �� ������� �� ������ ����� �� ������� (pp)
                // COALESCE ����������� NULL �������� � 0 (��� ����� � ������, ���� �� ����� ��������, � �������� ��� ������� � ��������) �� ��������� ������.
                // SUM ������� ����� ���������� �������, ������� �������� �������. 0 � ���� ������� ������ ���� ������ (NULL �� 0 �� ��). ����� ����������� ��� ����� ���������� ������ (total_sales)
                // FROM partners - ������ ������� �� ������� partners
                // LEFT JOIN ������������ ��������� ������� partner_products � ������� partners ����� ������� ���� pp.partner_id, �� ���� id_partner �� ������� partners ����� ����� � id_partner �� partner_products (������� ����)
                // GROUP BY ���������� ������ �� id ��������. ��� ����������, ��� ��� �� ���������� ���������� ������� SUM, ��� ���������� ������ �� ������ ���������� ������ (������� �� ��������� ��������)
                string query = @"SELECT p.partner_id, p.partner_type, p.partner_name, p.director, 
                          p.phone, p.rating, 
                          COALESCE(SUM(pp.quantity), 0) as total_sales
                          FROM partners p
                          LEFT JOIN partner_products pp ON p.partner_id = pp.partner_id
                          GROUP BY p.partner_id";

                // ������� ������� ��� ���������� ������� (cmd)
                // query - ����������, ���������� ������ � SQL-�������� (������� �� �������� ����)
                // NpgsqlCommand ����������� ����� ��� ���������� SQL-������ � PostgreSQL (�� ���������� Npgsql)
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    // ExecuteReader() - ��������� SQL-������ � ���������� NpgsqlDataReader ��� ������ �����������, ����� ��������� �������� ��� ��������
                    using (var reader = cmd.ExecuteReader())
                    {
                        // reader.Read() - ���������� "������" �� ��������� ������ ����������, ���������� false ����� ����� ������ ��� (�������� Enter � Word, ������� �� ����� ������)
                        while (reader.Read())
                        {
                            // ������� ���������� ������ � ������� � �������� � ����������� �� �����, ������� �������� ����
                            var partnerPanel = CreatePartnerPanel(reader);
                            // ������� ���������� flowLayoutPanel ������� ��������� (partnerPanel)
                            flowLayoutPanel.Controls.Add(partnerPanel);
                        }
                    }
                }
            }
        }

        // �� ������� ��������� ������ ��� �������� �������, � ������� ����� ������������ ������ ������� ���������� �������� 
        private Panel CreatePartnerPanel(NpgsqlDataReader reader)
        {
            // ����� ����������� �������� �������, ������� ��������� ���������� �� ������� ����� (� ������ ������ partners)
            // ���� ������ �� ����� ����� �� ������� ��. int = serial, integer; string = varchar/character varying;
            // ��� ��������� �������� (string) �� ����������� �������, ����� ������, ������� �� �������� ���� ����������, ����������� ��� string
            // ������� - �������������� ������ �� ������ ������� � ������
            // ? - �������� �� NULL ��������
            // Convert.ToInt ����� ������ ���� �������� ��� �������������� �������� ������ � int ��������
            int partnerId = Convert.ToInt32(reader["partner_id"]);
            string partnerType = reader["partner_type"]?.ToString() ?? "�� �������";
            string partnerName = reader["partner_name"]?.ToString() ?? "����������� �������";
            string director = reader["director"]?.ToString() ?? "�� �������";
            string phone = reader["phone"]?.ToString() ?? "�� �������";
            int rating = Convert.ToInt32(reader["rating"]);
            int totalSales = Convert.ToInt32(reader["total_sales"]);

            // ������� ���������� ������
            // ��������� �� ��������: ������ (Width), ������ (Height), ����� ������ ������ (BorderStyle), ������ ����� �������� (Margin), ��� (Tag) - �������������� ��� �������� �� ������ �����
            var panel = new Panel
            {
                Width = flowLayoutPanel.Width - 30,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Tag = partnerId
            };

            // ������� ���������� ��� �������� ������� �� �������� ������ (����������� � �����)
            int discount = CalculateDiscount(totalSales);

            // 
            var nameContainer = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(panel.Width - 20, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            // ������� label, ������������ ��� � ��� ��������, ����������� ��������� ����� (��� ������, ������, ���������� (�������� ������ - Bold)
            // AutoSize - ���������� ����� (Label) ������������� ������������ ���� ������ ��� ����� ������
            var lblName = new Label
            {
                Text = $"{partnerType} | {partnerName}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };

            // ������� label ��� ����������� �������� ������
            // Location �������� ��� ����������
            // Anchor ����������� ������� (discount) � ������� ���� ������
            var lblDiscount = new Label
            {
                Text = $"{discount}%",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point( 750, 0),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // ������� Label ��� ����������� ������ � ���������
            var lblDirector = new Label
            {
                Text = director,
                Location = new Point(10, 35),
                AutoSize = true
            };

            //������� Label ��� ����������� ������ � ������ ��������
            var lblPhone = new Label
            {
                Text = $"+7 {phone}",
                Location = new Point(10, 60),
                AutoSize = true
            };

            //������� Label ��� ����������� ������ � ��������
            var lblRating = new Label
            {
                Text = $"�������: {rating}",
                Location = new Point(10, 85),
                AutoSize = true
            };

            // ��� 4 ������ (�� �������)
            var btnHistory = new Button
            {
                Text = "������� ������",
                Location = new Point(10, 110),
                Size = new Size(150, 30),
                FlatStyle = FlatStyle.Flat,
                Tag = partnerId
            };

            btnHistory.Click += (sender, e) =>
            {
                var historyForm = new PartnerSalesHistoryForm(
                    partnerId,
                    partnerName,
                    connectionString);
                historyForm.Show();
            };

            // 3 ������ (�� �������)
            panel.Click += (sender, e) =>
            {
                if (e is MouseEventArgs mouseArgs && sender is Panel panelClicked)
                {
                    if (mouseArgs.X >= btnHistory.Left && mouseArgs.X <= btnHistory.Right &&
                        mouseArgs.Y >= btnHistory.Top && mouseArgs.Y <= btnHistory.Bottom)
                    {
                        return;
                    }

                    var editForm = new PartnerEditForm(partnerId, connectionString);
                    editForm.FormClosed += (s, args) => LoadPartners();
                    editForm.ShowDialog();
                }
            };

            // ����������� 2 ������

            // nameContainer - ��� ������������ ������� (Panel)
            // lblName - ��������� ����� ����� (Label) � ��������� �������
            //����� (Label) ���������� �������� ��������� ���������� (Panel) � ������������ ������ ����
            nameContainer.Controls.Add(lblName);
            nameContainer.Controls.Add(lblDiscount);
            panel.Controls.Add(nameContainer);
            panel.Controls.AddRange(new Control[] { lblDirector, lblPhone, lblRating, btnHistory });
            return panel;
        }

        // �����, ������������� ������ �������� �� ������ ����� ����� ���������� ���������
        private int CalculateDiscount(int totalSales)
        {
            // ���� (if) ����� ����� ������� �������� (totalSales) ������ ��� ����� (>=) ��� 300 000 (�� ��), �� ������������ ������� 15, ��� � �������� ��������� ������
            if (totalSales >= 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }

        // 3 ������ (�� �������)
        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            var editForm = new PartnerEditForm(connectionString);
            editForm.FormClosed += (s, args) => LoadPartners();
            editForm.ShowDialog();
        }
    }
}