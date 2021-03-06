﻿extern alias SystemDrawing;
using System;
using System.Collections.Generic;
using SystemDrawing::System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortraitsAdder
{
    public class PortraitEntity
    {
        public int IndexId { get; set; }
        public Portrait[] Portraits { get; set; }

        public static Image FromPortraitBase64(string portraitBase64)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] portraitBytes = Convert.FromBase64String(portraitBase64);

                stream.Write(portraitBytes, 0, portraitBytes.Length);

                return Image.FromStream(stream);
            }
        }

        public static string UrlFromPortrait(PortraitEntity portraitEntity, Portrait portrait)
        {
            return $"https://raw.githubusercontent.com/JordanZeotni/Explorers-Data/master/Data/Portraits/{portraitEntity.IndexId}/{portrait.PortraitId}.png";
        }
    }

    public class Portrait
    {
        public int PortraitId { get; set; }
        public string PortraitName { get; set; }
        public string PortraitImageBase64 { get; set; }
    }
}
