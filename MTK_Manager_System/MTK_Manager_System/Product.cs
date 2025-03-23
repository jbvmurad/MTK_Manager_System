using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MTK_Manager_System
{
    public partial class Product : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter dataAdapter;
        private DataTable dataTable;
        private string _loggedInUser;


        public Product(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;

            InitializeDatabaseConnection();
            LoadData();
            AttachEventHandlers();
            label1.Click += new EventHandler(Label_Click);
            label2.Click += new EventHandler(Label_Click);
            label3.Click += new EventHandler(Label_Click);
            label4.Click += new EventHandler(Label_Click);
            label6.Click += new EventHandler(Label_Click);
            pictureBox2.Click += new EventHandler(PictureBox_Click);
            pictureBox3.Click += new EventHandler(PictureBox_Click);
            pictureBox4.Click += new EventHandler(PictureBox_Click);
            pictureBox5.Click += new EventHandler(PictureBox_Click);
            pictureBox7.Click += new EventHandler(PictureBox_Click);
            pictureBox9.Click += new EventHandler(pictureBox9_Click);
        }

        private void InitializeDatabaseConnection()
        {
            string connectionString = "Server=ServerName;Database=MTK;Integrated Security=True;TrustServerCertificate=True;";
            connection = new SqlConnection(connectionString);
        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM Product";
                dataAdapter = new SqlDataAdapter(query, connection);
                dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void AttachEventHandlers()
        {
            bunifuThinButton21.Click += new EventHandler(SaveButton_Click);
            bunifuThinButton22.Click += new EventHandler(EditButton_Click);
            bunifuThinButton23.Click += new EventHandler(DeleteButton_Click);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = dataTable.NewRow();
                newRow["Name"] = textBox1.Text;
                newRow["JointoCompany"] = bunifuDatepicker1.Value;
                newRow["Model"] = textBox2.Text;
                newRow["Value"] = textBox3.Text;
                newRow["Country"] = comboBox1.SelectedItem.ToString();
                newRow["Address"] = textBox4.Text;

                dataTable.Rows.Add(newRow);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.Update(dataTable);

                LoadData();

                MessageBox.Show("Data saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                DataRowView rowView = row.DataBoundItem as DataRowView;

                if (rowView != null)
                {
                    rowView.BeginEdit();
                    rowView["Name"] = textBox1.Text;
                    rowView["JointoCompany"] = bunifuDatepicker1.Value;
                    rowView["Model"] = textBox2.Text;
                    rowView["Value"] = textBox3.Text;
                    rowView["Country"] = comboBox1.SelectedItem.ToString();
                    rowView["Address"] = textBox4.Text;
                    rowView.EndEdit();

                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    dataAdapter.Update(dataTable);

                    LoadData();

                    MessageBox.Show("Record updated successfully!");
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                DataRowView rowView = row.DataBoundItem as DataRowView;

                if (rowView != null)
                {
                    rowView.Row.Delete();

                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                    dataAdapter.Update(dataTable);

                    LoadData();

                    MessageBox.Show("Record deleted successfully!");
                }
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                Movepanel4To(clickedLabel);

                if (clickedLabel == label1)
                {
                    OpenManagerSystemForm();
                }
                else if (clickedLabel == label2)
                {
                    OpenEmployeeForm();
                }
                else if (clickedLabel == label3)
                {

                }
                else if (clickedLabel == label4)
                {
                    OpenSalaryForm();
                }
                else if (clickedLabel == label6)
                {

                    OpenReportForm();
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickPictureBox = sender as PictureBox;
            if (clickPictureBox != null)
            {
                Movepanel6To(clickPictureBox);
                if (clickPictureBox == pictureBox2)
                {
                    OpenManagerSystemForm();
                }
                else if (clickPictureBox == pictureBox3)
                {
                    OpenEmployeeForm();
                }
                else if (clickPictureBox == pictureBox4)
                {

                }
                else if (clickPictureBox == pictureBox5)
                {
                    OpenSalaryForm();
                }
                else if (clickPictureBox == pictureBox7)
                {
                    OpenReportForm();
                }
            }
        }

        private void Movepanel4To(Label targetLabel)
        {
            panel4.Location = new Point(targetLabel.Location.X, panel4.Location.Y);
            panel4.Size = new Size(targetLabel.Width, panel4.Height);
        }

        private void Movepanel6To(PictureBox targetPictureBox)
        {
            panel6.Location = new Point(targetPictureBox.Location.X, panel6.Location.Y);
            panel6.Size = new Size(targetPictureBox.Width, panel6.Height);
        }

        private void OpenManagerSystemForm()
        {
            ManagerSystem managerSystemForm = new ManagerSystem(_loggedInUser);
            managerSystemForm.Show();
            this.Hide();
        }

        private void OpenEmployeeForm()
        {
            Employee employeeForm = new Employee(_loggedInUser);
            employeeForm.Show();
            this.Hide();
        }

        private void OpenSalaryForm()
        {
            Salary salaryForm = new Salary(_loggedInUser);
            salaryForm.Show();
            this.Hide();
        }

        private void OpenReportForm()
        {

            List<UserAction> userActions = new List<UserAction>
            {
                new UserAction { ActionType = "Login", Description = "User logged in", ActionDate = DateTime.Now },
                new UserAction { ActionType = "Update", Description = "Updated product information", ActionDate = DateTime.Now }
            };


            ReportForm reportForm = new ReportForm(_loggedInUser, userActions);
            reportForm.Show();
            this.Hide();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }
    }
}