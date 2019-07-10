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
    public class AlunoRepositorio
    {
        Conexão conexão = new Conexão();

        public int Inserir(Aluno aluno)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"INSERT INTO alunos(nome, cpf,nota_1,nota_2,nota_3)VALUES(@NOME, @CPF, @NOTA_1, @NOTA_2, @NOTA_3)";

            comando.Parameters.AddWithValue("@NOME", aluno.Nome);
            comando.Parameters.AddWithValue("@CPF", aluno.Cpf);
            comando.Parameters.AddWithValue("@NOTA_1", aluno.Nota1);
            comando.Parameters.AddWithValue("@NOTA_2", aluno.Nota2);
            comando.Parameters.AddWithValue("@NOTA_3", aluno.Nota3);

            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();

            return id;
        }

        public List<Aluno> ObterTodos(string busca)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"SELECT * FROM alunos WHERE nome LIKE @BUSCA";
            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            List<Aluno> alunos = new List<Aluno>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            comando.Connection.Close();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Aluno aluno = new Aluno();

                aluno.Id = Convert.ToInt32(linha["Id"]);
                aluno.Nome = linha["Nome"].ToString();
                aluno.Cpf = linha["Cpf"].ToString();
                aluno.Nota1 = Convert.ToDecimal(linha["Nota_1"]);
                aluno.Nota2 = Convert.ToDecimal(linha["Nota_2"]);
                aluno.Nota3 = Convert.ToDecimal(linha["Nota_3"]);
                alunos.Add(aluno);

            }
            return alunos;

        }

        public Aluno ObterPeloId(int id)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"SELECT * FROM alunos WHERE id = @ID";

            comando.Parameters.AddWithValue("@ID", id);

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            if (tabela.Rows.Count == 1)
            {
                DataRow linha = tabela.Rows[0];
                Aluno aluno = new Aluno();
                aluno.Id = Convert.ToInt32(linha["id"]);
                aluno.Nome = linha["Nome"].ToString();
                aluno.Cpf = linha["Cpf"].ToString();
                aluno.Nota1 = Convert.ToDecimal(linha["Nota_1"]);
                aluno.Nota2 = Convert.ToDecimal(linha["Nota_2"]);
                aluno.Nota3 = Convert.ToDecimal(linha["Nota_3"]);

                return aluno;
            }
            return null;

        }

        public bool Atualizar(Aluno aluno)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"UPDATE alunos SET nome = @NOME, cpf = @CPF, nota_1 = @NOTA_1, nota_2 = @NOTA_2, nota_3 = @NOTA_3 WHERE id = @ID";

            comando.Parameters.AddWithValue("@NOME", aluno.Nome);
            comando.Parameters.AddWithValue("@CPF", aluno.Cpf);
            comando.Parameters.AddWithValue("@NOTA_1", aluno.Nota1);
            comando.Parameters.AddWithValue("@NOTA_2", aluno.Nota2);
            comando.Parameters.AddWithValue("@NOTA_3", aluno.Nota3);
            comando.Parameters.AddWithValue("@ID", aluno.Id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeAfetada == 1;

        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexão.Conectar();
            comando.CommandText = @"DELETE FROM alunos WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);

            int quantidadeAfetada = comando.ExecuteNonQuery();
            comando.Connection.Close();

            return quantidadeAfetada == 1;
        }
    }
}
