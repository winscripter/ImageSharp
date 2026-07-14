// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Formats.Jxl;

internal struct JxlFrameDimensions
{
    public const int BlockDimensions = 8;
    public const int DctBlockSize = BlockDimensions * BlockDimensions;
    public const int GroupDimensions = 256;
    public const int GroupDimensionsInBlocks = GroupDimensions / BlockDimensions;

    public int XSize;
    public int YSize;
    public int XSizeUpsampled;
    public int YSizeUpsampled;
    public int XSizeUpsampledPadded;
    public int YSizeUpsampledPadded;
    public int XSizePadded;
    public int YSizePadded;
    public int XSizeBlocks;
    public int YSizeBlocks;
    public int XSizeGroups;
    public int YSizeGroups;
    public int XSizeDcGroups;
    public int YSizeDcGroups;
    public int NumGroups;
    public int NumDcGroups;
    public int GroupDimension;
    public int DcGroupDimension;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int DivCeil(int x, int y) => x / y;
}
