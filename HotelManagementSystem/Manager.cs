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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HotelManagementSystem
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            BindData();
            DisplayTotalRooms(); // Update total rooms
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private string GetStatus()
        {
            if (radioButton1.Checked) return "Available";
            if (radioButton2.Checked) return "Not available";
            
            return "";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || !decimal.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Please fill all fields correctly.");
                return;
            }

            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection sql = new SqlConnection(ConnectionData))
                {
                    sql.Open();

                    // Insert query to add room details into the Room table
                    SqlCommand cmd = new SqlCommand("INSERT INTO Room (RoomNumber, Name, Capacity, Quality, Price, Date, Status) VALUES (@RoomNumber, @Name, @Capacity, @Quality, @Price, @Date, @Status)", sql);

                    // Add parameters from the input fields
                    cmd.Parameters.AddWithValue("@RoomNumber", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Capacity", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Quality", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Price", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Status", GetStatus()); // Get the status based on the selected radio button

                    // Execute the query
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Room added successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Room addition failed.");
                    }

                    // Clear the input fields after successful addition
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    dateTimePicker1.ResetText();
                }

                // Refresh the DataGridView to display the updated room list
                BindData();
                DisplayTotalRooms(); // Update total rooms
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        void BindData()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            SqlConnection sql = new SqlConnection(ConnectionData);
            SqlCommand sq3 = new SqlCommand("select * from Room", sql);
            SqlDataAdapter sd = new SqlDataAdapter(sq3);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Validate if RoomNumber is provided
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter a Room Number to search.");
                return;
            }

            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";
            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                try
                {
                    sql.Open();

                    // Search query based on RoomNumber
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Room WHERE RoomNumber = @RoomNumber", sql);
                    cmd.Parameters.AddWithValue("@RoomNumber", textBox1.Text);

                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        // Display the result in the DataGridView
                        dataGridView1.DataSource = dt;
                        MessageBox.Show("Room found and displayed.");
                    }
                    else
                    {
                        MessageBox.Show("No room found with the provided Room Number.");
                        dataGridView1.DataSource = null; // Clear DataGridView if no data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || !decimal.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show("Please fill all fields correctly.");
                return;
            }

            string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection sql = new SqlConnection(ConnectionData))
                {
                    sql.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Room SET Name=@Name, Capacity=@Capacity, Quality=@Quality, Price=@Price, Date=@Date, Status=@Status WHERE RoomNumber=@RoomNumber", sql))
                    {
                        cmd.Parameters.AddWithValue("@RoomNumber", textBox1.Text);
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Capacity", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@Quality", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@Price", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value);
                        cmd.Parameters.AddWithValue("@Status", GetStatus());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Room information updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No room found with the provided room number.");
                        }
                        // Clear the input fields after successful update
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        radioButton1.Checked = false;
                        radioButton2.Checked = false;
                        dateTimePicker1.ResetText();
                    }
                }

                // Refresh the DataGridView to show updated data
                BindData();
                DisplayTotalRooms(); // Update total rooms
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the RoomNumber from the selected row (assuming RoomNumber is in the first column)
                string roomNumber = dataGridView1.SelectedRows[0].Cells["RoomNumber"].Value.ToString();

                // Confirm deletion with the user
                DialogResult result = MessageBox.Show("Are you sure you want to delete Room Number: " + roomNumber + "?", "Delete Room", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";
                    using (SqlConnection sql = new SqlConnection(ConnectionData))
                    {
                        try
                        {
                            sql.Open();

                            // SQL command to delete the room based on RoomNumber
                            SqlCommand cmd = new SqlCommand("DELETE FROM Room WHERE RoomNumber = @RoomNumber", sql);
                            cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Room deleted successfully.");

                                // Refresh the DataGridView to remove the deleted row
                                BindData();
                                DisplayTotalRooms(); // Update total rooms
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete room. Room may not exist.");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();
            AllRoom form = new AllRoom();
            form.ShowDialog();
        }

        // Method to display the total number of rooms
        void DisplayTotalRooms()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                // Query to get the total count of rooms
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Room", sql);
                sql.Open();
                int totalRooms = (int)cmd.ExecuteScalar(); // Execute the command and get the total number of rooms

                // Display the total number of rooms in dataGridView2
                DataTable dt = new DataTable();
                dt.Columns.Add("Total Rooms");
                dt.Rows.Add(totalRooms);
                dataGridView2.DataSource = dt; // Bind the total number of rooms to dataGridView2
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
