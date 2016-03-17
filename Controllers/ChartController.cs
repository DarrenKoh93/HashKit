using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;

namespace WebApplication1.Controllers
{

    public class ChartController : Controller
    {
        public const string ImageMap = "Chart-ImageMap";

        // GET: Chart
        public ActionResult Details()
        {
            if (Request.Form["Export"] != null && Request.Form["FORMAT"].Equals("PDF"))
            {

                GetPdf();
            }
            else if (Request.Form["Export"] != null && Request.Form["FORMAT"].Equals("PNG"))
            { GetPng(); }
            return View();
        }


        public static Chart CreateChart()
        {
            //HashList Chart
            var HashList = Analyse.HashList;
            var chart = new Chart() { Width = 600, Height = 400 };
            chart.Legends.Add(new Legend("legend1") { Docking = Docking.Right });
            var title = new Title("Graph Of Number Of Attributes vs. Total Time Taken", Docking.Top, new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold), System.Drawing.Color.Brown);
            chart.Titles.Add(title);
            chart.ChartAreas.Add("Area 1");

            //CollisionList Chart
            //var CollisionList = Collision.CollisionList;
            var CollisionList = Analyse.HashList;
            var CollisionChart = new Chart() { Width = 600, Height = 400 };
            CollisionChart.Legends.Add(new Legend("legend2") { Docking = Docking.Right });
            var CollisionTitle = new Title("Graph Of Number of Collision", Docking.Top, new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold), System.Drawing.Color.Brown);
            CollisionChart.Titles.Add(CollisionTitle);
            CollisionChart.ChartAreas.Add("Area 2");

            #region Read and Deserialize Data
            var HashData = System.IO.File.ReadAllText(HashList);
            List<Analyse> hList = new List<Analyse>();
            hList = JsonConvert.DeserializeObject<List<Analyse>>(HashData);

            //var CollisionData = System.IO.File.ReadAllText(CollisionList);
            //var CollisionData = System.IO.File.ReadAllText(CollisionList);
            var CollisionData = System.IO.File.ReadAllText(HashList);
            //List<Analyse> cList = new List<Collision>();
            List<Analyse> cList = new List<Analyse>();
            //cList = JsonConvert.DeserializeObject<List<Collision>>(CollisionData);
            cList = JsonConvert.DeserializeObject<List<Analyse>>(CollisionData);
            #endregion
            decimal maxVal = 0;
            decimal maxVal2 = 0;

