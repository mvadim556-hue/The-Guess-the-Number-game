using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuessNumberWinForms
{
    public partial class Form1 : Form
    {
        private GameModel model;
        private TextBox txtGuess;
        private Button btnCheck;
        private ListBox lstHistory;
        private Label lblAttempts;
        private Label lblStatus;
        private Label lblTitle;
        private Button btnNewGame;

        public Form1()
        {
            InitializeComponent();
            model = new GameModel();
            UpdateUI();
        }

        private void InitializeComponent()
        {
            this.Text = "Угадай число";
            this.Size = new Size(400, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Заголовок
            lblTitle = new Label()
            {
                Text = "Компьютер загадал число от 1 до 100",
                Location = new Point(20, 20),
                Size = new Size(350, 30),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Поле ввода
            txtGuess = new TextBox()
            {
                Location = new Point(20, 70),
                Size = new Size(200, 30),
                Font = new Font("Arial", 12)
            };

            // Кнопка проверки
            btnCheck = new Button()
            {
                Text = "Проверить",
                Location = new Point(230, 68),
                Size = new Size(130, 35),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.LightGreen
            };
            btnCheck.Click += BtnCheck_Click;

            // Кнопка новой игры
            btnNewGame = new Button()
            {
                Text = "Новая игра",
                Location = new Point(20, 115),
                Size = new Size(340, 30),
                Font = new Font("Arial", 10),
                BackColor = Color.LightBlue
            };
            btnNewGame.Click += BtnNewGame_Click;

            // Статус
            lblStatus = new Label()
            {
                Text = "Введите число и нажмите 'Проверить'",
                Location = new Point(20, 160),
                Size = new Size(350, 30),
                Font = new Font("Arial", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Blue
            };

            // Счётчик попыток
            lblAttempts = new Label()
            {
                Text = "Попыток: 0",
                Location = new Point(20, 200),
                Size = new Size(350, 25),
                Font = new Font("Arial", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // История
            lstHistory = new ListBox()
            {
                Location = new Point(20, 240),
                Size = new Size(340, 200),
                Font = new Font("Consolas", 10),
                BackColor = Color.WhiteSmoke
            };

            this.Controls.AddRange(new Control[] { 
                lblTitle, txtGuess, btnCheck, btnNewGame, 
                lblStatus, lblAttempts, lstHistory 
            });
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtGuess.Text, out int guess))
            {
                lblStatus.Text = "Ошибка: введите целое число!";
                lblStatus.ForeColor = Color.Red;
                txtGuess.Clear();
                return;
            }

            if (guess < 1 || guess > 100)
            {
                lblStatus.Text = "Число должно быть от 1 до 100!";
                lblStatus.ForeColor = Color.Red;
                txtGuess.Clear();
                return;
            }

            string result = model.CheckGuess(guess);
            lstHistory.Items.Add($"{DateTime.Now:HH:mm:ss} - {guess} → {result}");
            lstHistory.TopIndex = lstHistory.Items.Count - 1;

            UpdateUI();

            if (model.IsGameOver)
            {
                btnCheck.Enabled = false;
                txtGuess.Enabled = false;
            }

            txtGuess.Clear();
            txtGuess.Focus();
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            model.NewGame();
            lstHistory.Items.Clear();
            btnCheck.Enabled = true;
            txtGuess.Enabled = true;
            UpdateUI();
            txtGuess.Focus();
        }

        private void UpdateUI()
        {
            lblAttempts.Text = $"Попыток: {model.Attempts}";
            lblStatus.Text = model.IsGameOver ? 
                $"Поздравляю! Вы угадали за {model.Attempts} попыток!" : 
                "Введите число от 1 до 100";
            lblStatus.ForeColor = model.IsGameOver ? Color.Green : Color.Blue;
        }
    }
}
