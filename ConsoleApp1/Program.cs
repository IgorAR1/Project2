using System;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;

public class FileWork
{
    public string[] ReadFile()
    {
        Console.WriteLine("Paste web::Example::https://www.simbirsoft.com/");
        string[] site = { Console.ReadLine() };
        return site;
    }
}

public class HTMLUploader
{

    public string[] site { get; private set; }

    public HTMLUploader(string[] sites)
    {
        site = sites;
    }

    public void SaveHTMLPages()
    {


        using (WebClient client = new WebClient())
        {
            string directory = Directory.GetCurrentDirectory();
            Console.WriteLine(site[0].ToString());
            string html = client.DownloadString(site[0].ToString());
            File.WriteAllText(directory + @"\" + 0 + ".html", html);
            Console.WriteLine("File save");
        }
    }



    class TextHandler
    {

        public string[] site { get; private set; }

        public TextHandler(string[] sites)
        {
            site = sites;
        }

        public void Work()
        {
            Console.WriteLine(site);
            string text = ReadTextFromSite(site[0]);

            if (text == null)
            {
                Console.WriteLine("No Text");
            }

            var result = Calc(text);
            result.Remove("");
            foreach (var pair in result)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        }

        static Dictionary<string, int> Calc(string site)
        {
            var res = new Dictionary<string, int>();

            foreach (var word in site.Split
                (' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t').Skip(1))
            {
                var count = 0;
                res.TryGetValue(word, out count);
                res[word] = count + 1;
            }

            return res;
        }


        public string ReadTextFromSite(string site)
        {
            HtmlWeb htmlWeb = new HtmlWeb();

            HtmlAgilityPack.HtmlDocument document = htmlWeb.Load(site);
            return document.DocumentNode.InnerText;
        }


    }

    class Program
    {
        static void Main(string[] args)
        {

            Program program = new Program();
            program.Start();

        }

        public void Start()
        {
            FileWork file = new FileWork();
            HTMLUploader htmlUploader;
            TextHandler textHandler;
            var urldata = file.ReadFile();
            Console.WriteLine(urldata);
            htmlUploader = new HTMLUploader(urldata);
            htmlUploader.SaveHTMLPages();
            textHandler = new TextHandler(urldata);
            textHandler.Work();
            Console.ReadKey();
        }
    }
}



