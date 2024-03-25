

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace prjLocadora
{
    public partial class frmProdutoras : Form
    {
        int registrosAtual = 0;
        int totalRegistros = 0;
        DataTable dtProdutora = new DataTable();
        String connectionString = @"Server=darnassus\motorhead;Database=db_230910;User Id=230910;Password=12345678";
        bool novo;
        public frmProdutoras()
        {
            InitializeComponent();
        }
        
        private void navegar()
        {
            txtCodProd.Text = dtProdutora.Rows[registrosAtual][0].ToString();
            txtProd.Text = dtProdutora.Rows[registrosAtual][1].ToString();
            txtTelProd.Text = dtProdutora.Rows[registrosAtual][2].ToString();
            txtEmailProd.Text = dtProdutora.Rows[registrosAtual][3].ToString();
        }
        private void carregar()
        {
            dtProdutora = new DataTable();
            string sql = "SELECT *FROM tblProdutora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutora.Load(reader);
                    totalRegistros = dtProdutora.Rows.Count;
                    registrosAtual = 0;
                    navegar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally { con.Close(); }

        }
        private void frmProdutoras_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodProd.Enabled = false;
            txtEmailProd.Enabled = false;
            txtProd.Enabled = false;
            txtTelProd.Enabled = false;
            string sql = "SELECT *FROM tblProdutora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutora.Load(reader);
                    totalRegistros = dtProdutora.Rows.Count;
                    registrosAtual = 0;
                    navegar();
                }
                    
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally { con.Close(); }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtCodProd.Text = "";
            txtEmailProd.Text = "";
            txtProd.Text = "";
            txtTelProd.Text = "";
            txtProd.Enabled = true;
            txtTelProd.Enabled = true;
            txtEmailProd.Enabled = true;
            btnPrimeiro.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            novo = true;
            txtProd.Focus();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO tblProdutora(nomeProd,telProd,emailProd)  " +
                    $"VALUES('{txtProd.Text}', '{txtProd.Text}' ,'{txtEmailProd.Text}')";
                // MessageBox.Show(sql);
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("cadastrda com sucesso");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Erro: "+ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            else
            {
                string sql = $"UPDATE tblProdutora SET nomeProd='{txtProd.Text}',emailProd='{txtEmailProd.Text}',telProd='{txtTelProd.Text}' WHERE codProd={txtCodProd.Text}";
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Produtora alterada com sucesso!!!");
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show("Erro :" + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            btnPrimeiro.Enabled = true;
            btnProximo.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            carregar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM tblProdutora WHERE" +
                $" codProd={txtCodProd.Text}";
            var con = new SqlConnection(connectionString);
            var cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Excluido com sucesso");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.ToString());
            }
            finally { con.Close(); }
            carregar();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            txtProd.Enabled = true;
            txtEmailProd.Enabled = true;
            txtTelProd.Enabled = true;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnUltimo.Enabled = false;
            
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if(registrosAtual<totalRegistros - 1)
            {
                registrosAtual++;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual--;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual = 0;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registrosAtual<totalRegistros-1 )
            {
                registrosAtual = totalRegistros - 1;
                navegar();
            }
        }
    }
}
