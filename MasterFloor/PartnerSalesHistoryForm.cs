using Npgsql;

namespace MasterFloor
{
    public partial class PartnerSalesHistoryForm : Form
    {
        // Создаем переменные для хранения информации о подключении к БД, ID партнера и его имени
        private string connectionString;
        private int partnerId;
        private string partnerName;

        public PartnerSalesHistoryForm(int partnerId, string partnerName, string connectionString)
        {
            // Присваиваем значения переменных из MainForm.cs к переменным из текущей формы
            // Выводим историю продаж партнера, используя метод LoadSalesHistory() (реализуем ниже)
            InitializeComponent();
            this.connectionString = connectionString;
            this.partnerId = partnerId;
            this.partnerName = partnerName;
            this.Text = $"История продаж: {partnerName}";
            LoadSalesHistory();
        }

        // Метод для загрузки истории продаж партнера
        private void LoadSalesHistory()
        {
            // Используем flowLayoutPanel, также как и на MainForm, т.к. по ТЗ надо придерживаться единого стиля форм
            // Также используем функцию Clear() для отображения актуальных данных, каждый раз, как мы открываем эту форму
            flowLayoutPanel.Controls.Clear();
            try
            {
                // Строка подключения к БД
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    // SELECT выбирает атрибуты имени, количества покупок, дату их продажи из таблицы partner_products
                    // JOIN присоединяет атрибуты из таблицы product, где совпадает атрибут product_id
                    // ORDER BY указывает на необходимость сортировки результатов запроса
                    // DESC модификатор сортировки, означающий "по убыванию" (от новых дат продаж к старым)
                    string query = @"SELECT p.product_name, pp.quantity, pp.sale_date 
                                    FROM partner_products pp
                                    JOIN products p ON pp.product_id = p.product_id
                                    WHERE pp.partner_id = @partnerId
                                    ORDER BY pp.sale_date DESC";

                    // Подключаемся снова к бд, чтобы считать все данные для заполнения ими Panel
                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        // Говорим БД, что сортировка продаж идет по ID, которую мы присвоили при переходе из MainForm
                        cmd.Parameters.AddWithValue("@partnerId", partnerId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            // Пояснение к функции Read() см. в модуле 2
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

        // Метод для создания панелей с продажами конкретного партнера
        private Panel CreateSalePanel(NpgsqlDataReader reader)
        {
            // см. модуль 2
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

        // Кнопка возврата на главную форму (MainForm)
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}