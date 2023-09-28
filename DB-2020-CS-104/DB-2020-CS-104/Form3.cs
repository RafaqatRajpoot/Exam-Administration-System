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
    public partial class Form3 : Form
    {
        public int r_id=-1;
        public Form3()
        {
            InitializeComponent();
        }
        private void buttonReport_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            this.Hide();
            f.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            this.Hide();
            f.Show();
        }
        private bool isValidRubricID(string ID)
        {
            bool flagID = Regex.IsMatch(ID, @"^[0-9]{1,8}$");
            return flagID;
        }
        private bool isValidDetails(string de)
        {
            bool flagID = Regex.IsMatch(de, @"^[a-z A-Z 0-9]+$");
            return flagID;
        }
        private bool isValidInfo()
        {
            bool flag = false;
            if(isValidRubricID(textBoxId.Text)==true && isValidDetails(textBoxDetails.Text)==true)
            {
                flag = true;
            }
            return flag;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (isValidInfo() == true)
            {
                if (isRubric() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into Rubric values (@Id,@Details,@CloId)", con);
                    cmd.Parameters.AddWithValue("@Id", int.Parse(textBoxId.Text));
                    cmd.Parameters.AddWithValue("@Details", textBoxDetails.Text);
                    cmd.Parameters.AddWithValue("@CloId", int.Parse(comboBoxCLO.SelectedValue.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rubric Has Been Added Successfully");
                }
                else
                {
                    MessageBox.Show("Rubric is Already Present");
                }
            }
            else
            {
                MessageBox.Show("Wrong Formatting");
            }
            Form3_Load(sender, e);
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            comboBoxCLO.SelectedItem = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            Clean();
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
        private void Clean()
        {
            ComboBoxFill();
            textBoxId.Text = "";
            textBoxDetails.Text = "";
        }
        public void ComboBoxFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Clo", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxCLO.DataSource = dt;
            comboBoxCLO.DisplayMember = "CLO";
            comboBoxCLO.ValueMember = "Id";
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            ComboBoxFill();
            dataGridView1.CurrentRow.Selected = true;
            textBoxId.Text =dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
            r_id =int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
            textBoxDetails.Text = dataGridView1.Rows[e.RowIndex].Cells["Details"].FormattedValue.ToString();
            comboBoxCLO.SelectedValue= dataGridView1.Rows[e.RowIndex].Cells["CloId"].FormattedValue.ToString();
        }
        private void removeStudentResult()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentResult where AssessmentComponentId IN (select Id from AssessmentComponent where RubricId='" + r_id + " ')", con);
            cmd.ExecuteNonQuery();
        }
        private void removeAssessmentComponent()
        { 
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from AssessmentComponent  where RubricId='" + r_id + " ' ", con);
            cmd.ExecuteNonQuery();
        }
        private void removeRubricLevel()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from RubricLevel where RubricId ='" + r_id + " ' ", con);
            cmd.ExecuteNonQuery();
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (r_id >= 0)
            {
                removeStudentResult();
                removeAssessmentComponent();
                removeRubricLevel();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from Rubric where Id ='" + r_id + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rubric Has Been Removed");
                r_id = -1;
            }
            Form3_Load(sender, e);
        } 
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (isRubricUpdated()==true)
            {

                if (textBoxDetails.Text != "")
                {
                    if (isValidDetails(textBoxDetails.Text))
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update Rubric set Details='" + textBoxDetails.Text + "' where Id ='" + r_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (comboBoxCLO.SelectedValue != null)
                {
                    if (isValidRubricID(textBoxId.Text))
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update Rubric set Cloid='" + comboBoxCLO.Text + "' where Id ='" + r_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if(flag)
                {
                    MessageBox.Show("Wrong Formatting");
                }
                else
                {
                    MessageBox.Show("Rubric Has Been Updated");
                }
                
            }
            else
            {
                MessageBox.Show("No Change Found");
            }
            Form3_Load(sender, e);
        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }
        private bool isRubric()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == textBoxId.Text)
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }
        private bool isRubricUpdated()
        {
            bool flag = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[1].ToString() == textBoxDetails.Text && rq[2].ToString() == comboBoxCLO.SelectedValue.ToString())
                {
                    flag = false;
                }
            }
            rq.Close();
            return flag;
        }
    }
}
