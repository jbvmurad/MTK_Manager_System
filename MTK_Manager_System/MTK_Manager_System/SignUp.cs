using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace MTK_Manager_System
{
    public partial class SignUp : Form
    {
        private string connectionString = "Server=ServerName;Database=MTK;Integrated Security=True;TrustServerCertificate=True;";

        public SignUp()
        {
            InitializeComponent();
            label11.Visible = false;
            label12.Visible = false;
            label14.Visible = false;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            textBox1.PasswordChar = '*';
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            string email = textBox3.Text;
            string password = textBox1.Text;

            if (string.IsNullOrEmpty(username))
            {
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }

            if (string.IsNullOrEmpty(email))
            {
                label12.Visible = true;
            }
            else
            {
                label12.Visible = false;
            }

            if (string.IsNullOrEmpty(password))
            {
                label14.Visible = true;
            }
            else
            {
                label14.Visible = false;
            }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                if (CreateUser(username, email, password))
                {
                    MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 loginForm = new Form1();
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Error creating account!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CreateUser(string username, string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Password", password);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
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
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }
    }
}