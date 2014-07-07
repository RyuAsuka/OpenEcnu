using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenEcnuSDK;
using OpenEcnuSDK.Entities;

namespace SdkTestConsole
{
    class Program
    {
        private readonly static Oauth oauth = new Oauth();
        static void Main(string[] args)
        {
            Process.Start("http://localhost:2718/oauth/authorize?client_id=6&redirect_uri=http://127.0.0.1&response_type=code");
            string code = Console.ReadLine();

            oauth.GetAccessToken("6", "abxjAzgRUady3BqpSVQK", "http://127.0.0.1", code);

            Console.WriteLine("{0},{1}",oauth.Token.user_id, oauth.Token.access_token);
            Console.ReadLine();
        }
    }
}
