using Microsoft.Data.SqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using static Projeto_BD.Login;
using SortOrder = System.Windows.Forms.SortOrder;

namespace Projeto_BD
{
    public partial class Inicial : Form
    {
        private SqlConnection CN;
        public Inicial()
        {
            InitializeComponent();
            CN = ConnectionManager.getSGBDConnection();

            //dataGridView1.CellClick += dataGridView1_CellClick;
            //dataGridView2.CellClick += dataGridView2_CellClick;
            //dataGridView3.CellClick += dataGridView3_CellClick;
            //dataGridView5.CellClick += dataGridView5_CellClick;
            //dataGridView6.CellClick += dataGridView6_CellClick;
            //dataGridView7.CellClick += dataGridView7_CellClick;
        }

        public static class ConnectionManager
        {
            public static SqlConnection getSGBDConnection()
            {
                return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER,8101; Initial Catalog = p8g10; uid = p8g10; password = SaGo.2021#; TrustServerCertificate=true");
            }
        }

        //Festivais Tab

        private void Festival_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Festival", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView1.DataSource = detailsTable;
            dataGridView1.Visible = true;

            Clear.Visible = false;
            Add.Visible = false;
            Del.Visible = false;
            back.Visible = false;
            Edit.Visible = false;
            ArtistsList.Visible = true;
            BarracasLIst.Visible = true;
            PassesList.Visible = true;
            AcampamentoPanel.Visible = false;
            ArtistasPanel.Visible = false;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            PassePanel.Visible = false;
            ConcertosPanel.Visible = false;

            LoadLocations();

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }

        private void LoadLocations()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Localizacao", CN);
                SqlDataReader reader = cmd.ExecuteReader();

                Lugar.Items.Clear();
                while (reader.Read())
                {
                    // Store both ID_Localizacao and Lugar as a KeyValuePair in the ComboBox
                    int idLocalizacao = (int)reader["ID_Localizacao"];
                    string lugar = reader["Lugar"].ToString();
                    id_localizacao.Text = lugar;
                    Lugar.Items.Add(new KeyValuePair<int, string>(idLocalizacao, lugar));
                }
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


        private void FestivaisTab_Click(object sender, EventArgs e)
        {
            FestivaisTab.FlatAppearance.BorderColor = Color.FromArgb(128, 128, 255);
            FestivalPanel.Visible = true;
            ConcertosPanel.Visible = false;
            PassePanel.Visible = false;
            AcampamentoPanel.Visible = false;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            ArtistasPanel.Visible = false;
            Festival_Load(sender, e);
        }

        private void LoadDataFest()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Festival", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView1.DataSource = detailsTable;
                    dataGridView1.Visible = true;

