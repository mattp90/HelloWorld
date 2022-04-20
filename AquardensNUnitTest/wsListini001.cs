using AquardensNUnitTest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace wsListini
{
    public class iElenchi001 : Base001
    {
        protected iElenchi_001Client ElenchiClient = new iElenchi_001Client();

        [Test]
        public async Task GetCategorie()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            var esito = await ElenchiClient.GetCategorieAsync(baseRequest, 1);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task GetListini()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            var esito = await ElenchiClient.GetListiniAsync(baseRequest, new dcListinoFilter());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            ElenchiClient.Close();
        }
    }
    
    public class Gestione001 : Base001
    {
        protected iGestione_001Client GestioneClient = new iGestione_001Client();

        [Test]
        public async Task SetListino()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            var esito = await GestioneClient.SetListinoAsync(baseRequest, new dcListinoCompleto(), true);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            GestioneClient.Close();
        }
    }
}