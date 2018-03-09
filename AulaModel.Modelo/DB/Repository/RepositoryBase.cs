using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaModel.Modelo.DB.Repository
{
    public class RepositoryBase<T> where T : class
    {
        protected ISession Session { get; set; }

        //iniciar sessao
        public RepositoryBase(ISession session)
        {
            Session = session;
        }

        //Deletar
        public void Delete(T entity)
        {
            try
            {
                Session.Clear();

                var transacao = Session.BeginTransaction();

                Session.Delete(entity);

                transacao.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception("Não deu para excluir", ex);
            }
        }

        //salvar e update
        public T SaveOrUpdate(T entity)
        {
            try
            {
                Session.Clear();

                //abrir sessão
                var transacao = Session.BeginTransaction();

                Session.SaveOrUpdate(entity);

                transacao.Commit();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Não deu para excluir", ex);
            }
        }

        //Buscar todos
        public IList<T> FindAll()
        {
            try
            {
                return Session.CreateCriteria(typeof(T)).List<T>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não achei todos", ex);
            }
        }

        //buscar por id
        public T FindById( Guid id)
        {
            try
            {
                return Session.Get<T>(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Não achei todos", ex);
            }
        }
    }
}
