﻿using System.IO;
using Extend;
using FluentValidation;
using Funhouse.Extensions;
using Funhouse.Models;
using Funhouse.Models.CommandLine;

namespace Funhouse.Validators
{
    public class OptionsValidator<T> : AbstractValidator<T> where T : BaseOptions
    {
        public OptionsValidator()
        {
            RuleFor(o => o.Tint.FromHexString())
                .NotNull()
                .WithName("Tint")
                .WithMessage("Unable to parse tint as a hex tuple. Expected format is 5ebfff");
            
            RuleFor(o => o.UnderlayPath)
                .Must(path => File.Exists(path ?? Constants.DefaultUnderlayPath))
                .WithMessage("Invalid underlay path");

            RuleFor(o => o.SpatialResolution)
                .Must(resolution => resolution.IsIn(Constants.Satellite.SpatialResolution.TwoKm, Constants.Satellite.SpatialResolution.FourKm))
                .WithMessage($"Unsupported output spatial resolution. Valid values are: {Constants.Satellite.SpatialResolution.TwoKm}, {Constants.Satellite.SpatialResolution.FourKm}");
        }
    }
}