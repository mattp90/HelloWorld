using NUnit.Framework;
using AquardensNUnitTest;
using System.Threading.Tasks;

namespace wsListini
{
    public class Elenchi : Base
    {
        protected iElenchiClient Client = new iElenchiClient();

        [Test]
        public async Task GetCategorie()
        {
            var esito = await Client.GetCategorieAsync(SessionId, Impianto, 2);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task GetListini()
        {
            var esito = await Client.GetListiniAsync(SessionId, Impianto);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task GetListiniFilterd()
        {
            var esito = await Client.GetListiniFilteredAsync(SessionId, Impianto, new dcListinoFilter()
            {
                TipiListini = new int[] { 5 }
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            Client.Close();
        }
    }
    
    public class Gestione : Base
    {
        protected iGestioneClient Client = new iGestioneClient();
        
        [Test]
        public async Task  AddListini()
        {
            var esito = await Client.AddListiniAsync(SessionId, Impianto, new[] { new dcListino() });
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            Client.Close();
        }
    }
}