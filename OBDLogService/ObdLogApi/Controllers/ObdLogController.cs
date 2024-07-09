namespace ObdLogApi.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using ObdLogApi.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Text;

    [Route("api/[controller]")]
    [ApiController]
    [DisableRequestSizeLimit, RequestSizeLimit(1073741824)]
    public class ObdLogController : ControllerBase
    {
        // GET api/ObdLog
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //using (var streamReader = new StreamReader("C:/Users/michael.stoyanov/Desktop/log2.txt"))
            //{
            //    using (var streamWriter = new StreamWriter("C:/Users/michael.stoyanov/Desktop/logs/log2.txt", true))
            //    {
            //        string line;
            //        int counter = 0;

            //        while ((line = streamReader.ReadLine()) != null)
            //        {
            //            counter++;

            //            if (line.Equals(string.Empty))
            //                continue;

            //            streamWriter.WriteLine(line);
            //        }
            //    }
            //}

            return new string[] { "Service is working fine!", "May the force be with you!" };
        }

        private IFormFile file;

        // POST: api/ObdLog
        [HttpPost]
        public HttpResponseMessage PostAsync()
        {
            string path = string.Empty;
            string line = string.Empty;
            string prevLine = string.Empty;
            //string fileName = string.Empty;
            string fullFilePath = string.Empty;

            string vinSerial = "";
            string vinString = "Vin ";
            string serialString = "Serial ";
            string nullLine = "null,null,null,null,null,null";
            string commmandsNamesLine = "Speed(km/h),RPM,Throttle(%),CoolantTemp(Cels),EngLoad(%),AbsLoad(%),Latitude,Longitude";

            var obdLogs = new List<ObdData>();
            StringBuilder sb = new StringBuilder();
            ObdData currLog = new ObdData();
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = "C:/Users/michael.stoyanov/Desktop/logs";
                else
                    path = "./app/bin/Release/netcoreapp2.2/publish/logs";

                file = HttpContext.Request.Form.Files.First();

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                using (var fileStream = new StreamReader(file.OpenReadStream()))
                {
                    while (!string.IsNullOrWhiteSpace(line = fileStream.ReadLine()))
                    {
                        if ((line.Contains(prevLine) && !string.IsNullOrWhiteSpace(prevLine))
                            || line.Equals(nullLine))
                            continue;

                        prevLine = line;

                        if (line.StartsWith(vinString) || line.StartsWith(serialString))
                        {
                            if (!string.IsNullOrWhiteSpace(sb.ToString()))
                                System.IO.File.AppendAllText(fullFilePath, sb.ToString().Trim() + Environment.NewLine);

                            string identifier = line.StartsWith(vinString) ? vinString : serialString;

                            sb.Clear();
                            fullFilePath = string.Empty;

                            //currLog.VinSerial = line.Substring(line.IndexOf(identifier) + identifier.Length);

                            fullFilePath = Path.Combine(path, vinSerial + ".csv");

                            if (!System.IO.File.Exists(fullFilePath))
                                System.IO.File.AppendAllText(fullFilePath, commmandsNamesLine + Environment.NewLine);
                        }
                        else
                        {
                            if (line.Contains("GPS: ") || line.Contains("GPSclass: "))
                            {
                                var emptyFields = ",,,,,,";
                                var gpsInfo = line.Substring(line.IndexOf("GPS: ") + 5);
                                                    //.Split(',')
                                                    //.Select(c => double.Parse(c.Trim()))
                                                    //.ToArray();

                                //currLog.Latitude = gpsInfo[0];
                                //currLog.Longitute = gpsInfo[1];

                                sb.AppendLine(emptyFields + gpsInfo);
                            }
                            else
                            {
                                //var carInfo = line.Split(',').ToList();

                                //if (line.Contains("null"))
                                //{
                                //    currLog.SpeedKmH = 0;
                                //}

                                //if (!carInfo[1].Contains(".") || !carInfo[1].Equals("null"))
                                //    currLog.Rpm = 0;

                                //currLog.SpeedKmH = !carInfo[0].Equals("null") ? int.Parse(carInfo[0]) : 0;

                                //currLog.Rpm = (carInfo[1].Contains(".") || !carInfo[1].Equals("null")) ? int.Parse(carInfo[1]) : 0;


                                //currLog.ThrottlePercentage = !carInfo[2].Equals("null") ? double.Parse(carInfo[2]) : 0.0;
                                //currLog.CoolantTempCels = !carInfo[3].Equals("null") ? int.Parse(carInfo[3]) : 0;
                                //currLog.EngLoadPercentage = !carInfo[4].Equals("null") ? double.Parse(carInfo[4]) : 0.0;
                                //currLog.AbsLoadPercentage = !carInfo[5].Equals("null") ? double.Parse(carInfo[5]) : 0.0;

                                sb.AppendLine(line);
                            }
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(vinSerial))
                    System.IO.File.AppendAllText(fullFilePath, sb.ToString() + Environment.NewLine);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }
    }
}