using System.Collections.Generic;
using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class TaskController : ControllerBase
    {
        IWebHostEnvironment environment;
        public TaskController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }


        [HttpPut]
        [Route("filesave")]
        public IActionResult PutFileNames([FromBody] FileModel model)
        {
            Console.WriteLine("check");
            string webRootPath = environment.WebRootPath;
            string filesPath = Path.Combine(webRootPath, "files");
            string json = JsonFilePath();
            var jsonString = System.IO.File.ReadAllText(json);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
            list.Add(model);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(json, convertedJson);
            return Ok();
        }

        [HttpPut]
        [Route("filesave2")]
        public IActionResult PutFileNames2([FromBody] FileModel model)
        {
            Console.WriteLine("check");
            string webRootPath = environment.WebRootPath;
            string filesPath = Path.Combine(webRootPath, "files");
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
            string json = jsonPath;
            var jsonString = System.IO.File.ReadAllText(json);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
            list.Add(model);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(json, convertedJson);
            return Ok();
        }
        [HttpPut]
        [Route("filesave3")]
        public IActionResult PutFileNames3([FromBody] FileModel model)
        {
            Console.WriteLine("check");
            string webRootPath = environment.WebRootPath;
            string filesPath = Path.Combine(webRootPath, "files");
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\finisheddata.json";
            string json = jsonPath;
            var jsonString = System.IO.File.ReadAllText(json);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
            list.Add(model);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(json, convertedJson);
            return Ok();
        }

        [HttpGet()]
        [Route("filedelete")]
        public String GetFileDelete(string file)
        {
            string[] fileProperties = file.Split('|');

            string json = JsonFilePath();
            var jsonString = System.IO.File.ReadAllText(json);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
            FileModel deletedItem = null;
            foreach (var listItem in list)
            {
                if (listItem.TaskName == fileProperties[0] && listItem.TaskDescription == fileProperties[1] && listItem.Priority == fileProperties[2])
                {
                    deletedItem = listItem;
                }
            }
            Console.WriteLine("HJGJK  " + deletedItem);
            list.Remove(deletedItem);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(json, convertedJson);
            if (deletedItem == null)
            {
                Console.WriteLine("null  " + deletedItem);
                var currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
                var jsonStrings = System.IO.File.ReadAllText(jsonPath);
                object jsonObjects = JsonConvert.DeserializeObject(jsonStrings);
                var list1 = JsonConvert.DeserializeObject<List<FileModel>>(jsonStrings);
                FileModel deletedItems = null;
                foreach (var listItems in list1)
                {
                    Console.WriteLine(listItems.TaskName);
                    if (listItems.TaskName == fileProperties[0] && listItems.TaskDescription == fileProperties[1] && listItems.Priority == fileProperties[2])
                    {
                        deletedItems = listItems;
                    }
                }
                list1.Remove(deletedItems);
                for (var i = 0; i < list1.Count; i++)
                {
                    Console.WriteLine(list1[i]);
                }
                var convertedJson2 = JsonConvert.SerializeObject(list1, Formatting.Indented);
                System.IO.File.WriteAllText(jsonPath, convertedJson2);
                if (deletedItems == null)
                {

                    Console.WriteLine("null  " + deletedItem);
                    var currentDirectory2 = System.IO.Directory.GetCurrentDirectory();
                    string jsonPath2 = currentDirectory2 + @"\wwwroot\ConfigData\finisheddata.json";
                    var jsonStrings2 = System.IO.File.ReadAllText(jsonPath2);
                    object jsonObjects2 = JsonConvert.DeserializeObject(jsonStrings2);
                    var list2 = JsonConvert.DeserializeObject<List<FileModel>>(jsonStrings2);
                    FileModel deletedItem2 = null;
                    foreach (var listItems in list2)
                    {
                        Console.WriteLine(listItems.TaskName);
                        if (listItems.TaskName == fileProperties[0] && listItems.TaskDescription == fileProperties[1] && listItems.Priority == fileProperties[2])
                        {
                            deletedItem2 = listItems;
                        }
                    }
                    list2.Remove(deletedItem2);
                    for (var i = 0; i < list2.Count; i++)
                    {
                        Console.WriteLine(list2[i]);
                    }
                    var convertedJson3 = JsonConvert.SerializeObject(list2, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonPath2, convertedJson3);

                }
            }



            return "File data deleted successfully";
        }

        [HttpGet]
        [Route("getdefaults")]

        public object GetDefaults(string initialize)
        {
            string json = JsonFilePath();
            var jsonString = System.IO.File.ReadAllText(json);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            return jsonString;
        }
        [HttpGet]
        [Route("getinprogressdefaults")]

        public object GetInProgressDefaults(string initialize)
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
            var jsonString = System.IO.File.ReadAllText(jsonPath);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            return jsonString;
        }
        [HttpGet]
        [Route("getfinisheddefaults")]

        public object GetFinishedDefaults(string initialize)
        {
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\finisheddata.json";
            var jsonString = System.IO.File.ReadAllText(jsonPath);
            object jsonObject = JsonConvert.DeserializeObject(jsonString);
            return jsonString;
        }
        public string JsonFilePath()
        {


            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\data.json";
            Console.WriteLine(jsonPath);
            return jsonPath;
        }

    }
}