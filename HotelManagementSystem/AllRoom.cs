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

namespace HotelManagementSystem
{
    public partial class AllRoom : Form
    {
        public AllRoom()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        void BindData()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                SqlCommand sq3 = new SqlCommand("SELECT * FROM Room", sql);
                SqlDataAdapter sd = new SqlDataAdapter(sq3);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void AllRoom_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindData(); // Refresh all room data
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager form = new Manager();
            form.ShowDialog();
        }

        void BindBookedRooms()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                // Query to select only booked rooms
                SqlCommand cmd = new SqlCommand("SELECT * FROM Room WHERE Status = 'Booked'", sql);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindBookedRooms(); // Fetch and display only booked rooms
        }

        // New method to bind available rooms
        void BindAvailableRooms()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                // Query to select only available rooms
                SqlCommand cmd = new SqlCommand("SELECT * FROM Room WHERE Status = 'Available'", sql);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BindAvailableRooms(); // Fetch and display only available rooms
        }

        // New method to bind "Not Available" rooms
        void BindNotAvailableRooms()
        {
            String ConnectionData = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\saju1\OneDrive\Documents\employee.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection sql = new SqlConnection(ConnectionData))
            {
                // Query to select only "Not Available" rooms
                SqlCommand cmd = new SqlCommand("SELECT * FROM Room WHERE Status = 'Not Available'", sql);
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sd.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindNotAvailableRooms(); // Fetch and display only "Not Available" rooms
            
        }






        private void button6_Click(object sender, EventArgs e)
        {
            //// Check if a row is selected
            //if (dataGridView1.SelectedRows.Count > 0)
            //{
            //    // Get the selected row
            //    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            //    // Assuming you have columns for RoomId and RoomDetails
            //    string roomId = selectedRow.Cells["RoomId"].Value.ToString(); // Adjust the column name as necessary
            //    string roomDetails = selectedRow.Cells["RoomDetails"].Value.ToString(); // Adjust this as well

            //    // Create an instance of BookingForm and pass the room information
            //    BookingForm bookingForm = new BookingForm(roomId, roomDetails);
            //    bookingForm.ShowDialog(); // Show the booking form as a dialog
            //}
            //else
            //{
            //    MessageBox.Show("Please select a room to book.");
            //}

            this.Hide();
            BookingForm form = new BookingForm();
            form.ShowDialog();
        }
    }
}
