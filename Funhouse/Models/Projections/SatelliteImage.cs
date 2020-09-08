﻿using Funhouse.Models.Angles;
using Funhouse.Models.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Funhouse.Models.Projections
{
    public class SatelliteImage
    {
        public SatelliteImage(string path, Image<Rgba32> image, SatelliteDefinition definition)
        {
            Path = path;
            Definition = definition;
            Image = image;
        }

        public SatelliteDefinition Definition { get; }
        public Image<Rgba32> Image { get; set; }
        public string Path { get; }
        public int OffsetX { get; set; }

        /// <summary>
        ///     Visible longitude range of satellite, minus overlapping ranges if required.
        /// </summary>
        public Range LongitudeRange { get; set; }

        /// <summary>
        ///     Visible latitude range of satellite.
        /// </summary>
        public Range LatitudeRange { get; set; }
    }
}