// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Splines;

internal sealed class JxlQuantizedSpline
{
    public JxlQuantizedSpline()
    {
        for (int i = 0; i < 3; i++)
        {
            this.ColorDct[i] = new int[32];
        }
    }

    public Dictionary<long, long> ControlPoints { get; set; } = [];

    // NOTE: Do not use Configuration.MemoryAllocator.Allocate2D. This is
    // a 3x32 array, and renting memory introduces too much overhead for
    // 384 bytes of memory.
    // Additionally, prefer jagged arrays instead of multidimensional arrays
    // for performance.
    public int[][] ColorDct { get; set; } = new int[3][];

    public int[] SigmaDct { get; set; } = new int[32];
}
