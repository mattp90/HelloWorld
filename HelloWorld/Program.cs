using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace HelloWorld
{
    class Program
    {
	    private static readonly string ftpHost = "ftp://ftpsgeie.aqdemo.it:5010/response";
	    private static readonly string ftpHostWrite = "ftp://ftpsgeie.aqdemo.it:5010/request";
	    // private static readonly string ftpHostProcessed = "ftp://ftpsgeie.aqdemo.it:5010/response/processed";
	    private static readonly string ftpUsername = "ftp_invoice_demo|ftp_invoice_demo";
	    private static readonly string ftpPassword = "7ujmNhgg!@jHUf";
	    
        static void Main(string[] args)
        {
	        try
	        {
		        MoveFileToProcessed("test_1.json");
		        
		        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpHost);
		        request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
		        request.EnableSsl = true;
		        request.Method = WebRequestMethods.Ftp.ListDirectory;
		        
		        var files = GetContentsFolder((FtpWebResponse) request.GetResponse());

		        foreach (var file in files.Where(x => x.EndsWith(".json")))
		        {
			        var a = DownloadFileFTP(file);

			        UploadFile(a);
		        }
		        
	        }
	        catch (Exception ex)
	        {
		        Console.WriteLine(ex.ToString());
	        }
        }

        private static List<string> GetContentsFolder(FtpWebResponse response)
        {
	        var result = new List<string>();
	        using (var res = response)
	        {
		        StreamReader streamReader = new StreamReader(response.GetResponseStream());
		        string line = streamReader.ReadLine();
		        while (!string.IsNullOrEmpty(line))
		        {
			        result.Add(line);
			        line = streamReader.ReadLine();
		        }

		        streamReader.Close();
	        }

	        return result;
        }

        private static void UploadFile(string content)
        {
	        // Get the object used to communicate with the server.
	        FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{ftpHostWrite}/{DateTime.Now.ToString("yyyyMMddHHmmss")}_request.json");
	        request.Method = WebRequestMethods.Ftp.UploadFile;

	        // This example assumes the FTP site uses anonymous logon.
	        request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

	        byte[] bytes = Encoding.ASCII.GetBytes(content);
	        // Write the bytes into the request stream.
	        request.ContentLength = bytes.Length;
	        using (Stream request_stream = request.GetRequestStream())
	        {
		        request_stream.Write(bytes, 0, bytes.Length);
		        request_stream.Close();
	        }
	        
	        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
	        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
        }

        public static void MoveFileToProcessed(string filename)
        {
	        FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{ftpHost}/{filename}");
	        request.Method = WebRequestMethods.Ftp.Rename;
	        request.EnableSsl = true;
	        request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
	        request.RenameTo = $"./processed/{filename}";
	        // request.RenameTo = $"ciao.json";

	        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
	        
        }
        // Use FTP to upload a file.
        private void FtpUploadFile(string filename, string to_uri, string user_name, string password)
        {
	        // Get the object used to communicate with the server.
	        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(to_uri);
	        request.Method = WebRequestMethods.Ftp.UploadFile;

	        // Get network credentials.
	        request.Credentials = new NetworkCredential(user_name, password);

	        // Read the file's contents into a byte array.
	        byte[] bytes = System.IO.File.ReadAllBytes(filename);

	        // Write the bytes into the request stream.
	        request.ContentLength = bytes.Length;
	        using (Stream request_stream = request.GetRequestStream())
	        {
		        request_stream.Write(bytes, 0, bytes.Length);
		        request_stream.Close();
	        }
        }
        
        public static Stream GenerateStreamFromString(string s)
        {
	        MemoryStream stream = new MemoryStream();
	        StreamWriter writer = new StreamWriter(stream);
	        writer.Write(s);
	        writer.Flush();
	        stream.Position = 0;

	        return stream;
        }


        private static string DownloadFileFTP(string fileName)
        {
	        FtpWebRequest req = (FtpWebRequest)WebRequest.Create($"{ftpHost}/{fileName}");
	        req.Method = WebRequestMethods.Ftp.DownloadFile;
	        req.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
	        
	        using (var response = (FtpWebResponse)req.GetResponse())
	        {
		        Stream responseStream = response.GetResponseStream();
		        StreamReader reader = new StreamReader(responseStream);
		        return reader.ReadToEnd();
	        }
        }
        
        private void ImportDataTenutaLuce()
        {
	        ProcessDirectory(@"C:\projects\tenutaluce\frontend\public\static\schede\");
	        Root json = JsonConvert.DeserializeObject<Root>(File.ReadAllText("..\\..\\tenutaLuce.json"));

	        foreach (Datum data in json.data)
	        {
		        foreach (Route call in data.item.routes)
		        {
			        string body = call.responses[0].body.Replace("\\\"", "\"").Replace("\\n", "\\n\\n");

			        string folder = call.endpoint.Split('/')[0];
			        for (int i = 1; i < call.endpoint.Split('/').Length; i++)
			        {
				        folder = Path.Combine(folder, call.endpoint.Split('/')[i]);
			        }

			        DirectoryInfo di = new DirectoryInfo(call.endpoint);
			        Console.WriteLine($"Writing file {data.item.name.Split('|')[1]}-{di.Name}.json...");

			        if (!Directory.Exists(folder))
				        Directory.CreateDirectory(folder);

			        File.WriteAllText(Path.Combine(folder, $"{di.Name}.{data.item.name.Split('|')[1].ToLower().Trim()}.json"),
				        body);
		        }
	        }
        }

		private static void ProcessDirectory(string rootDir)
		{
			string outputNew = @"C:\Users\matteo.piazzi\Desktop\tenuta luce\pdf_new\";

			// Process the list of files found in the directory.
			string[] subDirs = Directory.GetDirectories(rootDir);
			if (subDirs.Length == 0)
			{
				// Process PDFs
				string[] files = Directory.GetFiles(rootDir);

				if (files.Length > 0)
				{
					foreach (string file in files)
					{
						var paths = new List<string>(rootDir.Split('\\'));
						string lingua = paths[paths.Count - 1];
						string vino = paths[paths.Count - 2];
						string annata = Path.GetFileNameWithoutExtension(file);
						string newFileName = $"{lingua}_{vino}_{annata}.pdf";

						if (lingua != "ch")
						{
							string newFolder = Path.Combine(vino, annata);

							if (!Directory.Exists(Path.Combine(outputNew, newFolder)))
							{
								Directory.CreateDirectory(Path.Combine(outputNew, newFolder));
							}
							File.Copy(file, Path.Combine(outputNew, newFolder, newFileName));
						}
					}					
				}
			}
			else
			{
				foreach (string subDir in subDirs)
				{
					ProcessDirectory(subDir);
				}
			}
		}		
	}
    
    public class Root
		{
			public string source { get; set; }
			public List<Datum> data { get; set; }
		}

		public class Rule
		{
			public string target { get; set; }
			public string modifier { get; set; }
			public string value { get; set; }
			public bool isRegex { get; set; }
		}

		public class Respons
		{
			public string uuid { get; set; }
			public string body { get; set; }
			public int latency { get; set; }
			public int statusCode { get; set; }
			public string label { get; set; }
			public List<object> headers { get; set; }
			public string filePath { get; set; }
			public bool sendFileAsBody { get; set; }
			public List<Rule> rules { get; set; }
			public string rulesOperator { get; set; }
			public bool disableTemplating { get; set; }
			public bool fallbackTo404 { get; set; }
		}

		public class Route
		{
			public string uuid { get; set; }
			public string documentation { get; set; }
			public string method { get; set; }
			public string endpoint { get; set; }
			public List<Respons> responses { get; set; }
			public bool enabled { get; set; }
			public bool randomResponse { get; set; }
			public bool sequentialResponse { get; set; }
		}

		public class Header
		{
			public string key { get; set; }
			public string value { get; set; }
		}

		public class ProxyReqHeader
		{
			public string key { get; set; }
			public string value { get; set; }
		}

		public class ProxyResHeader
		{
			public string key { get; set; }
			public string value { get; set; }
		}

		public class Item
		{
			public string uuid { get; set; }
			public int lastMigration { get; set; }
			public string name { get; set; }
			public string endpointPrefix { get; set; }
			public int latency { get; set; }
			public int port { get; set; }
			public List<Route> routes { get; set; }
			public bool proxyMode { get; set; }
			public string proxyHost { get; set; }
			public bool https { get; set; }
			public bool cors { get; set; }
			public List<Header> headers { get; set; }
			public List<ProxyReqHeader> proxyReqHeaders { get; set; }
			public List<ProxyResHeader> proxyResHeaders { get; set; }
			public bool proxyRemovePrefix { get; set; }
			public string hostname { get; set; }
		}

		public class Datum
		{
			public string type { get; set; }
			public Item item { get; set; }
		}
}
