using FUE.Web.Models;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FUE.Web.DataAccess
{
    public class dalGetFTP
    {
        EFDBContext _db = new EFDBContext();
        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + "test.rebex.net" + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential("demo", "password");
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public void GetFilesFromServer(string Connectiontype, string FtpURL, string Pattern, string UserName, string Password, string DownloadPath, string remoteDirectory)
        {
            try
            {
                if (Connectiontype.ToString().ToLower() == "sftp")
                {
                    GETSFTPFILE(FtpURL, Pattern, UserName, Password, DownloadPath, remoteDirectory);
                }
                else
                {
                    GETFTPFILE(FtpURL, Pattern, UserName, Password, DownloadPath);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void GETFTPFILE(string FtpURL, string Pattern, string UserName, string Password, string DownloadPath)
        {

            FtpURL = FtpURL + "/";
            //FtpURL = "ftp://" + FtpURL + "/";
            var myRegex = (dynamic)null;
            var resultlist = (dynamic)null;

            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpURL));
            ftpRequest.Credentials = new NetworkCredential(UserName, Password);

            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();


            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(UserName, Password);

                if (!string.IsNullOrEmpty(Pattern))
                {
                    myRegex = new Regex(Pattern);
                    resultlist = directories.Where(f => myRegex.IsMatch(f)).ToList().OrderBy(f => f.Length);
                }

                else
                {
                    resultlist = directories;
                }

                //var myRegex = new Regex(Pattern);
                //List<string> resultlist = directories.Where(f => myRegex.IsMatch(f)).ToList();

                for (int i = 0; i <= resultlist.Count - 1; i++)
                {
                    if (resultlist[i].Contains("."))
                    {
                        string path = FtpURL + resultlist[i].ToString();
                        string trnsfrpth = @"" + DownloadPath + "" + resultlist[i].ToString();
                        ftpClient.DownloadFile(path, trnsfrpth);

                    }
                }
            }
        }


        public void GETFTPFILEBackup(string FtpURL, string UserName, string Password, string DownloadPath, string RegexPattern)
        {
            FtpURL = "ftp://" + FtpURL + "/";

            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(FtpURL));
            ftpRequest.Credentials = new NetworkCredential(UserName, Password);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            List<string> directories = new List<string>();

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();


            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential(UserName, Password);

                for (int i = 0; i <= directories.Count - 1; i++)
                {
                    if (directories[i].Contains("."))
                    {
                        if (RegexPattern != null)
                        {
                            var match = Regex.Match(directories[i].ToString(), RegexPattern);


                        }
                        string path = FtpURL + directories[i].ToString();
                        string trnsfrpth = @"" + DownloadPath + "" + directories[i].ToString();
                        //string trnsfrpth = @"C:\Test\" + directories[i].ToString();
                        ftpClient.DownloadFile(path, trnsfrpth);
                    }
                }
            }
        }

        //sftp

        public void GETSFTPFILE(string SFtpURL, string RegexPattern, string UserName, string Password, string DownloadPath, string remoteDirectory)
        {

            remoteDirectory = "/" + remoteDirectory + "/";
            //string remoteDirectory = "/initial_export/";

            var myRegex = (dynamic)null;
            var resultlist = (dynamic)null;
            string localDirectory = DownloadPath;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                using (var sftp = new SftpClient(SFtpURL, UserName, Password))
                //using (var sftp = new SftpClient(host, username, password))
                {
                    sftp.ConnectionInfo.Timeout = TimeSpan.FromMinutes(60);
                  
                    sftp.KeepAliveInterval = TimeSpan.FromMinutes(60);
                    sftp.OperationTimeout = TimeSpan.FromMinutes(180);
                    sftp.BufferSize = 4 * 1024;
                    
                    sftp.Connect();
                    var files = sftp.ListDirectory(remoteDirectory);
                    if (!string.IsNullOrEmpty(RegexPattern))
                    {
                        myRegex = new Regex(RegexPattern);
                        resultlist = files.Where(f => myRegex.IsMatch(f.Name)).ToList().OrderBy(f => f.Length);
                    }

                    else
                    {
                        resultlist = files;
                    }


                    if (!String.IsNullOrEmpty(RegexPattern))
                    {

                        foreach (var file in resultlist)
                        {
                            string remoteFileName = file.Name;

                            if ((!file.Name.StartsWith(".")))

                                using (Stream file1 =  File.OpenWrite(localDirectory + remoteFileName))
                                {
                                    string from = remoteDirectory + remoteFileName;
                                    var path = $"/{from.Replace(@"\", "/")}";
                                    sftp.DownloadFile(path, file1);
                                }
                        }
                    }

                    else
                    {
                        foreach (var file in resultlist)
                        {
                            string remoteFileName = file.Name;
                            //&& ((file.LastWriteTime.Date == DateTime.Today))
                            if ((!file.Name.StartsWith(".")))
                                using (Stream file1 = File.OpenWrite(localDirectory + remoteFileName))
                                {
                                    string from = remoteDirectory + remoteFileName;
                                    var path = $"/{from.Replace(@"\", "/")}";
                                    sftp.DownloadFile(path, file1);
                                }
                        }
                    }

                    sftp.Disconnect();
                }
            }

            catch (System.Net.Sockets.SocketException ex)
            {
                throw new Exception(ex.Message);
                //throw new Exception("An-existing-connection-was-forcibly-closed-by-the-remote-host");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public C_connection MstGetFTPConnection(int? Connection_K)
        {
            C_connection results = new C_connection();

            var parConnection_K = new SqlParameter("@Connection_K", Connection_K);
            results = _db.Database.SqlQuery<C_connection>("select * from C_connection WHERE Connection_K=@Connection_K", parConnection_K).FirstOrDefault();
            return results;
        }



        public void SftpTest()
        {
            string Host = "sftp.axgsolutions.com";
            int Port = 22;
            string Username = "svc_vestracare";
            string Password = "nY5$huf^3D";
            string Source = "/initial_export";
            const string Destination = @"c:\Sftp";


            var connectionInfo = new PasswordConnectionInfo(Host, Port, Username, Password);

            //connectionInfo.AuthenticationPrompt += delegate (object sender, AuthenticationPromptEventArgs e)
            //{
            //    foreach (var prompt in e.Prompts)
            //    {
            //        if (prompt.Request.Equals("Password: ", StringComparison.InvariantCultureIgnoreCase))
            //        {
            //            prompt.Response = Password;
            //        }
            //    }
            //};

            using (var client = new SftpClient(connectionInfo))
            {
                client.Connect();
                DownloadDirectory(client, Source, Destination);
            }



        }

        private static void DownloadDirectory(SftpClient client, string source, string destination)
        {
            var myRegex = (dynamic)null;
            var resultlist = (dynamic)null;
            var files = client.ListDirectory(source);
            string Regexpattern = "^viv*";
            if (!string.IsNullOrEmpty(Regexpattern))
            {
                myRegex = new Regex(Regexpattern);
                resultlist = files.Where(f => myRegex.IsMatch(f.Name)).ToList().OrderBy(f => f.Length);
            }

            else
            {
                resultlist = files;
            }
            foreach (var file in resultlist)
            {
                if (!file.IsDirectory && !file.IsSymbolicLink)
                {
                    DownloadFile(client, file, destination);
                }
                else if (file.IsSymbolicLink)
                {
                    Console.WriteLine("Ignoring symbolic link {0}", file.FullName);
                }
                else if (file.Name != "." && file.Name != "..")
                {
                    var dir = Directory.CreateDirectory(Path.Combine(destination, file.Name));
                    DownloadDirectory(client, file.FullName, dir.FullName);
                }
            }
        }

        private static void DownloadFile(SftpClient client, SftpFile file, string directory)
        {
            Console.WriteLine("Downloading {0}", file.FullName);
            using (Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name)))
            {
                client.DownloadFile(file.FullName, fileStream);
            }
        }
    }
}