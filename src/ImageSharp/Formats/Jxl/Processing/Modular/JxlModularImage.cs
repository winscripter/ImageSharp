// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular;

internal sealed class JxlModularImage
{
    public List<JxlModularChannel> Channels { get; set; } = [];

    /// <summary>
    /// Gets or sets the total number of metachannels in this image.
    /// </summary>
    public int MetaChannels { get; set; }

    /// <summary>
    /// Gets or sets the bit depth used in this image.
    /// </summary>
    public int BitDepth { get; set; }
}
