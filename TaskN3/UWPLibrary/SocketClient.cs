using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace UWPLibrary
{
    public class SocketClient
    {
        private StreamSocket socket;
        private HostName hostName = new HostName("api.github.com");
        private string port = "443";

        public async Task<List<RepositoryIdNameModel>> GetUserReposytories(string username)
        {
            string answer = string.Empty;
            List<RepositoryIdNameModel> list = new List<RepositoryIdNameModel>();

            socket = new StreamSocket();
            socket.Control.NoDelay = false;
            

            await socket.ConnectAsync(hostName, port, SocketProtectionLevel.Tls12);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /users/{0}/repos HTTP/1.1", username));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            await this.send(sb.ToString());
            answer = await this.read();
            socket.Dispose();

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

        public async Task<RepositoryNameUserNameModel> GetRepository(int id)
        {
            
            string answer = string.Empty;
            RepositoryNameUserNameModel model = null;

            socket = new StreamSocket();
            socket.Control.NoDelay = false;

            await socket.ConnectAsync(hostName, port, SocketProtectionLevel.Tls12);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repositories/{0} HTTP/1.1", id));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            await this.send(sb.ToString());
            answer = await this.read();
            socket.Dispose();

            if (checker(answer))
            {
                string kr1 = answer.Substring(answer.IndexOf("{"), (answer.IndexOf(",\"full_name\":") - answer.IndexOf("{")));
                string etb1 = answer.Substring(answer.IndexOf("\"login\""), (answer.IndexOf("\"avatar_url\"") - answer.IndexOf("\"login\"")));
                string kr2 = string.Empty;
                string etb2 = etb1.Substring(9, etb1.IndexOf("\",\"id\"") - etb1.LastIndexOf("\"login\":\""));
                string etb3 = etb2.Substring(0, etb2.IndexOf("\",\"id\""));


                for (int i = kr1.Length - 2; kr1[i] != '\"'; i--)
                {
                    kr2 += kr1[i];
                }

                string kr3 = string.Empty;

                for (int i = kr2.Length - 1; i >= 0; i--)
                {
                    kr3 += kr2[i];
                }

                 model = new RepositoryNameUserNameModel { RepositoryName = kr3, UserName = etb3 };
            }

            return model;
        }

        public async Task<RepositoryDescriptionModel> GetDescription(RepositoryNameUserNameModel model)
        {
            string answer = string.Empty;
            RepositoryDescriptionModel description = null;
            socket = new StreamSocket();
            socket.Control.NoDelay = false;

            await socket.ConnectAsync(hostName, port, SocketProtectionLevel.Tls12);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repos/{0}/{1} HTTP/1.1", model.UserName, model.RepositoryName));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp");
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            await this.send(sb.ToString());

            answer = await this.read();
            socket.Dispose();

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

        public async Task<List<RepositoryCommitsDateMessageModel>> GetCommits(RepositoryNameUserNameModel model)
        {
            List<RepositoryCommitsDateMessageModel> commits = new List<RepositoryCommitsDateMessageModel>();
            string answer = string.Empty;

            socket = new StreamSocket();
            socket.Control.NoDelay = false;

            await socket.ConnectAsync(hostName, port, SocketProtectionLevel.Tls12);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("GET /repos/{0}/{1}/commits HTTP/1.1", model.UserName, model.RepositoryName));
            sb.AppendLine(string.Format("Host: {0}", "api.github.com"));
            sb.AppendLine("Connection: close");
            sb.AppendLine("User-Agent: CSharp"); //nedeed
            sb.AppendLine("Accept: application/vnd.github.v3+json");
            sb.AppendLine();

            await this.send(sb.ToString());

            answer = await this.read();
            socket.Dispose();

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

        private async Task send(string message)
        {
            DataWriter writer;

            // Create the data writer object backed by the in-memory stream. 
            using (writer = new DataWriter(socket.OutputStream))
            {
                // Set the Unicode character encoding for the output stream
                writer.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                // Specify the byte order of a stream.
                writer.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

                // Gets the size of UTF-8 string.
                writer.MeasureString(message);
                // Write a string value to the output stream.
                writer.WriteString(message);

                // Send the contents of the writer to the backing stream.
                try
                {
                    await writer.StoreAsync();
                }
                catch (Exception exception)
                {
                    switch (SocketError.GetStatus(exception.HResult))
                    {
                        case SocketErrorStatus.HostNotFound:
                            // Handle HostNotFound Error
                            throw;
                        default:
                            // If this is an unknown status it means that the error is fatal and retry will likely fail.
                            throw;
                    }
                }

                await writer.FlushAsync();
                // In order to prolong the lifetime of the stream, detach it from the DataWriter
                writer.DetachStream();
            }
        }

        private async Task<String> read()
        {
            DataReader reader;
            StringBuilder strBuilder;

            using (reader = new DataReader(socket.InputStream))
            {
                strBuilder = new StringBuilder();

                // Set the DataReader to only wait for available data (so that we don't have to know the data size)
                reader.InputStreamOptions = Windows.Storage.Streams.InputStreamOptions.Partial;
                // The encoding and byte order need to match the settings of the writer we previously used.
                reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                reader.ByteOrder = Windows.Storage.Streams.ByteOrder.LittleEndian;

                // Send the contents of the writer to the backing stream. 
                // Get the size of the buffer that has not been read.
                await reader.LoadAsync(256);

                // Keep reading until we consume the complete stream.
                while (reader.UnconsumedBufferLength > 0)
                {
                    strBuilder.Append(reader.ReadString(reader.UnconsumedBufferLength));
                    await reader.LoadAsync(256);
                }

                reader.DetachStream();
                return strBuilder.ToString();
            }
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
