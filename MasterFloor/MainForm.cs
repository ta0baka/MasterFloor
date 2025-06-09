using Npgsql;

namespace MasterFloor
{
    public partial class MainForm : Form
    {   // Строка подключения
        private string connectionString = "Host=localhost;Username=postgres;Password=****;Database=master_floor";

        public MainForm()
        {
            // Инициализируем элементы на форме
            InitializeComponent();
           // Загружаем данные о партнерах (метод который создадим ниже)
            LoadPartners();
        }

        // Метод для отображения партнеров (подтягиваем инфу из БД)
        private void LoadPartners()
        {
            // Используем flowLayoutPanel (для динамического создания Panel внутри)
            // Clear очищает данные, чтобы потом с помощью запроса отобразить актуальные (обновленные) данные
            flowLayoutPanel.Controls.Clear();
            // Подключаемся к БД
            // Здесь using предотвращает утечки данных из БД (его нужно всегда использовать в моментах обращения к БД)
            // при создании переменных, тип данных которых вы не знаете, пишите перед ними var (неявная типизация)
            // connection - подключение. присваем ему строку, которую писали вначале (connectionString)
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                // Запрос для отображения данных на форме.
                // SELECT обеспечивает выборку атрибутов из таблицы partners, отсюда и p. (сокращенное имя таблицы), если у таблицы двойное имя (partner_products), то берется по первой букве от каждого (pp)
                // COALESCE преобразует NULL значение в 0 (это важно в случае, если мы имеем партнера, у которого нет записей о покупках) во избежания ошибок.
                // SUM считает сумму количества товаров, которые приобрел партнер. 0 в этом запросе играет роль замены (NULL на 0 из БД). Сумма сохраняется как общее количество продаж (total_sales)
                // FROM partners - данные берутся из таблицы partners
                // LEFT JOIN присоединяет связующую таблицу partner_products к таблице partners через внешний ключ pp.partner_id, то есть id_partner из таблицы partners имеет связь с id_partner из partner_products (внешний ключ)
                // GROUP BY группирует данные по id партнера. Это необходимо, так как мы используем агрегатную функцию SUM, для вычисления скидки по общему количеству продаж (каждого из партнеров отдельно)
                string query = @"SELECT p.partner_id, p.partner_type, p.partner_name, p.director, 
                          p.phone, p.rating, 
                          COALESCE(SUM(pp.quantity), 0) as total_sales
                          FROM partners p
                          LEFT JOIN partner_products pp ON p.partner_id = pp.partner_id
                          GROUP BY p.partner_id";

                // Создаем команду для выполнения запроса (cmd)
                // query - переменная, содержащая строку с SQL-запросом (который мы написали выше)
                // NpgsqlCommand специальный класс для выполнения SQL-команд в PostgreSQL (из библиотеки Npgsql)
                using (var cmd = new NpgsqlCommand(query, connection))
                {
                    // ExecuteReader() - выполняет SQL-запрос и возвращает NpgsqlDataReader для чтения результатов, чтобы программа понимала что выводить
                    using (var reader = cmd.ExecuteReader())
                    {
                        // reader.Read() - перемещает "курсор" на следующую строку результата, возвращает false когда строк больше нет (например Enter в Word, переход на новою строку)
                        while (reader.Read())
                        {
                            // создаем переменную панели с данными о партнере и присваеваем ей метод, который создадим ниже
                            var partnerPanel = CreatePartnerPanel(reader);
                            // Команда заполнения flowLayoutPanel данными партнеров (partnerPanel)
                            flowLayoutPanel.Controls.Add(partnerPanel);
                        }
                    }
                }
            }
        }

