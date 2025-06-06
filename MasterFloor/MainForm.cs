using Npgsql;

namespace MasterFloor
{
    public partial class MainForm : Form
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=2928;Database=master_floor";

        public MainForm()
        {
            InitializeComponent();
            LoadPartners();
        }

        private void LoadPartners()
        {
            flowLayoutPanel.Controls.Clear();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT p.partner_id, p.partner_type, p.partner_name, p.director, 
                          p.phone, p.rating, 
                          COALESCE(SUM(pp.quantity), 0) as total_sales
                          FROM partners p
                          LEFT JOIN partner_products pp ON p.partner_id = pp.partner_id
                          GROUP BY p.partner_id";

                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var partnerPanel = CreatePartnerPanel(reader);
                            flowLayoutPanel.Controls.Add(partnerPanel);
                        }
                    }
                }
            }
        }

        private Panel CreatePartnerPanel(NpgsqlDataReader reader)
        {
            int partnerId = Convert.ToInt32(reader["partner_id"]);
            string partnerType = reader["partner_type"]?.ToString() ?? "Не указано";
            string partnerName = reader["partner_name"]?.ToString() ?? "Неизвестный партнер";
            string director = reader["director"]?.ToString() ?? "Не указано";
            string phone = reader["phone"]?.ToString() ?? "Не указано";
            int rating = Convert.ToInt32(reader["rating"]);
            int totalSales = Convert.ToInt32(reader["total_sales"]);

            var panel = new Panel
            {
                Width = flowLayoutPanel.Width - 30,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Tag = partnerId
            };

            int discount = CalculateDiscount(totalSales);

            var nameContainer = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(panel.Width - 20, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            var lblName = new Label
            {
                Text = $"{partnerType} | {partnerName}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };

            var lblDiscount = new Label
            {
                Text = $"{discount}%",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(nameContainer.Width - 100, 0),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var lblDirector = new Label
            {
                Text = director,
                Location = new Point(10, 35),
                AutoSize = true
            };

            var lblPhone = new Label
            {
                Text = phone,
                Location = new Point(10, 60),
                AutoSize = true
            };

            var lblRating = new Label
            {
                Text = $"Рейтинг: {rating}",
                Location = new Point(10, 85),
                AutoSize = true
            };

            var btnHistory = new Button
            {
                Text = "История продаж",
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

            // Обработчик клика для редактирования инфы о партнере
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

            nameContainer.Controls.Add(lblName);
            nameContainer.Controls.Add(lblDiscount);
            panel.Controls.Add(nameContainer);
            panel.Controls.AddRange(new Control[] { lblDirector, lblPhone, lblRating, btnHistory });
            return panel;
        }

        private int CalculateDiscount(int totalSales)
        {
            if (totalSales >= 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            var editForm = new PartnerEditForm(connectionString);
            editForm.FormClosed += (s, args) => LoadPartners();
            editForm.ShowDialog();
        }
    }
}