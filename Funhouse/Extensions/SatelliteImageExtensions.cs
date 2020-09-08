﻿using System;
using System.IO;
using System.Threading.Tasks;
using Extend;
using Funhouse.Extensions.Images;
using Funhouse.ImageProcessing.Mask;
using Funhouse.Models;
using Funhouse.Models.Projections;
using SixLabors.ImageSharp.Processing;

namespace Funhouse.Extensions
{
    public static class SatelliteImageExtensions
    {
        /// <summary>
        ///     Mask all pixels outside the Earth to assist image stitching of projected images.
        /// </summary>
        public static SatelliteImage RemoveBackground(this SatelliteImage image)
        {
            image.Image.RemoveBackground();
            return image;
        }

        public static SatelliteImage CropBorders(this SatelliteImage image)
        {
            // Perform crop
            if (image.Definition.Crop != null) image.Image.AutoCropBorder(image.Definition.Crop);
            return image;
        }

        public static SatelliteImage NormaliseHistogram(this SatelliteImage image)
        {
            // Normalise brightness and contrast
            image.Image.Mutate(c => c.HistogramEqualization());
            image.Image.Mutate(c => c.Brightness(image.Definition.Brightness));
            image.Image.Mutate(c => c.Brightness(1.1f));

            return image;
        }

        /// <summary>
        ///     Normalises the source image size to match the specified output spatial resolution. Doing
        ///     so simplifies maths when blending multiple pages.
        /// </summary>
        public static SatelliteImage NormaliseSize(this SatelliteImage image, int imageSize)
        {
            if (image.Image.Width != imageSize || image.Image.Height != imageSize)
            {
                // TODO test results of different interpolation types
                image.Image.Mutate(c => c.Resize(imageSize, imageSize, KnownResamplers.Welch));
            }

            return image;
        }

        public static async Task SaveWithExifAsync(this SatelliteImage image, RenderOptions options, string suffix)
        {
            string outputPath;

            // Determine output file location
            if (options.MultipleTargets)
            {
                Directory.CreateDirectory(options.OutputPath);
                outputPath = Path.Combine(options.OutputPath, $"{Path.GetFileNameWithoutExtension(image.Path)}{suffix}.jpg");
            }
            else
            {
                outputPath = options.OutputPath;
            }
            
           // Save image
            await image.Image.SaveWithExifAsync(outputPath, options);
        }
    }
}