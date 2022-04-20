using AquardensNUnitTest;
using NUnit.Framework;
using System.Threading.Tasks;

namespace wsCommerce
{
    public class Report002 : Base001
    {
        protected iReport_002Client ReportClient = new iReport_002Client();
        
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
        
        [OneTimeTearDown]
        public void TearDownClient()
        {
            ReportClient.Close();
        }    
    }
}