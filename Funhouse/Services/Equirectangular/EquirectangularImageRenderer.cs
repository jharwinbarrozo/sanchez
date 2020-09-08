﻿using System.Threading.Tasks;
using Funhouse.Extensions.Images;
using Funhouse.ImageProcessing.Tint;
using Funhouse.Models;
using Funhouse.Services.Underlay;
using Serilog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Funhouse.Services.Equirectangular
{
    public interface IEquirectangularImageRenderer
    {
        Task<Image<Rgba32>> StitchImagesAsync(Image<Rgba32> stitched, SatelliteImages activities);
    }

    public class EquirectangularImageRenderer : IEquirectangularImageRenderer
    {
        private readonly RenderOptions _options;
        private readonly IUnderlayService _underlayService;

        public EquirectangularImageRenderer(
            RenderOptions options,
            IUnderlayService underlayService)
        {
            _options = options;
            _underlayService = underlayService;
        }

        public async Task<Image<Rgba32>> StitchImagesAsync(Image<Rgba32> stitched, SatelliteImages activities)
        {

            // Determine visible range of all satellite imagery
            activities.GetVisibleRange(out var latitudeRange, out var longitudeRange);

            // Load underlay
            Image<Rgba32> target;
            if (_options.NoUnderlay) target = stitched;
            else
            {
                Log.Information("Tinting and normalising IR imagery");

                var clone = stitched.Clone();
                clone.Mutate(c => c.HistogramEqualization());
                stitched.Tint(_options.Tint);

                stitched.Mutate(c => c.DrawImage(clone, PixelColorBlendingMode.HardLight, 0.5f));

                var underlayOptions = new UnderlayProjectionOptions(
                    ProjectionType.Equirectangular,
                    _options.InterpolationType,
                    _options.ImageSize,
                    _options.UnderlayPath,
                    stitched.Height,
                    latitudeRange, longitudeRange);

                Log.Information("Retrieving underlay");
                target = await _underlayService.GetUnderlayAsync(underlayOptions);

                // Render underlay and optionally crop to size
                Log.Information("Blending with underlay");
                target.Mutate(ctx => ctx.DrawImage(stitched, PixelColorBlendingMode.Screen, 1.0f));
                
                // Perform global colour correction
                target.ColourCorrect(_options);
            }

            return target;
        }
    }
}