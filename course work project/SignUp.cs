using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace course_work_project
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }


        private void Registerbtn_Click(object sender, EventArgs e)
        {
            // Logic to gather form input and register a user
            string id = CIDtextBox.Text;
            string name = CNametextBox.Text;
            string address = CAddresstextBox.Text;
            string phone = CPhoneNotextBox.Text;
            string email = CEmailtextBox.Text;

            // Categories Checkboxes
            string categories = "";
            if (checkBoxGames.Checked) categories += "Games, ";
            if (checkBoxOfficeTools.Checked) categories += "Office Tools, ";
            if (checkBoxSoftware.Checked) categories += "Software, ";
            if (checkBoxAccessories.Checked) categories += "Accessories, ";
            if (checkBoxLaptopsPC.Checked) categories += "Laptops_PC";

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Please enter a valid ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter an address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Please enter a phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(categories))
            {
                MessageBox.Show("Please select at least one category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Connection string to connect to MySQL
                string connectionString = "Server=localhost;Database=ClientRegistrationDB;User ID=root;Password=;";

                // SQL query to insert data
                string query = "INSERT INTO Clients (ID, Name, Address, Phone, Email, Categories) VALUES (@ID, @Name, @Address, @Phone, @Email, @Categories)";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Categories", categories);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data successfully registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registration failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            CIDtextBox.Text = "";
            CNametextBox.Text = "";
            CAddresstextBox.Text = "";
            CPhoneNotextBox.Text = "";
            CEmailtextBox.Text = "";
            checkBoxGames.Checked = false;
            checkBoxOfficeTools.Checked = false;
            checkBoxSoftware.Checked = false;
            checkBoxAccessories.Checked = false;
            checkBoxLaptopsPC.Checked = false;
        }


        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void buttonSignIn_Click_1(object sender, EventArgs e)
        {
            SignIn signIn1 = new SignIn();
            signIn1.Show();
            this.Hide();
        }

    }
}
