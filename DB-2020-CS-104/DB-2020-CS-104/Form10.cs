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
    public partial class Form10 : Form
    {
        public Form10()
        {

            InitializeComponent();
            clean();
        }
        private void Form10_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
       
        }
        public void ComboBoxStudentID()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            dat.Fill(dtable);
            comboBoxStudent.DataSource = dtable;
            comboBoxStudent.DisplayMember = "Student";
            comboBoxStudent.ValueMember = "ID";
        }
        
        private void ComboBoxAssessmentFill()
        {
            ComboBoxAssessmentComponent.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from AssessmentComponent where AssessmentComponent.AssessmentId = "+ int.Parse(comboBox1.GetItemText(comboBox1.SelectedItem).ToString()), con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                IDataReader datarecord = rq;

                ComboBoxAssessmentComponent.Items.Add(datarecord[0].ToString());
                
            }
            rq.Close();
        }
        public void ComboBoxRubricMeasureFill()
        {
            comboBoxRubricMeasure.Items.Clear();
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from RubricLevel where RubricLevel.RubricId IN (select AC.RubricId from AssessmentComponent as AC where AC.Id=" + int.Parse(ComboBoxAssessmentComponent.GetItemText(ComboBoxAssessmentComponent.SelectedItem).ToString()) +" )", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                IDataReader datarecord = rq;
                comboBoxRubricMeasure.Items.Add(datarecord[0].ToString());
            }
            rq.Close();   
        }
        private void ComboBoxAssessFill()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Assessment", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                IDataReader datarecord = rq;
                comboBox1.Items.Add(datarecord[0].ToString());
            }
            rq.Close();
        }
        private void clean()
        {
            ComboBoxStudentID();
            ComboBoxAssessFill();   
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxRubricMeasure.SelectedIndex >= 0 && comboBoxRubricMeasure.SelectedIndex >= 0)
            {

                if (IsEvaluation() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into StudentResult values (@StudentId,@AssessmentComponentId,@RubricMeasurementId,GETDATE())", con);
                    cmd.Parameters.AddWithValue("@StudentId", int.Parse(comboBoxStudent.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@AssessmentComponentId", int.Parse(ComboBoxAssessmentComponent.GetItemText(ComboBoxAssessmentComponent.SelectedItem).ToString()));
                    cmd.Parameters.AddWithValue("@RubricMeasurementId", int.Parse(comboBoxRubricMeasure.GetItemText(comboBoxRubricMeasure.SelectedItem).ToString()));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Evaluation Has Been Added");
                }
                else
                {
                    MessageBox.Show("Evauation is Already Done");
                }
            }

            comboBoxRubricMeasure.Items.Clear();
            comboBoxRubricMeasure.ResetText();
            ComboBoxAssessmentComponent.Items.Clear();
            ComboBoxAssessmentComponent.ResetText();
            Form10_Load(sender,e);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            if ((ComboBoxAssessmentComponent.Text) != "")
            {
                SqlCommand cmd = new SqlCommand("delete from StudentResult where AssessmentComponentId ='" + int.Parse(ComboBoxAssessmentComponent.Text) + " '  and  StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Evaluation Has Been Removed");
            }
            else
            {
                MessageBox.Show("Invalid Attempt");
            }
            Form10_Load(sender, e);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (comboBoxRubricMeasure.SelectedIndex >= 0 && comboBoxRubricMeasure.SelectedIndex >= 0)
            {
                if (IsEvaluationUpdated() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    if (comboBoxStudent.SelectedValue.ToString() != null)
                    {
                        SqlCommand cmd = new SqlCommand("update StudentResult set RubricMeasurementId =" + int.Parse(comboBoxRubricMeasure.GetItemText(comboBoxRubricMeasure.SelectedItem).ToString()) + " where  AssessmentComponentId='" + int.Parse(ComboBoxAssessmentComponent.Text) + " ' and StudentId ='" + int.Parse(comboBoxStudent.Text) + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Student Evaluation Has Been Updated");
                }
                else
                {
                    MessageBox.Show("No Change Found");
                }
            }
            comboBoxRubricMeasure.Items.Clear();
            comboBoxRubricMeasure.ResetText();
            ComboBoxAssessmentComponent.Items.Clear();
            ComboBoxAssessmentComponent.ResetText();
            Form10_Load(sender, e);
        }

        private bool IsEvaluationUpdated()
        {
            bool flag = false;
            
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
                SqlDataReader rq = cmd.ExecuteReader();
                while (rq.Read())
                {
                    if (rq[0].ToString() == comboBoxStudent.SelectedValue.ToString() && rq[1].ToString() == ComboBoxAssessmentComponent.GetItemText(ComboBoxAssessmentComponent.SelectedItem).ToString() && rq[2].ToString() == comboBoxRubricMeasure.GetItemText(comboBoxRubricMeasure.SelectedItem).ToString())
                    {
                        flag = true;
                    }
                }
                rq.Close();
            return flag;
        }
        private bool IsEvaluation()
        {
            bool flag = false;
            if (ComboBoxAssessmentComponent.SelectedIndex >= 0 && comboBoxRubricMeasure.SelectedIndex >=0)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("Select * from StudentResult", con);
                SqlDataReader rq = cmd.ExecuteReader();
                while (rq.Read())
                {
                    if (rq[0].ToString() == comboBoxStudent.SelectedValue.ToString() && rq[1].ToString() == ComboBoxAssessmentComponent.GetItemText(ComboBoxAssessmentComponent.SelectedItem).ToString())
                    {
                        flag = true;
                    }
                }
                rq.Close();
            }
            return flag;
        }
        
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue != null)
            {
                    dataGridView1.CurrentRow.Selected = true;
                    comboBoxStudent.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                    ComboBoxAssessmentComponent.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                    comboBoxRubricMeasure.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
            }*/
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRubricMeasure.Items.Clear();
            comboBoxRubricMeasure.SelectedValue = "";
            ComboBoxAssessmentComponent.Items.Clear();
            ComboBoxAssessmentComponent.ResetText();
           ComboBoxAssessmentFill();
        }

        private void comboBoxRubricMeasure_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ComboBoxAssessmentComponent_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxRubricMeasure.Items.Clear();
            comboBoxRubricMeasure.ResetText();
            ComboBoxRubricMeasureFill();
        }
    }
}
