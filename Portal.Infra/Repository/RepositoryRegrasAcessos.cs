using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryRegrasAcessos : Repository<RegrasAcessos>, IRepositoryRegrasAcessos
    {
        public RepositoryRegrasAcessos(DbContext context) : base(context) { }

        public RolesViewModel ActionByAction(string controle, string Methods, string HttpMethodsID) =>
                Items.Where(s => s.Controllers.Controller == controle && s.Methods.Method == Methods && s.HttpMethods.HttpMethod == HttpMethodsID)
                     .Include(s => s.HttpMethods)
                     .Include(s => s.Methods)
                     .Include(s => s.Controllers)
                     .Select(sin => new RolesViewModel
                     {
                         Value = sin.HttpMethods.Id.ToString(),
                         Action = sin.Controllers.Controller + " --> " + sin.Methods.Method + "(" + sin.HttpMethods.HttpMethod + ")  " + sin.HttpMethods.Descricao
                     }).FirstOrDefault();
    }
}
