using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace DB_2020_CS_104
{
    public partial class Form6 : Form
    {
        int selectedIndex;
        public Form6()
        {
            InitializeComponent();
        }
        
        private void buttonAttand_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            this.Hide();
            f.Show();
        }
        private void buttonAssessment_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            this.Hide();
            f.Show();
        }
        private void buttonRub_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Hide();
            f.Show();
        }
        private void buttonCLO_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }
        private void buttonStudent_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
        private DataTable LOAD_StudentAttendanceReport()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Column1", "Student ID");
            dataGridView1.Columns.Add("Column2", "Name");
            dataGridView1.Columns.Add("Column3", "Registration Number");
            dataGridView1.Columns.Add("Column4", "Marked Attendance");
            dataGridView1.Columns.Add("Column5", "Present ");
            dataGridView1.Columns.Add("Column6", "Absent");
            dataGridView1.Columns.Add("Column7", "Leave");
            dataGridView1.Columns.Add("Column8", "Late");
            

            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select S.Id,S.FirstName+' '+S.LastName as [Name],S.RegistrationNumber,(Select COUNT(SA.AttendanceId) from StudentAttendance as SA where SA.StudentId=S.Id) as [Attendance Marked],(select COUNT(SA.AttendanceId) from StudentAttendance as SA where SA.AttendanceStatus=01 and Sa.StudentId=S.Id) as [Present],(select COUNT(SA.AttendanceId) from StudentAttendance as SA where SA.AttendanceStatus=02 and Sa.StudentId=S.Id) as [Absent],(select COUNT(SA.AttendanceId) from StudentAttendance as SA where SA.AttendanceStatus=03 and Sa.StudentId=S.Id) as [Leave],(select COUNT(SA.AttendanceId) from StudentAttendance as SA where SA.AttendanceStatus=04 and Sa.StudentId=S.Id) as [Late] from Student as S where S.Status = '05'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();

            DataTable dd = new DataTable();

             dd.Columns.Add("Student ID", typeof(string));
             dd.Columns.Add("Name", typeof(string));
             dd.Columns.Add("Registration Number", typeof(string));
            dd.Columns.Add("Marked Attendance", typeof(string));
            dd.Columns.Add("Present ", typeof(string));
             dd.Columns.Add("Absent", typeof(string));
             dd.Columns.Add("Leave", typeof(string));
             dd.Columns.Add("Late", typeof(string));
            


            var cell = new object[dataGridView1.Columns.Count];
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                for (int p = 0; p < x.Cells.Count; p++)
                {
                    cell[p] = x.Cells[p].Value;
                }
                dd.Rows.Add(cell);
            }
            return dd;
        }
         private DataTable LOAD_AttendanceREport()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Column1", "Date");
            dataGridView1.Columns.Add("Column2", "Student Present ");
            dataGridView1.Columns.Add("Column3", "Student Absent");
            dataGridView1.Columns.Add("Column4", "Student on Leave");
            dataGridView1.Columns.Add("Column5", "Student Late");
            dataGridView1.Columns.Add("Column6", "Attendance not Marked");
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select CA.AttendanceDate,(select COUNT(SA.StudentId) from StudentAttendance as SA where SA.AttendanceId=CA.Id and SA.AttendanceStatus=01) as [Present Students],(select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='02') as [Absent Students],(select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='03')  as [Students on Leave],(select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='04') as [Late Student],(select COUNT(SS.Id) from Student as SS where SS.Status='05' ) - ((select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='01')+ (select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='02')+ (select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='03')+(select COUNT(StudentId) from StudentAttendance as SSA where SSA.AttendanceId=CA.Id and SSA.AttendanceStatus='04')) AS [Attendance Not Marked] from ClassAttendance as CA", con);
            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            DataTable dd = new DataTable();
            dd.Columns.Add("Date",typeof(string));
            dd.Columns.Add("Student Present ", typeof(string));
            dd.Columns.Add("Student Absent", typeof(string));
            dd.Columns.Add("Student on Leave", typeof(string));
            dd.Columns.Add("Student Late", typeof(string));
            dd.Columns.Add("Attendance not Marked", typeof(string));
            var cell = new object[dataGridView1.Columns.Count];
            foreach(DataGridViewRow x in dataGridView1.Rows)
            {
                for(int p=0;p<x.Cells.Count;p++)
                {
                    cell[p] = x.Cells[p].Value;
                }
                dd.Rows.Add(cell);
            }
            return dd;
        }

           private DataTable  Load_CLOReport()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
          
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id From CLO",con);
            SqlDataReader rq = cmd1.ExecuteReader();
            dataGridView1.Columns.Add("Column1", "CLO");
            dataGridView1.Columns.Add("Column2", "StudentID");
            dataGridView1.Columns.Add("Column3", "Registration Number");
            dataGridView1.Columns.Add("Column4", "Obtained Marks");
            dataGridView1.Columns.Add("Column4", "Total Marks Evaluated");
            dataGridView1.Columns.Add("Column4", "Total CLO Marks");
            List<string> clo = new List<string>();
            while (rq.Read())
            {
                clo.Add(rq[0].ToString());
            }
            rq.Close();
            foreach (string s in clo)
            {
                con = Configuration.getInstance().getConnection();
                 cmd1 = new SqlCommand("select Student.id,Student.RegistrationNumber,SUM(T.[Obtained Marks]) as [Obtained Marks],Sum(T.[Total Marks]) as [Total  Marks Evaluated],(select SUM(AssessmentComponent.TotalMarks) from AssessmentComponent where AssessmentComponent.RubricId IN (select R.Id from Rubric as R where R.CloId=@MYCLOID)) as [Total CLO Marks] from(select SR.StudentId, CAST((select Rl.MeasurementLevel from RubricLevel as RL where Rl.Id = SR.RubricMeasurementId) * (AC.TotalMarks) as float) / (select MAX(RL.MeasurementLevel) from RubricLevel as RL where RL.RubricId = AC.RubricId) as [Obtained Marks],(select SUM(AssessmentComponent.TotalMarks) from AssessmentComponent where AssessmentComponent.Id = AC.Id) as [Total Marks] from StudentResult as SR JOIN AssessmentComponent as AC ON SR.AssessmentComponentId = AC.Id where AC.RubricId IN(select R.Id from Rubric as R where R.CloId = @MYCLOID)) as T JOIN Student ON Student.Id = T.StudentId group by Student.Id,Student.RegistrationNumber",con);
                cmd1.Parameters.AddWithValue("MYCLOID", s);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(s, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(),dr[4].ToString());
                }
            }
            DataTable dd = new DataTable();
            dd.Columns.Add("CLO", typeof(string));
            dd.Columns.Add("Student ID ", typeof(string));
            dd.Columns.Add("Registration Number", typeof(string));
            dd.Columns.Add("Obtained Marks", typeof(string));
            dd.Columns.Add("Total Marks Evaluated", typeof(string));
            dd.Columns.Add("Total Clo Marks", typeof(string));
            var cell = new object[dataGridView1.Columns.Count];
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                for (int p = 0; p < x.Cells.Count; p++)
                {
                    cell[p] = x.Cells[p].Value;
                }
                dd.Rows.Add(cell);
            }
            return dd;
        }
        private DataTable AssessmentWise()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id From Assessment ", con);
            SqlDataReader rq = cmd1.ExecuteReader();
            dataGridView1.Columns.Add("Column1", "Assessment ID");
            dataGridView1.Columns.Add("Column2", "StudentID");
            dataGridView1.Columns.Add("Column3", "Registration Number");
            dataGridView1.Columns.Add("Column4", "Obtained Marks");
            dataGridView1.Columns.Add("Column5", "Total Evaluated Marks");
            dataGridView1.Columns.Add("Column6", "Total Marks");
            dataGridView1.Columns.Add("Column7", "Obtained Percentage");
            List<string> Assessment = new List<string>();
            while (rq.Read())
            {
                Assessment.Add(rq[0].ToString());
            }
            rq.Close();
            foreach (string s in Assessment)
            {
                con = Configuration.getInstance().getConnection();
                cmd1 = new SqlCommand("select T.StudentId,Student.RegistrationNumber,SUM(T.[Obtained Marks]) as [Obtained Marks],SUM(T.[Total Marks]) as [Total Marks Evaluated],(select SUM(AssessmentComponent.TotalMarks) from AssessmentComponent where AssessmentComponent.AssessmentId = @MYAssessment) as [Total Marks],Cast(SUM(T.[Obtained Marks]) / SUM(T.[Total Marks]) * 100 as float) as [Obtained Percentage] from(select  SR.StudentId, AC.Name, CAST((select Rl.MeasurementLevel from RubricLevel as RL where Rl.Id = SR.RubricMeasurementId) * (AC.TotalMarks) as float) / (select MAX(RL.MeasurementLevel) from RubricLevel as RL where RL.RubricId = AC.RubricId) as [Obtained Marks],(select SUM(AssessmentComponent.TotalMarks) from AssessmentComponent where AssessmentComponent.Id = AC.Id) as [Total Marks] from StudentResult as SR JOIN AssessmentComponent AS AC ON AC.Id = SR.AssessmentComponentId where AC.AssessmentId = @MYAssessment) as T JOIN Student ON Student.Id = T.StudentId group by T.StudentId,Student.RegistrationNumber", con);
                cmd1.Parameters.AddWithValue("MYAssessment", s);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(s, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
                }
                dr.Close();
            }
            DataTable dd = new DataTable();
            dd.Columns.Add("Assessment ID", typeof(string));
            dd.Columns.Add("StudentID", typeof(string));
            dd.Columns.Add("Registration Number", typeof(string));
            dd.Columns.Add("Obtained Marks", typeof(string));
            dd.Columns.Add("Total Evaluated Marks", typeof(string));
            dd.Columns.Add("Total Marks", typeof(string));
            dd.Columns.Add("Obtained Percentage", typeof(string));

            var cell = new object[dataGridView1.Columns.Count];
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                for (int p = 0; p < x.Cells.Count; p++)
                {
                    cell[p] = x.Cells[p].Value;
                }
                dd.Rows.Add(cell);
            }
            return dd;


         
        }
        private DataTable LoadAssessmentReport()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd1 = new SqlCommand("Select Id From Assessment", con);
            SqlDataReader rq = cmd1.ExecuteReader();
            dataGridView1.Columns.Add("Column1", "Assessment");
            dataGridView1.Columns.Add("Column2", "StudentID");
            dataGridView1.Columns.Add("Column3", "Registration Number");
            dataGridView1.Columns.Add("Column4", "Assessment Component Name");
            dataGridView1.Columns.Add("Column5", "Obtained Marks");
            dataGridView1.Columns.Add("Column6", "Total Marks");
            List<string> Assessment = new List<string>();
            List<String> Name = new List<string>();
            while (rq.Read())
            {
                Assessment.Add(rq[0].ToString());
               // Name.Add(rq[1].ToString());
            }
            rq.Close();
            foreach (string s in Assessment)
            {
                con = Configuration.getInstance().getConnection();
                cmd1 = new SqlCommand("select T.StudentId,Student.RegistrationNumber,T.Name,T.[Obtained Marks],T.[Total Marks] from(select  SR.StudentId, AC.Name, CAST((select Rl.MeasurementLevel from RubricLevel as RL where Rl.Id = SR.RubricMeasurementId) * (AC.TotalMarks) as float) / (select MAX(RL.MeasurementLevel) from RubricLevel as RL where RL.RubricId = AC.RubricId) as [Obtained Marks],(select SUM(AssessmentComponent.TotalMarks) from AssessmentComponent where AssessmentComponent.Id = AC.Id) as [Total Marks] from StudentResult as SR JOIN AssessmentComponent AS AC ON AC.Id = SR.AssessmentComponentId  where AC.AssessmentId = @MYASSESSMENT) as T JOIN Student ON Student.Id = T.StudentId order by T.StudentId", con);
                cmd1.Parameters.AddWithValue("MYASSESSMENT",s);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    dataGridView1.Rows.Add(s, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString());
                }
                dr.Close();
            }
            DataTable dd = new DataTable();
             dd.Columns.Add("Assessment",typeof(string));
             dd.Columns.Add("StudentID", typeof(string));
             dd.Columns.Add("Registration Number", typeof(string));
             dd.Columns.Add("Assessment Component Name", typeof(string));
             dd.Columns.Add("Obtained Marks", typeof(string));
             dd.Columns.Add("Total Marks", typeof(string));
           
            var cell = new object[dataGridView1.Columns.Count];
            foreach (DataGridViewRow x in dataGridView1.Rows)
            {
                for (int p = 0; p < x.Cells.Count; p++)
                {
                    cell[p] = x.Cells[p].Value;
                }
                dd.Rows.Add(cell);
            }
            return dd;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBoxReport.SelectedIndex >= 0)
            {
                if (comboBoxReport.SelectedItem.ToString() == "Attandance Report")
                {
                    LOAD_AttendanceREport();
                    selectedIndex = 1;
                }
                else if (comboBoxReport.SelectedItem.ToString() == "CLO Report")
                {
                    Load_CLOReport();
                    selectedIndex = 2;
                }
                else if (comboBoxReport.SelectedItem.ToString() == "Assessment Component Report")
                {
                    LoadAssessmentReport();
                    selectedIndex = 3;
                }
                else if (comboBoxReport.SelectedItem.ToString() == "Assessment Report")
                {
                    AssessmentWise();
                    selectedIndex = 4;
                }
                else if(comboBoxReport.SelectedItem.ToString() == "Student Attendance Report")
                {
                    LOAD_StudentAttendanceReport();
                    selectedIndex = 5;
                }
                else
                {
                    MessageBox.Show("No item selected ");
                    Form6_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("No item selected ");
                Form6_Load(sender, e);
            }

        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
           if(selectedIndex==1)
            {
                Generate_AttendanceReport();
            }
           else if(selectedIndex==2)
            {
                Generate_CLOReport();
            }
           else if(selectedIndex==3)
            {
                Generate_AssessmentReport();
            }
           else if (selectedIndex==4)
            {
                Generate_AssessmentWise();
            }
           else if(selectedIndex==5)
            {
                Generate_StudentAttendanceReport();
            }
            selectedIndex = 0;
        }

        private void Generate_StudentAttendanceReport()
        {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog save = new SaveFileDialog();

                    save.Filter = "PDF (*.pdf)|*.pdf";

                    save.FileName = "Result.pdf";

                    bool ErrorMessage = false;

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(save.FileName))
                        {
                            try

                            {

                                File.Delete(save.FileName);

                            }

                            catch (Exception ex)

                            {

                                ErrorMessage = true;

                                MessageBox.Show("Unable to wride data in disk" + ex.Message);

                            }

                        }

                        if (!ErrorMessage)

                        {

                            try

                            {
                                PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                                pTable.DefaultCell.Padding = 2;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            BaseColor color = new BaseColor(108, 113, 233);
                            foreach (DataGridViewColumn col in dataGridView1.Columns)

                                {

                                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pCell.BackgroundColor = color;
                                    pTable.AddCell(pCell);

                                }


                                DataTable tb = LOAD_StudentAttendanceReport();
                                for (int i = 0; i < tb.Rows.Count; i++)
                                {
                                    for (int j = 0; j < tb.Columns.Count; j++)
                                    {
                                        pTable.AddCell(tb.Rows[i][j].ToString());
                                    }
                                }

                                using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                                {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                Paragraph p1 = new Paragraph("University of Engineering and  Technology ");
                                p1.Font.Size = 20;
                                p1.Alignment = Element.ALIGN_CENTER;
                                p1.SpacingBefore = 5;
                                document.Add(p1);

                                Paragraph p2 = new Paragraph("Lahore , Pakistan ");
                                p2.Font.Size = 20;
                                p2.Alignment = Element.ALIGN_CENTER;
                                document.Add(p2);
                                Paragraph p3 = new Paragraph("Student Attendance Report");
                                p3.SpacingBefore = 10;
                                p3.SpacingAfter = 15;
                                p3.Font.Size = 15;
                                p3.Alignment = Element.ALIGN_CENTER;
                                document.Add(p3);
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                                }
                                MessageBox.Show("Data Export Successfully", "info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error while exporting Data" + ex.Message);
                            }

                        }

                    }

                }

                else

                {

                    MessageBox.Show("No Record Found", "Info");

                }
            }
        private void Generate_AssessmentWise()
        {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog save = new SaveFileDialog();

                    save.Filter = "PDF (*.pdf)|*.pdf";

                    save.FileName = "Result.pdf";

                    bool ErrorMessage = false;

                    if (save.ShowDialog() == DialogResult.OK)
                    {
                        if (File.Exists(save.FileName))
                        {
                            try

                            {

                                File.Delete(save.FileName);

                            }

                            catch (Exception ex)

                            {

                                ErrorMessage = true;

                                MessageBox.Show("Unable to wride data in disk" + ex.Message);

                            }

                        }

                        if (!ErrorMessage)

                        {

                            try

                            {
                                PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                                pTable.DefaultCell.Padding = 2;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            BaseColor color = new BaseColor(108, 113, 233);
                            foreach (DataGridViewColumn col in dataGridView1.Columns)

                                {

                                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pCell.BackgroundColor = color;
                                    pTable.AddCell(pCell);

                                }


                                DataTable tb = AssessmentWise();
                                for (int i = 0; i < tb.Rows.Count; i++)
                                {
                                    for (int j = 0; j < tb.Columns.Count; j++)
                                    {
                                        pTable.AddCell(tb.Rows[i][j].ToString());
                                    }
                                }

                                using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                                {
                                    Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                    PdfWriter.GetInstance(document, fileStream);
                                    document.Open();
                                Paragraph p1 = new Paragraph("University of Engineering and  Technology ");
                                p1.Font.Size = 20;
                                p1.Alignment = Element.ALIGN_CENTER;
                                p1.SpacingBefore = 5;
                                document.Add(p1);

                                Paragraph p2 = new Paragraph("Lahore , Pakistan ");
                                p2.Font.Size = 20;
                                p2.Alignment = Element.ALIGN_CENTER;
                                p2.SpacingAfter = 10;
                                Paragraph p3 = new Paragraph("Assessment  Report ");
                                p3.Font.Size = 15;
                                p3.Alignment = Element.ALIGN_CENTER;
                                p3.SpacingAfter = 10;
                                document.Add(p2);
                                document.Add(p3);
                                document.Add(pTable);
                                    document.Close();
                                    fileStream.Close();
                                }
                                MessageBox.Show("Data Export Successfully", "info");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error while exporting Data" + ex.Message);
                            }

                        }

                    }

                }

                else

                {

                    MessageBox.Show("No Record Found", "Info");

                }
            }
        private void Generate_AssessmentReport()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            BaseColor color = new BaseColor(108, 113, 233);
                            foreach (DataGridViewColumn col in dataGridView1.Columns)

                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pCell.BackgroundColor = color;
                                pTable.AddCell(pCell);

                            }


                            DataTable tb = LoadAssessmentReport();
                            for (int i = 0; i < tb.Rows.Count; i++)
                            {
                                for (int j = 0; j < tb.Columns.Count; j++)
                                {
                                   
                                    pTable.AddCell(tb.Rows[i][j].ToString());
                                }
                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                Paragraph p1 = new Paragraph("University of Engineering and  Technology ");
                                p1.Font.Size = 20;
                                p1.Alignment = Element.ALIGN_CENTER;
                                p1.SpacingBefore = 5;
                                document.Add(p1);
                               
                                Paragraph p2 = new Paragraph("Lahore , Pakistan ");
                                p2.Font.Size = 20;
                                p2.Alignment = Element.ALIGN_CENTER;
                                p2.SpacingAfter = 10;
                                Paragraph p3 = new Paragraph("Assessment Component Report ");
                                p3.Font.Size = 15;
                                p3.Alignment = Element.ALIGN_CENTER;
                                p3.SpacingAfter = 10;
                                document.Add(p2);
                                document.Add(p3);
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Data Export Successfully", "info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data" + ex.Message);
                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }
        private void Generate_CLOReport()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {
                            BaseColor color = new BaseColor(108, 113, 233);
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                           
                            foreach (DataGridViewColumn col in dataGridView1.Columns)

                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pCell.BackgroundColor = color;
                                pTable.AddCell(pCell);

                            }


                            DataTable tb = Load_CLOReport();
                            for (int i = 0; i < tb.Rows.Count; i++)
                            {
                                for (int j = 0; j < tb.Columns.Count; j++)
                                {
                                    pTable.AddCell(tb.Rows[i][j].ToString());
                                }
                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                Paragraph p1 = new Paragraph("University of Engineering and  Technology ");
                                p1.Font.Size = 20;
                                p1.Alignment = Element.ALIGN_CENTER;
                                p1.SpacingBefore = 5;
                                document.Add(p1);

                                Paragraph p2 = new Paragraph("Lahore , Pakistan ");
                                p2.Font.Size = 20;
                                p2.Alignment = Element.ALIGN_CENTER;
                                p2.SpacingAfter = 10;
                                Paragraph p3 = new Paragraph("CLO Report ");
                                p3.Font.Size = 15;
                                p3.Alignment = Element.ALIGN_CENTER;
                                p3.SpacingAfter = 10;
                                document.Add(p2);
                                document.Add(p3);
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Data Export Successfully", "info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data" + ex.Message);
                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }
        private void Generate_AttendanceReport()
        {

            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {
                            BaseColor color = new BaseColor(108, 113, 233);
                            PdfPTable pTable = new PdfPTable(dataGridView1.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)

                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pCell.BackgroundColor = color;
                                pTable.AddCell(pCell);

                            }


                            DataTable tb = LOAD_AttendanceREport();
                            for (int i = 0; i < tb.Rows.Count; i++)
                            {
                                for (int j = 0; j < tb.Columns.Count; j++)
                                {
                                    pTable.AddCell(tb.Rows[i][j].ToString());
                                }
                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                Paragraph p1 = new Paragraph("University of Engineering and  Technology ");
                                p1.Font.Size = 20;
                                p1.Alignment = Element.ALIGN_CENTER;
                                p1.SpacingBefore = 5;
                                document.Add(p1);

                                Paragraph p2 = new Paragraph("Lahore , Pakistan ");
                                p2.Font.Size = 20;
                                p2.Alignment = Element.ALIGN_CENTER;
                                p2.SpacingAfter = 10;
                                Paragraph p3 = new Paragraph("Attendance Report ");
                                p3.Font.Size = 15;
                                p3.Alignment = Element.ALIGN_CENTER;
                                p3.SpacingAfter = 10;
                                document.Add(p2);
                                document.Add(p3);
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Data Export Successfully", "info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error while exporting Data" + ex.Message);
                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