        // Мы создаем отдельный модуль для создания панелей, в которых будут отображаться данные каждого отдельного партнера 
        private Panel CreatePartnerPanel(NpgsqlDataReader reader)
        {
            // здесь прописываем атрибуты таблицы, которую требуется отобразить на главной форме (в данном случае partners)
            // Типы данных мы берем также из таблицы БД. int = serial, integer; string = varchar/character varying;
            // Для строковых значений (string) мы прописываем парсинг, чтобы данные, которые мы присвоем этой переменной, сохранились как string
            // Парсинг - преобразования данных из одного формата в другой
            // ? - проверка на NULL значение
            // Convert.ToInt также играет роль парсинга для преобразования вводимых данных в int значение
            int partnerId = Convert.ToInt32(reader["partner_id"]);
            string partnerType = reader["partner_type"]?.ToString() ?? "Не указано";
            string partnerName = reader["partner_name"]?.ToString() ?? "Неизвестный партнер";
            string director = reader["director"]?.ToString() ?? "Не указано";
            string phone = reader["phone"]?.ToString() ?? "Не указано";
            int rating = Convert.ToInt32(reader["rating"]);
            int totalSales = Convert.ToInt32(reader["total_sales"]);

            // Создаем переменную панели
            // Объявялем ее свойства: ширину (Width), высоту (Height), стиль границ панели (BorderStyle), отступ между панелями (Margin), таг (Tag) - индентификатор для перехода на другую форму
            var panel = new Panel
            {
                Width = flowLayoutPanel.Width - 30,
                Height = 150,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Tag = partnerId
            };

            // Создаем переменную для хранения формулы по подсчету скидки (реализована в конце)
            int discount = CalculateDiscount(totalSales);

            // 
            var nameContainer = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(panel.Width - 20, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top
            };

            // Создаем label, отображающий тип и имя партнера, ОБЯЗАТЕЛЬНО указываем шрифт (имя шрифта, размер, начертание (например жирный - Bold)
            // AutoSize - заставляет метку (Label) автоматически подстраивать свой размер под длину текста
            var lblName = new Label
            {
                Text = $"{partnerType} | {partnerName}",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };

            // Создаем label для отображения процента скидки
            // Location работает как координаты
            // Anchor Прикрепляет процент (discount) к правому краю панели
            var lblDiscount = new Label
            {
                Text = $"{discount}%",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Location = new Point( 750, 0),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            // Создаем Label для отображения данных о директоре
            var lblDirector = new Label
            {
                Text = director,
                Location = new Point(10, 35),
                AutoSize = true
            };

            //Создаем Label для отображения данных о номере телефона
            var lblPhone = new Label
            {
                Text = $"+7 {phone}",
                Location = new Point(10, 60),
                AutoSize = true
            };

            //Создаем Label для отображения данных о рейтинге
            var lblRating = new Label
            {
                Text = $"Рейтинг: {rating}",
                Location = new Point(10, 85),
                AutoSize = true
            };

            // Для 4 модуля (НЕ ТРОГАЕМ)
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

            // 3 модуль (НЕ ТРОГАЕМ)
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

            // Продолжение 2 модуля

            // nameContainer - это контейнерный элемент (Panel)
            // lblName - созданная ранее метка (Label) с названием партнёра
            //Метка (Label) становится дочерним элементом контейнера (Panel) и отображается внутри него
            nameContainer.Controls.Add(lblName);
            nameContainer.Controls.Add(lblDiscount);
            panel.Controls.Add(nameContainer);
            panel.Controls.AddRange(new Control[] { lblDirector, lblPhone, lblRating, btnHistory });
            return panel;
        }

        // Метод, высчитывающий скидку партнера на основе общей суммы заказанных продуктов
        private int CalculateDiscount(int totalSales)
        {
            // Если (if) общая сумма покупок партнера (totalSales) больше или равно (>=) чем 300 000 (по ТЗ), то возвращается зачение 15, что и является процентом скидки
            if (totalSales >= 300000) return 15;
            if (totalSales >= 50000) return 10;
            if (totalSales >= 10000) return 5;
            return 0;
        }

        // 3 модуль (НЕ ТРОГАЕМ)
        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            var editForm = new PartnerEditForm(connectionString);
            editForm.FormClosed += (s, args) => LoadPartners();
            editForm.ShowDialog();
        }
    }
}