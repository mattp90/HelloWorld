using System;
using System.Linq;
using AquardensUnitTest.wsAnagrafica;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AquardensUnitTest
{
    [TestClass]
    public class Elenchi : Base
    {
        protected iElenchiClient ElenchiClient = new iElenchiClient();

        [TestMethod]
        public void GetAnagrafiche()
        {
            DoLogin();
            var esito = ElenchiClient.GetAnagrafiche(SessionId, Impianto, new dcAnagraficaFilter()
            {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037"
                }
            });
            ElenchiClient.Close();
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }
        
        [TestMethod]
        public void GetIscrizioni()
        {
            DoLogin();

            var idBox = new IdBox()
            {
                Id = "130301520201212363153260194267", // "130301020220317557309490223412",
                IdEsterno = ""
            };
            
            var esito = ElenchiClient.GetIscrizioni(SessionId, Impianto, idBox, true);
            ElenchiClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
    
    [TestClass]
    public class Gestione : Base
    {
        protected iGestioneClient Client = new iGestioneClient();
        protected iElenchiClient ElenchiClient = new iElenchiClient();
        
        [TestMethod]
        public void Aggiungi()
        {
            DoLogin();

            var anagrafica = new dcAnagraficaCompleta()
            {
                Cellulare = "123456789",
                Cognome = "Test Cognome",
                Email = "matteo.piazzi@aquest.it",
                Impianto = "1303",
                Nome = "Test Nome",
                Nota = "Test Nota",
                Sesso = "M",
                Telefono = "123456789",
                Username = "aquesttest",
                IsAzienda = false,
                IsPersonaFisica = true,
                DataNascita = new DateTime(1990, 01, 01)
            };
            var esito = Client.Aggiungi(SessionId, Impianto, null);
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void Modifica()
        {
            DoLogin();

            var anagrafica = ElenchiClient.GetAnagrafiche(SessionId, Impianto, new dcAnagraficaFilter()
            {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037" 
                } 
            }).Elenco.First();
            
            anagrafica.Cellulare = "123456789";
            anagrafica.Nome = "Matteo";
            anagrafica.RagioneSociale = "Ragione sociale";
            
            var esito = Client.Modifica(SessionId, Impianto, anagrafica);
            Client.Close();
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }
    }
}