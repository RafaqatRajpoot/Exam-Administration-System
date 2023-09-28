using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace DB_2020_CS_104
{
    public partial class Form2 : Form
    {
        public int cloid;
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonStudent_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
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

        private bool isValidCLOName(string name)
        {
            bool flagFirstName = Regex.IsMatch(name, @"^[a-z A-Z 0-9]+$");
            return flagFirstName;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (isValidCLOName(textBoxName.Text) == true)
            {
                if (isClo() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into Clo values (@Name,GETDATE(),GETDATE())", con);
                    cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Clo Has Been Added Successfully");
                }
                else
                {
                    MessageBox.Show("CLO is Already Present");
                }
            }
            else
            {
                MessageBox.Show("Wrong Formatting");
            }
                Form2_Load(sender, e);
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (isCloUpdated())
            {
                if (isValidCLOName(textBoxName.Text) == true)
                {
                    if (textBoxName.Text != "")
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update Clo set Name='" + textBoxName.Text + " ', DateUpdated=GETDATE() where Id='" + cloid + " ' ", con);
                        MessageBox.Show("CLO Updated Successfully ..");
                        cmd.ExecuteNonQuery();

                    }
                }
                else
                {
                    MessageBox.Show("Wrong Formatting");
                }
            }
            else
            {
                MessageBox.Show("No change Found");
            }
            Form2_Load(sender, e);
        }
        public void Clean()
        {
            textBoxName.Text = "";
        }
        private void buttonView_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            Clean();
        }
        private void removeStudentResult()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentResult where AssessmentComponentId IN (select Id from AssessmentComponent where RubricId IN (select ID from Rubric where CloId='" + cloid + " '))", con);
            cmd.ExecuteNonQuery();
        }
        private void removeRubricLevel()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from RubricLevel  where RubricId IN (select ID from Rubric where CloId='" + cloid + " ') ", con);
            cmd.ExecuteNonQuery();
        }
        private void removeAssessmentComponent()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from AssessmentComponent  where RubricId IN (select ID from Rubric where CloId='" + cloid + " ')", con);
            cmd.ExecuteNonQuery();
        }
        private void removeRubric()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand(" delete from rubric where CloId ='" + cloid + " ' ", con);
            cmd.ExecuteNonQuery();
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (cloid > 0)
            {
                removeStudentResult();
                removeAssessmentComponent();
                removeRubricLevel();
                removeRubric();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from Clo where Id ='" + cloid + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("CLO Has Been Removed");
            }
            Form2_Load(sender, e);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            Clean();
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            cloid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            textBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }
        private bool isClo()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[1].ToString() == textBoxName.Text)
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }

        private bool isCloUpdated()
        {
            bool flag = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[1].ToString() == textBoxName.Text)
                {
                    flag = false;
                }
            }
            rq.Close();
            return flag;
        }
    }
}
