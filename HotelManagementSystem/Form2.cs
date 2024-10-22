using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.ShowDialog();
        }
        private string GetGender()
        {
            if (radioButton1.Checked) return "Male";
            if (radioButton2.Checked) return "Female";
            if (radioButton3.Checked) return "Others";
            return "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Check if Password and Confirm Password match
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Stop further execution
            }
            //                      @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\Employee.mdf;Integrated Security=True;Connect Timeout=30;;
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                sql.Open();

                // Check if the username already exists
                SqlCommand checkUser = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Username = @Username", sql);
                checkUser.Parameters.AddWithValue("@Username", textBox2.Text);
                int userExists = (int)checkUser.ExecuteScalar(); // Get the number of matching usernames

                if (userExists > 0)
                {
                    MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;  // Stop further execution if username exists
                }

                // If username doesn't exist, proceed with the insertion
                SqlCommand sq2 = new SqlCommand("INSERT INTO Login (Username,Password,Confirm_Password,Gender) VALUES (@Username, @Password, @Confirm_Password, @Gender)", sql);

                sq2.Parameters.AddWithValue("@Username", textBox2.Text);
                sq2.Parameters.AddWithValue("@Password", textBox3.Text);
                sq2.Parameters.AddWithValue("@Confirm_Password", textBox4.Text);
                sq2.Parameters.AddWithValue("@Gender", GetGender());

                sq2.ExecuteNonQuery();

                MessageBox.Show("Successfully Added");

                // Clear fields after successful addition
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==false)
            {

                textBox4.UseSystemPasswordChar=false;
                textBox3.UseSystemPasswordChar=false;

            }
            else
            {
                textBox4.UseSystemPasswordChar=true;
                textBox3.UseSystemPasswordChar=true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
