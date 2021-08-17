using Portal.Core.Entities;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryProdutos : IRepository<Produtos>
    {
        void AddProcedure(Produtos Produto);
        void UpdateProcedure(Produtos Produto);
        void DeleteProcedure(Produtos Produto);
        Produtos SelectIdProcedure(long Id);
        List<Produtos> ListProcedure();
        // customizados 
        Produtos AddVerificarExistePorDescricao(Produtos Produto);
    }
}
