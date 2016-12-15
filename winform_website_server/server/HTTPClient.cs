using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace winform_website_server
{
    class HTTPClient
    {
        public static HTTPClient Handle(TcpClient tcp)
        {
            return new HTTPClient(tcp);
        }

        private TcpClient tcp = null;

        private Thread worker = null;

        public bool IsActive
        {
            get {
                return (this.worker.IsAlive == true);
            }
        }

        public HTTPClient(TcpClient tcp)
        {
            this.tcp = tcp;
        }

        public void Start()
        {
            // Start our client's worker thread.
            this.worker = new Thread(new ThreadStart(Work));
            this.worker.Start();
        }
        
        public void Stop()
        {
            // Abort the worker thread and close the connection.
            this.worker.Abort();
            this.tcp.Close();
        }

        private TcpState GetState() {
            // Check our Tcp's state.
            TcpConnectionInformation info = IPGlobalProperties.GetIPGlobalProperties()
                .GetActiveTcpConnections()
                .SingleOrDefault(x => x.LocalEndPoint.Equals(this.tcp.Client.LocalEndPoint));
            return (info != null) ? info.State : TcpState.Unknown;
        }

        private void Work()
        {
            // 
            bool closed = false;
            
            // Retrieve the client's ip address.
            IPAddress ip = ((IPEndPoint) this.tcp.Client.RemoteEndPoint).Address;
            
            // Get the stream.
            StreamReader stream = new StreamReader(this.tcp.GetStream());
            
            // Our HTTP data.
            HTTPRequest request = null;
            HTTPHeader header = null;
            HTTPResponse response = null;
            
            // Loop while the connection is still open.
            while(closed == false)
            {
                // Check if there's new data on our network stream.
                if(stream.Peek() != -1)
                {
                    // Handle the request.
                    RetrieveRequest(stream, out request);
                    
                    if(request != null)
                    {
                        RetrieveHeaders(stream, out header);
                        RetrieveResponse(request, header, stream, out response);
                        
                        Send(response);
                        
                        request = null;
                        header = null;
                        response = null;
                    }

                    this.tcp.Close();
                    closed = true;
                }
            }
        }

        // Try to get the request from the network stream.
        private void RetrieveRequest(StreamReader stream, out HTTPRequest request)
        {
            request = HTTPRequest.Process(stream.ReadLine());
        }

        // Try to get the headers from the network stream.
        private void RetrieveHeaders(StreamReader stream, out HTTPHeader header)
        {
            // Start with an empty string.
            string process = string.Empty;
            
            // While there's still data.
            while(stream.Peek() != -1)
            {
                // Read the next line.
                process += stream.ReadLine() + '\n';
                
                // If line is empty, break out of our loop.
                if(string.IsNullOrWhiteSpace(process) == true)
                {
                    break;
                }
            }

            // Process the headers.
            header = HTTPHeader.Process(process);
        }

        // Based on Request, Headers, and Content, retrieve a response.
        private void RetrieveResponse(HTTPRequest request, HTTPHeader header, StreamReader stream, out HTTPResponse response)
        {
            // We're going to retrieve the rest of the request.
            string content = string.Empty;
            
            // While there's still data.
            while(stream.Peek() != -1)
            {
                // Retrieve it.
                content += stream.Read();
            }
            
            // Process our response.
            response = HTTPResponse.Process(request, header, content);
        }

        // Send a response to our client.
        private void Send(HTTPResponse response)
        {
            byte[] stream = Encoding.UTF8.GetBytes(response.message.ToArray<char>(), 0, response.message.Length);
            
            this.tcp.GetStream().Write(stream, 0, stream.Length);
        }
    }
}
