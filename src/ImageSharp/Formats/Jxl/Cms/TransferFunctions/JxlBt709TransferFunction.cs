// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Cms.TransferFunctions;

/// <summary>
/// ITU-R BT.709 transfer function
/// </summary>
internal static class JxlBt709TransferFunction
{
    public static double EncodedFromDisplay(double d)
    {
        if (d < Threshold)
        {
            return MulLow * d;
        }

        return (MulHi * Math.Pow(d, PowHi)) + Sub;
    }
}
