// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing;

internal sealed class JxlFrameDimensions
{
    public const int BlockDimensions = 8;
    public const int DctBlockSize = BlockDimensions * BlockDimensions;
    public const int GroupDimensions = 256;
    public const int GroupDimensionsInBlocks = GroupDimensions / BlockDimensions;

    public JxlFrameDimensions(int xSizePixel, int ySizePixel, int groupSizeShift, int maxHorizontalShift, int maxVerticalShift, bool modularMode, int upsampling)
    {
        this.GroupDimension = (GroupDimensions >> 1) << groupSizeShift;
        this.DcGroupDimension = this.GroupDimension * BlockDimensions;
        this.XSizeUpsampled = xSizePixel;
        this.YSizeUpsampled = ySizePixel;
        this.XSize = DivCeil(xSizePixel, upsampling);
        this.YSize = DivCeil(ySizePixel, upsampling);
        this.XSizeBlocks = DivCeil(this.XSize, BlockDimensions << maxHorizontalShift) << maxHorizontalShift;
        this.YSizeBlocks = DivCeil(this.YSize, BlockDimensions << maxVerticalShift) << maxVerticalShift;
        this.XSizePadded = this.XSizeBlocks * BlockDimensions;
        this.YSizePadded = this.YSizeBlocks * BlockDimensions;

        if (modularMode)
        {
            this.XSizePadded = this.XSize;
            this.YSizePadded = this.YSize;
        }

        this.XSizeUpsampledPadded = this.XSizePadded * upsampling;
        this.YSizeUpsampledPadded = this.YSizePadded * upsampling;
        this.XSizeGroups = DivCeil(this.XSize, GroupDimensions);
        this.YSizeGroups = DivCeil(this.YSize, GroupDimensions);
        this.XSizeDcGroups = DivCeil(this.XSizeBlocks, GroupDimensions);
        this.YSizeDcGroups = DivCeil(this.YSizeBlocks, GroupDimensions);
        this.NumGroups = this.XSizeGroups * this.YSizeGroups;
        this.NumDcGroups = this.XSizeDcGroups * this.YSizeDcGroups;
    }

    public int XSize { get; set; }

    public int YSize { get; set; }

    public int XSizeUpsampled { get; set; }

    public int YSizeUpsampled { get; set; }

    public int XSizeUpsampledPadded { get; set; }

    public int YSizeUpsampledPadded { get; set; }

    public int XSizePadded { get; set; }

    public int YSizePadded { get; set; }

    public int XSizeBlocks { get; set; }

    public int YSizeBlocks { get; set; }

    public int XSizeGroups { get; set; }

    public int YSizeGroups { get; set; }

    public int XSizeDcGroups { get; set; }

    public int YSizeDcGroups { get; set; }

    public int NumGroups { get; set; }

    public int NumDcGroups { get; set; }

    public int GroupDimension { get; set; }

    public int DcGroupDimension { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DivCeil(int x, int y) => x / y;
}
