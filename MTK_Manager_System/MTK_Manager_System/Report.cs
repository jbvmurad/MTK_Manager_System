using System;
using System.Collections.Generic;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace MTK_Manager_System
{
    public partial class ReportForm : Form
    {
        private string _loggedInUser;
        private List<UserAction> _userActions;
        public ReportForm(string loggedInUser, List<UserAction> userActions)
        {
            InitializeComponent();
            _loggedInUser = loggedInUser;
            _userActions = userActions;
            LoadData();
        }
        private void LoadData()
        {
            dataGridView1.DataSource = _userActions;
        }
        private void btnExportToPdf_Click(object sender, EventArgs e)
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{_loggedInUser}_Report.pdf");


            PdfReportGenerator.GeneratePdfReport(_loggedInUser, _userActions, filePath);


            MessageBox.Show($"Report successfully created: {filePath}", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ManagerSystem managerSystemForm = new ManagerSystem(_loggedInUser);
            managerSystemForm.Show();
            this.Hide();
        }
    }
    public class UserAction
    {
        public string ActionType { get; set; }
        public string Description { get; set; }
        public DateTime ActionDate { get; set; }
    }
    public static class PdfReportGenerator
    {
        public static void GeneratePdfReport(string loggedInUser, List<UserAction> userActions, string filePath)
        {

            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();


            Paragraph title = new Paragraph($"User Actions Report - {loggedInUser}", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);


            document.Add(new Paragraph(" "));


            Paragraph userInfo = new Paragraph($"User: {loggedInUser}", new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL));
            document.Add(userInfo);


            document.Add(new Paragraph(" "));


            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 2, 4, 3 });


            table.AddCell("Action Type");
            table.AddCell("Description");
            table.AddCell("Date");


            foreach (var action in userActions)
            {
                table.AddCell(action.ActionType);
                table.AddCell(action.Description);
                table.AddCell(action.ActionDate.ToString("dd.MM.yyyy HH:mm"));
            }

            document.Add(table);
            document.Close();
        }
    }
}

