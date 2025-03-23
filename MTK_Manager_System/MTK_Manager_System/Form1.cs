using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace MTK_Manager_System
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=ServerName;Database=MTK;Integrated Security=True;TrustServerCertificate=True;";

        public Form1()
        {
            InitializeComponent();
            label11.Visible = false;
            label12.Visible = false;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            textBox1.PasswordChar = '*';
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            string password = textBox1.Text;

            if (string.IsNullOrEmpty(username))
            {
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                label12.Visible = true;
            }
            else
            {
                label12.Visible = false;
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (ValidateUser(username, password))
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Giriş yapan kullanıcı adını ManagerSystem formuna aktar
                    ManagerSystem managerSystemForm = new ManagerSystem(username);
                    managerSystemForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUp createAccountForm = new SignUp();
            createAccountForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
    }
}
