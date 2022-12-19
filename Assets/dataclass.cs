using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.Windows;

public class dataclass : MonoBehaviour
{

    public FishClass[] fisharray = new FishClass[]
        {
            new FishClass("Iowa darter", "test","JSON/Images/Fish/redfish.png", 8,true),
            new FishClass("Black crappie", "", "JSON/Images/Fish/bluefish.png", 8,true),
            new FishClass("channel catfish", "", null, 8,false),
            new FishClass("Yellow perch", "", null, 8,false),
            new FishClass("Rock bass", "", null, 8,false),
            new FishClass("Horneyhard chub", "", null, 8,false),
            new FishClass("Sand shiner", "", null,8,false),
            new FishClass("Southern redbelly dace", "", null, 8,false),
            new FishClass("Golden shiner", "", null, 10,false),
            new FishClass("Northern pike", "", null, 10,true),
            new FishClass("Largemouth bass", "", null, 10,true),
            new FishClass("Bluntnose minnow", "", null, 10,false),
            new FishClass("Johhny darter", "", null, 10,true),
            new FishClass("Common shiner", "", null, 10,false),
            new FishClass("Creek chub", "", null, 12,false),
            new FishClass("Fathead minnow", "", null, 12,false),
            new FishClass("Green sunfish", "", null, 12,false),
            new FishClass("White sucker", "", null, 12,false),
            new FishClass("Brook stickeback", "", null, 12,true)
        };
    public SimStep[] SimStepArray = new SimStep[]
        {
            new SimStep("Inital simstep","descriptipn","Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id","JSON/Images/property0.png","JSON/Images/testback.png",0),
            new SimStep("Small footprint","7% Impervious","All fish survived this footprint","JSON/Images/property.png","JSON/Images/testback2.png",7),
            new SimStep("Medium footprint","10% Impervious","Some Fish survived this footprint","JSON/Images/property2.png","JSON/Images/testback3.png",10),
            new SimStep("Large footprint","15% Impervious","No Fish survived this footprint","JSON/Images/property3.png","JSON/Images/testback4.png",15),
        };
    public GenSettings GenSetting = new GenSettings(
            "start title",
            "start desc - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            "end title",
            "end desc - Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
            "JSON/Images/bulldozer.png",
            "JSON/Images/sapling.png",
            "JSON/Images/skull.png",
            "JSON/Images/start.png",
            "JSON/Images/end.png");

    /*
     * DATA FROM POWERPOINT
     *  === less than 8%
     *  Iowa darter
     *  Black crappie
     *  Channel catfish
     *  Yellow perch
     *  Rock bass
     *  Horneyhard chub
     *  Sand Shiner
     *  Southern redbelly dace
     *  === 10% - 12%
     *  Golden shiner
     *  Northern pike
     *  Largemouth bass
     *  Bluntnose minnow
     *  Johnny darter
     *  Common shiner
     *  === more than 12%
     *  Creek chub
     *  Fathead minnow
     *  Green sunfish
     *  White sucker
     *  Brook stickleback
     */

    public class FishClass
    {

        public string fishname;
        public string fishdesc;
        public string fishpath;//You can't put an image in a json file
        public int impermthreshold;
        public Boolean major;
        [JsonIgnore]public Sprite fishpic;//so we need to write the path to the image and then load it in later
        public FishClass(string fishname, string fishdesc,  string fishpath, int impermthreshold, bool major)
        {
            this.fishname = fishname;
            this.fishdesc = fishdesc;
            this.fishpath = fishpath;
            this.impermthreshold = impermthreshold;
            this.major = major;
        }
    }

    public class SimStep
    {

        public string stepname;
        public string stepdesc;
        public string infobox;
        public string propertypicpath;
        [JsonIgnore] public Sprite propertypic;
        public string aerialpicpath;
        [JsonIgnore] public Sprite aerialpic;
        public int imperm;

        public SimStep(string stepname, string stepdesc, string infobox, string propertypicpath, string aerialpicpath, int imperm)
        {
            this.stepname = stepname;
            this.stepdesc = stepdesc;
            this.infobox = infobox;
            this.propertypicpath = propertypicpath;
            this.aerialpicpath = aerialpicpath;
            this.imperm = imperm;
        }
    }

    public class GenSettings
    {
        public string starttitle;
        public string startdesc;
        public string endtitle;
        public string enddesc;
        public string forwardpicpath;
        public string backwardpicpath;
        public string deathpicpath;
        public string startpicpath;
        public string endpicpath;
        [JsonIgnore] public Sprite forwardpic;
        [JsonIgnore] public Sprite backwardpic;
        [JsonIgnore] public Sprite deathpic;
        [JsonIgnore] public Sprite startpic;
        [JsonIgnore] public Sprite endpic;
        public GenSettings(string starttitle, string startdesc, string endtitle, string enddesc, string forwardpicpath, string backwardpicpath, string deathpicpath, string startpicpath, string endpicpath)
        {
            this.starttitle = starttitle;
            this.startdesc = startdesc;
            this.endtitle = endtitle;
            this.enddesc = enddesc;
            this.forwardpicpath = forwardpicpath;
            this.backwardpicpath = backwardpicpath;
            this.deathpicpath = deathpicpath;
            this.startpicpath = startpicpath;
            this.endpicpath = endpicpath;
            
        }
    }