            if (hList.Count > 0)
            {
                maxVal = hList[0].hashTiming[0];
                for (int i = 0; i < hList.Count; i++)
                {
                    List<String> yFie = new List<String>();
                    for (int o = 0; o < hList[i].hashTiming.Count; o++)
                    {
                        if (hList[i].hashTiming[o] > maxVal)
                        { maxVal = hList[i].hashTiming[o]; }
                    }
                    string name = hList[i].plainText + "(" + hList[i].hashType + ")";
                    chart.Series.Add(name);
                    chart.Series[name].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                    for (int t = 0; t < hList[i].hashTiming.Count; t++)
                    {
                        chart.Series[name].Points.AddY(hList[i].hashTiming[t]);
                    }
                }
            }
            return chart;
        }

        public static Chart CreateChart2()
        {
            //CollisionList Chart
            //var CollisionList = Collision.CollisionList;
            var CollisionList = Analyse.HashList;
            var CollisionChart = new Chart() { Width = 600, Height = 400 };
            CollisionChart.Legends.Add(new Legend("legend2") { Docking = Docking.Right });
            var CollisionTitle = new Title("Graph Of Number of Collision", Docking.Top, new System.Drawing.Font("Arial", 15, System.Drawing.FontStyle.Bold), System.Drawing.Color.Brown);
            CollisionChart.Titles.Add(CollisionTitle);
            CollisionChart.ChartAreas.Add("Area 2");

            #region Read and Deserialize Data
            //var CollisionData = System.IO.File.ReadAllText(CollisionList);
            //var CollisionData = System.IO.File.ReadAllText(CollisionList);
            var CollisionData = System.IO.File.ReadAllText(CollisionList);
            //List<Analyse> cList = new List<Collision>();
            List<Analyse> cList = new List<Analyse>();
            //cList = JsonConvert.DeserializeObject<List<Collision>>(CollisionData);
            cList = JsonConvert.DeserializeObject<List<Analyse>>(CollisionData);
            #endregion
            decimal maxVal2 = 0;

            if (cList.Count > 0)
            {
                //maxVal2 = cList[0].Collision[0];
                maxVal2 = cList[0].hashTiming[0];
                for (int i = 0; i < cList.Count; i++)
                {
                    List<String> yFie2 = new List<String>();
                    //for (int p = 0; p < cList[i].Collision.Count; p++)
                    for (int p = 0; p < cList[i].hashTiming.Count; p++)
                    {
                        //if (cList[i].Collision[p] > maxVal2)
                        if (cList[i].hashTiming[p] > maxVal2)
                        //{ maxVal2 = cList[i].Collision[p]; }
                        { maxVal2 = cList[i].hashTiming[p]; }
                    }
                    string col = cList[i].hashText + "(" + cList[i].hashType + ")";
                    CollisionChart.Series.Add(col);
                    CollisionChart.Series[col].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                    //for (int t = 0; t < cList[i].NumOfTries.Count; t++)
                    for (int t = 0; t < cList[i].hashTiming.Count; t++)
                    {
                        //chart.Series[col].Points.AddY(cList[i].NumOfTries[t]);
                        CollisionChart.Series[col].Points.AddY(cList[i].hashTiming[t]);
                    }
                }
            }
            return CollisionChart;
        }

        public static string getCImage(Chart chart)
        {
            using (var stream = new MemoryStream())
            {
                var img = "<img src='data:image/png;base64,{0}' alt='' usemap='#" + ImageMap + "'>";
                chart.SaveImage(stream, ChartImageFormat.Png);
                var encoded = Convert.ToBase64String(stream.ToArray());
                return string.Format(img, encoded);
            }
        }

        public ActionResult DisplayChart()
        {
            var chart = CreateChart();
            var result = new StringBuilder();
            result.Append(getCImage(chart));
            result.Append(chart.GetHtmlImageMap(ImageMap));
            return Content(result.ToString());
        }

        // May not require!!!
        public ActionResult DisplayChart2()
        {
            var CollisionChart = CreateChart2();
            var CollisionResult = new StringBuilder();
            CollisionResult.Append(getCImage(CollisionChart));
            CollisionResult.Append(CollisionChart.GetHtmlImageMap(ImageMap));
            return Content(CollisionResult.ToString());
        }

        public void GetPdf()
        {
            Document Doc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var memoryStream2 = new MemoryStream();
                CreateChart().SaveImage(memoryStream, ChartImageFormat.Png);
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
                img.ScalePercent(75f);
                Doc.Add(img);

                CreateChart2().SaveImage(memoryStream2, ChartImageFormat.Png);
                iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(memoryStream2.GetBuffer());
                img2.ScalePercent(75f);
                Doc.Add(img2);
                Doc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Chart.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(Doc);
                Response.End();
            }

            /*using (MemoryStream memoryStream2 = new MemoryStream())
            {
                CreateChart2().SaveImage(memoryStream2, ChartImageFormat.Png);
                iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(memoryStream2.GetBuffer());
                img2.ScalePercent(75f);
                Doc.Add(img2);
                Doc.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Chart.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(Doc);
                Response.End();
            }*/
        }
        public void GetPng()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CreateChart().SaveImage(memoryStream, ChartImageFormat.Png);
                memoryStream.WriteTo(Response.OutputStream);
                Response.ContentType = "image/png";
                Response.AddHeader("content-disposition", "attachment;filename=Chart.png");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(Response.OutputStream);
                Response.End();
            }
        }
    }
}