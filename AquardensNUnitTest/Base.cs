using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using wsAccesso;

namespace AquardensNUnitTest
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

        [SetUp]
        public async Task Setup()
        {
            await DoLogin();
        }

        protected void Print(object esito)
        {
            var responseJson = JsonConvert.SerializeObject(esito, Formatting.Indented);
            TestContext.WriteLine($"{responseJson}");
        }
        
        protected async Task<dcEsito> DoLogin()
        {
            var user = new dcUser()
            {
                UserName = "aquardens",
                Password = "aquardens",
                UserType = 400
            };
            dcClientInfo clientInfo = new dcClientInfo();
            var esitoLogin = await AccessClient.LoginAsync(Impianto, user, clientInfo);
            SessionId = esitoLogin.SessionId;
            return esitoLogin;
        }
        
        [OneTimeTearDown]
        public void TearDownClientBase()
        {
            AccessClient.Close();
        }
    }
}