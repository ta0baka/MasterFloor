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

        // Метод для расчета количества материала (в самом приложении никак не используется)
        public static int CalculateMaterialRequired(
            int productTypeId,
            int materialTypeId,
            int productQuantity,
            double productParam1,
            double productParam2,
            string connectionString)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Получаем коэффициент типа продукции
                    decimal typeCoefficient = 0;
                    string productTypeQuery = "SELECT type_coefficient FROM product_types WHERE product_type_id = @id";
                    using (var cmd = new NpgsqlCommand(productTypeQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", productTypeId);
                        var result = cmd.ExecuteScalar();
                        if (result == null) return -1;
                        typeCoefficient = (decimal)result;
                    }

                    // Получаем процент брака материала
                    decimal defectPercentage = 0;
                    string materialTypeQuery = "SELECT defect_percentage FROM material_types WHERE material_type_id = @id";
                    using (var cmd = new NpgsqlCommand(materialTypeQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", materialTypeId);
                        var result = cmd.ExecuteScalar();
                        if (result == null) return -1;
                        defectPercentage = (decimal)result;
                    }

                    // Проверка входных параметров
                    if (productQuantity <= 0 || productParam1 <= 0 || productParam2 <= 0)
                        return -1;

                    // Расчет количества материала на одну единицу продукции
                    double materialPerUnit = productParam1 * productParam2 * (double)typeCoefficient;

                    // Расчет общего количества материала с учетом брака
                    double totalMaterial = materialPerUnit * productQuantity;
                    double materialWithDefect = totalMaterial / (1 - (double)defectPercentage);

                    // Округляем вверх до целого числа
                    return (int)Math.Ceiling(materialWithDefect);
                }
            }
            catch
            {
                return -1;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}