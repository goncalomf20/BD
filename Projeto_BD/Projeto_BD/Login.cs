using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projeto_BD.Inicial;

namespace Projeto_BD
{
    public partial class Login : Form
    {
        private SqlConnection CN;
        private int ID_Festival;

        public Login()
        {
            InitializeComponent();
            CN = ConnectionManager.getSGBDConnection();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();

                DataTable detailsTable = new DataTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (CN.State == ConnectionState.Open)
                    CN.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Login_Load_1(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string id_fest = textBox1.Text;
            string password = textBox2.Text;

            if (!string.IsNullOrEmpty(id_fest) && !string.IsNullOrEmpty(password))
            {
                using (SqlConnection connection = ConnectionManager.getSGBDConnection())
                {
                    try
                    {
                        connection.Open();

                        // Check if the festival ID and password match
                        string query = "SELECT COUNT(*) FROM SummerFest.Festival WHERE ID_Festival = @ID_Festival AND Password_Festival = @Password_Festival";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID_Festival", id_fest);
                            command.Parameters.AddWithValue("@Password_Festival", password);

                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                // The festival ID and password match
                                // Proceed with the login logic
                                this.Hide();
                                Inicial inicial = new Inicial();
                                inicial.Show();
                            }
                            else
                            {
                                // The festival ID and password do not match
                                MessageBox.Show("Invalid festival ID or password");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox1.Clear();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
