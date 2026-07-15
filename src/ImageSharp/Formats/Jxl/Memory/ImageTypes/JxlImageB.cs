// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Diagnostics;

namespace SixLabors.ImageSharp.Formats.Jxl.Memory.ImageTypes;

/// <summary>
/// Represents a single-plane, 2D raster image of type <see cref="byte"/>.
/// </summary>
internal sealed class JxlImageB : JxlPlane<byte>
{
    public JxlImageB()
    {
    }

    public JxlImageB(int width, int height)
        : base(width, height)
    {
    }

    public JxlImageB(Configuration configuration, int xSize, int ySize, int prePadding = 0)
        : base(xSize, ySize)
        => this.Allocate(configuration, prePadding);

    public Memory<byte> GetRowBytesMemory(int y)
    {
        Debug.Assert(y < this.YSize, "Attempted to access out-of-bounds Y coordinate");

        Memory<byte> row = this.Bytes[(y * this.BytesPerRow)..];

        return row;
    }
}
