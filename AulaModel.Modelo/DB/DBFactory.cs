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
                var psw = "aluno";

                var stringConexao = "Persist Security Info=False;" + 
                    "server=" + server + 
                    ";port=" + port + 
                    ";database=" + dbName + 
                    ";uid=" + user +
                    ";pwd=" + psw;

                try
                {

                }
                catch
                {

                }

                ConfigurarNHibernate(stringConexao);
            } catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar", ex);
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
