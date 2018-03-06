using MySql.Data.MySqlClient;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaModel.Modelo.DB
{
    public class DbFactory
    {
        private static DbFactory _instance = null;

        private DbFactory()
        {
            Conexao();
        }

        public static DbFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbFactory();
                }

                return _instance;
            }
        }

        private void Conexao()
        {
            try
            {
                var server = "localhost";
                var port = "3306";
                var dbName = "db_agenda";
                var user = "root";
                var psw = "247845";

                var stringConexao = "Persist Security Info=False;" + 
                    "server=" + server + 
                    ";port=" + port + 
                    ";database=" + dbName + 
                    ";uid=" + user +
                    ";pwd=" + psw;

                try
                {
                    //e o caminho para conexar com sql
                    var mysql = new MySqlConnection(stringConexao);
                    // tenta conectar com a tabela do banco
                    mysql.Open();

                    if(mysql.State == System.Data.ConnectionState.Open)
                    {
                        mysql.Close();
                    }
                }
                catch
                {
                    CriarSchema(server, port, dbName, psw, user);
                }

                ConfigurarNHibernate(stringConexao);

            } catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar", ex);
            }
        }

        private void CriarSchema(string server, string port, string dbName, string psw, string user)
        {
            try
            {
                var stringConexao = "server=" + server +
                    ";user=" + user +
                    ";port=" + port +
                    ";password=" + psw + ";";

                var mySql = new MySqlConnection(stringConexao);
                var cmd = mySql.CreateCommand();

                mySql.Open();
                cmd.CommandText = " CREATE DATABASE IF NOT EXISTS `" + dbName + "`;";
                cmd.ExecuteNonQuery();
                mySql.Close();

            }catch(Exception ex)
            {
                throw new Exception("Não deu para criar o Schema", ex);
            }
        }

        private void ConfigurarNHibernate(String stringConexao){

            try
            {
                var config = new Configuration();

                //Configuração do NH com MSQL
                config.DataBaseIntegration(i =>
                {
                    //Dialeto do Banco
                    i.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //Coneção string
                    i.ConnectionString = stringConexao;
                    //Drive de conexão com o banco
                    i.Driver<NHibernate.Driver.MySqlDataDriver>();
                    //Provesor de Conexão do MySQl
                    i.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    //Gera Log dos SQL Executados no console
                    i.LogSqlInConsole = true;
                    //desconetar caso queira visualizar o log de sql formatado no console
                    i.LogFormattedSql = true;
                    //Cria o Schema do banco de dados sempre que a configuration for utilizada
                    i.SchemaAction = SchemaAutoAction.Update;
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel confegurar NH", ex);
            }
        }
    }
}
