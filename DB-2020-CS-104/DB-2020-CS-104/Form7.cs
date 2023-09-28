using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace DB_2020_CS_104
{
    public partial class Form7 : Form
    {
        public int rl_id=-1;
        public Form7()
        {
            InitializeComponent();
        }
        private void buttonRub_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
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
        private void Form7_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            clean();
        }
        public void clean()
        {
            ComboboxFill();
            textBoxMeasure.Text = "";
            textBoxDetails.Text = "";
        }

        public void ComboboxFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Rubric", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBoxRubric.DataSource = dt;
            comboBoxRubric.DisplayMember = "Rubric";
            comboBoxRubric.ValueMember = "Id";
        }
        private bool isValidDetails(string d)
        {
            bool flag = Regex.IsMatch(d, @"^[a-z A-Z 0-9]+$");
            return flag;
        }
        private bool isValidMeasurementLevel(string ml)
        {
            bool flag = Regex.IsMatch(ml, @"^[0-9]{1,9}$");
            return flag;
        }
        private bool isValidInfo()
        {
            bool flag = false;
            if(isValidDetails(textBoxDetails.Text)==true && isValidMeasurementLevel(textBoxMeasure.Text)==true)
            {
                flag = true;
            }
            return flag;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (isValidInfo())
            {
                if (IsRubricLevel() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into RubricLevel values (@RubricId,@Details,@MeasurementLevel)", con);
                    cmd.Parameters.AddWithValue("@RubricId", int.Parse(comboBoxRubric.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@Details", textBoxDetails.Text);
                    cmd.Parameters.AddWithValue("@MeasurementLevel", int.Parse(textBoxMeasure.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("RubricLevel Has Been Added Successfully");
                }
                else
                {
                    MessageBox.Show("Rubric Level is Already Present");
                }
            }
            else
            {
                MessageBox.Show("Wrong Formatting");
            }
            Form7_Load(sender, e);
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            rl_id = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString());
            textBoxMeasure.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            textBoxDetails.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            comboBoxRubric.SelectedValue= dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (rl_id >= 0)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from RubricLevel where Id ='" + rl_id + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("RubricLevel Has Been Removed");
                rl_id = -1;
            }
            Form7_Load(sender, e);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            bool flag = true;
            if (IsRubricLevelUpdated() == true)
            {
                if (textBoxDetails.Text != "")
                {
                    if (isValidDetails(textBoxDetails.Text))
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update RubricLevel set Details='" + textBoxDetails.Text + "' where Id ='" + rl_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (comboBoxRubric.SelectedValue.ToString() != null)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("update RubricLevel set Rubricid='" + comboBoxRubric.Text + "' where Id ='" + rl_id + " '  ", con);
                    cmd.ExecuteNonQuery();
                }
                if (textBoxMeasure.Text != "")
                {
                    if (isValidMeasurementLevel(textBoxMeasure.Text))
                    {
                        var con = Configuration.getInstance().getConnection();
                        SqlCommand cmd = new SqlCommand("update RubricLevel set MeasurementLevel='" + textBoxMeasure.Text + "' where Id ='" + rl_id + " '  ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag == true)
                {
                    MessageBox.Show("RubricLevel Has Been Updated");
                }
                else
                {
                    MessageBox.Show("Wrong Formatting");
                }
            }
            else
            {
                MessageBox.Show("No Change Found");
            }
            Form7_Load(sender, e);
        }

        private bool IsRubricLevel()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() ==  rl_id.ToString())
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }
        private bool IsRubricLevelUpdated()
        {
            bool flag = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == rl_id.ToString() && rq[1].ToString() == comboBoxRubric.SelectedValue.ToString() && rq[2].ToString() == textBoxDetails.Text && rq[3].ToString() == textBoxMeasure.Text)
                {
                    flag = false;
                }
            }
            rq.Close();
            return flag;
        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }
    }
}
