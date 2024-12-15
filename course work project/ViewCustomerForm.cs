using MySql.Data.MySqlClient; // MySQL library 
using System;
using System.Data;
using System.Windows.Forms;

namespace course_work_project
{
    public partial class ViewCustomerForm : Form
    {
        public ViewCustomerForm()
        {
            InitializeComponent();
        }

        // MySQL Connection String
        string connectionString = "Server=localhost;Database=ClientRegistrationDB;User ID=root;Password=;";

        // Load all data from Clients table
        private void buttonView_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Clients";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable; // Bind data to DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Search by ID
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string id = textBoxSearch.Text; // Get the entered ID
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please enter a valid ID.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM Clients WHERE ID = @ID";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", id);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable; // Display the row for the given ID
                    }
                    else
                    {
                        MessageBox.Show("No data found for the given ID.");
                        dataGridView1.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // Delete the selected row
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected row?",
                                                      "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                            {
                                if (!row.IsNewRow)
                                {
                                    string id = row.Cells["ID"].Value.ToString();
                                    string query = "DELETE FROM Clients WHERE ID = @ID";
                                    MySqlCommand command = new MySqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@ID", id);
                                    command.ExecuteNonQuery();
                                    dataGridView1.Rows.Remove(row); // Remove from DataGridView
                                }
                            }
                            MessageBox.Show("Selected row(s) deleted successfully!");
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
        // Back button
        private void buttonBack_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (row.IsNewRow) continue; // Skip new rows

                        // Retrieve updated values from the DataGridView row
                        string id = row.Cells["ID"].Value.ToString();
                        string name = row.Cells["Name"].Value.ToString();
                        string email = row.Cells["Email"].Value.ToString();
                        string phone = row.Cells["Phone"].Value.ToString();

                        // MySQL query for updating the record
                        string query = @"UPDATE Clients SET 
                                 Name = @Name, 
                                 Email = @Email, 
                                 Phone = @Phone 
                                 WHERE ID = @ID";

                        MySqlCommand command = new MySqlCommand(query, connection);

                        // Bind parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@ID", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Phone", phone);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Selected row(s) updated successfully!");

                    // Refresh the DataGridView to reflect the updated data
                    string refreshQuery = "SELECT * FROM Clients";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(refreshQuery, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}
