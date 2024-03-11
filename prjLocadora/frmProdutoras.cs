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
        String connectonString = @"Server=darnassus\motorhead;Database=db_230910;User Id=230910;Password=12345678";
        bool novo;
        public frmProdutoras()
        {
            InitializeComponent();
        }

        private void frmProdutoras_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodProd.Enabled = false;
            txtEmailProd.Enabled = false;
            txtProd.Enabled = false;
            txtTelProd.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
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
                var con = new SqlConnection(connectonString);
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
        }
    }
}
