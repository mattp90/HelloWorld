using AquardensUnitTest.wsListini;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AquardensUnitTest.wsAccesso
{
    [TestClass]
    public class Elenchi : Base
    {
        protected iElenchi_001Client ElenchiClient = new iElenchi_001Client();

        [TestMethod]
        public void GetCategorie()
        {
            DoLogin001();
            var esito = ElenchiClient.GetCategorie(new wsListini.dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, 2);
            ElenchiClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void GetListini()
        {
            DoLogin001();
            var esito = ElenchiClient.GetListini(new wsListini.dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, new dcListinoFilter()
            {
                TipiListini = new int[] { 1 }
            });
            ElenchiClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
    
    [TestClass]
    public class Gestione : Base
    {
        protected iGestione_001Client Client = new iGestione_001Client();

        [TestMethod]
        public void SetListino()
        {
            DoLogin001();
            var esito = Client.SetListino(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, new dcListinoCompleto()
            {
                Descrizione = "Listino Test",
                Disponibilita = 10,
                Impianto = "400"
            }, true);
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}