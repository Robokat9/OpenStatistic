using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Text;
using DocumentFormat.OpenXml.Office2016.Excel;
using System.Drawing;

namespace OpenStatistic

{
    public partial class Form1 : Form
    {
        public bool m_Saving = false;
        public Form1()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {




        }
        public void OpenxlsFile(string tbPath)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void CloseProcess()
        {
            Process[] List;
            List = Process.GetProcessesByName("EXCEL");
            foreach (Process proc in List)
            {
                proc.Kill();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


        }

        private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;
            foreach (var process in processes)
            {
                if (process.MainWindowTitle == "Microsoft Excel - " + excelFileName)
                    process.Kill();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xApp;
            Microsoft.Office.Interop.Excel.Workbook workbook;

            Microsoft.Office.Interop.Excel.Worksheet worksheet;
            Microsoft.Office.Interop.Excel.Range range;
            string fullname = "";
            CloseProcess();

            int xlRow;
            string filename;
            openFileDialog1.Filter = "Excel Office | *CSV";
           // openFileDialog1.ShowDialog();
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                if (filename != string.Empty)
                {
                    textBox1.Text = filename;
                    xApp = new Microsoft.Office.Interop.Excel.Application();
                    workbook = xApp.Workbooks.Open(filename);
                    string pathfile = Path.GetDirectoryName(filename);
                    string fName = Path.GetFileNameWithoutExtension(filename);
                    worksheet = workbook.Worksheets[1];
                    range = worksheet.UsedRange;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    for (xlRow = 2; xlRow <= range.Rows.Count ; xlRow++)
                    {
                        
                        dataGridView1.Rows.Add((range.Cells[xlRow, 1].Text).Split(";"));
                    }

                    worksheet = workbook.Worksheets.Add(workbook.Worksheets[1]);

                    worksheet.Activate();
                   // worksheet.Name = "Result";
                    worksheet.Columns.EntireColumn.AutoFit();
                    worksheet.Range["A1"].Value = "Номер программы";
                    worksheet.Range["B1"].Value = "Дата";
                    worksheet.Range["C1"].Value = "Программа";
                    worksheet.Range["D1"].Value = "Время цикла";
                    worksheet.Range["E1"].Value = "Время устранения остановки";
                    worksheet.Range["F1"].Value = "Время работы робота 1";
                    worksheet.Range["G1"].Value = "Время работы робота 2";
                    worksheet.Range["H1"].Value = "Количество";
                    worksheet.Range["I1"].Value = "Логин";

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            xApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Trim(new Char[] { '"'});
                        }
                    }
                    //fName = fName.Substring(0, 15);
                    workbook.SaveAs(pathfile + "/Convert_" + fName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                    
                    //using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                    //{
                        
                    //    if (sfd.ShowDialog() == DialogResult.OK)
                    //    {
                    //        fullname = sfd.FileName;
                    //        workbook.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                    //    }
                    //    else
                    //    {
                            
                    //    }

                    //}

                    //workbook.Save();
                    // workbook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                    workbook.Close(); 
                    xApp.Quit();



                    KillSpecificExcelFileProcess("filename");
                   
                    MessageBox.Show("Файл сохранен",fullname, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            { }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            button1.Enabled = true;
            button1.UseVisualStyleBackColor = false;
            button1.BackColor = System.Drawing.Color.Green;
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
           
        }
    }
}



