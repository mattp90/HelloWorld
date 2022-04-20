using AquardensNUnitTest;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace wsAnagrafica
{
    public class Elenchi : Base
    {
        protected iElenchiClient ElenchiClient = new iElenchiClient();

        [Test]
        public async Task GetAnagrafiche()
        {
            var esito = await ElenchiClient.GetAnagraficheAsync(SessionId, Impianto, new dcAnagraficaFilter()
            {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037"
                }
            });
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }
        
        [Test]
        public async Task GetIscrizioni()
        {
            var idBox = new IdBox()
            {
                Id = "130301520201212363153260194267", // "130301020220317557309490223412",
                IdEsterno = ""
            };
            
            var esito = await ElenchiClient.GetIscrizioniAsync(SessionId, Impianto, idBox, true);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            ElenchiClient.Close();
        }
    }
    
    public class Gestione : Base
    {
        protected iGestioneClient Client = new iGestioneClient();
        protected iElenchiClient ElenchiClient = new iElenchiClient();
        
        [Test]
        public async Task Aggiungi()
        {
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
            var esito = await Client.AggiungiAsync(SessionId, Impianto, null);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task Modifica()
        {
            var anagrafica = ElenchiClient.GetAnagraficheAsync(SessionId, Impianto, new dcAnagraficaFilter()
            {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037" 
                } 
            }).Result.Elenco.First();
            
            anagrafica.Cellulare = "123456789";
            anagrafica.Nome = "Matteo";
            anagrafica.RagioneSociale = "Ragione sociale";
            
            var esito = await Client.ModificaAsync(SessionId, Impianto, anagrafica);
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            Client.Close();
            ElenchiClient.Close();
        }
    }
}