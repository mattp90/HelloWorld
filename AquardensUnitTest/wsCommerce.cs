using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AquardensUnitTest.wsCommerce
{
    [TestClass]
    public class Commerce: Base
    {
        protected iCommerceClient CommerceClient = new iCommerceClient();
        protected iFirmaDigitaleClient FirmaDigitaleClient = new iFirmaDigitaleClient();

        [TestMethod]
        public void Acquista()
        {
            DoLogin();

            var esito = CommerceClient.Acquista(SessionId, Impianto, null);
            CommerceClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void GetPayPalId()
        {
            DoLogin();

            var esito = CommerceClient.GetPayPalId(SessionId, Impianto);
            CommerceClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
    
    [TestClass]
    public class Documenti : Base
    {
        protected iDocumentiClient Client = new iDocumentiClient();
        
        [TestMethod]
        public void GetDocumenti()
        {
            DoLogin();

            var esito = Client.GetDocumenti(new dcBaseRequest()
                {
                    Impianto = Impianto, 
                    SessionId = SessionId
                }, 
                new dcDocumentoFilter()
                {
                    Impianto = Impianto,
                    IdAnagrafica = new IdBox()
                    {
                        Id = "ANA000120220415570028060000037"
                    }
                }
            );
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void GetDocumentiXml()
        {
            DoLogin();

            var esito = Client.GetDocumentiXml(new dcBaseRequest()
                {
                    Impianto = Impianto, 
                    SessionId = SessionId
                }, 
                new dcDocumentoFilter()
                {
                    Impianto = Impianto,
                    IdAnagrafica = new IdBox()
                    {
                        Id = "ANA000120220415570028060000037"
                    }
                }
            );
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void SetAcquisiti()
        {
            DoLogin();

            var esito = Client.SetAcquisiti(new dcBaseRequest()
                {
                    Impianto = Impianto, 
                    SessionId = SessionId
                }, 
                new []{ new IdBox() }
            );
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }

    [TestClass]
    public class Report : Base
    {
        protected iReportClient Client = new iReportClient();
        
        [TestMethod]
        public void VoucherList()
        {
            DoLogin();

            var esito = Client.VoucherList(SessionId, Impianto, new dcReportFilter());
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
        
        [TestMethod]
        public void VoucherDetail()
        {
            DoLogin();
            var esito = Client.VoucherDetail(SessionId, Impianto, new dcVoucherDetailRequest()
            {
                CodiceVoucher = new IdBox()
                {
                    Id = ""
                }
            });
            Client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}