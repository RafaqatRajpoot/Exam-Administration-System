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
    public partial class Form8 : Form
    {
        public int MaxMarks;
        public int AssessmentCompId;
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
           
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            clean();
        }
        public void clean()
        {
            ComboboxRubricFill();
            ComboBoxAssessmentFill();
            textBoxName.Text = "";
            textBoxTotalMarks.Text = "";
        }
        public void ComboboxRubricFill()
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
        public void ComboBoxAssessmentFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBoxAssessmentId.DataSource = dtable;
            comboBoxAssessmentId.DisplayMember = "Assessment";
            comboBoxAssessmentId.ValueMember = "ID";
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBoxName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
            AssessmentCompId = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
            textBoxTotalMarks.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
            comboBoxRubric.SelectedValue= dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            comboBoxAssessmentId.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[6].FormattedValue.ToString();
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
        private bool isValidInput()
        {
            bool flag = false;
            if(textBoxName.Text!="" && textBoxTotalMarks.Text!="")
            {
                flag = true;
            }
            return flag;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (isValidInput() == true)
            {
                ISMAXIMUMMARKS();
                if (IsAssessmentComponent() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into AssessmentComponent values (@Name,@RubricID,@TotalMarks,GETDATE(),GETDATE(),@AssessmentId)", con);
                    cmd.Parameters.AddWithValue("@Name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@RubricId", int.Parse(comboBoxRubric.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@TotalMarks", int.Parse(textBoxTotalMarks.Text));
                    cmd.Parameters.AddWithValue("@AssessmentId", int.Parse(comboBoxAssessmentId.SelectedValue.ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("AssessmentComponent Has Been Added Successfully");
                }
                else
                {
                    MessageBox.Show("Assessment Component is Already Present");
                }
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
            Form8_Load(sender, e);
        }
         private void RemoveStudentResult()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentResult where StudentResult.AssessmentComponentId IN(Select Id from AssessmentComponent  where  Id='" + AssessmentCompId + " ') ", con);
            cmd.ExecuteNonQuery();
        }
        private void ISMAXIMUMMARKS()
        {
            MaxMarks = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select * FROM ASSESSMENT ",con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
               if (rq[0].ToString() == comboBoxAssessmentId.SelectedValue.ToString())
                {
                    MaxMarks = int.Parse(rq[3].ToString());
                }
               
            }
            rq.Close();
            SqlCommand cmd1 = new SqlCommand("select * from  AssessmentComponent ",con);
            SqlDataReader reader = cmd1.ExecuteReader();
            int size = 0;
            while (reader.Read())
            {
                if (reader[6].ToString() == comboBoxAssessmentId.SelectedValue.ToString())
                {
                    size = size + int.Parse(reader[3].ToString()); 
                }
            }
            MaxMarks = MaxMarks - size;
            reader.Close();
        }
        private void updatedMaxMarks()
        {
            MaxMarks = 0;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select * FROM ASSESSMENT ", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[0].ToString() == comboBoxAssessmentId.SelectedValue.ToString())
                {
                    MaxMarks = int.Parse(rq[3].ToString());
                }

            }
            rq.Close();
            SqlCommand cmd1 = new SqlCommand("select * from  AssessmentComponent where AssessmentComponent.Id='"+ AssessmentCompId +"'", con);
            SqlDataReader reader = cmd1.ExecuteReader();
            int size = 0;
            while (reader.Read())
            {
                if (reader[6].ToString() == comboBoxAssessmentId.SelectedValue.ToString())
                {
                    size = size + int.Parse(reader[3].ToString());
                }
            }
            MaxMarks = MaxMarks - size;
            reader.Close();
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (isValidInput() == true && IsAssessmentComponent()==true)
            {
                RemoveStudentResult();
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from AssessmentComponent  where  Id='" + AssessmentCompId + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("AssessmentComponent Has Been Removed");
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
            Form8_Load(sender, e);
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (isValidInput() == true)
            {
                updatedMaxMarks();
                if (MaxMarks >= int.Parse(textBoxTotalMarks.Text.ToString()))
                {
                    if (IsAssessmentComponentUpdated() == true)
                    {
                        var con = Configuration.getInstance().getConnection();
                        if (textBoxName.Text != "")
                        {
                            SqlCommand cmd = new SqlCommand("update AssessmentComponent set Name='" + textBoxName.Text + "',DateUpdated=GETDATE() where Id ='" + AssessmentCompId + " '  ", con);
                            cmd.ExecuteNonQuery();
                        }
                        if (comboBoxRubric.SelectedValue != null)
                        {
                            SqlCommand cmd = new SqlCommand("update AssessmentComponent set Rubricid='" + comboBoxRubric.Text + "'  ,DateUpdated=GETDATE() where Id ='" + AssessmentCompId + " '  ", con);
                            cmd.ExecuteNonQuery();
                        }
                        if (textBoxTotalMarks.Text != "")
                        {
                            SqlCommand cmd = new SqlCommand("update AssessmentComponent set TotalMarks='" + int.Parse(textBoxTotalMarks.Text) + "' ,DateUpdated=GETDATE() where Id ='" + AssessmentCompId + " '  ", con);
                            cmd.ExecuteNonQuery();
                        }
                        if (comboBoxAssessmentId.SelectedValue != null)
                        {
                            SqlCommand cmd = new SqlCommand("update AssessmentComponent set  AssessmentId='" + comboBoxAssessmentId.Text + "' " + ",DateUpdated=GETDATE() where Id ='" + AssessmentCompId + " '  ", con);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("AssessmentComponent Has Been Updated");
                    }
                    else
                    {
                        MessageBox.Show("No Change Found");
                    }
                }
                else
                {
                    MessageBox.Show("Marks are not valid");
                }
            }
            else
            {
                MessageBox.Show("Invalid Input");
            }
            Form8_Load(sender, e);
        }
        private bool IsAssessmentComponentUpdated()
        {
            bool flag = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[2].ToString() == comboBoxRubric.SelectedValue.ToString() && rq[6].ToString() == comboBoxAssessmentId.SelectedValue.ToString() && rq[1].ToString() == textBoxName.Text && rq[3].ToString() == textBoxTotalMarks.Text)
                {
                    flag = false;
                }
            }
            rq.Close();
            return flag;
        }
        private bool IsAssessmentComponent()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if(rq[2].ToString() == comboBoxRubric.SelectedValue.ToString() && rq[6].ToString() == comboBoxAssessmentId.SelectedValue.ToString() && rq[1].ToString() == textBoxName.Text)
                {
                    flag = true;
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
