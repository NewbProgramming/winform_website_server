using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_website_server
{
    class HTTPHeader
    {
        public static HTTPHeader Process(string process)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            
            string[] lines = process.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            
            string key = string.Empty;
            string value = string.Empty;
            int index = -1;
            
            foreach(string line in lines)
            {
                index = line.IndexOf(": ");
                
                if(index == -1)
                {
                    return null;
                }
                
                key = line.Substring(0, index);
                value = line.Substring((index + 1));

                headers.Add(key, value);
            }
            return new HTTPHeader(headers);
        }

        public Dictionary<string, string> headers = new Dictionary<string, string>();

        public HTTPHeader(Dictionary<string, string> headers)
        {
            this.headers = headers;
        }
    }
}
