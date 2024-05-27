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
    public partial class PatrocinadoresBList : Form
    {
        private SqlConnection CN;
        private int ID_Patrocinador;

        public PatrocinadoresBList(int ID_Patrocinador)
        {
            InitializeComponent();
            this.ID_Patrocinador = ID_Patrocinador;
            CN = ConnectionManager.getSGBDConnection();
            LoadData();

        }

        private void PatrocinadoresB_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ViewPatrocinador_Barraca WHERE ID_Patrocinador = @ID_Patrocinador", CN);
                cmd.Parameters.AddWithValue("@ID_Patrocinador", ID_Patrocinador);

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

        private void PatrocinadoresBList_Load(object sender, EventArgs e)
        {

        }
    }
}
