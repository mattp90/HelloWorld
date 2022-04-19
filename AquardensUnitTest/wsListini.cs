using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AquardensUnitTest.wsListini
{
    [TestClass]
    public class Elenchi : Base
    {
        protected iElenchiClient Client = new iElenchiClient();

        [TestMethod]
        public void GetCategorie()
        {
            DoLogin();

            var esito = Client.GetCategorie(SessionId, Impianto, 2);
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void GetListini()
        {
            DoLogin();
            var esito = Client.GetListini(SessionId, Impianto);
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void GetListiniFilterd()
        {
            DoLogin();
            var esito = Client.GetListiniFiltered(SessionId, Impianto, new dcListinoFilter()
            {
                TipiListini = new int[] { 5 }
            });
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
    
    [TestClass]
    public class Gestione : Base
    {
        protected iGestioneClient Client = new iGestioneClient();
        
        [TestMethod]
        public void AddListini()
        {
            DoLogin();

            var esito = Client.AddListini(SessionId, Impianto, new []{ new dcListino() });
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}