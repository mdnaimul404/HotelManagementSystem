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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validate if Username is provided
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a Username to search.");
                return;
            }

            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                try
                {
                    // Open the SQL connection
                    sql.Open();

                    // Create a SQL query to find a user by Username
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Login WHERE Username = @Username", sql);
                    cmd.Parameters.AddWithValue("@Username", textBox1.Text);

                    // Execute the query and fill the DataTable
                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);

                    // Close the SQL connection
                    sql.Close();

                    // Check if the user was found (i.e., there is at least one row in the DataTable)
                    if (dt.Rows.Count > 0)
                    {
                        // Hide the current form and open Form4
                        this.Hide();
                        Form4 form = new Form4();
                        form.ShowDialog();
                    }
                    else
                    {
                        // Show a message if the username was not found
                        MessageBox.Show("Username not found.");
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
