using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTK_Manager_System
{
    public partial class ManagerSystem : Form
    {
        private string _loggedInUser;


        public ManagerSystem(string loggedInUser)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;

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

                }
                else if (clickedLabel == label2)
                {
                    OpenEmployeeForm();
                }
                else if (clickedLabel == label3)
                {
                    OpenProductForm();
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

                }
                else if (clickPictureBox == pictureBox3)
                {
                    OpenEmployeeForm();
                }
                else if (clickPictureBox == pictureBox4)
                {
                    OpenProductForm();
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

        private void OpenEmployeeForm()
        {

            Employee employeeForm = new Employee(_loggedInUser);
            employeeForm.Show();
            this.Hide();
        }

        private void OpenProductForm()
        {

            Product productForm = new Product(_loggedInUser);
            productForm.Show();
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
                new UserAction { ActionType = "Update", Description = "Updated salary information", ActionDate = DateTime.Now }
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