using AquardensNUnitTest;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace wsAccesso
{
    public class Access001 : Base001
    {
        [Test]
        public async Task Logout()
        {
            var esito = await Access001Client.LogoutAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task ChangePassword()
        {
            var esito = await Access001Client.ChangePasswordAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, "aquardens", "aquardensnew");
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task Register()
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
            
            var esito = await Access001Client.RegisterAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, user, clientInfo);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task ConfirmRegister()
        {
            var esito = await Access001Client.ConfirmRegisterAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            }, new dcConfirmRequest()
            {
                Code = "",
                Username = "test",
                Type = 100
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task GetImpianti()
        {
            var esito = await Access001Client.GetImpiantiAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            });
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task GetDispositivi()
        {
            var esito = await Access001Client.GetDispositiviAsync(new dcBaseRequest()
            {
                Impianto = Impianto,
                SessionId = SessionId
            },400);
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}
