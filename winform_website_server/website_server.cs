using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_website_server
{
    public partial class website_server : Form
    {
        private HTTPServer server = new HTTPServer();
        
        // Send the server host to a website.
        private static void GoToSite(string url)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd", "/C start" + " " + url)
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false
            };
            Process.Start(psi);
        }

        // Microsoft's auto-generated Designer.
        public website_server()
        {
            InitializeComponent();
        }

        // Our program sent the load event.
        private void website_server_Load(object sender, EventArgs e)
        {
            // Send our program to the background tasks.
            this.Hide();
            
            // Start our HTTP server.
            this.server.Start();
        }

        // Someone clicked on the "Exit" button from Icon > Menu > Exit.
        private void icon_menu_exit_Click(object sender, EventArgs e)
        {
            // Stop our HTTP server.
            this.server.Stop();
            
            // Exit the program.
            Application.Exit();
        }

        // Someone double-clicked on the Taskbar Icon.
        private void icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Convert the hostname.
            string hostname = (this.server.host == IPAddress.Any) ? "127.0.0.1" : this.server.host.ToString();
            
            // Send them to the index of the website.
            GoToSite("http://" + hostname + ":" + this.server.port.ToString());
        }
    }
}
