using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AquardensUnitTest.wsAccesso
{
    [TestClass]
    public class Access001 : Base
    {
        protected iAccess_001Client client = new iAccess_001Client();
        
        [TestMethod]
        public void Login()
        {
            var esito = DoLogin001();
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void Logout()
        {
            DoLogin001();
            var esito = client.Logout(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            });
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void GetImpianti()
        {
            DoLogin001();
            var esito = client.GetImpianti(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            });
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void GetDispositivi()
        {
            DoLogin001();
            var esito = client.GetDispositivi(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            },400);
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}