                    if (dataGridView1.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView1.Rows.Count - 1;
                        dataGridView1.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView1.Rows[lastIndex].Selected = true;
                    }
                }

                id.Visible = true;
                nome.Visible = true;
                data_inicio.Visible = true;
                duracao.Visible = true;
                lotacao.Visible = true;
                id_localizacao.Visible = true;
                ArtistsList.Visible = true;
                BarracasLIst.Visible = true;
                PassesList.Visible = true;
                back.Visible = false;
                Clear.Visible = false;
                Add.Visible = false;
                Del.Visible = false;
                Edit.Visible = false;
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id.Text = row.Cells["ID_Festival"].Value.ToString();
                nome.Text = row.Cells["Nome"].Value.ToString();
                data_inicio.Text = row.Cells["Data_de_Inicio"].Value.ToString();
                duracao.Text = row.Cells["Duracao_Dias"].Value.ToString();
                lotacao.Text = row.Cells["Lotacao_Maxima"].Value.ToString();
                id_localizacao.Text = row.Cells["ID_Localizacao"].Value.ToString();

                int selectedID = (int)row.Cells["ID_Localizacao"].Value;

                // Iterate through the ComboBox items and find the KeyValuePair with the matching ID_Localizacao
                foreach (var item in Lugar.Items)
                {
                    var kvp = (KeyValuePair<int, string>)item;
                    if (kvp.Key == selectedID)
                    {
                        Lugar.SelectedItem = item;
                        break;
                    }
                }

            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            DataGridViewColumn clickedColumn = dataGridView1.Columns[e.ColumnIndex];

            if (clickedColumn.SortMode != DataGridViewColumnSortMode.Programmatic)
                return;

            ListSortDirection sortDirection = ListSortDirection.Ascending;
            if (dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending)
            {
                sortDirection = ListSortDirection.Descending;
            }

            dataGridView1.Sort(dataGridView1.Columns[e.ColumnIndex], sortDirection);
        }

        private void pesquisar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dados_pesq_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesquisar.Text;
            string filtro = dados_pesq.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "ID":
                        dados = "ID_Festival";
                        break;
                    case "Nome":
                        dados = "Nome";
                        break;
                    case "Data de Inicio":
                        dados = "Data_de_Inicio";
                        break;
                    case "Duração":
                        dados = "Duracao_Dias";
                        break;
                    case "Lotação Máxima":
                        dados = "Lotacao_Maxima";
                        break;
                    case "ID da Localizacao":
                        dados = "Id_Localizacao";
                        break;
                    default:
                        break;

                }

                //comboBox1.Items.Clear();
                //string query = "SELECT * FROM SummerFest.Festival WHERE " + dados + " LIKE @filtro";
                //using (SqlCommand cmd = new SqlCommand(query, CN))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Festival WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView1.DataSource = detailsTable;
                    dataGridView1.Visible = true;
                }

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

        private void clear_filters_Click(object sender, EventArgs e)
        {
            dados_pesq.Clear();
            pesquisar.SelectedIndex = -1;
            Festival_Load(sender, e);
        }

        private void ArtistsList_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID_Festival = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Festival"].Value);

                ArtistaList artistas = new ArtistaList(ID_Festival);
                artistas.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione o festival!");
            }
        }

        private void BarracasLIst_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID_Festival = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Festival"].Value.ToString());

                BarracasList barracas = new BarracasList(ID_Festival);
                barracas.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione o festival!");
            }
        }

        private void PassesList_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int ID_Festival = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_Festival"].Value);

                PasseList passes = new PasseList(ID_Festival);
                passes.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione o festival!");
            }
        }

        private void AddFest_Click(object sender, EventArgs e)
        {
            id.Visible = true;
            nome.Visible = true;
            data_inicio.Visible = true;
            duracao.Visible = true;
            lotacao.Visible = true;
            id_localizacao.Visible = true;
            Add.Visible = true;
            Clear.Visible = true;
            Del.Visible = false;
            ArtistsList.Visible = false;
            BarracasLIst.Visible = false;
            PassesList.Visible = false;
            Edit.Visible = false;
            back.Visible = true;
        }

        private void DelFest_Click(object sender, EventArgs e)
        {
            id.Visible = false;
            nome.Visible = false;
            data_inicio.Visible = false;
            duracao.Visible = false;
            lotacao.Visible = false;
            id_localizacao.Visible = false;
            Add.Visible = false;
            id.Clear();
            id.Visible = true;
            Del.Visible = true;
            Clear.Visible = true;
            ArtistsList.Visible = false;
            BarracasLIst.Visible = false;
            PassesList.Visible = false;
            Edit.Visible = false;
            back.Visible = true;
            Lugar.Visible = false;
        }
        private void EditFest_Click(object sender, EventArgs e)
        {
            id.Visible = true;
            nome.Visible = true;
            data_inicio.Visible = true;
            duracao.Visible = true;
            lotacao.Visible = true;
            id_localizacao.Visible = true;
            Add.Visible = false;
            Clear.Visible = true;
            Del.Visible = false;
            Edit.Visible = true;
            ArtistsList.Visible = false;
            BarracasLIst.Visible = false;
            PassesList.Visible = false;
            back.Visible = true;
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            string ID_Festival = id.Text;

            if (ID_Festival != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarFestival", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Festival", int.Parse(ID_Festival));
                    cmd.Parameters.AddWithValue("@Nome", nome.Text);
                    cmd.Parameters.AddWithValue("@Data_De_Inicio", data_inicio.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Dias", int.Parse(duracao.Text));
                    cmd.Parameters.AddWithValue("@Lotacao_Maxima", int.Parse(lotacao.Text));
                    var selectedLugar = (KeyValuePair<int, string>)Lugar.SelectedItem;
                    cmd.Parameters.AddWithValue("@ID_Localizacao", selectedLugar.Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Festival atualizado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao atualizar o festival: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
                LoadDataFest();
            }
            else
            {
                MessageBox.Show("Festival não foi selecionado corretamente...Insira o ID do festival que pretende editar.");
            }
        }

        private void FestivalPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void duracao_TextChanged(object sender, EventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            string ID_Festival = id.Text;

            if (ID_Festival != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarFestival", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Festival", int.Parse(id.Text));
                    cmd.Parameters.AddWithValue("@Nome", nome.Text);
                    cmd.Parameters.AddWithValue("@Data_De_Inicio", data_inicio.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao.Text);
                    cmd.Parameters.AddWithValue("@Lotacao_Maxima", lotacao.Text);
                    //cmd.Parameters.AddWithValue("@ID_Localizacao", id_localizacao.Text);

                    var selectedLugar = (KeyValuePair<int, string>)Lugar.SelectedItem;
                    cmd.Parameters.AddWithValue("@Lugar", selectedLugar.Value); // Pass the selected Lugar value as @Lugar parameter

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Festival adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o festival: " + ex.Message);
                }
                LoadDataFest();
            }
            else
            {
                MessageBox.Show("Festival não foi selecionado corretamente...Insira o ID do festival que pretende eliminar.");
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            id.Clear();
            nome.Clear();
            data_inicio.Clear();
            duracao.Clear();
            lotacao.Clear();
            id_localizacao.Clear();
        }


        private void id_localizacao_TextChanged(object sender, EventArgs e)
        {
        }

        private void lotacao_TextChanged(object sender, EventArgs e)
        {
        }
        private void Del_Click(object sender, EventArgs e)
        {
            string ID_Festival = id.Text;

            if (ID_Festival != "")
            {
                try
                {
                    if (CN.State == ConnectionState.Closed)
                        CN.Open();

                    using (SqlCommand cmd = new SqlCommand("SummerFest.EliminarFestival", CN))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Festival", ID_Festival);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Festival excluído com sucesso");
                    LoadDataFest();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao excluir o festival: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
            }
            else
            {
                MessageBox.Show("Festival não foi selecionado corretamente...Insira o ID do festival que pretende eliminar.");
            }
            LoadDataFest();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Festival_Load(sender, e);
        }

        private void data_inicio_TextChanged(object sender, EventArgs e)
        {
        }

        private void nome_TextChanged(object sender, EventArgs e)
        {
        }

        private void id_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void Lugar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public int GetAvailablePasses(int ID_Festival)
        {
            int passes_number = 0;

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SummerFest.PassesDisponiveis", CN))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Festival", ID_Festival);

                    SqlParameter passes_disponiveis = new SqlParameter("@PassesDisponiveis", SqlDbType.Int);
                    passes_disponiveis.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(passes_disponiveis);

                    cmd.ExecuteNonQuery();

                    passes_number = (int)passes_disponiveis.Value;

                    Console.WriteLine("Número de passes disponíveis: " + passes_number);
                }
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
            return passes_number;
        }



        private void Dpasses_Click(object sender, EventArgs e)
        {
            int ID_Festival = int.Parse(id.Text);

            if (!string.IsNullOrEmpty(id.Text))
            {
                try
                {
                    int availablePasses = GetAvailablePasses(ID_Festival);

                    MessageBox.Show("Número de Passes disponíveis: " + availablePasses);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Concerto não foi selecionado corretamente...Insira o ID do festival que pretende eliminar.");
            }
        }

        //Concertos Tab
        private void ConcertosTab_Click_1(object sender, EventArgs e)
        {
            FestivalPanel.Visible = false;
            ConcertosPanel.Visible = true;
            AcampamentoPanel.Visible = false;
            PassePanel.Visible = false;
            ArtistasPanel.Visible = false;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            Concertos_Load(sender, e);
        }
        private void Concertos_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Concertos", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView2.DataSource = detailsTable;
            dataGridView2.Visible = true;

            ClearC.Visible = false;
            AddC.Visible = false;
            DelC.Visible = false;
            backC.Visible = false;
            EditC.Visible = false;
            SeeCArtists.Visible = true;
        }

        private void LoadDataConcert()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Concertos", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView2.DataSource = detailsTable;
                    dataGridView2.Visible = true;

                    if (dataGridView2.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView2.Rows.Count - 1;
                        dataGridView2.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView2.Rows[lastIndex].Selected = true;
                    }
                }

                AddC.Visible = false;
                DelC.Visible = false;
                ClearC.Visible = false;
                EditC.Visible = false;
                backC.Visible = false;
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
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                numero_concerto.Text = row.Cells["Numero_Concerto"].Value.ToString();
                data_concerto.Text = row.Cells["Data_do_Concerto"].Value.ToString();
                duracao_concerto.Text = row.Cells["Duracao_Minutos"].Value.ToString();
                id_palcoC.Text = row.Cells["ID_Palco"].Value.ToString();
                id_festC.Text = row.Cells["ID_Festival"].Value.ToString();
            }
        }

        private void AddConcert_Click(object sender, EventArgs e)
        {
            numero_concerto.Visible = true;
            data_concerto.Visible = true;
            duracao_concerto.Visible = true;
            id_palcoC.Visible = true;
            id_festC.Visible = true;
            AddC.Visible = true;
            ClearC.Visible = true;
            DelC.Visible = false;
            EditC.Visible = false;
            backC.Visible = true;
            SeeCArtists.Visible = false;

        }
        private void DelConcert_Click(object sender, EventArgs e)
        {
            numero_concerto.Visible = true;
            data_concerto.Visible = false;
            duracao_concerto.Visible = false;
            id_palcoC.Visible = false;
            id_festC.Visible = false;
            AddC.Visible = false;
            ClearC.Visible = true;
            DelC.Visible = true;
            EditC.Visible = false;
            backC.Visible = true;
            SeeCArtists.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ConcertosPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
        }

        private void ClearC_Click(object sender, EventArgs e)
        {
            numero_concerto.Clear();
            data_concerto.Clear();
            duracao_concerto.Clear();
            id_palcoC.Clear();
            id_festC.Clear();
        }

        private void DelC_Click(object sender, EventArgs e)
        {
            string Numero_Concerto = numero_concerto.Text;

            if (Numero_Concerto != "")
            {
                try
                {
                    if (CN.State == ConnectionState.Closed)
                        CN.Open();

                    using (SqlCommand cmd = new SqlCommand("SummerFest.EliminarConcerto", CN))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Numero_Concerto", Numero_Concerto);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Concerto excluído com sucesso");
                    LoadDataConcert();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao excluir o concerto: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
            }
            else
            {
                MessageBox.Show("Concerto não foi selecionado corretamente...Insira o ID do festival que pretende eliminar.");
            }
            LoadDataConcert();
        }

        private void AddC_Click(object sender, EventArgs e)
        {
            string Numero_Concerto = numero_concerto.Text;

            if (Numero_Concerto != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarConcerto", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_Concerto", int.Parse(numero_concerto.Text));
                    cmd.Parameters.AddWithValue("@Data_do_Concerto", data_concerto.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Minutos", duracao_concerto.Text);
                    cmd.Parameters.AddWithValue("@ID_Palco", id_palcoC.Text);
                    cmd.Parameters.AddWithValue("@ID_Festival", id_festC.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Concerto adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o concerto: " + ex.Message);
                }
                LoadDataConcert();
            }
            else
            {
                MessageBox.Show("Concerto não foi selecionado corretamente...Insira o numero do concerto que pretende eliminar.");
            }
        }

        private void backC_Click(object sender, EventArgs e)
        {
            Concertos_Load(sender, e);
        }


        private void EditC_Click(object sender, EventArgs e)
        {
            string Numero_Concerto = numero_concerto.Text;

            if (Numero_Concerto != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarConcerto", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_Concerto", int.Parse(numero_concerto.Text));
                    cmd.Parameters.AddWithValue("@Data_do_Concerto", data_concerto.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Minutos", duracao_concerto.Text);
                    cmd.Parameters.AddWithValue("@ID_Palco", id_palcoC.Text);
                    cmd.Parameters.AddWithValue("@ID_Festival", id_festC.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Concerto atualizado com sucesso!");
                    Concertos_Load(sender, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao atualizar o concerto: " + ex.GetBaseException().Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
            }
            else
            {
                MessageBox.Show("Concerto não foi selecionado corretamente...Insira o ID do concerto que pretende editar.");
            }
            LoadDataConcert();
        }

        private void duracao_concerto_TextChanged(object sender, EventArgs e)
        {
        }

        private void data_concerto_TextChanged(object sender, EventArgs e)
        {
        }

        private void EditConcert_Click(object sender, EventArgs e)
        {
            numero_concerto.Visible = true;
            data_concerto.Visible = true;
            duracao_concerto.Visible = true;
            id_palcoC.Visible = true;
            id_festC.Visible = true;
            AddC.Visible = false;
            ClearC.Visible = true;
            DelC.Visible = false;
            EditC.Visible = true;
            backC.Visible = true;
            SeeCArtists.Visible = false;
        }

        private void PesquisarC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DadosPesqC_TextChanged(object sender, EventArgs e)
        {
            string criterio = PesquisarC.Text;
            string filtro = DadosPesqC.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "Número do Concerto":
                        dados = "Numero_Concerto";
                        break;
                    case "Data":
                        dados = "Data_do_Concerto";
                        break;
                    case "Duração":
                        dados = "Duracao_Minutos";
                        break;
                    case "ID do Palco":
                        dados = "ID_Palco";
                        break;
                    case "ID do Festival":
                        dados = "ID_Festival";
                        break;
                    default:
                        break;

                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Concerto WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView2.DataSource = detailsTable;
                    dataGridView2.Visible = true;
                }

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

        private void ClearFilterC_Click(object sender, EventArgs e)
        {
            DadosPesqC.Clear();
            PesquisarC.SelectedIndex = -1;
            Concertos_Load(sender, e);
        }

        private void nome_palco_C_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void id_festC_TextChanged(object sender, EventArgs e)
        {

        }

        private void SeeCArtists_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int Numero_Do_Concerto = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["Numero_Concerto"].Value.ToString());

                Artista CArtista = new Artista(Numero_Do_Concerto);
                CArtista.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione o passe!");
            }
        }

        //Artistas

        private void ArtistasTab_Click(object sender, EventArgs e)
        {
            ArtistasPanel.Visible = true;
            FestivalPanel.Visible = false;
            ConcertosPanel.Visible = false;
            PassePanel.Visible = false;
            AcampamentoPanel.Visible = false;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            Artista_Load(sender, e);
        }

        private void Artista_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Artistas", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView7.DataSource = detailsTable;
            dataGridView7.Visible = true;

            id_A.Visible = true;
            nome_A.Visible = true;
            estilo_A.Visible = true;
            nomev_A.Visible = true;
            textBox5.Visible = true;
            premios.Visible = true;
            nacionalidade.Visible = true;
            back_A.Visible = false;
            clear_A.Visible = false;
            add_A.Visible = false;
            del_A.Visible = false;
            edit_A.Visible = false;

        }

        private void LoadDataArtista()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Artistas", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView7.DataSource = detailsTable;
                    dataGridView7.Visible = true;

                    if (dataGridView7.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView7.Rows.Count - 1;
                        dataGridView7.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView7.Rows[lastIndex].Selected = true;
                    }
                }
                id_A.Visible = true;
                nome_A.Visible = true;
                estilo_A.Visible = true;
                nomev_A.Visible = true;
                textBox5.Visible = true;
                premios.Visible = true;
                nacionalidade.Visible = true;
                back_A.Visible = false;
                clear_A.Visible = false;
                add_A.Visible = false;
                del_A.Visible = false;
                edit_A.Visible = false;
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

        private void dataGridView7_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView7.Rows[e.RowIndex];
                id_A.Text = row.Cells["ID_Artista"].Value.ToString();
                nome_A.Text = row.Cells["Nome_Artistico"].Value.ToString();
                estilo_A.Text = row.Cells["Estilo_Musical"].Value.ToString();
                nomev_A.Text = row.Cells["Nome_Verdadeiro"].Value.ToString();
                textBox5.Text = row.Cells["Idade"].Value.ToString();
                premios.Text = row.Cells["Premios"].Value.ToString();
                nacionalidade.Text = row.Cells["Nacionalidade"].Value.ToString();
            }
        }

        private void AdicionarA_Click(object sender, EventArgs e)
        {
            id_A.Visible = true;
            nome_A.Visible = true;
            estilo_A.Visible = true;
            nomev_A.Visible = true;
            textBox5.Visible = true;
            premios.Visible = true;
            nacionalidade.Visible = true;
            add_A.Visible = true;
            clear_A.Visible = true;
            del_A.Visible = false;
            edit_A.Visible = false;
            back_A.Visible = true;

        }

        private void EditarA_Click_1(object sender, EventArgs e)
        {
            id_A.Visible = true;
            nome_A.Visible = true;
            estilo_A.Visible = true;
            nomev_A.Visible = true;
            textBox5.Visible = true;
            premios.Visible = true;
            nacionalidade.Visible = true;
            add_A.Visible = false;
            clear_A.Visible = true;
            del_A.Visible = false;
            edit_A.Visible = true;
            back_A.Visible = true;

        }

        private void EliminarA_Click(object sender, EventArgs e)
        {
            id_A.Visible = true;
            nome_A.Visible = false;
            estilo_A.Visible = false;
            nomev_A.Visible = false;
            textBox5.Visible = false;
            premios.Visible = false;
            nacionalidade.Visible = false;
            add_A.Visible = false;
            clear_A.Visible = true;
            del_A.Visible = true;
            edit_A.Visible = false;
            back_A.Visible = true;

        }

        private void add_A_Click(object sender, EventArgs e)
        {
            string ID_Artista = id_A.Text;

            if (ID_Artista != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarArtista", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Artista", int.Parse(id_A.Text));
                    cmd.Parameters.AddWithValue("@Nome_Artistico", nome_A.Text);
                    cmd.Parameters.AddWithValue("@Estilo_Musical", estilo_A.Text);
                    cmd.Parameters.AddWithValue("@Nome_Verdadeiro", nomev_A.Text);
                    cmd.Parameters.AddWithValue("@Idade", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Premios", premios.Text);
                    cmd.Parameters.AddWithValue("@Nacionalidade", nacionalidade.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Artista adicionado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o artista: " + ex.Message);
                }
                LoadDataArtista();
            }
            else
            {
                MessageBox.Show("Artista não foi selecionado corretamente...Insira o ID do artista que pretende eliminar.");
            }
        }

        private void edit_A_Click(object sender, EventArgs e)
        {
            string ID_Artista = id_A.Text;

            if (ID_Artista != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarArtista", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Artista", int.Parse(id_A.Text));
                    cmd.Parameters.AddWithValue("@Nome_Artistico", nome_A.Text);
                    cmd.Parameters.AddWithValue("@Estilo_Musical", estilo_A.Text);
                    cmd.Parameters.AddWithValue("@Nome_Verdadeiro", nomev_A.Text);
                    cmd.Parameters.AddWithValue("@Idade", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Premios", premios.Text);
                    cmd.Parameters.AddWithValue("@Nacionalidade", nacionalidade.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Artista atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao atualizar o artista: " + ex.Message);
                }
                LoadDataArtista();
            }
            else
            {
                MessageBox.Show("Artista não foi selecionado corretamente...Insira o ID do artista que pretende atualizar.");
            }
        }

        private void del_A_Click(object sender, EventArgs e)
        {
            string ID_Artista = id_A.Text;

            if (ID_Artista != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EliminarArtista", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Artista", int.Parse(id_A.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Artista eliminado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao eliminar o artista: " + ex.Message);
                }
                LoadDataArtista();
            }
            else
            {
                MessageBox.Show("Artista não foi selecionado corretamente...Insira o ID do artista que pretende eliminar.");
            }
        }

        private void clear_A_Click(object sender, EventArgs e)
        {
            id_A.Clear();
            nome_A.Clear();
            estilo_A.Clear();
            nomev_A.Clear();
            textBox5.Clear();
            premios.Clear();
            nacionalidade.Clear();
        }

        private void back_A_Click(object sender, EventArgs e)
        {
            Artista_Load(sender, e);
        }

        private void dados_pesq_A_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesq_A.Text;
            string filtro = dados_pesq_A.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "ID_Artista":
                        dados = "ID_Artista";
                        break;
                    case "Nome_Artistico":
                        dados = "Nome_Artistico";
                        break;
                    case "Estilo_Musical":
                        dados = "Estilo_Musical";
                        break;
                    case "Nome_Verdadeiro":
                        dados = "Nome_Verdadeiro";
                        break;
                    case "Idade":
                        dados = "Idade";
                        break;
                    case "Premios":
                        dados = "Premios";
                        break;
                    case "Nacionalidade":
                        dados = "Nacionalidade";
                        break;
                    default:
                        break;

                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Artista WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView7.DataSource = detailsTable;
                    dataGridView7.Visible = true;
                }

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

        private void clearf_A_Click(object sender, EventArgs e)
        {
            dados_pesq_A.Clear();
            pesq_A.SelectedIndex = -1;
            Artista_Load(sender, e);
        }


        //Passes

        private void Passe_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Passes", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView3.DataSource = detailsTable;
            dataGridView3.Visible = true;

            numero_de_serie.Visible = true;
            duracao_dias.Visible = true;
            preco.Visible = true;
            id_festival_p.Visible = true;
            backPas.Visible = false;
            ClearP.Visible = false;
            AddP.Visible = false;
            DelP.Visible = false;
            button1.Visible = false;

        }

        private void LoadDataPasse()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Passes", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView3.DataSource = detailsTable;
                    dataGridView3.Visible = true;

                    if (dataGridView3.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView3.Rows.Count - 1;
                        dataGridView3.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView3.Rows[lastIndex].Selected = true;
                    }
                }
                numero_de_serie.Visible = true;
                duracao_dias.Visible = true;
                preco.Visible = true;
                id_festival_p.Visible = true;
                backP.Visible = false;
                ClearP.Visible = false;
                AddP.Visible = false;
                DelP.Visible = false;
                button1.Visible = false;
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

        private void PasseTab_Click(object sender, EventArgs e)
        {
            FestivalPanel.Visible = false;
            ArtistasPanel.Visible = false;
            ConcertosPanel.Visible = false;
            PassePanel.Visible = true;
            AcampamentoPanel.Visible = false;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            Passe_Load(sender, e);
        }
        private void PassePanel_Paint(object sender, PaintEventArgs e)
        {
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView3.Rows[e.RowIndex];
                numero_de_serie.Text = row.Cells["Numero_De_Serie"].Value.ToString();
                duracao_dias.Text = row.Cells["Duracao_Dias"].Value.ToString();
                preco.Text = row.Cells["Preco"].Value.ToString();
                id_festival_p.Text = row.Cells["ID_Festival"].Value.ToString();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dados_pesq_passe_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesqPasses.Text;
            string filtro = dados_pesq_passes.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "Número de Série":
                        dados = "Numero_De_Serie";
                        break;
                    case "Duração":
                        dados = "Duracao_Dias";
                        break;
                    case "Preço":
                        dados = "Preco";
                        break;
                    case "ID do Festival":
                        dados = "ID_Festival";
                        break;

                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Passe WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView3.DataSource = detailsTable;
                    dataGridView3.Visible = true;
                }

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

        private void ClearFiltersPasses_Click(object sender, EventArgs e)
        {
            dados_pesq_passes.Clear();
            pesqPasses.SelectedIndex = -1;
            LoadDataPasse();
        }

        private void AddP_Click(object sender, EventArgs e)
        {
            string Numero_de_serie = numero_de_serie.Text;

            if (Numero_de_serie != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarPasse", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_De_Serie", int.Parse(numero_de_serie.Text));
                    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao_dias.Text);
                    cmd.Parameters.AddWithValue("@Preco", preco.Text);
                    cmd.Parameters.AddWithValue("@ID_Festival", id_festival_p.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passe adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o Passe: " + ex.Message);
                }
                LoadDataPasse();
            }
            else
            {
                MessageBox.Show("Passe não foi selecionado corretamente...Insira o Número de Série do Passe que pretende adicionar.");
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string Numero_de_serie = numero_de_serie.Text;

            if (Numero_de_serie != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EliminarPasse", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_De_Serie", int.Parse(numero_de_serie.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passe eliminado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao eliminar o Passe: " + ex.Message);
                }
                LoadDataPasse();
            }
            else
            {
                MessageBox.Show("Passe não foi selecionado corretamente...Insira o Número de Série do Passe que pretende eliminar.");
            }
        }

        private void DelP_Click(object sender, EventArgs e)
        {
            string Numero_de_serie = numero_de_serie.Text;

            if (Numero_de_serie != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EliminarPasse", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_De_Serie", int.Parse(numero_de_serie.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passe eliminado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao eliminar o Passe: " + ex.Message);
                }
                LoadDataPasse();
            }
            else
            {
                MessageBox.Show("Passe não foi selecionado corretamente...Insira o Número de Série do Passe que pretende eliminar.");
            }
        }

        private void EditP_Click(object sender, EventArgs e)
        {
            string Numero_de_serie = numero_de_serie.Text;

            if (Numero_de_serie != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarPasse", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@Numero_De_Serie", int.Parse(numero_de_serie.Text));
                    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao_dias.Text);
                    cmd.Parameters.AddWithValue("@Preco", preco.Text);
                    cmd.Parameters.AddWithValue("@ID_Festival", id_festival_p.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Passe editado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao editar o Passe: " + ex.Message);
                }
                LoadDataPasse();
            }
            else
            {
                MessageBox.Show("Passe não foi selecionado corretamente...Insira o Número de Série do Passe que pretende editar.");
            }
        }

        private void EditarPasse_Click(object sender, EventArgs e)
        {
            numero_de_serie.Visible = true;
            duracao_dias.Visible = true;
            preco.Visible = true;
            id_festival_p.Visible = true;
            AddP.Visible = false;
            ClearP.Visible = true;
            DelP.Visible = false;
            backP.Visible = true;
            EditP.Visible = true;
            backPas.Visible = true;
            button1.Visible = false;
            SeeAcampamento.Visible = false;
        }
        private void AdicionarPasse_Click(object sender, EventArgs e)
        {
            numero_de_serie.Visible = true;
            duracao_dias.Visible = true;
            preco.Visible = true;
            id_festival_p.Visible = true;
            AddP.Visible = true;
            ClearP.Visible = true;
            DelP.Visible = false;
            backPas.Visible = true;
            EditP.Visible = false;
            button1.Visible = false;
            SeeAcampamento.Visible = false;
        }

        private void EliminarPasse_Click(object sender, EventArgs e)
        {
            numero_de_serie.Clear();
            numero_de_serie.Visible = true;
            preco.Visible = false;
            id_festival_p.Visible = false;
            duracao_dias.Visible = false;
            AddP.Visible = false;
            ClearP.Visible = true;
            DelP.Visible = true;
            backP.Visible = true;
            EditP.Visible = false;
            backPas.Visible = true;
            button1.Visible = false;
            SeeAcampamento.Visible = false;

        }
        private void ClearP_Click(object sender, EventArgs e)
        {
            numero_de_serie.Clear();
            duracao_dias.Clear();
            preco.Clear();
            id_festival_p.Clear();
        }

        private void SeeAcampamento_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int Numero_Serie = Convert.ToInt32(dataGridView3.SelectedRows[0].Cells["Numero_De_Serie"].Value.ToString());

                AcampamentosList camps = new AcampamentosList(Numero_Serie);
                camps.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione o passe!");
            }
        }

        private void backP_Click(object sender, EventArgs e)
        {
            LoadDataPasse();
        }
        private void backPas_Click(object sender, EventArgs e)
        {
            Passe_Load(sender, e);
        }

        //Acampamento

        private void Acampamento_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Acampamento", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView4.DataSource = detailsTable;
            dataGridView4.Visible = true;

            backAc.Visible = false;
            EditAc.Visible = false;
            AddAc.Visible = false;
            DelAc.Visible = false;
            ClearAc.Visible = false;
        }
        private void LoadDataAcampamento()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Acampamento", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView4.DataSource = detailsTable;
                    dataGridView4.Visible = true;

                    if (dataGridView4.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView4.Rows.Count - 1;
                        dataGridView4.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView4.Rows[lastIndex].Selected = true;
                    }
                }
                id_acampamento.Visible = true;
                espaco_disponivel.Visible = true;
                nome_acampamento.Visible = true;
                duracao_dias_acampamento.Visible = true;
                localizacao_acamp.Visible = true;
                acomodidades.Visible = true;
                backAc.Visible = false;
                EditAc.Visible = false;
                AddAc.Visible = false;
                DelAc.Visible = false;
                ClearAc.Visible = false;
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
        private void AcampamentoTab_Click(object sender, EventArgs e)
        {
            FestivalPanel.Visible = false;
            ConcertosPanel.Visible = false;
            PassePanel.Visible = false;
            AcampamentoPanel.Visible = true;
            BarracasPanel.Visible = false;
            PalcoPanel.Visible = false;
            ArtistasPanel.Visible = false;
            Acampamento_Load(sender, e);
        }
        private void dados_pesq_acamp_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesquisarBarraca.Text;
            string filtro = dados_pesqBarraca.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "ID_Acampamento":
                        dados = "ID_Acampamento";
                        break;
                    case "Nome":
                        dados = "Nome";
                        break;
                    case "Espaco_Disponivel":
                        dados = "Espaco_Disponivel";
                        break;
                    case "Duracao_Dias":
                        dados = "Duracao_Dias";
                        break;
                    case "Acomodidades":
                        dados = "Acomodidades";
                        break;
                    case "ID_Localizacao":
                        dados = "ID_Localizacao";
                        break;
                    default:
                        break;


                }

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Acampamento WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView4.DataSource = detailsTable;
                    dataGridView4.Visible = true;
                }

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
        private void EditarAcamp_Click(object sender, EventArgs e)
        {

        }
        private void AdicionarAcamp_Click(object sender, EventArgs e)
        {
            id_acampamento.Visible = true;
            nome_acampamento.Visible = true;
            espaco_disponivel.Visible = true;
            duracao_dias_acampamento.Visible = true;
            localizacao_acamp.Visible = true;
            acomodidades.Visible = true;
            AddAc.Visible = true;
            ClearAc.Visible = true;
            DelAc.Visible = false;
            EditAc.Visible = false;
            backAc.Visible = true;
        }
        private void EliminarAcamp_Click(object sender, EventArgs e)
        {
            id_acampamento.Visible = true;
            nome_acampamento.Visible = false;
            espaco_disponivel.Visible = false;
            duracao_dias_acampamento.Visible = false;
            localizacao_acamp.Visible = false;
            acomodidades.Visible = false;
            AddAc.Visible = false;
            ClearAc.Visible = true;
            DelAc.Visible = true;
            EditAc.Visible = false;
            backAc.Visible = true;
        }
        private void backAc_Click(object sender, EventArgs e)
        {
            LoadDataAcampamento();
        }
        private void DelAc_Click(object sender, EventArgs e)
        {
            string ID_Acampamento = id_acampamento.Text;

            if (ID_Acampamento != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EliminarAcampamento", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Acampamento", int.Parse(id_acampamento.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Acampamento eliminado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao eliminar o acampamento: " + ex.Message);
                }
                LoadDataAcampamento();
            }
            else
            {
                MessageBox.Show("Acampamento não foi selecionado corretamente...Insira o ID do Acampamento que pretende eliminar.");
            }
        }
        private void AddAc_Click(Object sender, EventArgs e)
        {
            string ID_Acampamento = id_acampamento.Text;

            if (ID_Acampamento != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarAcampamento", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Acampamento", int.Parse(id_acampamento.Text));
                    cmd.Parameters.AddWithValue("@Nome", nome_acampamento.Text);
                    cmd.Parameters.AddWithValue("@Espaco_Disponivel", espaco_disponivel.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao_dias_acampamento.Text);
                    cmd.Parameters.AddWithValue("@Acomodidades", acomodidades.Text);
                    cmd.Parameters.AddWithValue("@ID_Localizacao", localizacao_acamp.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Acampamento adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o acampamento: " + ex.Message);
                }
                LoadDataAcampamento();
            }
            else
            {
                MessageBox.Show("Acampamento não foi selecionado corretamente...Insira o ID do Acampamento que pretende adicionar.");
            }
        }
        private void ClearAc_Click(object sender, EventArgs e)
        {
            id_acampamento.Clear();
            nome_acampamento.Clear();
            espaco_disponivel.Clear();
            duracao_dias_acampamento.Clear();
            localizacao_acamp.Clear();
            acomodidades.Clear();
        }
        private void EditAc_Click(object sender, EventArgs e)
        {
            string ID_Acampamento = id_acampamento.Text;

            if (ID_Acampamento != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarAcampamento", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Acampamento", int.Parse(id_acampamento.Text));
                    cmd.Parameters.AddWithValue("@Nome", nome_acampamento.Text);
                    cmd.Parameters.AddWithValue("@Espaco_Disponivel", espaco_disponivel.Text);
                    cmd.Parameters.AddWithValue("@Duracao_Dias", duracao_dias_acampamento.Text);
                    cmd.Parameters.AddWithValue("@Acomodidades", acomodidades.Text);
                    cmd.Parameters.AddWithValue("@ID_Localizacao", localizacao_acamp.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Acampamento editado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao editar o acampamento: " + ex.Message);
                }
                LoadDataAcampamento();
            }
            else
            {
                MessageBox.Show("Acampamento não foi selecionado corretamente...Insira o ID do Acampamento que pretende editar.");
            }
        }
        private void AcampamentoPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView4.Rows[e.RowIndex];
                id_acampamento.Text = row.Cells["ID_Acampamento"].Value.ToString();
                nome_acampamento.Text = row.Cells["Nome"].Value.ToString();
                espaco_disponivel.Text = row.Cells["Espaco_Disponivel"].Value.ToString();
                duracao_dias_acampamento.Text = row.Cells["Duracao_Dias"].Value.ToString();
                acomodidades.Text = row.Cells["Acomodidades"].Value.ToString();
                localizacao_acamp.Text = row.Cells["ID_Localizacao"].Value.ToString();
            }
        }

        //Barracas

        private void Barraca_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Barracas", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView5.DataSource = detailsTable;
            dataGridView5.Visible = true;

            Clear_b.Visible = false;
            Add_b.Visible = false;
            Del_b.Visible = false;
            back_b.Visible = false;
            edit_b.Visible = false;
            SeePatroB.Visible = true;

            foreach (DataGridViewColumn column in dataGridView5.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        private void LoadDataBarracas()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Barraca", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView5.DataSource = detailsTable;
                    dataGridView5.Visible = true;

                    if (dataGridView5.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView5.Rows.Count - 1;
                        dataGridView5.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView5.Rows[lastIndex].Selected = true;
                    }
                }
                id_barraca_origin.Visible = true;
                id_patrocinador_barraca.Visible = true;
                tipo_alimentacao.Visible = true;
                edit_b.Visible = false;
                back_b.Visible = false;
                Clear_b.Visible = false;
                Add_b.Visible = false;
                Del_b.Visible = false;
                SeePatroB.Visible = true;

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
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView5.Rows[e.RowIndex];
                id_barraca_origin.Text = row.Cells["ID_Barraca"].Value.ToString();
                tipo_alimentacao.Text = row.Cells["Tipo_de_Alimentacao"].Value.ToString();
                id_patrocinador_barraca.Text = row.Cells["ID_Patrocinador"].Value.ToString();
            }
        }
        private void dados_pesqBarraca_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesquisarBarraca.Text;
            string filtro = dados_pesqBarraca.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "ID_Barraca":
                        dados = "ID_Barraca";
                        break;
                    case "Tipo_de_Alimentacao":
                        dados = "Tipo_de_Alimentacao";
                        break;
                    case "ID_Patrocinador":
                        dados = "ID_Patrocinador";
                        break;


                }

                //comboBox1.Items.Clear();
                //string query = "SELECT * FROM SummerFest.Festival WHERE " + dados + " LIKE @filtro";
                //using (SqlCommand cmd = new SqlCommand(query, CN))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Barraca WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView5.DataSource = detailsTable;
                    dataGridView5.Visible = true;
                }

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
        private void clear_filtersBarraca_Click(object sender, EventArgs e)
        {
            dados_pesqBarraca.Clear();
            pesquisarBarraca.SelectedIndex = -1;
            Barraca_Load(sender, e);
        }
        private void edit_barraca_Click(object sender, EventArgs e)
        {
            id_barraca_origin.Visible = true;
            id_patrocinador_barraca.Visible = true;
            tipo_alimentacao.Visible = true;
            Add_b.Visible = false;
            Clear_b.Visible = true;
            Del_b.Visible = false;
            edit_b.Visible = true;
            back_b.Visible = true;
            SeePatroB.Visible = false;
        }
        private void AddBarraca_Click(object sender, EventArgs e)
        {
            id_barraca_origin.Visible = true;
            tipo_alimentacao.Visible = true;
            id_patrocinador_barraca.Visible = true;

            Add_b.Visible = true;
            Clear_b.Visible = true;
            Del_b.Visible = false;
            edit_b.Visible = false;
            back_b.Visible = true;
            SeePatroB.Visible = false;
        }
        private void DelBarraca_Click(object sender, EventArgs e)
        {
            id_barraca_origin.Visible = false;
            tipo_alimentacao.Visible = false;
            id_patrocinador_barraca.Visible = false;
            Add_b.Visible = false;
            id_barraca_origin.Clear();
            id_barraca_origin.Visible = true;
            Del_b.Visible = true;
            Clear_b.Visible = true;
            edit_b.Visible = false;
            back_b.Visible = true;
            SeePatroB.Visible = false;
        }
        private void edit_b_Click(object sender, EventArgs e)
        {
            string ID_Barraca = id_barraca_origin.Text;

            if (ID_Barraca != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarBarraca", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Barraca", int.Parse(ID_Barraca));
                    cmd.Parameters.AddWithValue("@Tipo_de_Alimentacao", tipo_alimentacao.Text);
                    cmd.Parameters.AddWithValue("@ID_Patrocinador", int.Parse(id_patrocinador_barraca.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Barraca atualizado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao atualizar a barraca: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
                LoadDataBarracas();
            }
            else
            {
                MessageBox.Show("Barraca não foi selecionado corretamente...Insira o ID da barraca que pretende editar.");
            }
        }
        private void BarracasPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void tipo_alimentacao_TextChanged(object sender, EventArgs e)
        {

        }
        private void id_patrocinador_barraca_TextChanged(object sender, EventArgs e)
        {

        }
        private void id_barraca_origin_TextChanged(object sender, EventArgs e)
        {

        }
        private void Add_b_Click(object sender, EventArgs e)
        {
            string ID_Barraca = id_barraca_origin.Text;

            if (ID_Barraca != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarBarraca", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Barraca", int.Parse(id_barraca_origin.Text));
                    cmd.Parameters.AddWithValue("@Tipo_de_Alimentacao", tipo_alimentacao.Text);
                    cmd.Parameters.AddWithValue("@ID_Patrocinador", int.Parse(id_patrocinador_barraca.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Barraca adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o Barraca: " + ex.Message);
                }
                LoadDataFest();
            }
            else
            {
                MessageBox.Show("Barraca não foi selecionado corretamente...Insira o ID do barraca que pretende eliminar.");
            }
        }
        private void Clear_b_Click(object sender, EventArgs e)
        {
            id_barraca_origin.Clear();
            id_patrocinador_barraca.Clear();
            tipo_alimentacao.Clear();

        }
        private void ClearBarraca_Click(object sender, EventArgs e)
        {
            id_barraca_origin.Visible = false;
            tipo_alimentacao.Visible = false;
            id_patrocinador_barraca.Visible = false;
        }
        private void Del_b_Click(object sender, EventArgs e)
        {
            string ID_Barraca = id_barraca_origin.Text;

            if (ID_Barraca != "")
            {
                try
                {
                    if (CN.State == ConnectionState.Closed)
                        CN.Open();

                    using (SqlCommand cmd = new SqlCommand("SummerFest.EliminarBarraca", CN))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Barraca", ID_Barraca);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Barraca excluído com sucesso");
                    LoadDataBarracas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao excluir o Barraca: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
            }
            else
            {
                MessageBox.Show("Barraca não foi selecionado corretamente...Insira o ID do barraca que pretende eliminar.");
            }
            LoadDataBarracas();
        }
        private void back_b_Click(object sender, EventArgs e)
        {
            Barraca_Load(sender, e);
        }
        private void pesquisarBarraca_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void BarracasTab_Click(object sender, EventArgs e)
        {
            Barraca_Load(sender, e);
            FestivalPanel.Visible = false;
            PalcoPanel.Visible = false;
            ArtistasPanel.Visible = false;
            BarracasPanel.Visible = true;
            PassePanel.Visible = false;
            AcampamentoPanel.Visible = false;
            PalcoPanel.Visible = false;

        }

        private void SeePatroB_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                int ID_Patrocinador = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["ID_Patrocinador"].Value.ToString());

                PatrocinadoresBList patroB = new PatrocinadoresBList(ID_Patrocinador);
                patroB.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione a barraca!");
            }
        }

        //Palco
        private void Palco_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Palco", CN);

            DataTable detailsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            sqlDataAdapter.Fill(detailsTable);
            dataGridView6.DataSource = detailsTable;
            dataGridView6.Visible = true;

            Clear.Visible = false;
            Add.Visible = false;
            Del.Visible = false;
            back.Visible = false;
            Edit.Visible = false;
            edit_p.Visible = false;
            seePatroP.Visible = true;

            foreach (DataGridViewColumn column in dataGridView6.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        private void LoadDataPalco()
        {
            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Palco", CN))
                {
                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView6.DataSource = detailsTable;
                    dataGridView6.Visible = true;

                    if (dataGridView6.Rows.Count > 0)
                    {
                        int lastIndex = dataGridView6.Rows.Count - 1;
                        dataGridView6.FirstDisplayedScrollingRowIndex = lastIndex;
                        dataGridView6.Rows[lastIndex].Selected = true;
                    }
                }

                id_palco_origin.Visible = true;
                id_patrocinador_palco.Visible = true;
                nome_palco.Visible = true;
                edit_p.Visible = false;
                back_p.Visible = false;
                Clear_p.Visible = false;
                Add_p.Visible = false;
                Del_p.Visible = false;
                seePatroP.Visible = true;
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
        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PalcoTab_Click(object sender, EventArgs e)
        {
            Palco_Load(sender, e);
            FestivalPanel.Visible = false;
            PalcoPanel.Visible = true;
            ConcertosPanel.Visible = false;
            PassePanel.Visible = false;
            BarracasPanel.Visible = false;
            AcampamentoPanel.Visible = false;
        }
        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                id_palco_origin.Text = row.Cells["ID_Palco"].Value.ToString();
                nome_palco.Text = row.Cells["Nome"].Value.ToString();
                id_patrocinador_palco.Text = row.Cells["Patrocinador"].Value.ToString();
            }
        }
        private void pesquisarPalco_SelectedIndexChanged(object sender, EventArgs e) { }
        private void dados_pesqPalco_TextChanged(object sender, EventArgs e)
        {
            string criterio = pesquisarPalco.Text;
            string filtro = dados_pesqPalco.Text;
            string dados = "";

            try
            {
                if (CN.State == ConnectionState.Closed)
                    CN.Open();

                switch (criterio)
                {
                    case "ID_Palco":
                        dados = "ID_Palco";
                        break;
                    case "Nome":
                        dados = "Nome";
                        break;
                    case "Patrocinador":
                        dados = "Patrocinador";
                        break;


                }

                //comboBox1.Items.Clear();
                //string query = "SELECT * FROM SummerFest.Festival WHERE " + dados + " LIKE @filtro";
                //using (SqlCommand cmd = new SqlCommand(query, CN))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM SummerFest.Palco WHERE " + dados + " LIKE '%' + @filtro + '%'", CN))
                {
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    DataTable detailsTable = new DataTable();
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                    sqlDataAdapter.Fill(detailsTable);
                    dataGridView6.DataSource = detailsTable;
                    dataGridView6.Visible = true;
                }

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
        private void clear_filtersPalco_Click(object sender, EventArgs e)
        {
            dados_pesqPalco.Clear();
            pesquisarPalco.SelectedIndex = -1;
            Palco_Load(sender, e);
        }
        private void edit_palco_Click(object sender, EventArgs e)
        {
            id_palco_origin.Visible = true;
            id_patrocinador_palco.Visible = true;
            nome_palco.Visible = true;
            Add_p.Visible = false;
            Clear_p.Visible = true;
            Del_p.Visible = false;
            edit_p.Visible = true;
            back_p.Visible = true;
            seePatroP.Visible = false;
        }
        private void AddPalco_Click(object sender, EventArgs e)
        {
            id_palco_origin.Visible = true;
            nome_palco.Visible = true;
            id_patrocinador_palco.Visible = true;

            Add_p.Visible = true;
            Clear_p.Visible = true;
            Del_p.Visible = false;
            edit_p.Visible = false;
            back_p.Visible = true;
            seePatroP.Visible = false;
        }
        private void DelPalco_Click(object sender, EventArgs e)
        {
            id_palco_origin.Visible = false;
            nome_palco.Visible = false;
            id_patrocinador_palco.Visible = false;
            Add_p.Visible = false;
            id_palco_origin.Clear();
            id_palco_origin.Visible = true;
            Del_p.Visible = true;
            Clear_p.Visible = true;
            edit_p.Visible = false;
            back_p.Visible = true;
            seePatroP.Visible = false;
        }
        private void edit_p_Click(object sender, EventArgs e)
        {
            string ID_Palco = id_palco_origin.Text;

            if (ID_Palco != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.EditarPalco", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Palco", int.Parse(ID_Palco));
                    cmd.Parameters.AddWithValue("@Nome", nome_palco.Text);
                    cmd.Parameters.AddWithValue("@Patrocinador", int.Parse(id_patrocinador_palco.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Palco atualizado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao atualizar o palco: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
                LoadDataPalco();
            }
            else
            {
                MessageBox.Show("Palco não foi selecionado corretamente...Insira o ID do palco que pretende editar.");
            }
        }
        private void PalcoPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void nome_palco_TextChanged(object sender, EventArgs e)
        {

        }
        private void id_patrocinador_palco_TextChanged(object sender, EventArgs e)
        {

        }
        private void Add_p_Click(object sender, EventArgs e)
        {
            string ID_Palco = id_palco_origin.Text;

            if (ID_Palco != "")
            {
                try
                {
                    CN.Open();
                    SqlCommand cmd = new SqlCommand("SummerFest.AdicionarPalco", CN);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters with values from the text boxes
                    cmd.Parameters.AddWithValue("@ID_Palco", int.Parse(id_palco_origin.Text));
                    cmd.Parameters.AddWithValue("@Nome", nome_palco.Text);
                    cmd.Parameters.AddWithValue("@Patrocinador", int.Parse(id_patrocinador_palco.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Palco adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao adicionar o Palco: " + ex.Message);
                }
                LoadDataFest();
            }
            else
            {
                MessageBox.Show("Palco não foi selecionado corretamente...Insira o ID do palco que pretende eliminar.");
            }
        }
        private void Clear_p_Click(object sender, EventArgs e)
        {
            id_palco_origin.Clear();
            id_patrocinador_palco.Clear();
            nome_palco.Clear();

        }
        private void Del_p_Click(object sender, EventArgs e)
        {
            string ID_Palco = id_palco_origin.Text;

            if (ID_Palco != "")
            {
                try
                {
                    if (CN.State == ConnectionState.Closed)
                        CN.Open();

                    using (SqlCommand cmd = new SqlCommand("SummerFest.EliminarPalco", CN))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Palco", ID_Palco);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Palco excluído com sucesso");
                    LoadDataFest();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocorreu um erro ao excluir o palco: " + ex.Message);
                }
                finally
                {
                    if (CN.State == ConnectionState.Open)
                        CN.Close();
                }
            }
            else
            {
                MessageBox.Show("Palco não foi selecionado corretamente...Insira o ID do palco que pretende eliminar.");
            }
            LoadDataFest();
        }
        private void back_p_Click(object sender, EventArgs e)
        {
            LoadDataPalco();
        }
        private void id_palco_origin_TextChanged(object sender, EventArgs e)
        {

        }
        private void seePatroP_Click_1(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                int ID_Patrocinador = Convert.ToInt32(dataGridView6.SelectedRows[0].Cells["ID_Patrocinador"].Value.ToString());

                PatrocinadoresPList patroP = new PatrocinadoresPList(ID_Patrocinador);
                patroP.ShowDialog();
            }
            else
            {
                MessageBox.Show("selecione a barraca!");
            }
        }

        private void AcampamentoPanel_Paint_1(object sender, PaintEventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        private void Inicial_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Localizacao localizacao = new Localizacao();
            localizacao.Show(this);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}