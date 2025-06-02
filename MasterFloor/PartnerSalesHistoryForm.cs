using Npgsql;

namespace MasterFloor
{
    public partial class PartnerSalesHistoryForm : Form
    {
        private string connectionString;
        private int partnerId;
        private string partnerName;

        public PartnerSalesHistoryForm(int partnerId, string partnerName, string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            this.Text = $"История продаж: {partnerName}";
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            flowLayoutPanel.Controls.Clear();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT p.product_name, pp.quantity, pp.sale_date 
                                    FROM partner_products pp
                                    JOIN products p ON pp.product_id = p.product_id
                                    WHERE pp.partner_id = @partnerId
                                    ORDER BY pp.sale_date DESC";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@partnerId", partnerId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var salePanel = CreateSalePanel(reader);
                                flowLayoutPanel.Controls.Add(salePanel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории продаж: {ex.Message}");
            }
        }

        private Panel CreateSalePanel(NpgsqlDataReader reader)
        {
            var panel = new Panel
            {
                Width = flowLayoutPanel.Width - 30,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5)
            };

            var lblProductName = new Label
            {
                Text = reader["product_name"].ToString(),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var lblQuantity = new Label
            {
                Text = $"Количество: {reader["quantity"]}",
                Location = new Point(10, 40),
                AutoSize = true
            };

            var saleDate = Convert.ToDateTime(reader["sale_date"]);
            var lblSaleDate = new Label
            {
                Text = $"Дата продажи: {saleDate:dd.MM.yyyy}",
                Location = new Point(panel.Width - 200, 40),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            panel.Controls.Add(lblProductName);
            panel.Controls.Add(lblQuantity);
            panel.Controls.Add(lblSaleDate);

            return panel;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}