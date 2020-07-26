﻿# Sanchez 
![.NET Core](https://github.com/nullpainter/sanchez/workflows/.NET%20Core/badge.svg) 
![Publish](https://github.com/nullpainter/sanchez/workflows/Publish/badge.svg)
[![Github All Releases](https://img.shields.io/github/downloads/nullpainter/sanchez/total.svg)]()
![Happiness](https://img.shields.io/badge/happiness-100%25-orange)

<img src="Documentation/sample-output-h.jpg" width="400" title="Himawari 8" align="left"> 
<img src="Documentation/sample-output-gk.jpg" width="400" title="GK-2A"> 

**Sanchez brings your dull IR satellite images to life.**

Utilising a full-colour static ~~ándale~~ underlay image, combining it with a single greyscale IR satellite image, an optional mask and overlay and some zhushing, Sanchez will create beautiful images to be proud of.

This could be considered cheating, but this is the approach that NASA used to utilise for older weather satellites. If it's good enough for NASA, it should be good enough for you.

If you provide a mask image, you can compensate for discrepancies in scale or distortion between the satellite image and the full-colour image. Sanchez also provides options for colour tinting, an overlay for text or other imagery, brightness and contrast adjustment.

*¡Arriba, Arriba! ¡Ándale, Ándale!*

## Sample images
Sample images can be [found here](https://github.com/nullpainter/sanchez/wiki/Sample-images). If you have interesting images to contribute, [let me know](https://github.com/nullpainter/sanchez/issues/new?assignees=nullpainter&labels=&template=sample-image.md&title=)!

## Download
Releases are available for Raspberry Pi, Linux, Mac OS X and Windows. [Head on over](https://github.com/nullpainter/sanchez/releases) and pick your poison!

For Raspberry Pi, pick the ARM build.

## Image resources
Sample underlays, masks and IR images for Himawari-8 and GK-2A are in the [Resources](Sanchez/Resources) folder. 

## Usage

```
  -u, --underlay      Required. Path to full-colour underlay image

  -s, --source        Required. Path to IR satellite image(s)

  -m, --mask          Optional path to mask image

  -O, --overlay       Optional path to overlay image

  -o, --output        Required. Path to output file or folder

  -t, --tint          (Default: 5ebfff) Tint to apply to satellite image

  -b, --brightness    (Default: 1.2) Brightness adjustment

  -S, --saturation    (Default: 0.7) Saturation adjustment

  --help              Display this help screen.

  --version           Display version information.

```

Sample usage:

```
./Sanchez -s c:\images\Himawari8\**\*-IR*.jpg -m Resources\Mask.jpg -u Resources\Himawari-8\Underlay.jpg` -o Output.jpg
```

It is assumed that all input images are the same size.

## Tint formats
Sanchez supports any of the following tint formats, with or without the leading `#`:

* `#xxx`
* `#xxxxxx`
* `#xxxxxxxx`

## Batch file conversion
Sanchez supports converting single or batch satellite files. If converting a batch, the output argument is assumed to be a folder and is created if needed. Original file names are preserved, with a `-fc` suffix.

### Sample batch patterns
Sanchez supports glob and directory patterns for the `--source` argument. 

Examples are:

* `images/`
* `images/*.*`
* `images/*.jpg`
* `images/**/*.*`
* `images/2020-*/*IR*.jpg`

## Creating underlay images
NASA's collection of [Blue Marble](https://visibleearth.nasa.gov/collection/1484/blue-marble) images is an excellent source of high resolution underlay images.

Websites such as [Map To Globe](https://www.maptoglobe.com/) can be used to map the underlay to a globe. Images produced by both GK-2A and Himawari-8 are slightly warped so don't fully map to texture-mapped globes. In order to correct for this, Photoshop's lens correction filter can be used.

This is the approach used for the sample underlay images in the [Resources](Sanchez/Resources) folder.

