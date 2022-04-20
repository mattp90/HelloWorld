using Newtonsoft.Json;
using NUnit.Framework;
using System.Threading.Tasks;
using wsAccesso;

namespace AquardensNUnitTest
{
    public class Base001
    {
        protected string SessionId { get; set; }
        protected const string Impianto = "1303";
        protected iAccess_001Client Access001Client = new iAccess_001Client();

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

            var clientInfo = new dcClientInfo()
            {
                Device = "smartphone",
                OsName = "Windows",
                OsVersion = "10.0"
            };

            var esito = await Access001Client.LoginAsync(new dcBaseRequest()
            {
                Impianto = Impianto
            }, user, clientInfo);

            SessionId = esito.SessionId;
            return esito;
        }
        
        [OneTimeTearDown]
        public void TearDownClientBase()
        {
            Access001Client.Close();
        }
    }
}