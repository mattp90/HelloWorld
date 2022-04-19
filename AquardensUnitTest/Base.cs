using AquardensUnitTest.wsAccesso;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using dcEsito = AquardensUnitTest.wsAccesso.dcEsito;

namespace AquardensUnitTest
{
    public class Base
    {
        protected string SessionId { get; set; }
        protected const string Impianto = "1303";
        protected iAccessClient AccessClient = new iAccessClient();

        private TestContext testContextInstance;
        
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        protected void Print(object esito)
        {
            var responseJson = JsonConvert.SerializeObject(esito, Formatting.Indented);
            TestContext.WriteLine($"{responseJson}");
        }
        
        protected dcEsito DoLogin()
        {
            var user = new dcUser()
            {
                UserName = "aquardens",
                Password = "aquardens",
                UserType = 400
            };
            dcClientInfo clientInfo = new dcClientInfo();
            var esitoLogin = AccessClient.Login(Impianto, user, clientInfo);
            // AccessClient.Close();
            SessionId = esitoLogin.SessionId;
            return esitoLogin;
        }
    }
}