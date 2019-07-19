using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TechChallenge
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> urlList = new List<string>();
            urlList.Add(@"http://www.google.com");
            urlList.Add(@"https://localhost:5000");
            urlList.Add(@"http://127.0.0.1:8080/ ");
            urlList.Add(@"telnet://192.0.2.16:80/ ");
            urlList.Add(@"https://alexwohlbruck.github.io/cat-facts/");
            urlList.Add(@"https://memegenerator.net/img/images/200x/42.jpg");
            urlList.Add(@"https://memegenerator.net/img/images/200x/42.jpg ");
            urlList.Add(@"news:comp.infosystems.www.servers.unix");
            urlList.Add(@"https://www.youtube.com/results?search_query=test+search");
            urlList.Add(@"https://http.cat/200 ");
            urlList.Add(@"https://en.wikipedia.org/wiki/Uniform_Resource_Identifier#Examples ");
            urlList.Add(@"https://john.doe@www.example.com:123/forum/questions/?tag=networking&order=newest#top ");
            urlList.Add(@"https://www.google.com/search?q=google&rlz=1C1CHBF_enGB854GB854&oq=google&aqs=chrome ..69i57j0l2j69i60j69i59j35i39.831j0j4&sourceid=chrome&ie=UTF-8 ");
            urlList.Add(@"http://version1.api.memegenerator.net/Comment_Create?entityName=Instance&entityID=7262835 5&parentCommentID=&text=first%20post%20best%20post&apiKey=demo ");
            urlList.Add(@"http://version1.api.memegenerator.net//ContentFlag_Create?contentUrl=https://memegenerator.n et/JohnDoe&reason=personal%20information%20exposed&email=email@domain.com&apiKey=demo ");
            urlList.Add(@"http://version1.api.memegenerator.net//ContentFlag_Create?contentUrl=https%3A%2F%2Fmemeg enerator.net%2FJohnDoe%26reason%3Dpersonal%2520information%2520exposed%26email%3Demail%40domain.com% 26apiKey%3Ddemo ");

            foreach (var url in urlList)
            {
                Console.WriteLine(url);
                Console.WriteLine();
                Console.WriteLine();
                Urlcomponents urlComponentObject = GetPartsOfUrl(url);
                Console.WriteLine(" Scheme    " + urlComponentObject.Scheme);
                Console.WriteLine(" Authority " + urlComponentObject.Authority);
                Console.WriteLine(" Host      " + urlComponentObject.Host);
                Console.WriteLine(" Port      " + urlComponentObject.Port);
                Console.WriteLine(" Path      " + urlComponentObject.Path);
                Console.WriteLine(" Query     " + urlComponentObject.Query);
                Console.WriteLine(" Fragment  " + urlComponentObject.Fragment);
                Console.WriteLine("*********************************************************************************");
                Console.WriteLine();
                Console.ReadLine();




            }

        }
        public static Urlcomponents GetPartsOfUrl(string url)
        {
            var ifAuthority = url.Split('/').Length > 1 ? true : false;
            var urlComponents = new Urlcomponents();
            var urlComponentsArray = url.Split('/');
            urlComponents.Scheme = url.Split(':')[0];
            urlComponents.Authority = ifAuthority ? getAuthority(url) : "";
            urlComponents.Host = !string.IsNullOrEmpty(urlComponents.Authority) ? getHost(urlComponents.Authority) : "";
            urlComponents.Port = !string.IsNullOrEmpty(urlComponents.Authority) ? getPort(urlComponents.Authority) : "";
            urlComponents.Path = ifAuthority ? getPath(url, '/') : getPath(url, ':');
            urlComponents.Query = getQuery(url);
            urlComponents.Fragment = getFragment(url);


            return urlComponents;
        }

        private static string getPort(string authority)
        {
            var portString = "";
            portString = authority.Split(':').Length > 1 ? authority.Split(':')[1] : "";
            return portString;
        }

        private static string getHost(string AuthorityString)
        {
            var hostString = "";
            if (AuthorityString.Contains("@"))
            {
                hostString = AuthorityString.Split('@', ':').Length > 1 ? AuthorityString.Split('@', ':')[1] : "";
            }
            else if (AuthorityString.Contains(":"))
            {
                hostString = AuthorityString.Split(':')[0];
            }
            else
                hostString = AuthorityString;
            // hostString = string.IsNullOrEmpty(hostString) ? AuthorityString.Split('@').Length > 1 ? AuthorityString.Split('@')[1] : "" : hostString;
            return hostString;

        }

        private static string getFragment(string url)
        {
            var fragmentStringArray = url.Split('#');
            return fragmentStringArray.Length > 1 ? fragmentStringArray[1] : "";
        }

        private static string getQuery(string url)
        {
            var queryStringArray = url.Split('?');
            return queryStringArray.Length > 1 ? queryStringArray[1].Split('#')[0] : "";
        }

        private static string getPath(string url, char checkChar)
        {
            return checkChar == '/' && url.Split(checkChar).Length > 3 ? getFullPath(url.Split('/'), url) : checkChar == ':' ? url.Split(checkChar)[1] : "";
        }

        private static string getAuthority(string url)
        {
            return url.Split('/')[2];
        }

        public static string getFullPath(string[] pathString, string url)
        {
            var returnString = "";
            if (pathString.Length > 3)
            {
                for (int i = 0; i <= pathString.Length - 1; i++)
                {
                    if (i > 2)
                        returnString += pathString[i].Contains('?') || pathString[i].Contains('#') ? pathString[i].Split('?')[0] : "/" + pathString[i];
                }
            }
            else
            {
                returnString = pathString[3];
            }
            return returnString;

        }
    }
    public class Urlcomponents
    {
        public string Scheme { get; set; }
        public string Authority { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string Fragment { get; set; }
    }
}
