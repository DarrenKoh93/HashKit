using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Analyse
    {
        public static string HashList = HttpContext.Current.Server.MapPath("~/App_Data/HashList.json");
        public string plainText { get; set; }
        public string hashType { get; set; }
        public List<decimal> hashTiming { get; set; }
        public string hashText { get; set; }

        public static List<Analyse> GetHashList()
        {
            List<Analyse> HashLists = new List<Analyse>();
            if (File.Exists(HashList))
            {
                string content = File.ReadAllText(HashList);
                HashLists = JsonConvert.DeserializeObject<List<Analyse>>(content);
                return HashLists;
            }
            else
            {
                // Create the file 
                File.Create(HashList).Close();
                File.WriteAllText(HashList, "[]");
                //Recursion
                GetHashList();
            }
            return HashLists;
        }
    }
}
