﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RawDataExplorers.Data.Items
{

    public class Xml
    {

        [JsonProperty("@version")]
        public string Version { get; set; }
    }

    public class English
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("LongDescription")]
        public string LongDescription { get; set; }
    }

    public class Strings
    {

        [JsonProperty("English")]
        public English English { get; set; }
    }

    public class Data
    {

        [JsonProperty("BuyPrice")]
        public string BuyPrice { get; set; }

        [JsonProperty("SellPrice")]
        public string SellPrice { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("SpriteID")]
        public string SpriteID { get; set; }

        [JsonProperty("ItemID")]
        public string ItemID { get; set; }

        [JsonProperty("Param1")]
        public string Param1 { get; set; }

        [JsonProperty("Param2")]
        public string Param2 { get; set; }

        [JsonProperty("Param3")]
        public string Param3 { get; set; }

        [JsonProperty("Unk1")]
        public string Unk1 { get; set; }

        [JsonProperty("Unk2")]
        public string Unk2 { get; set; }

        [JsonProperty("Unk3")]
        public string Unk3 { get; set; }

        [JsonProperty("Unk4")]
        public string Unk4 { get; set; }
    }

    public class Item
    {

        [JsonProperty("@gameVersion")]
        public string GameVersion { get; set; }

        [JsonProperty("#comment")]
        public object[] Comment { get; set; }

        [JsonProperty("Strings")]
        public Strings Strings { get; set; }

        [JsonProperty("Data")]
        public Data Data { get; set; }

        [JsonProperty("ExclusiveData")]
        public ExclusiveData ExclusiveData { get; set; }
    }

    public class ExclusiveData
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Parameter")]
        public string Parameter { get; set; }
    }

    public class RawItemData
    {

        [JsonProperty("?xml")]
        public Xml Xml { get; set; }

        [JsonProperty("Item")]
        public Item Item { get; set; }
    }



}