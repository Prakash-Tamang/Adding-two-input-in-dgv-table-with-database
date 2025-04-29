using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adding_two_input_in_dgv_table_with_database
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=Furba;Initial Catalog=Details;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                //- Creates a new `SqlConnection` object using the provided connection string.
                //The `using` block ensures the connection is properly closed and disposed, even if an error occurs.
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT FirstName, LastName FROM PersonDetails";

                    //- `SqlDataAdapter` acts as a bridge between the SQL Server and your `DataTable`.
                    //It uses the query to fetch data.
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    //A new DataTable is created.
                    //The adapter fills it with the result of the query.
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvNameDetails.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Please Enter First Name","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Please Enter Last Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try {
                using (SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO PersonDetails(FirstName,LastName) VALUES(@FirstName,@LastName)";
                    using (SqlCommand command=new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName",txtFirstName.Text);
                        command.Parameters.AddWithValue("@LastName",txtLastName.Text);
                        command.ExecuteNonQuery();
                    }
                }
                txtFirstName.Clear();
                txtLastName.Clear();
                LoadData();
                MessageBox.Show("Name added successfully!");
            }
            catch (Exception ex) {
                MessageBox.Show("Error adding Name: " + ex.Message);
            }
        }
    }
}
