using AquardensNUnitTest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace wsCommerce
{
    public class Commerce001 : Base001
    {
        protected iCommerce_001Client CommerceClient = new iCommerce_001Client();

        [Test]
        public async Task Acquista()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.AcquistaAsync(baseRequest, new dcOrdineAcquisto()
            {
                Numero = "1"
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task AcquistaIscrizione()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.AcquistaIscrizioneAsync(baseRequest, new dcOrdineIscrizione());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task AcquistaSospesi()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.AcquistaSospesiAsync(baseRequest, new dcOrdineSospesi());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task Fruisci()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.FruisciAsync(baseRequest, new dcOrdineAcquisto(), new dcConto());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task GetNexiConfig()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.GetNexiConfigAsync(baseRequest);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task GetPayPalId()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.GetPayPalIdAsync(baseRequest);
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task SetNexiConfig()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.SetNexiConfigAsync(baseRequest, new dcNexiConfig());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task SetVoucherAsGift()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.SetVoucherAsGiftAsync(baseRequest, new dcVoucherGiftRequest());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task SetVoucherUser()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.SetVoucherUserAsync(baseRequest, "", new IdBox());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task TestAcquistaIscrizione()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.TestAcquistaIscrizioneAsync(baseRequest, new dcOrdineIscrizione());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task TestCart()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await CommerceClient.TestCartAsync(baseRequest, new dcOrdineAcquisto());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            CommerceClient.Close();
        }
    }
    
    
    public class Report001 : Base001
    {
        protected iReport_001Client ReportClient = new iReport_001Client();
        
        [Test]
        public async Task GetAquisti()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await ReportClient.GetAcquistiAsync(baseRequest, new dcAcquistoFilter());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task VoucherDetail()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await ReportClient.VoucherDetailAsync(baseRequest, new dcVoucherDetailRequest());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task VoucherList()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await ReportClient.VoucherListAsync(baseRequest, new dcReportFilter());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [Test]
        public async Task VoucherReport()
        {
            var baseRequest = new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            };

            var esito = await ReportClient.VoucherReportAsync(baseRequest, new dcReportFilter());
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            ReportClient.Close();
        }
    }
}