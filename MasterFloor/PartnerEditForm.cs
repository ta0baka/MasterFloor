using Npgsql;

namespace MasterFloor
{
    public partial class PartnerEditForm : Form
    {
        // Получаем строку подключения к БД
        private readonly string connectionString;
        private NpgsqlConnection? connection;
        // По ТЗ мы должны передать ID партнера (partnerId) с MainForm.cs на PartnerEditForm.cs для передачи данных в текстовые поля при редактировании партнера
        private readonly int? partnerId;
        
        public PartnerEditForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        // Принимаем входные данные (ID партнера и строку подключения к БД) и выводим информацию о партнере (LoadPartnerData())
        public PartnerEditForm(int partnerId, string connectionString) : this(connectionString)
        {
            this.partnerId = partnerId;
            LoadPartnerData();
        }

        // Реализация метода для отображения данных о партнере
        private void LoadPartnerData()
        {
            // Проверяем, что ID не NULL
            // ! = не
            if (!partnerId.HasValue)
                return;
            // Если все ок, то подключаемся к БД и считываем всю информацию из таблицы partners по id через запрос
            // Используем конструкцию try catch, чтобы поймать ошибку (в случае ее наличия). Это требуется по ТЗ и полезно в целом
            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM partners WHERE partner_id = @id";
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", partnerId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Считываем атрибуты с таблицы partners, присваиваем их значения текстовым полям для отображения на форме
                        if (reader.Read())
                        {
                            txtName.Text = reader["partner_name"].ToString();
                            // По ТЗ требуется тип партнера выносить в comboBox
                            cmbType.SelectedItem = reader["partner_type"].ToString();
                            // Рейтинг выводим в NumericUpDown чтобы ничего кроме кроме цифр нельзя бло ввести (во избежание ошибок)
                            numRating.Value = Convert.ToDecimal(reader["rating"]);
                            txtAddress.Text = reader["legal_address"].ToString();
                            txtDirector.Text = reader["director"].ToString();
                            // Здесь выводим номер в maskedTextBox также для того чтобы ничего кроме цифр ввести было нельзя
                            maskedTextBoxPhone.Text = reader["phone"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            // Также как и номер телефона
                            maskedTxtINN.Text = reader["inn"].ToString();
                        }
                    }
                }
            }
            // В случае ошибки выводим информацию используя {ex.Massage}
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных партнера: {ex.Message}");
            }
            // Закрываем подключение к БД, чтобы не было утечек, т.к. не использовали using (пояснение к using см. модуль 2)
            finally
            {
                connection?.Close();
            }
        }

        // Метод для заполнения текстовых полей данными
        private void AddParameters(NpgsqlCommand cmd)
        {
            // Переводим данные в формат, подходящий под ComboBox - в String и проверяем на NULL (пустое значение)
            cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem?.ToString() ?? string.Empty);
            // Trim используем для удаления пробелов и табуляции (оставляет только текст, без лишних пробелов при отображении в textBox).
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@director", txtDirector.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", maskedTextBoxPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@inn", maskedTxtINN.Text.Trim());
            cmd.Parameters.AddWithValue("@rating", (int)numRating.Value);
        }

        // По ТЗ требуется осуществить переход между формами, поэтому создаем кнопку для возвращения на главную форму
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Валидация (проверка) на пустой ввод
        // По ТЗ требуется прописывать в уведомлениях об ошибке подробно дальнейшие шаги пользователя, для ее устранения и пиктограмму (знак), например MessageBoxIcon.Warning
        private bool ValidateInput()
        {
            // Проверка наименования партнера
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Поле 'Наименование партнера' обязательно для заполнения",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            // Проверка типа партнера
            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип партнера из списка",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }

            // Проверка ИНН
            if (!maskedTxtINN.MaskCompleted)
            {
                MessageBox.Show("ИНН должен содержать 10 цифр\nФормат: 1234567890",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                maskedTxtINN.Focus();
                return false;
            }

            // Проверка юридического адреса
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле 'Юридический адрес' обязательно для заполнения",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            // Проверка ФИО директора
            if (string.IsNullOrWhiteSpace(txtDirector.Text))
            {
                MessageBox.Show("Поле 'Директор' обязательно для заполнения",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txtDirector.Focus();
                return false;
            }

            // Проверка телефона
            if (!maskedTextBoxPhone.MaskCompleted)
            {
                MessageBox.Show("Введите корректный номер телефона\nФормат: XXX XXX XX XX",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                maskedTextBoxPhone.Focus();
                return false;
            }

            // Проверка email (базовая валидация)
            // !(логическое НЕ) означает, что условие сработает, если строка НЕ соответствует шаблону
            // Regex.IsMatch() — проверяет, соответствует ли строка (txtEmail.Text) регулярному выражению (шаблону email).
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email адрес\nФормат: example@domain.com",
                              "Ошибка ввода",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        // Кнопка сохранения/обновления данных
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка на корректный ввод данных (используем метод, который написали выше)
            if (!ValidateInput())
                return;

            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();

                if (partnerId.HasValue)
                {
                    // Обновление атрибутов существующего партнера
                    string updateQuery = @"UPDATE partners SET 
                            partner_type = @type, 
                            partner_name = @name, 
                            director = @director, 
                            email = @email, 
                            phone = @phone, 
                            legal_address = @address, 
                            inn = @inn,
                            rating = @rating 
                            WHERE partner_id = @id";

                    using (var cmd = new NpgsqlCommand(updateQuery, connection))
                    {
                        AddParameters(cmd);
                        cmd.Parameters.AddWithValue("@id", partnerId.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Добавление нового партнера
                    string insertQuery = @"INSERT INTO partners 
                            (partner_type, partner_name, director, email, phone, legal_address, inn, rating) 
                            VALUES (@type, @name, @director, @email, @phone, @address, @inn, @rating)";

                    using (var cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        AddParameters(cmd);
                        cmd.ExecuteNonQuery();
                    }
                }

                // При нажатии кнопки сохранения (btnSave) форма редактирования/добавления закрывается
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            // Ловим ошибки, в этом примере проверяем наличие уже существующего партнера (чтобы не было дублирования)
            catch (Npgsql.PostgresException ex) when (ex.SqlState == "23505")
            {
                if (ex.Message.Contains("partner_name"))
                    MessageBox.Show("Партнер с таким наименованием уже существует");
                else if (ex.Message.Contains("inn"))
                    MessageBox.Show("Партнер с таким ИНН уже существует");
                else
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}