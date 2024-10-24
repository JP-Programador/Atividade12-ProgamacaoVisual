using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Atividade12
{
    public partial class FormCadastros : Form
    {
        private int? productId = null;
        Database db = new Database();

        public FormCadastros()
        {
            InitializeComponent();
        }

        public FormCadastros(int id, string nome, decimal preco)
        {
            InitializeComponent();
            productId = id;
            txtNome.Text = nome;
            txtPreco.Text = preco.ToString();
        }

        private void FormCadastros_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("O nome do produto é obrigatório!", "Erro de validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal preco;
            if (!decimal.TryParse(txtPreco.Text, out preco) || preco <= 0)
            {
                MessageBox.Show("O preço deve ser um número válido e maior que zero!", "Erro de validação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            // Abrir a conexão com o banco de dados
            db.OpenConnection();
            string query;

            if (productId == null) // Inserção
            {
                query = "INSERT INTO Produto (Nome, Preco) VALUES (@nome, @preco)";
            }
            else // Atualização
            {
                query = "UPDATE Produto SET Nome = @nome, Preco = @preco WHERE Id = @id";
            }

            MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@preco", preco);

            if (productId != null)
            {
                cmd.Parameters.AddWithValue("@id", productId);
            }

            // Executar a query
            cmd.ExecuteNonQuery();

            // Fechar a conexão com o banco de dados
            db.CloseConnection();

            // Fecha o formulário com sucesso
            DialogResult = DialogResult.OK;
        }
    }
}
