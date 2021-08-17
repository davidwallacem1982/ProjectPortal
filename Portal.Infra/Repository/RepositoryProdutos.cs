using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Portal.Infra.Repository
{
    public class RepositoryProdutos : Repository<Produtos>, IRepositoryProdutos
    {
        public RepositoryProdutos(DbContext context) : base(context) { }
        public Produtos AddVerificarExistePorDescricao(Produtos produto)
        {
            var check = VerificaSeExistePorclienteNomePrincipal(produto);

            if (!check)
            {
                Add(produto);
            }
            else //Verificar se o ideal aqui não é dar um exception tipo (Já existe um produto com esse nome)
            {
                produto.Notifications.Add(
                    new Core.Notifications.Notifica
                    {
                        NomePropriedade = "NomePrincipal",
                        Mensagem = "Já Existe um clinte com esse NomePrincipal."
                    });
            }
            return produto;
        }

        protected bool VerificaSeExistePorclienteNomePrincipal(Produtos produto)
        {
            bool ret = false;
            var items = FindBy(p => p.Descricao.ToUpper() == produto.Descricao.ToUpper());
            produto.Descricao = produto.Descricao.ToUpper();
            if (items is null) ret = true;

            return ret;
        }

        public void AddProcedure(Produtos Produto)
        {
            //var param = new SqlParameter("@NomeProduto", Produto.Nome);
            //var param2 = new SqlParameter("@Preco", Produto.Preco);
            //db.Database.ExecuteSqlCommand("SalvarProduto @NomeProduto , @Preco", param, param2);
            //db.SaveChanges();
        }

        public void DeleteProcedure(Produtos Produto)
        {
            //var param = new SqlParameter("@Id", Produto.Id);
            //db.Database.ExecuteSqlCommand("ExcluirProduto @Id", param);
            //db.SaveChanges();
        }

        public List<Produtos> ListProcedure()
        {

            //return db.Produtos.FromSql("ListarTodos").ToList();
            throw new NotImplementedException();
        }

        public Produtos SelectIdProcedure(long Id)
        {
            //var param = new SqlParameter("@Id", Id);
            //return db.Produtos.FromSql("ObterPorId @Id", param).FirstOrDefault();
            throw new NotImplementedException();
        }

        public void UpdateProcedure(Produtos Produto)
        {
            //var param = new SqlParameter("@NomeProduto", Produto.Nome);
            //var param2 = new SqlParameter("@Preco", Produto.Preco);
            //var param3 = new SqlParameter("@Id", Produto.Id);
            //db.Database.ExecuteSqlCommand("AtualizarProduto @NomeProduto,@Preco, @Id", param, param2, param3);
            //db.SaveChanges();
            throw new NotImplementedException();
        }
    }
}
