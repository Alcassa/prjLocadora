using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLocadora
{
    public partial class frmFilmes : Form
    {
        int registrosAtual = 0;
        int totalRegistros = 0;
        String connectionString = @"Server=darnassus\motorhead;Database=db_230910;User Id=230910;Password=12345678";
        bool novo;
        DataTable dtFilmes = new DataTable();
        DataTable dtProdutoras = new DataTable();

        public frmFilmes()
        {
            InitializeComponent();
        }
        private void navegar()
        {
            carregarComboProdutoras();
            txtCodFilme.Text = dtFilmes.Rows[registrosAtual][0].ToString();
            txtTituloFilme.Text = dtFilmes.Rows[registrosAtual][1].ToString();
            txtAnoFilme.Text = dtFilmes.Rows[registrosAtual][2].ToString();
            //cbbProdutora.Text = dtFilmes.Rows[registrosAtual][3].ToString();
            cbbGenero.Text = dtFilmes.Rows[registrosAtual][4].ToString();
        }

        private void carregarComboProdutoras()
        {
            dtProdutoras = new DataTable();
            string sql = $"SELECT *FROM tblProdutora where codProd="+dtFilmes.Rows[registrosAtual][3].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilmes.Load(reader);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            finally { con.Close(); }
        
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        private void frmFilmes_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodFilme.Enabled = false;
            txtTituloFilme.Enabled = false;
            txtAnoFilme.Enabled = false;
            cbbProdutora.Enabled = false;
            cbbGenero.Enabled = false;
            string sql = "SELECT *FROM tblFilme ";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilmes.Load(reader);
                    totalRegistros = dtFilmes.Rows.Count;
                    registrosAtual = 0;
                    navegar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally { con.Close(); }
            cbbProdutora.DataSource = dtProdutoras;
            cbbProdutora.DisplayMember = "nomeProd";
            cbbProdutora.ValueMember = "codProd";
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (registrosAtual < totalRegistros - 1) {
                registrosAtual++;
                navegar();
            }
        }
    }
}
