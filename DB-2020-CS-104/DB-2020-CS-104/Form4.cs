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
    public partial class Form4 : Form
    {
        public int assessment_id=-1;
        public Form4()
        {
            InitializeComponent();
        }
        private void buttonReport_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            this.Hide();
            f.Show();
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
        private void buttonAttand_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            this.Hide();
            f.Show();
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBoxTitle.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            assessment_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            textBoxTotalMarks.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            textBoxTotalWeightage.Text=dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();

            ;        }
        private void Form4_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            clean();
        }
        public void clean()
        {
            textBoxTitle.Text = "";
            textBoxTotalMarks.Text = "";
            textBoxTotalWeightage.Text = "";
        }
        private bool isAssessmentUpdated()
        {
                bool flag = true;
                var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select * from Assessment", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == assessment_id.ToString() && rq[1].ToString() == textBoxTitle.Text && rq[3].ToString() == textBoxTotalMarks.Text && rq[4].ToString() == textBoxTotalWeightage.Text)
                {
                    flag = false;

                }
            }
            rq.Close();
            return flag;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
                if (IsAssessment() == false && textBoxTotalMarks.Text != "" && textBoxTotalWeightage.Text != "")
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into Assessment values (@Title,GETDATE(),@TotalMarks,@TotalWeightage)", con);
                    cmd.Parameters.AddWithValue("@Title", textBoxTitle.Text);
                    cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBoxTotalMarks.Text));
                    cmd.Parameters.AddWithValue("@TotalWeightage", int.Parse(textBoxTotalWeightage.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Assessment Has Been Added Successfully");
                }
                else
                {
                    MessageBox.Show("Assessment is Already Present");
                }
                
            Form4_Load(sender, e);
        }
        private void buttonStudent_Click_1(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();
        }
        private void buttonCLO_Click_1(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            this.Hide();
            f.Show();
        }
        private void buttonRub_Click_1(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            this.Hide();
            f.Show();
        }
        private void buttonAttand_Click_1(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            this.Hide();
            f.Show();
        }
        private void buttonReport_Click_1(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            this.Hide();
            f.Show();
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (assessment_id >= 0)
            {
                if (isAssessmentUpdated())
                {
                    var con = Configuration.getInstance().getConnection();
                    if (textBoxTitle.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("update Assessment set Title='" + textBoxTitle.Text + "' where Id ='" + assessment_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    if (textBoxTotalMarks.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("update Assessment set TotalMarks='" + int.Parse(textBoxTotalMarks.Text) + "' where Id ='" + assessment_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    if (textBoxTotalWeightage.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("update Assessment set TotalWeightage='" + int.Parse(textBoxTotalWeightage.Text) + "' where Id ='" + assessment_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Assessment Has Been Updated");
                }
                else
                {
                    MessageBox.Show("No Change Found");
                }
                assessment_id = -1;
            }
            Form4_Load(sender, e);
        }
        public void RemoveAssessmentComponent()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from AssessmentComponent where AssessmentId='" + assessment_id + " '  ", con);
            cmd.ExecuteNonQuery();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (assessment_id >= 0)
            {
                RemoveAssessmentComponent();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from Assessment where Id ='" + assessment_id + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Assessment Has Been Removed");
                assessment_id = -1;
            }
            Form4_Load(sender, e);
        }
        private void buttonComponent_Click(object sender, EventArgs e)
        {
            Form8 f = new Form8();
            this.Hide();
            f.Show();
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }
        private bool IsAssessment()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from  Assessment", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == assessment_id.ToString() && rq[1].ToString() == textBoxTitle.Text)
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }
    }
}