    public void Start()
    {

        //Build JSON files based off of placeholder data
        //will overwrite any existing files, only do this if you lose them somehow.
        //if (!System.IO.File.Exists("JSON/Fish.json"))
        //{
        //    System.IO.File.WriteAllText("JSON/Fish.json", JsonConvert.SerializeObject(fisharray, Formatting.Indented));
        //}
        //if (!System.IO.File.Exists("JSON/Sim.json"))
        //{
        //    System.IO.File.WriteAllText("JSON/Sim.json", JsonConvert.SerializeObject(SimStepArray, Formatting.Indented));
        //}
        //if (!System.IO.File.Exists("JSON/Settings.json"))
        //{
        //    System.IO.File.WriteAllText("JSON/Settings.json", JsonConvert.SerializeObject(GenSetting, Formatting.Indented));
        //}

        //pull JSON data and replace placeholder data with it
        //To make this more robust later, have it look in multiple folders so the diffrent OS versions of the program can read
        //the JSON from the same folder
        fisharray = JsonConvert.DeserializeObject<FishClass[]>(System.IO.File.ReadAllText("JSON/Fish.json"));
        SimStepArray = JsonConvert.DeserializeObject<SimStep[]>(System.IO.File.ReadAllText("JSON/Sim.json"));
        GenSetting = JsonConvert.DeserializeObject<GenSettings>(System.IO.File.ReadAllText("JSON/Settings.json"));

        //populate data with images from imagepath data
        
        for (int i = 0; i < fisharray.Length; i++)
        {
            if (System.IO.File.Exists(fisharray[i].fishpath))
            {
                //take the raw data of the image
                byte[] data = System.IO.File.ReadAllBytes(fisharray[i].fishpath);
                //define a new texture with an arbitrary size, height and width 0 or 1000 works the same
                //because a new texture2d needs a constructor even though we overwrite it with LoadImage()
                Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                //load the data into that texture
                texture.LoadImage(data);
                //then convert that texture into a sprite
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                fisharray[i].fishpic = sprite;
            }
        }
        for (int i = 0; i < SimStepArray.Length; i++)
        {
            if (System.IO.File.Exists(SimStepArray[i].propertypicpath))
            {
                byte[] data = System.IO.File.ReadAllBytes(SimStepArray[i].propertypicpath);
                Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                texture.LoadImage(data);
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                SimStepArray[i].propertypic = sprite;
            }
        }
        for (int i = 0; i < SimStepArray.Length; i++)
        {
            if (System.IO.File.Exists(SimStepArray[i].aerialpicpath))
            {
                byte[] data = System.IO.File.ReadAllBytes(SimStepArray[i].aerialpicpath);
                Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
                texture.LoadImage(data);
                Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                SimStepArray[i].aerialpic = sprite;
            }
        }
        //forward transition pic
        if (System.IO.File.Exists(GenSetting.forwardpicpath))
        {
            byte[] data = System.IO.File.ReadAllBytes(GenSetting.forwardpicpath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GenSetting.forwardpic = sprite;
        }
        //backward transition pic
        if (System.IO.File.Exists(GenSetting.backwardpicpath))
        {
            byte[] data = System.IO.File.ReadAllBytes(GenSetting.backwardpicpath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GenSetting.backwardpic = sprite;
        }
        //death overlay pic
        if (System.IO.File.Exists(GenSetting.deathpicpath))
        {
            byte[] data = System.IO.File.ReadAllBytes(GenSetting.deathpicpath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GenSetting.deathpic = sprite;
        }
        if (System.IO.File.Exists(GenSetting.startpicpath))
        {
            byte[] data = System.IO.File.ReadAllBytes(GenSetting.startpicpath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GenSetting.startpic = sprite;
        }
        if (System.IO.File.Exists(GenSetting.endpicpath))
        {
            byte[] data = System.IO.File.ReadAllBytes(GenSetting.endpicpath);
            Texture2D texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
            texture.LoadImage(data);
            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            GenSetting.endpic = sprite;
        }

        

    }

    //Returns a FishClass object based on name
    //Never ended up using this one.
    public FishClass returnfishobj(string fishname)
    {
        for (int i = 0; i < this.fisharray.Length; ++i)
        {
            if (fisharray[i].fishname == fishname)
            {
                return fisharray[i];
            }
        }
        Debug.LogError("No fish by the name of \"" + fishname + "\"found in dataclass fish array");
        return null;
    }
    //Returns the full fish array. Never used this either.
    public FishClass[] returnfullarray()
    {
        
        return fisharray;
    }
    //Takes a fishname, finds it in the fisharray, then builds an array where that fish is at index 0 and removes
    //the duplicate entry.
    public FishClass[] fishfirstarray(string fishname)
    {

        FishClass firstfish = null;
        FishClass[] array = fisharray;
        FishClass[] outarray = new FishClass[fisharray.Length];
        for (int i = 0; i < array.Length; ++i)
        {
            if (array[i].fishname == fishname)
            {
                firstfish = array[i];
                array[i] = null;
                break;
            }

        }

        outarray[0] = firstfish;
        array = array.Where(c => c != null).ToArray();
        for (int i = 0; i < array.Length; ++i)
        {
            if (array[i] != null)
            {
                outarray[i+1] = array[i];
            }
        }

        return outarray;


    }
    
}
