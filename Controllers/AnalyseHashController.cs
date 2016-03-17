using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApplication1
{
    public class AnalyseHashController : Controller
    {
        // GET: Hash List
        public ActionResult Details()
        {
            var hash = Analyse.GetHashList();
            //Save Button Event 
            if (Request.Form["Save"] != null)
            {
                var plain = Request.Form["txtPlain"].ToString();
                var type = Request.Form["drphashType"].ToString();
                var hText = "";
                List<decimal> hTiming = new List<decimal>();

                //start the stopwatch event to get the starting time
                if (type.Equals("SHA1")) //if hashing type to sha1
                {
                    Stopwatch sw = new Stopwatch(); 
                    for (int i = 0; i < 8; i++)
                    {
                        sw.Restart();                           //restart the stopwatch  
                        customSHA1 sh1Hash = new customSHA1();              //calling the function for convertion
                        hText = sh1Hash.CalculateSHA1(plain);   //convert string to hashed string 
                        sw.Stop();                              //stop the stop watch after hashing module return with value
                        hTiming.Add(Convert.ToDecimal(sw.Elapsed.TotalMilliseconds)); //store the time taken in the list 
                    }
                }
                else if (type.Equals("MD5")) //hashing type to MD5
                {
                    Stopwatch sw = new Stopwatch(); 
                    for (int i = 0; i < 8; i++)
                    {
                        sw.Restart();                               //restart the stopwatch  
                        customMD5 md5Hash = new customMD5 { Value = plain };    //calling the function for convertion
                        hText = md5Hash.FingerPrint;                //convert string to hashed string
                        sw.Stop();                                  //stop the stop watch after hashing module return with value
                        hTiming.Add(Convert.ToDecimal(sw.Elapsed.TotalMilliseconds));   //store the time taken in the list 
                    }
                }
                //Need only 2 data to compute the hashing string and time taken
                Analyse aNa = new Analyse()
                {
                    plainText = plain,      //PlainText value
                    hashType = type,        //Hashing Type
                    hashText = hText,       //Hashing Text
                    hashTiming = hTiming   //Total Hashing time 
                };

                //Deserialize the json to object data
                var HashList = Analyse.HashList;
                var HashData = System.IO.File.ReadAllText(HashList);
                List<Analyse> hList = new List<Analyse>();
                hList = JsonConvert.DeserializeObject<List<Analyse>>(HashData);

                //if dynamic list is null or empty
                if (hList == null)

                { hList = new List<Analyse>(); } //declare new list  

                hList.Add(aNa); //add the object to the list 
                //append json file (since there are no database, we need to store the value at some other place)
                System.IO.File.WriteAllText(HashList, JsonConvert.SerializeObject(hList));
                hash = Analyse.GetHashList(); //Reload the data list after insert
            }

            // Load the data for the hash json
            return View(hash);
        }
    }


}
