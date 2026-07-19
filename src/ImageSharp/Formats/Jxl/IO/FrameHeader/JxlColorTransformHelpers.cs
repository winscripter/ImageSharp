// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.IO.FrameHeader;

/// <summary>
/// Helper methods associated with JxlColorTransform.
/// </summary>
internal static class JxlColorTransformHelpers
{
    private static readonly int[][] JpegOrders =
    [
        [0, 0, 0], // Grayscale
        [1, 0, 2], // Y'Cb'Cr
        [0, 1, 2], // None
        [0, 1, 2] // Anything else
    ];

    public static ReadOnlySpan<int> GetJpegOrder(JxlColorTransform transform, bool isGraysacle)
    {
        if (isGraysacle)
        {
            return JpegOrders[0];
        }

        if (transform == JxlColorTransform.YCbCr)
        {
            return JpegOrders[1];
        }
        else if (transform == JxlColorTransform.None)
        {
            return JpegOrders[2];
        }
        else
        {
            return JpegOrders[3];
        }
    }
}
