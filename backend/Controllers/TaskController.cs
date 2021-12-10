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
        [Route("filesave2")]
        public IActionResult PutFileNames2([FromBody] FileModel model)
        {
            Console.WriteLine("hi enterd");

            string webRootPath = environment.WebRootPath;
            string filesPath = Path.Combine(webRootPath, "files");
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string jsonPath = "";
            string[] state = model.Mode.Split("|");
            Console.WriteLine(state[0]);
            if (state[0] == "edit")
            {

                if (state[1] == "Open")
                {
                    Console.WriteLine("EDIT IN OPEN");
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\new.json";

                }
                else if (state[1] == "In Progress")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
                    Console.WriteLine("EDIT IN INPROGRESS");
                }
                else
                {
                    Console.WriteLine("EDIT IN FINISHED");
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\finisheddata.json";
                }
                var jsonString = System.IO.File.ReadAllText(jsonPath);

                Console.WriteLine("--------------------");
                object jsonObject = JsonConvert.DeserializeObject(jsonString);
                var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
                FileModel MovingItem = null;
                foreach (var i in list)
                {
                    Console.WriteLine(i.TaskName);
                    if (i.TaskName == model.TaskName)
                    {
                        i.TaskDescription = model.TaskDescription;
                        i.Priority = model.Priority;
                        MovingItem = i;
                    }
                }
                list.Remove(MovingItem);
                Console.WriteLine(model.Status);
                if (model.Status == "Open Task")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\new.json";
                }
                else if (model.Status == "In Progress")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
                }
                else
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\finished.json";
                }
                var jsonString2 = System.IO.File.ReadAllText(jsonPath);
                object jsonObject2 = JsonConvert.DeserializeObject(jsonString2);
                var list2 = JsonConvert.DeserializeObject<List<FileModel>>(jsonString2);
                list2.Add(MovingItem);
                var convertedJson = JsonConvert.SerializeObject(list2, Formatting.Indented);
                Console.WriteLine(convertedJson);
                Console.WriteLine(jsonPath);
                System.IO.File.WriteAllText(jsonPath, convertedJson);

            }
            else
            {
                if (model.Mode == "In Progress")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
                }
                else if (model.Mode == "Open Task")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\new.json";
                }
                else if (model.Mode == "Finished")
                {
                    jsonPath = currentDirectory + @"\wwwroot\ConfigData\finisheddata.json";

                }

                var jsonString = System.IO.File.ReadAllText(jsonPath);


                object jsonObject = JsonConvert.DeserializeObject(jsonString);
                var list = JsonConvert.DeserializeObject<List<FileModel>>(jsonString);
                list.Add(model);
                var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
                Console.WriteLine(convertedJson);
                Console.WriteLine(jsonPath);
                System.IO.File.WriteAllText(jsonPath, convertedJson);
            }
            return Ok();
        }


        [HttpGet()]
        [Route("filedelete")]
        public String GetFileDelete(string file)
        {
            Console.WriteLine(file);
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

            list.Remove(deletedItem);
            var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            System.IO.File.WriteAllText(json, convertedJson);
            if (deletedItem == null)
            {

                var currentDirectory = System.IO.Directory.GetCurrentDirectory();
                string jsonPath = currentDirectory + @"\wwwroot\ConfigData\inprogressdata.json";
                var jsonStrings = System.IO.File.ReadAllText(jsonPath);
                object jsonObjects = JsonConvert.DeserializeObject(jsonStrings);
                var list1 = JsonConvert.DeserializeObject<List<FileModel>>(jsonStrings);
                FileModel deletedItems = null;
                foreach (var listItems in list1)
                {


                    if (listItems.TaskName == fileProperties[0] && listItems.TaskDescription == fileProperties[1] && listItems.Priority == fileProperties[2])
                    {
                        deletedItems = listItems;
                    }
                }
                list1.Remove(deletedItems);

                var convertedJson2 = JsonConvert.SerializeObject(list1, Formatting.Indented);
                System.IO.File.WriteAllText(jsonPath, convertedJson2);
                if (deletedItems == null)
                {
                    var currentDirectory2 = System.IO.Directory.GetCurrentDirectory();
                    string jsonPath2 = currentDirectory2 + @"\wwwroot\ConfigData\finisheddata.json";
                    var jsonStrings2 = System.IO.File.ReadAllText(jsonPath2);
                    object jsonObjects2 = JsonConvert.DeserializeObject(jsonStrings2);
                    var list2 = JsonConvert.DeserializeObject<List<FileModel>>(jsonStrings2);
                    FileModel deletedItem2 = null;
                    foreach (var listItems in list2)
                    {


                        if (listItems.TaskName == fileProperties[0] && listItems.TaskDescription == fileProperties[1] && listItems.Priority == fileProperties[2])
                        {
                            deletedItem2 = listItems;
                        }
                    }
                    list2.Remove(deletedItem2);

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
            string jsonPath = currentDirectory + @"\wwwroot\ConfigData\new.json";
            return jsonPath;
        }

    }
}