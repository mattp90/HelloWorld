using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AquardensUnitTest.wsAccesso;

namespace AquardensUnitTest
{
    [TestClass]
    public class Access : Base
    {
        [TestMethod]
        public void Login()
        {
            var esito = DoLogin();
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }

        [TestMethod]
        public void Logout()
        {
            DoLogin();
            var esito = AccessClient.Logout(SessionId, Impianto);
            AccessClient.Close();
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }

        [TestMethod]
        public void GetImpianti()
        {
            var esito = AccessClient.GetImpianti(Impianto);
            AccessClient.Close();
            Print(esito);
            Assert.IsTrue(esito.EsitoCodice == 0);
        }

        [TestMethod]
        public void Register()
        {
            var user = new dcUser()
            {
                UserName = "aquesttest",
                Nome = "aquest",
                Cognome = "test",
                Email = "matteo.piazzi@aquest.it",
                UserType = 200,
                Cellulare = "111111",
                Password = "Aquesttest2022",
                DataNascita = new DateTime(1990, 01, 01)
            };

            var clientInfo = new dcClientInfo()
            {
                Device = "smartphone",
                OsName = "Windows",
                OsVersion = "10.0"
            };

            var esito = AccessClient.Register(Impianto, user, clientInfo);
            AccessClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void ChangePassword()
        {
            DoLogin();
            var esito = AccessClient.ChangePassword(SessionId, Impianto, "aquardens", "aquardensnew");
            AccessClient.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}
