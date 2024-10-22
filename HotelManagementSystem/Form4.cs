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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form = new Form3();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Step 1: Validate if Password and Confirm Password match
            if (textBox1.Text != textBox2.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Stop further execution if passwords don't match
            }

            // Step 2: Validate if username exists
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                sql.Open();

                // Step 3: Check if the user exists
                SqlCommand checkUser = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Username = @Username", sql);
                checkUser.Parameters.AddWithValue("@Username", textBox3.Text); // Assuming textBox3 holds the username
                int userExists = (int)checkUser.ExecuteScalar();

                if (userExists == 0)
                {
                    MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;  // Stop further execution if the user is not found
                }

                // Step 4: Update the password if the user exists and passwords match
                SqlCommand updatePassword = new SqlCommand("UPDATE Login SET Password = @Password, Confirm_Password = @ConfirmPassword WHERE Username = @Username", sql);
                updatePassword.Parameters.AddWithValue("@Username", textBox3.Text); // Username from the search
                updatePassword.Parameters.AddWithValue("@Password", textBox1.Text); // New password
                updatePassword.Parameters.AddWithValue("@ConfirmPassword", textBox2.Text); // Confirm password

                int rowsAffected = updatePassword.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password has been reset successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Login form = new Login();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error resetting the password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Step 5: Clear fields after resetting the password
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear(); // Clear the username textbox (if necessary)
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
