using System;
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
        public void ChangePassword()
        {
            DoLogin001();
            var esito = client.ChangePassword(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, "aquardens", "aquardensnew");
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void Register()
        {
            var user = new dcUser()
            {
                Cellulare = "111111111",
                Cognome = "Cognome Test",
                Email = "test@test.com",
                Nome = "Nome test",
                Password = "password",
                CodiceFiscale = "",
                DataNascita = new DateTime(1990, 1, 1),
                UserName = "test",
                UserType = 100
            };

            var clientInfo = new dcClientInfo()
            {
                Device = "smartphone",
                OsName = "Windows",
                OsVersion = "10.0"
            };
            
            DoLogin001();
            var esito = client.Register(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, user, clientInfo);
            client.Close();
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [TestMethod]
        public void ConfirmRegister()
        {
            DoLogin001();
            var esito = client.ConfirmRegister(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, new dcConfirmRequest()
            {
                Code = "",
                Username = "test",
                Type = 100
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
