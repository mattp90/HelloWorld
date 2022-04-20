using AquardensNUnitTest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace wsListini
{
    public class iElenchi002 : Base001
    {
        protected iElenchi_002Client ElenchiClient = new iElenchi_002Client();

        [Test]
        public async Task GetCategorie()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            var esito = await ElenchiClient.GetCategorieAsync(baseRequest, new dcCategoriaFilter()
            {
                Tipo = 1
            });
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
        
        [Test]
        public async Task GetListiniCompatibiliRinnovo()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            var esito = await ElenchiClient.GetListiniCompatibiliRinnovoAsync(baseRequest, 1, "");
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            ElenchiClient.Close();
        }
    }
}