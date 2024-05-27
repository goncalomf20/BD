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
    public partial class Artista : Form
    {
        private SqlConnection CN;
        private int Numero_Do_Concerto;
        public Artista(int Numero_Do_Concerto)
        {
            InitializeComponent();
            this.Numero_Do_Concerto = Numero_Do_Concerto;

            CN = ConnectionManager.getSGBDConnection();
            LoadData();
        }

        private void Artista_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ViewArtista_Concerto WHERE Numero_Concerto = @Numero_Concerto", CN);
                cmd.Parameters.AddWithValue("@Numero_Concerto", Numero_Do_Concerto);

                DataTable detailsTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(detailsTable);
                dataGridView1.DataSource = detailsTable;
                dataGridView1.Visible = true;

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
    }
}
