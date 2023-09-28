using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
namespace DB_2020_CS_104
{
    public partial class Form1 : Form
    {
        public int studentid;
        public bool IsStudent()
        {
            bool flag = false;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while(rq.Read())
            {
                if(rq[5].ToString()==textBoxRegNo.Text)
                {
                    flag = true;
                }
            }
            rq.Close();
            return flag;
        }
        public bool IsStudentUpdated()
        {
            int status;
            if(checkBoxStatus.Checked==true)
            {
                status = 5;
            }
            else
            {
                status = 6;
            }
            bool flag = true;
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("Select * from Student", con);
            SqlDataReader rq = cmd.ExecuteReader();
            while (rq.Read())
            {
                if (rq[1].ToString() == textBoxfname.Text && rq[2].ToString() == textBoxlaname.Text && rq[5].ToString() == textBoxRegNo.Text && rq[3].ToString() == textBoxcontact.Text && rq[4].ToString() == textBoxEmail.Text && rq[6].ToString() == status.ToString())
                {
                    flag = false;
                }
            }
            rq.Close();
            return flag;
        }
        public Form1()
        {
            InitializeComponent();
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
            if (isValidInfo() == true)
            {
                if (IsStudent() == false)
                {
                    var con = Configuration.getInstance().getConnection();
                    SqlCommand cmd = new SqlCommand("Insert into Student values (@FirstName, @LastName,@Contact,@Email,@RegistrationNumber,@Status)", con);
                    cmd.Parameters.AddWithValue("@FirstName", textBoxfname.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxlaname.Text);
                    cmd.Parameters.AddWithValue("@Contact", textBoxcontact.Text);
                    cmd.Parameters.AddWithValue("@Email", textBoxEmail.Text);
                    cmd.Parameters.AddWithValue("@RegistrationNumber", textBoxRegNo.Text);
                    if (checkBoxStatus.Checked == true)
                    {
                        cmd.Parameters.AddWithValue("@Status", 5);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Status", 6);
                    }
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully saved");
                }
                else
                {
                    MessageBox.Show("Student is Already Present");
                }
            }
            else
            {
                MessageBox.Show("InValid Formatting");
            }
            Form1_Load(sender, e);
        }
        private void RemoveStudentAttendance()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentAttendance where StudentId='" + studentid + " '", con);
            cmd.ExecuteNonQuery();
        }
        private void RomoveStudentResult()
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("delete from StudentResult where StudentId='" + studentid + " ' ", con);
            cmd.ExecuteNonQuery();
        }
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveStudentAttendance();
            RomoveStudentResult();
            if (studentid > 0)
            {
                var con = Configuration.getInstance().getConnection();
                SqlCommand cmd = new SqlCommand("delete from Student where Id ='" + studentid + " '  ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Has Been Removed");
            }
            Form1_Load(sender, e);
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (isValidInfo() == true)
            {
                if (IsStudentUpdated() == true)
                {
                    var con = Configuration.getInstance().getConnection();
                    if (textBoxRegNo.Text != "")
                    {
                            SqlCommand cmd = new SqlCommand("update Student set RegistrationNumber='" + textBoxRegNo.Text + "' where Id ='" + studentid + " '  ", con);
                            cmd.ExecuteNonQuery();
                        
                    }
                    if (textBoxcontact.Text != "")
                    {
                        
                            SqlCommand cmd = new SqlCommand("update Student set Contact='" + textBoxcontact.Text + "' where Id ='" + studentid + " '  ", con);
                            cmd.ExecuteNonQuery();
                       
                    }
                    if (textBoxEmail.Text != "")
                    {
                            SqlCommand cmd = new SqlCommand("update Student set Email='" + textBoxEmail.Text + "' where Id ='" + studentid + " '  ", con);
                            cmd.ExecuteNonQuery();
                      
                    }
                    if (textBoxfname.Text != "")
                    {
                        
                            SqlCommand cmd = new SqlCommand("update Student set FirstName='" + textBoxfname.Text + "' where Id ='" + studentid + " '  ", con);
                            cmd.ExecuteNonQuery();
                       
                    }
                    if (textBoxlaname.Text != "")
                    {
                            SqlCommand cmd = new SqlCommand("update Student set LastName='" + textBoxlaname.Text + "' where Id ='" + studentid + " ' ", con);
                            cmd.ExecuteNonQuery();
                        
                    }
                    if (checkBoxStatus.Checked == true)
                    {
                        SqlCommand cmd = new SqlCommand("update Student set Status='" + 5 + "' where Id ='" + studentid + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("update Student set Status='" + 6 + "' where Id ='" + studentid + " ' ", con);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Student Has Been Updated");
                }
                else
                {
                    MessageBox.Show("No change Found");
                }
            }
            else
            {
                MessageBox.Show("InValid Formatting");
            }
               
            Form1_Load(sender,e);
        }
        public void Clean()
        {
            textBoxcontact.Text = "";
            textBoxEmail.Text = "";
            textBoxfname.Text = "";
            textBoxlaname.Text = "";
            textBoxRegNo.Text = "";
            checkBoxStatus.Checked = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var con = Configuration.getInstance().getConnection();
            SqlCommand cmd = new SqlCommand("select S.Id,S.FirstName,S.LastName,S.Contact,S.Email,S.RegistrationNumber,L.[Name] as [Status] from Student as S JOIN Lookup as L ON L.LookupId=S.Status", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable(); da.Fill(dt);
            dataGridView1.DataSource = dt;
            Clean();
        }
        private void Cell_Clicked(object sender, DataGridViewCellEventArgs e)
        {
                dataGridView1.CurrentRow.Selected = true;
                studentid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                textBoxfname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].FormattedValue.ToString();
                textBoxlaname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].FormattedValue.ToString();
                textBoxcontact.Text = dataGridView1.Rows[e.RowIndex].Cells[3].FormattedValue.ToString();
                textBoxEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].FormattedValue.ToString();
                textBoxRegNo.Text = dataGridView1.Rows[e.RowIndex].Cells[5].FormattedValue.ToString();

        }
        private void buttonResult_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            this.Hide();
            f.Show();
        }
        private bool isValidName(string name)
        {
            bool flagFirstName = Regex.IsMatch(name, @"^[a-z A-Z]+$");
            return flagFirstName;
        }
        private bool isValidEmail(string email)
        {
            bool flagEmail = Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return flagEmail;
        }
        private bool isValidRegNo(string reg)
        {
            bool flagRegNo = Regex.IsMatch(reg, @"^[0-9]{4}[- _][A-Z a-z]{1,5}[- _][0-9]{1,4}$");
            return flagRegNo;
        }
        private bool isValidContact(string contact)
        {
            bool flagContact = Regex.IsMatch(contact, @"^[0][3][0-4][0-9][0-9]{7}$");
            return flagContact;
        }
        private bool isValidInfo()
        {
            bool flag = false;
            if (isValidName(textBoxfname.Text)==true && isValidName(textBoxlaname.Text)==true && isValidEmail(textBoxEmail.Text)==true &&  isValidRegNo(textBoxRegNo.Text)==true && isValidContact(textBoxcontact.Text)==true)
            {
                 flag = true;
            }
            return flag;
        }
    }
}
