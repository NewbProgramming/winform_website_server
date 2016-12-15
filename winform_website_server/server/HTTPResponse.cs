using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_website_server
{
    class HTTPResponse
    {
        public static HTTPResponse Process(HTTPRequest request, HTTPHeader header, string content)
        {
            return new HTTPResponse(request, header, content);
        }

        public string message = string.Empty;

        public HTTPResponse(HTTPRequest request, HTTPHeader header, string content)
        {
            string html = string.Empty;
            
            // HTML
            html += "<h2>Request</h2><p>";
            html += "Method: " + request.method + "<br />";
            html += "URL: " + request.url_string + "<br />";
            html += "HTTP Version: " + request.version + "<br />";
            html += "</p>";
            html += "<h2>Headers</h2><p>";
            
            foreach(KeyValuePair<string, string> item in header.headers)
            {
                html += item.Key + ": " + item.Value + "<br />";
            }
            
            html += "</p>";
            
            if(string.IsNullOrWhiteSpace(content) == false) {
                html += "<h2>Content</h2><p>";
                html += content + "<br />";
                html += "</p>";
            }
            
            // Response
            message += "HTTP/1.1" + " " + "404" + " " + "Not Found"         + '\n';
            
            html += "<h2>Response</h2><p>";
            html += message;
            html += "</p>";
            
            message += "Server"         + ": " + "C# Website Server (x64)"  + '\n';
            message += "Content-Length" + ": " + html.Length.ToString()     + '\n';
            message += "Content-Type"   + ": " + "text/html"                + '\n';
            message += "Connection"     + ": " + "closed"                   + '\n';
            message +=                                                        '\n';
            message += html;
        }
    }
}
