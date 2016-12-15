using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace winform_website_server
{
    class HTTPServer
    {
        // Our Tcp connection information.
        public IPAddress host = IPAddress.Any;
        public int port = 80;

        // Our HTTPClients.
        private List<HTTPClient> clients = new List<HTTPClient>();

        // Our thread and Tcp listener.
        private TcpListener listener = null;
        private Thread worker = null;

        public HTTPServer()
        {
        }

        public HTTPServer(IPAddress ip, int port = 80)
        {
            this.host = ip;
            this.port = port;
        }

        public void Start()
        {
            // Check if our worker is currently active.
            if(this.worker != null && this.worker.IsAlive == true)
            {
                return;
            }
            
            // Start our worker thread that will listen for Tcp requests.
            this.worker = new Thread(new ThreadStart(Work));
            this.worker.Start();
        }
        
        public void Stop()
        {
            // Check if our worker is inactive.
            if(this.worker == null || this.worker.IsAlive == false)
            {
                return;
            }
            
            // Abort the worker.
            this.worker.Abort();
            
            // Close the connection to all clients.
            foreach(HTTPClient client in this.clients)
            {
                if(client == null || client.IsActive == true) {
                    client.Stop();
                }
            }

            this.clients.Clear();
            
            // Stop listening for Tcp requests.
            this.listener.Stop();
        }

        private void Work()
        {
            // Listen for Tcp requests.
            this.listener = new TcpListener(this.host, this.port);
            this.listener.Start();
            
            // Clear our HTTPClient list.
            this.clients.Clear();
            
            // Loop endlessly (Until aborted).
            while(true) {
                // Remove inactive HTTPClients from the server.
                this.clients.RemoveAll(client => (client == null || client.IsActive == false));
                
                // Check for pending Tcp requests.
                while(this.listener.Pending() == true) {
                    // Have our HTTPClient validate the request.
                    HTTPClient client = HTTPClient.Handle(this.listener.AcceptTcpClient());
                    
                    // Check if it's a valid HTTPClient.
                    if(client != null)
                    {
                        // Start its worker.
                        client.Start();
                        
                        // Add it to the HTTPClient list.
                        this.clients.Add(client);
                    }
                }
            }
        }
    }
}
