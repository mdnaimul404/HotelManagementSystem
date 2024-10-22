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
    public partial class BookingForm : Form
    {
        public BookingForm()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

        }


        private void BookingForm_Load(object sender, EventArgs e)
        {
            // Define the connection string
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            // SQL query to fetch available rooms
            string query = "SELECT * FROM Room WHERE Status = 'available'";

            // Use SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Use SqlDataAdapter to execute the query and fill the data into a DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the query result
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the clicked row is valid (not the header row)
            if (e.RowIndex >= 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Fill the textboxes with the selected row's data
                label19.Text = selectedRow.Cells["RoomNumber"].Value.ToString();  // Room Number
                label20.Text = selectedRow.Cells["Name"].Value.ToString();        // Name
                label21.Text = selectedRow.Cells["Capacity"].Value.ToString();    // Capacity
                label22.Text = selectedRow.Cells["Quality"].Value.ToString();    // Quality
                label23.Text = selectedRow.Cells["Price"].Value.ToString();      // Price
            }
        }

        // Method to collect data and insert into the 'Client' database table
        private void CollectAndSaveClientData()
        {
            // Collect form data
            string name = textBox1.Text.Trim();
            string contactNumber = textBox2.Text.Trim();
            string idNumber = textBox3.Text.Trim();
            string email = textBox4.Text.Trim();
            string ageText = textBox5.Text.Trim();
            string address = textBox6.Text.Trim();

            // Parse age (default to 0 if parsing fails)
            int age = int.TryParse(ageText, out int parsedAge) ? parsedAge : 0;

            // ID Type from ComboBox
            string idType = comboBox1.SelectedItem?.ToString() ?? string.Empty;

            // Gender from RadioButtons
            string gender = radioButton1.Checked ? "Male" :
                            radioButton2.Checked ? "Female" :
                            radioButton3.Checked ? "Other" : string.Empty;

            // Dates from DateTimePickers
            DateTime dateOfBirth = dateTimePicker1.Value;
            DateTime checkInDate = dateTimePicker2.Value;
            DateTime checkOutDate = dateTimePicker3.Value;

            // Now, insert this data into the 'Client' table in the database
            InsertClientIntoDatabase(name, contactNumber, idNumber, email, age, address, idType, gender, dateOfBirth, checkInDate, checkOutDate);
        }

        // Separate method to handle database insertion
        private void InsertClientIntoDatabase(string name, string contactNumber, string idNumber, string email, int age, string address, string idType, string gender, DateTime dateOfBirth, DateTime checkInDate, DateTime checkOutDate)
        {
            // Connection string for your database
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            // SQL query to insert data into the 'Client' table
            //string query = "INSERT INTO Clint (Name, Contact number, ID number, Email, Age, Address, ID type, Gender, Date of birth, Check in date, Check out date) " +
            //               "VALUES (@Name, @ContactNumber, @IDNumber, @Email, @Age, @Address, @IDType, @Gender, @DateOfBirth, @CheckInDate, @CheckOutDate)";

            // SQL query to insert data into the 'Client' table with column names wrapped in square brackets
            string query = "INSERT INTO Clint ([Name], [Contact number], [ID number], [Email], [Age], [Address], [ID type], [Gender], [Date of birth], [Check in date], [Check out date]) " +
                           "VALUES (@Name, @ContactNumber, @IDNumber, @Email, @Age, @Address, @IDType, @Gender, @DateOfBirth, @CheckInDate, @CheckOutDate)";


            // Use SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Use SqlCommand to execute the query
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@ContactNumber", contactNumber);
                        command.Parameters.AddWithValue("@IDNumber", idNumber);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Age", age);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@IDType", idType);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@CheckInDate", checkInDate);
                        command.Parameters.AddWithValue("@CheckOutDate", checkOutDate);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if the insertion was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Client data saved successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to save client data.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }



        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Define the connection string
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            // Get the selected room number from label14 (since it contains RoomNumber)
            string roomNumber = label19.Text;  // Update this to use label19

            if (!string.IsNullOrEmpty(roomNumber))
            {
                // SQL query to update the room status to 'booked'
                string query = "UPDATE Room SET Status = 'booked' WHERE RoomNumber = @RoomNumber";

                // Use SqlConnection to connect to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Use SqlCommand to execute the update query
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Add parameter for RoomNumber
                            command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            // Check if the update was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Room booked successfully!");
                                // Call the method to collect data and save to the database
                                CollectAndSaveClientData();
                                // Refresh the DataGridView to reflect the updated status
                                RefreshGridView();
                            }
                            else
                            {
                                MessageBox.Show("No room not found with the specified room number.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that may occur
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room first.");
            }

        }

        // Method to refresh the DataGridView after updating the room status
        private void RefreshGridView()
        {
            // Define the connection string
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            // SQL query to fetch available rooms (after update)
            string query = "SELECT * FROM Room WHERE Status = 'available'";

            // Use SqlConnection to connect to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection.Open();

                    // Use SqlDataAdapter to execute the query and fill the data into a DataTable
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();

                    // Fill the DataTable with the query result
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    // Handle any errors that may occur
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AllRoom form = new AllRoom();
            form.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}