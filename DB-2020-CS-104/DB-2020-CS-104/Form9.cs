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
    public partial class Form9 : Form
    {
        public int StudentAttendanceID;
        public Form9()
        {
            InitializeComponent();
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from ClassAttendance", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void buttonStudent_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
        private void buttonCLO_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }

        private void buttonRub_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Hide();
            f.Show();
        }

        private void buttonAssessment_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            this.Hide();
            f.Show();
        }

        private void buttonAttand_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            this.Hide();
            f.Show();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            this.Hide();
            f.Show();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Insert into ClassAttendance values (@DateAttendance)", con);
            cmd.Parameters.AddWithValue("@DateAttendance", dateTimePicker1.Value);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Class Attendance Has Been Added Successfully");
            Form9_Load(sender,e);

        }
        public void  RemoveStudentAttandance()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentAttendance where AttendanceId='" +  StudentAttendanceID+ " '  ", con);
            cmd.ExecuteNonQuery();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            RemoveStudentAttandance();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from ClassAttendance where Id ='" +  StudentAttendanceID + " '  ", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Class Attandance Has Been Removed");
            Form9_Load(sender, e);
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            StudentAttendanceID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
