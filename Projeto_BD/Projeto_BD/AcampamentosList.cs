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
    public partial class AcampamentosList : Form
    {
        private SqlConnection CN;
        private int Numero_De_Serie;
        public AcampamentosList(int Numero_De_Serie)
        {
            InitializeComponent();
            this.Numero_De_Serie = Numero_De_Serie;

            CN = ConnectionManager.getSGBDConnection();
            LoadData();
        }

        private void AcampamentosList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ViewPasses_Acampamentos WHERE Numero_De_Serie = @Numero_Serie", CN);
                cmd.Parameters.AddWithValue("@Numero_Serie", Numero_De_Serie);

                DataTable detailsTable = new DataTable();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                sqlDataAdapter.Fill(detailsTable);
                campList.DataSource = detailsTable;
                campList.Visible = true;

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

        private void campList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
