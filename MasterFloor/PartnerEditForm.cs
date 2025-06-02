using Npgsql;

namespace MasterFloor
{
    public partial class PartnerEditForm : Form
    {
        private readonly string connectionString;
        private readonly int? partnerId;
        private NpgsqlConnection? connection;

        public PartnerEditForm(string connectionString)
        {
            this.connectionString = connectionString;
            InitializeComponent();
        }

        public PartnerEditForm(int partnerId, string connectionString) : this(connectionString)
        {
            this.partnerId = partnerId;
            LoadPartnerData();
        }

        private void LoadPartnerData()
        {
            if (!partnerId.HasValue)
                return;

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
                        if (reader.Read())
                        {
                            txtName.Text = reader["partner_name"].ToString();
                            cmbType.SelectedItem = reader["partner_type"].ToString();
                            numRating.Value = Convert.ToDecimal(reader["rating"]);
                            txtAddress.Text = reader["legal_address"].ToString();
                            txtDirector.Text = reader["director"].ToString();
                            txtPhone.Text = reader["phone"].ToString();
                            txtEmail.Text = reader["email"].ToString();
                            txtINN.Text = reader["inn"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных партнера: {ex.Message}");
            }
            finally
            {
                connection?.Close();
            }
        }

        private void AddParameters(NpgsqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@type", cmbType.SelectedItem?.ToString() ?? string.Empty);
            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@director", txtDirector.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@inn", txtINN.Text.Trim());
            cmd.Parameters.AddWithValue("@rating", (int)numRating.Value);
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите наименование партнера");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtINN.Text))
            {
                MessageBox.Show("Введите ИНН партнера");
                return false;
            }

            if (txtINN.Text.Length != 10 && txtINN.Text.Length != 12)
            {
                MessageBox.Show("ИНН должен содержать 10 или 12 цифр");
                return false;
            }

            if (!long.TryParse(txtINN.Text, out _))
            {
                MessageBox.Show("ИНН должен содержать только цифры");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                connection = new NpgsqlConnection(connectionString);
                connection.Open();

                if (partnerId.HasValue)
                {
                    // Обновление существующего партнера
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

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
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