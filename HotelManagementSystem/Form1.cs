using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\Employee.mdf;Integrated Security=True;Connect Timeout=30";
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionData))
                {
                    con.Open();
                    //string query = "SELECT COUNT(1) FROM login WHERE Username=@Username AND Password=@Password";

                    // For case sensitive issue of username and password:
                    // Here "COLLATE SQL_Latin1_General_CP1_CS_AS" checkes a case-sensitive issue. 
                    string query = "SELECT COUNT(1) FROM login WHERE Username COLLATE SQL_Latin1_General_CP1_CS_AS = @Username AND Password COLLATE SQL_Latin1_General_CP1_CS_AS = @Password";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Password", textBox2.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    // using Convert.ToInt32 because ExecuteScalar is a kind of object not sting. 
                    // we use int.Parse to convert sting to int. But Convert.ToInt32 handle many cases like object, string, null etc.


                    //Here we are using ExecuteScalar() which returns a result like SELECT COUNT(1), SELECT MAX(ID) ETC

                    if (count == 1)
                    {
                        this.Hide();
                        Manager Manager = new Manager();
                        Manager.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Id or Password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Id or Password Error: "+ ex.Message);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form = new Form2();
            form.ShowDialog();

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==false)
            {

                textBox2.UseSystemPasswordChar=false;
            }
            else
            {
                textBox2.UseSystemPasswordChar=true;   
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
