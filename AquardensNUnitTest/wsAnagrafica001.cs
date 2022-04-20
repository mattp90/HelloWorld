using System;
using System.Linq;
using AquardensNUnitTest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace wsAnagrafica
{
    public class Elenchi001 : Base001
    {
        protected iElenchi_001Client ElenchiClient = new iElenchi_001Client();

        [Test]
        public async Task GetAnagrafiche()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await ElenchiClient.GetAnagraficheAsync(baseRequest, new dcAnagraficaFilter() {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037"
                }
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task GetIscrizioni()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await ElenchiClient.GetIscrizioniAsync(baseRequest, new IdBox(), true);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task GetMemberCard()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await ElenchiClient.GetMemberCardAsync(baseRequest, new dcMemberCardFilter()
            {
                AllCards = true
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task GetPagamentiSospesi()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await ElenchiClient.GetPagamentiSospesiAsync(baseRequest, new dcPagamentoSospesoFilter()
            {
                DataDal = DateTime.MinValue,
                DataAl = DateTime.MaxValue
            });
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
        protected iElenchi_001Client ElenchiClient = new iElenchi_001Client();

        [Test]
        public async Task Aggiungi()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

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
            
            var esito = await GestioneClient.AggiungiAsync(baseRequest, anagrafica);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task Modifica()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var anagrafica = ElenchiClient.GetAnagraficheAsync(baseRequest, new dcAnagraficaFilter()
            {
                IdAnagrafica = new IdBox()
                {
                    Id = "ANA000120220415570028060000037" 
                } 
            }).Result.Elenco.First();
            
            anagrafica.Cellulare = "123456789";
            anagrafica.Nome = "Matteo";
            anagrafica.RagioneSociale = "Ragione sociale";
            
            var esito = await GestioneClient.ModificaAsync(baseRequest, anagrafica);
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }

        [Test]
        public async Task SetUserNote()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await GestioneClient.SetUserNoteAsync(baseRequest, new dcNoteRequest()
            {
                Append = true,
                Note = "prova note test",
                IdUser = new IdBox()
                {
                    Id = "ANA000120220415570028060000037" 
                } 
            });
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }

        [Test]
        public async Task UploadUserFile()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };
            
            var esito = await GestioneClient.UploadUserFileAsync(baseRequest, new dcFileRequest()
            {
                Description = "File test for user",
                File = null,
                FileName = "test.txt",
                IdUser = new IdBox()
                {
                    Id = "ANA000120220415570028060000037" 
                }
            });
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            GestioneClient.Close();
            ElenchiClient.Close();
        }
    }
}