﻿using Funhouse.Extensions;
using Funhouse.Models.Angles;
using Funhouse.Services.Filesystem;

namespace Funhouse.Models.Configuration
{
    public class SatelliteDefinition
    {
        /// <param name="filenamePattern"></param>
        /// <param name="displayName"></param>
        /// <param name="filenameParserType"></param>
        /// <param name="longitude"></param>
        /// <param name="latitudeRange"></param>
        /// <param name="longitudeRange"></param>
        /// <param name="height">Satellite height above ellipsoid (metres)</param>
        /// <param name="crop"></param>
        /// <param name="brightness"></param>
        public SatelliteDefinition(
            string displayName, 
            string filenamePattern, 
            FilenameParserType filenameParserType,
            double longitude,
            Range latitudeRange,
            Range longitudeRange,
            double height = Constants.Satellite.DefaultHeight,
            double[]? crop = null,
            float brightness = 1.0f)
        {
            FilenamePattern = filenamePattern;
            FilenameParserType = filenameParserType;
            DisplayName = displayName;
            LatitudeRange = latitudeRange;
            LongitudeRange = longitudeRange;
            Height = height;
            Crop = crop;
            Brightness = brightness;

            // Convert satellite longitude to lat/long scale of -180 to 180 degrees
            Longitude = longitude.NormaliseLongitude();
        }

        public string FilenamePattern { get; }
        public FilenameParserType FilenameParserType { get; }
        public string DisplayName { get; }
        public Range LatitudeRange { get; }
        public Range LongitudeRange { get; }
        public double Longitude { get; }
        public double Height { get; }
        public double[]? Crop { get; }
        public float Brightness { get; }
    }
}