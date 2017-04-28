using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WFLibrary
{
    public class SocketClient
    {
        string host = "api.github.com";
        TcpClient client; 
        private int port = 443;

        public List<RepositoryIdNameModel> GetUserReposytories(string username)
        {
            string answer = string.Empty;
            List<RepositoryIdNameModel> list = new List<RepositoryIdNameModel>();

            client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();
            SslStream sslStream = new SslStream(stream);
            sslStream.AuthenticateAsClient(host);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /users/{0}/repos HTTP/1.1", username));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            string akl = sb.ToString();

            byte[] headerBytes = Encoding.UTF8.GetBytes(sb.ToString());

            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[2048];
            int bytes;

            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                answer += (Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (bytes != 0);

            sslStream.Close();
            stream.Close();
            client.Close();
            

            if (checker(answer))
            {
                string kur1 = string.Empty;
                string ept1 = string.Empty;

                while (answer.Contains("\"name\":\""))
                {
                    kur1 = answer.Substring(answer.IndexOf("\"name\":\""), (answer.IndexOf("\",\"full_name\"") - answer.IndexOf("\"name\":\"")));
                    ept1 = answer.Substring(answer.IndexOf("\"id\":"), (answer.IndexOf(",\"name\"") - answer.IndexOf("\"id\":")));

                    kur1 = kur1.Substring(8);
                    ept1 = ept1.Substring(5);

                    answer = answer.Substring(answer.IndexOf("},{") + 1);

                    if (list.Count > 0)
                    {
                        if (list[list.Count - 1].Id == int.Parse(ept1))
                        {
                            break;
                        }
                    }
                    list.Add(new RepositoryIdNameModel { Id = int.Parse(ept1), Name = kur1 });
                }
            }

            return list;

        }

        public RepositoryNameUserNameModel GetRepository(int id)
        {

            string answer = string.Empty;
            RepositoryNameUserNameModel model = null;

            client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();
            SslStream sslStream = new SslStream(stream);
            sslStream.AuthenticateAsClient(host);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repositories/{0} HTTP/1.1", id));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            byte[] headerBytes = Encoding.UTF8.GetBytes(sb.ToString());
            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[2048];
            int bytes;

            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                answer += (Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (bytes != 0);

            sslStream.Close();
            stream.Close();
            client.Close();

            if (checker(answer))
            {
                string kurwa1 = answer.Substring(answer.IndexOf("{"), (answer.IndexOf(",\"full_name\":") - answer.IndexOf("{")));
                string eptab1 = answer.Substring(answer.IndexOf("\"login\""), (answer.IndexOf("\"avatar_url\"") - answer.IndexOf("\"login\"")));
                string kurwa2 = string.Empty;
                string eptab2 = eptab1.Substring(9, eptab1.IndexOf("\",\"id\"") - eptab1.LastIndexOf("\"login\":\""));
                string eptab3 = eptab2.Substring(0, eptab2.IndexOf("\",\"id\""));


                for (int i = kurwa1.Length - 2; kurwa1[i] != '\"'; i--)
                {
                    kurwa2 += kurwa1[i];
                }

                string kurwa3 = string.Empty;

                for (int i = kurwa2.Length - 1; i >= 0; i--)
                {
                    kurwa3 += kurwa2[i];
                }

                model = new RepositoryNameUserNameModel { RepositoryName = kurwa3, UserName = eptab3 };
            }

            return model;
        }

        public RepositoryDescriptionModel GetDescription(RepositoryNameUserNameModel model)
        {
            string answer = string.Empty;
            RepositoryDescriptionModel description = null;

            client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();
            SslStream sslStream = new SslStream(stream);
            sslStream.AuthenticateAsClient(host);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repos/{0}/{1} HTTP/1.1", model.UserName, model.RepositoryName));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            byte[] headerBytes = Encoding.UTF8.GetBytes(sb.ToString());
            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[2048];
            int bytes;

            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                answer += (Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (bytes != 0);

            sslStream.Close();
            stream.Close();
            client.Close();

            if (checker(answer))
            {
                string kurw = answer.Substring(answer.IndexOf("\"description\""), (answer.IndexOf(",\"fork\"") - answer.IndexOf("\"description\"")));
                string hen = string.Empty;

                for (int i = kurw.Length - 1; kurw[i] != '\"'; i--)
                {
                    hen += kurw[i];
                }

                string tai = string.Empty;

                for (int i = hen.Length - 1; i >= 0; i--)
                {
                    tai += hen[i];
                }



                description = new RepositoryDescriptionModel { Description = tai };
            }
            return description;
        }

        public List<RepositoryCommitsDateMessageModel> GetCommits(RepositoryNameUserNameModel model)
        {
            List<RepositoryCommitsDateMessageModel> commits = new List<RepositoryCommitsDateMessageModel>();
            string answer = string.Empty;

            client = new TcpClient(host, port);
            NetworkStream stream = client.GetStream();
            SslStream sslStream = new SslStream(stream);
            sslStream.AuthenticateAsClient(host);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repos/{0}/{1}/commits HTTP/1.1", model.UserName, model.RepositoryName));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp"); //nedeed
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            byte[] headerBytes = Encoding.UTF8.GetBytes(sb.ToString());
            sslStream.Write(headerBytes, 0, headerBytes.Length);
            sslStream.Flush();

            byte[] buffer = new byte[2048];
            int bytes;

            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);
                answer += (Encoding.UTF8.GetString(buffer, 0, bytes));
            } while (bytes != 0);

            sslStream.Close();
            stream.Close();
            client.Close();
            

            if (checker(answer))
            {
                string tanya = answer;
                while (tanya.Contains("\"date\""))
                {
                    tanya = tanya.Substring(tanya.IndexOf("\"date\":\"") + 8);
                    tanya = tanya.Substring(tanya.IndexOf("\"date\":\"") + 8);

                    string time = tanya.Substring(0, tanya.IndexOf("\"},\"message"));
                    string mssg = tanya.Substring(tanya.IndexOf("\"message\""), (tanya.IndexOf(",\"tree\"") - tanya.IndexOf("\"message\"")));

                    commits.Add(new RepositoryCommitsDateMessageModel { CommitDate = time, CommitMessage = mssg });

                }

            }




            return commits;
        }

        public bool checker(string answer)
        {
            string zooski = answer.Substring(9, 3);
            if (zooski == "404")
            {
                return false;
            }
            if (zooski == "200")
            {
                return true;
            }
            return false;
        }
    }
}
