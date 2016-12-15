using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_website_server
{
    class HTTPRequest
    {
        // The HTTP Methods that our server will accept.
        private static string[] VALID_METHODS =
        {
            "GET", "HEAD", "POST",
            "PUT", "DELETE", "CONNECT",
            "OPTIONS", "TRACE", "PATCH"
        };

        // Process and validate our HTTPRequest string.
        public static HTTPRequest Process(string process)
        {
            // Check if our request string is empty.
            if(string.IsNullOrWhiteSpace(process) == true)
            {
                return null;
            }

            // Split the request into three segments.
            string[] tokens = process.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            
            // Make sure it's three segments.
            if(tokens.Length != 3)
            {
                return null;
            }
            
            // Check the Method.
            if(VALID_METHODS.Contains(tokens[0].ToUpper()) == false)
            {
                return null;
            }

            /* Display our Request string to the server.
             *    System.Windows.Forms.MessageBox.Show(string.Format(
             *      "Method: {0}"   + Environment.NewLine +
             *      "URL: {1}"      + Environment.NewLine +
             *      "Version: {2}"  + Environment.NewLine,
             *
             *      tokens[0],
             *      tokens[1],
             *      tokens[2]
             *  ));
             */
            
            // As far as we know, it's valid :)
            return new HTTPRequest(tokens[0], tokens[1], tokens[2]);
        }
        
        public string method = string.Empty;
        public string url_string = string.Empty;
        public string version = string.Empty;

        public HTTPRequest(string method, string url_string, string version)
        {
            this.method = method;
            this.url_string = url_string;
            this.version = version;
        }
    }
}
