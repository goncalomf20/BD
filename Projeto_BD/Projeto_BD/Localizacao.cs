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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_BD
{
    public partial class Localizacao : Form
    {
        private SqlConnection CN;
        public Localizacao()
        {
            InitializeComponent();
            CN = new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER,8101; Initial Catalog = p8g10; uid = p8g10; password = SaGo.2021#; TrustServerCertificate=true");

            //CN.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Localizacao", CN);

            //DataTable detailsTable = new DataTable();
            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            //sqlDataAdapter.Fill(detailsTable);
            //dataGridView1.DataSource = detailsTable;
            //dataGridView1.Visible = true;
            //CN.Close();

            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Localizacao_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Localizacao", CN);

                DataTable detailsTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(detailsTable);
                dataGridView1.DataSource = detailsTable;
                dataGridView1.Visible = true;

                CN.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please, try again");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SummerFest.AdicionarLocalizacao", CN);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters with values from the text boxes
                cmd.Parameters.AddWithValue("@ID_Localizacao", int.Parse(textBox1.Text));
                cmd.Parameters.AddWithValue("@Coordenadas", textBox6.Text);
                cmd.Parameters.AddWithValue("@Pais", textBox5.Text);
                cmd.Parameters.AddWithValue("@Cidade", textBox4.Text);
                cmd.Parameters.AddWithValue("@Lugar", textBox3.Text);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Data inserted successfully.");

                CN.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            LoadData();
        }
    }
}
