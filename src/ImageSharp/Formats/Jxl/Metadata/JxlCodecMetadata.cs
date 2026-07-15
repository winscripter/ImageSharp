// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Metadata;

internal sealed class JxlCodecMetadata
{
    public JxlImageMetadata? ImageMetadata { get; set; }

    public SizeHeader Size { get; set; }

    public JxlCustomTransformData? CustomTransformData { get; set; }

    public int XSize => this.Size.XSize;

    public int YSize => this.Size.YSize;

    public int GetOrientedPreviewXSize(bool keepOrientation)
    {
        if (this.ImageMetadata!.Orientation > 4 && !keepOrientation)
        {
            return this.ImageMetadata.PreviewSize.YSize;
        }

        return this.ImageMetadata.PreviewSize.XSize;
    }

    public int GetOrientedPreviewYSize(bool keepOrientation)
    {
        if (this.ImageMetadata!.Orientation > 4 && !keepOrientation)
        {
            return this.ImageMetadata.PreviewSize.XSize;
        }

        return this.ImageMetadata.PreviewSize.YSize;
    }

    public int GetOrientedXSize(bool keepOrientation)
    {
        if (this.ImageMetadata!.Orientation > 4 && !keepOrientation)
        {
            return this.YSize;
        }

        return this.XSize;
    }

    public int GetOrientedYSize(bool keepOrientation)
    {
        if (this.ImageMetadata!.Orientation > 4 && !keepOrientation)
        {
            return this.XSize;
        }

        return this.YSize;
    }
}
