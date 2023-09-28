using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DB_2020_CS_104
{
    public partial class Form5 : Form
    {
        public int Attandance_ID;
        public int s;
        public Form5()
        {
            InitializeComponent();
        }
        private void buttonReport_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
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
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            Attandance_ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            comboBoxAttandId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
            comboBoxStudent.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            comboBoxStatus.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (IsStudentAttendance()== false)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Insert into StudentAttendance values (@AttendanceId,@StudentId,@AttendanceStatus)", con);
                cmd.Parameters.AddWithValue("@AttendanceId", int.Parse(comboBoxAttandId.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@StudentId", int.Parse(comboBoxStudent.SelectedValue.ToString()));
                if (comboBoxStatus.SelectedValue.ToString() == "Present")
                {
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 1);
                }
                else if (comboBoxStatus.SelectedValue.ToString() == "Absent")
                {
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 2);
                }
                else if (comboBoxStatus.SelectedValue.ToString() == "Leave")
                {
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 3);
                }
                else if(comboBoxStatus.SelectedValue.ToString() == "Late")
                {
                    cmd.Parameters.AddWithValue("@AttendanceStatus", 4);
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("Attandance Has Been Added Successfully");
            }
            else
            {
                MessageBox.Show("Student Attendance is Already Marked");
            }
            Form5_Load(sender, e);
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select SA.AttendanceId,SA.StudentId,L.[Name] as [Status] from StudentAttendance as SA JOIN Lookup as L ON SA.AttendanceStatus=L.LookupId", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            clean();
        }
        private void clean()
        {
            ComboBoxStudentID();
            ComboBoxAttandanceFill();
            ComboBoxStatusFill();
        }
        private void ComboBoxStatusFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * FROM LookUp where LookupId <> 5 and LookupId <> 6", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBoxStatus.DataSource = dtable;
            comboBoxStatus.DisplayMember = "LookUp";
            comboBoxStatus.ValueMember = "Name";

        }
        public void ComboBoxAttandanceFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBoxAttandId.DataSource = dtable;
            comboBoxAttandId.DisplayMember = "ClassAttendance";
            comboBoxAttandId.ValueMember = "ID";
        }
        public void ComboBoxStudentID()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student where Status='05'", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBoxStudent.DataSource = dtable;
            comboBoxStudent.DisplayMember = "Student";
            comboBoxStudent.ValueMember = "ID";
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (IsStudentAttendanceUpdated() == true)
            {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update StudentAttendance set AttendanceStatus='" + s + "' where  AttendanceId='" + int.Parse(comboBoxAttandId.Text) + " ' and StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                        cmd.ExecuteNonQuery();
                    /* if (comboBoxStatus.SelectedItem.ToString() == "Absent")
                    {
                        SqlCommand cmd = new SqlCommand("update StudentAttendance set AttendanceStatus='" + 2 + "' where  AttendanceId='" + int.Parse(comboBoxAttandId.Text) + " ' and StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else if (comboBoxStatus.SelectedItem.ToString() == "Leave")
                    {
                        SqlCommand cmd = new SqlCommand("update StudentAttendance set AttendanceStatus='" + 3 + "' where  AttendanceId='" + int.Parse(comboBoxAttandId.Text) + " ' and StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("update StudentAttendance set AttendanceStatus='" + 4 + "' where  AttendanceId='" + int.Parse(comboBoxAttandId.Text) + " ' and StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }*/
                  MessageBox.Show("Student Attendance Has Been Updated");
            }
            else
            {
                MessageBox.Show("No Change Found");
            }
            Form5_Load(sender, e);
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentAttendance where AttendanceId ='" + int.Parse(comboBoxAttandId.Text) + " '  and  StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Student Attendance Has Been Removed");
            Form5_Load(sender, e);
        }
        private void buttonAttandance_Click(object sender, EventArgs e)
        {
            Form9 f = new Form9();
            this.Hide();
            f.Show();
        }

        private void buttonAttand_Click(object sender, EventArgs e)
        {

        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }

        private bool IsStudentAttendanceUpdated()
        { 
            bool flag = true;
            if(comboBoxStatus.SelectedValue.ToString() == "Present")
            {
                s = 1;
            }
            else if(comboBoxStatus.SelectedValue.ToString() == "Absent")
            {
                s = 2;
            }
            else if(comboBoxStatus.SelectedValue.ToString() == "Leave")
            {
                s = 3;
            }
            else if(comboBoxStatus.SelectedValue.ToString()=="Late")
            {
                s = 4;
            }
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select * from StudentAttendance", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                
                if (rq[1].ToString() == comboBoxStudent.SelectedValue.ToString() && rq[2].ToString() == s.ToString() && rq[0].ToString() == comboBoxAttandId.SelectedValue.ToString())
                {
                    flag = false;
                }
            }
     
            rq.Close();
            return flag;
        }
        private bool IsStudentAttendance()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentAttendance", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == comboBoxAttandId.SelectedValue.ToString() && rq[1].ToString() == comboBoxStudent.SelectedValue.ToString())
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }
    }
}
