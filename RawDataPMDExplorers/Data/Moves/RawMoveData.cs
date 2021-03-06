﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using Newtonsoft.Json;

namespace RawDataPMDExplorers.Data.Moves
{
    public class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public class English
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }
    }

    public class Strings
    {

        [JsonProperty("English")]
        public English English { get; set; }
    }

    public class Datum
    {
        public bool Equals(Datum other)
        {
            return string.Equals(this.BasePower, other.BasePower) 
                && string.Equals(this.Type, other.Type) 
                && string.Equals(this.Category, other.Category) 
                && string.Equals(this.Unk4, other.Unk4) 
                && string.Equals(this.Unk5, other.Unk5) 
                && string.Equals(this.BasePP, other.BasePP) 
                && string.Equals(this.Unk6, other.Unk6) 
                && string.Equals(this.Unk7, other.Unk7) 
                && string.Equals(this.Accuracy, other.Accuracy) 
                && string.Equals(this.Unk9, other.Unk9) 
                && string.Equals(this.Unk10, other.Unk10) 
                && string.Equals(this.Unk11, other.Unk11) 
                && string.Equals(this.Unk12, other.Unk12) 
                && string.Equals(this.Unk13, other.Unk13) 
                && string.Equals(this.Unk14, other.Unk14) 
                && string.Equals(this.Unk15, other.Unk15) 
                && string.Equals(this.Unk16, other.Unk16) 
                && string.Equals(this.Unk17, other.Unk17) 
                && string.Equals(this.Unk18, other.Unk18) 
                && string.Equals(this.MoveID, other.MoveID) 
                && string.Equals(this.Unk19, other.Unk19);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (this == obj) return true;
            return obj.GetType() == this.GetType() && this.Equals((Datum)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.BasePower != null ? this.BasePower.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Type != null ? this.Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Category != null ? this.Category.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk4 != null ? this.Unk4.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk5 != null ? this.Unk5.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BasePP != null ? this.BasePP.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk6 != null ? this.Unk6.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk7 != null ? this.Unk7.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Accuracy != null ? this.Accuracy.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk9 != null ? this.Unk9.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk10 != null ? this.Unk10.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk11 != null ? this.Unk11.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk12 != null ? this.Unk12.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk13 != null ? this.Unk13.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk14 != null ? this.Unk14.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk15 != null ? this.Unk15.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk16 != null ? this.Unk16.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk17 != null ? this.Unk17.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk18 != null ? this.Unk18.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.MoveID != null ? this.MoveID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Unk19 != null ? this.Unk19.GetHashCode() : 0);
                return hashCode;
            }
        }

        [JsonProperty("BasePower")]
        public string BasePower { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("Unk4")]
        public string Unk4 { get; set; }

        [JsonProperty("Unk5")]
        public string Unk5 { get; set; }

        [JsonProperty("BasePP")]
        public string BasePP { get; set; }

        [JsonProperty("Unk6")]
        public string Unk6 { get; set; }

        [JsonProperty("Unk7")]
        public string Unk7 { get; set; }

        [JsonProperty("Accuracy")]
        public string Accuracy { get; set; }

        [JsonProperty("Unk9")]
        public string Unk9 { get; set; }

        [JsonProperty("Unk10")]
        public string Unk10 { get; set; }

        [JsonProperty("Unk11")]
        public string Unk11 { get; set; }

        [JsonProperty("Unk12")]
        public string Unk12 { get; set; }

        [JsonProperty("Unk13")]
        public string Unk13 { get; set; }

        [JsonProperty("Unk14")]
        public string Unk14 { get; set; }

        [JsonProperty("Unk15")]
        public string Unk15 { get; set; }

        [JsonProperty("Unk16")]
        public string Unk16 { get; set; }

        [JsonProperty("Unk17")]
        public string Unk17 { get; set; }

        [JsonProperty("Unk18")]
        public string Unk18 { get; set; }

        [JsonProperty("MoveID")]
        public string MoveID { get; set; }

        [JsonProperty("Unk19")]
        public string Unk19 { get; set; }
    }

    public class Move
    {

        [JsonProperty("@gameVersion")]
        public string GameVersion { get; set; }

        [JsonProperty("#comment")]
        public object[] Comment { get; set; }

        [JsonProperty("Strings")]
        public Strings Strings { get; set; }

        [JsonProperty("Data")]
        public Datum[] Data { get; set; }
    }

    public class RawMoveData
    {

        [JsonProperty("?xml")]
        public Xml Xml { get; set; }

        [JsonProperty("Move")]
        public Move Move { get; set; }
    }

}