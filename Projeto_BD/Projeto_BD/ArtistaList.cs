using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projeto_BD.Inicial;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_BD
{
    public partial class ArtistaList : Form
    {
        private SqlConnection CN;
        private int ID_Festival;

        public ArtistaList(int ID_Festival)
        {
            InitializeComponent();
            this.ID_Festival = ID_Festival;
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.ViewArtista_Festival WHERE ID_Festival = @ID_Festival", CN);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //try
            //{
            //    CN.Open();
            //    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarArtista_Festival");
            //    cmd.CommandType = CommandType.StoredProcedure;

            //    // Add parameters with values from the text boxes
            //    cmd.Parameters.AddWithValue("@ID_Festival", int.Parse(id.Text));
            //    cmd.Parameters.AddWithValue("@Nome", nome.Text);
            //    cmd.Parameters.AddWithValue("@Data_De_Inicio", data_inicio.Text);
            //    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao.Text);
            //    cmd.Parameters.AddWithValue("@Lotacao_Maxima", lotacao.Text);
            //    cmd.Parameters.AddWithValue("@ID_Localizacao", id_localizacao.Text);

            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Festival adicionado com sucesso!");

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ocorreu um erro ao adicionar o festival: " + ex.Message);
            //}
            //LoadData();

        }
        //    try
        //    {
        //        CN.Open();
        //        SqlCommand cmd = new SqlCommand("SummerFest.AdicionarArtista", CN);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // Add parameters with values from the text boxes
        //        cmd.Parameters.AddWithValue("@ID_Artista", int.Parse(textBox1.Text));
        //        cmd.Parameters.AddWithValue("@Nome_Artistico", textBox2.Text);
        //        cmd.Parameters.AddWithValue("@Estilo_Musical", textBox3.Text);
        //        cmd.Parameters.AddWithValue("@Nome_Verdadeiro", textBox4.Text);
        //        cmd.Parameters.AddWithValue("@Idade", textBox5.Text);
        //        cmd.Parameters.AddWithValue("@Premios", textBox6.Text);
        //        cmd.Parameters.AddWithValue("@Nacionalidade", textBox7.Text);

        //        cmd.ExecuteNonQuery();
        //        MessageBox.Show("Data inserted successfully.");

        //        CN.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("An error occurred: " + ex.Message);
        //    }
        //    LoadData();
        //}
    }
}
