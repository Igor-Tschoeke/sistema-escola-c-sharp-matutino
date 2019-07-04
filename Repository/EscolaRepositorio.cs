using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EscolaRepositorio
    {
        Conexão conexão = new Conexão();

        public int Inserir(Escola escola)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"INSERT INTO(nome)VALUES(@NOME)";
            comando.Parameters.AddWithValue("@NOME", escola.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();

            return id;
        }

        public List<Escola> ObterTodos(string busca)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"SELECT * FROM escolas WHERE nome LIKE @BUSCA";
            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            List<Escola> escola = new List<Escola>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            comando.Connection.Close();

        }
    }
}
