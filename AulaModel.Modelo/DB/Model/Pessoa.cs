using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaModel.Modelo.DB.Model
{
    public class Pessoa
    {
        public static List<Pessoa> Pessoas = new List<Pessoa>();
        // todas tem que ter o virtual , para poder ser sobreesrito
        public virtual Guid Id { get; set;}
        public virtual String Nome { get; set; }
        public virtual DateTime DtNascimento { get; set; }
        public virtual String Email { get; set; }
        public virtual String Telefone { get; set; }
    }
    //ClassMapping bibioteca que serve para mapear o banco de dados
    public class PessoaMap : ClassMapping<Pessoa>
    {
        public PessoaMap()
        {
            //para usar tabela que ja existe
            //Table("tblPessoa");

            Id(x => x.Id, m=> m.Generator(Generators.Guid));

            Property(x => x.Id);
            Property(x => x.DtNascimento);
            Property(x => x.Telefone);
            Property(x => x.Email);
        }
    }
}
