using AquardensNUnitTest;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace wsAccesso
{
    public class Access : Base
    {
        [Test]
        public async Task Logout()
        {
            var esito = await AccessClient.LogoutAsync(SessionId, Impianto);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public void GetImpianti()
        {
            var esito = AccessClient.GetImpiantiAsync(Impianto);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task Register()
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

            var esito = await AccessClient.RegisterAsync(Impianto, user, clientInfo);
            Print(esito);
            Assert.IsNotNull(esito);
        }

        [Test]
        public async Task ChangePassword()
        {
            var esito = await AccessClient .ChangePasswordAsync(SessionId, Impianto, "aquardens", "aquardensnew");
            Print(esito);
            Assert.IsNotNull(esito);
        }
    }
}
