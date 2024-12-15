using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace course_work_project
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }
        string connectionString = "Server=localhost;Database=ClientRegistrationDB;User ID=root;Password=;";

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string customerId = CIDtextBox.Text;
            string customerEmail = CEmailtextBox.Text;

            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(customerEmail))
            {
                MessageBox.Show("Please enter both ID and Email.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Clients WHERE ID = @ID AND Email = @Email";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", customerId);
                    command.Parameters.AddWithValue("@Email", customerEmail);

                    int result = Convert.ToInt32(command.ExecuteScalar());

                    if (result > 0)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dashboard dashboard = new Dashboard();
                        dashboard.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("ID or Email is not correct.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Hide();
        }
    }
}
