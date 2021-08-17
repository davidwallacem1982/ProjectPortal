using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Infra.Configuration;
using Portal.Infra.Repository;
using Xunit;
using Xunit.Abstractions;

namespace Portal.Testes_Repository
{
    public class Testes_Repository
    {
        private readonly ITestOutputHelper output;
        private readonly DbContextOptions<Context> options;
        private readonly Context context;
        private readonly Repository<Ufs> db;

        public Testes_Repository(ITestOutputHelper output)
        {
            this.output = output;
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "dbTest");
            options = builder.Options;
            context = new Context(options);
            db = new Repository<Ufs>(context);
        }

        [Fact]
        public void Should_Create_New_Context()
        {
            output.WriteLine("Não deve retorna nulo");
            //assert
            Assert.NotNull(context);
        }

        [Fact]
        public void Should_Create_New_Repository()
        {
            output.WriteLine("Não deve retorna nulo");
            //assert
            Assert.NotNull(db);
        }

        [Fact]
        public void Should_Exec_CRUD()
        {
            var uf = new Ufs
            {
                ID = 1,
                IBGE = 123,
                UF = "SP"
            };

            //act CREATE
            uf = db.Add(uf);
            var count = db.SaveChanges();
            output.WriteLine($"Registro incluído com UF = SP");

            //assert CREATE
            Assert.True(uf.ID != 0);
            output.WriteLine($"Item Cadastrado - OK!");
            Assert.Equal(1, count);
            output.WriteLine($"Contou se a 1 Item Cadastrado - OK!");
            //act READ
            var readUF = db.FindById(uf.ID);


            // arrange READ_ALL
            var uf2 = new Ufs
            {
                ID = 0,
                IBGE = 456,
                UF = "RJ"
            };
            db.Add(uf2);
            db.SaveChanges();
            output.WriteLine($"Registro incluído com UF = RJ");

            //act READ_ALL
            var ufs = db.ToList();
            output.WriteLine($"Leitura de todos registro incluído até o momento");

            //assert
            Assert.Equal(2, ufs.Count);
            output.WriteLine($"Verificou se esse a mais de 1 Item Cadastrado - OK!");

            //act UPDATE
            uf.UF = "AM";
            db.SaveChanges();
            output.WriteLine($"UF alterado de SP para AM");

            uf = db.FindBy(o => o.UF == "AM");
            output.WriteLine($"Leitura do UF = AM");

            //assert UPDATE
            Assert.NotNull(uf);
            output.WriteLine($"Verificou se o UF = SP alterado para  UF = AM - OK!");

            //act DELETE
            db.Remove(uf);
            db.SaveChanges();
            output.WriteLine($"Exclusão do UF = AM");
            ufs = db.ToList();

            //assert DELETE
            Assert.Single(ufs);
            output.WriteLine($"Contou se a 1 Item Cadastrado novamente para confirmar exclusão do item - OK!");
        }
    }
}
