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
    public partial class PasseList : Form
    {
        private SqlConnection CN;
        private int ID_Festival;

        public PasseList(int ID_Festival)
        {
            InitializeComponent();
            this.ID_Festival = ID_Festival;
            CN = ConnectionManager.getSGBDConnection();
            LoadData();
        }

        private void Passe_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                CN.Open();
                SqlCommand cmd = new SqlCommand("SELECT p.* FROM SummerFest.Passe p INNER JOIN SummerFest.Festival f ON p.ID_Festival = f.ID_Festival WHERE f.ID_Festival = @ID_Festival", CN);
                cmd.Parameters.AddWithValue("@ID_Festival", ID_Festival);


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

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void PasseList_Load(object sender, EventArgs e)
        {

        }
    }
}
