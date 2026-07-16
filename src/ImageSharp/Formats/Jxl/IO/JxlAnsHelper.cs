// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static SixLabors.ImageSharp.Formats.Jxl.IO.JxlAnsConstants;

namespace SixLabors.ImageSharp.Formats.Jxl.IO;

internal static class JxlAnsHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetPopulationCountPrecision(int logCount, int shift)
    {
        int r = Math.Min(logCount, shift - ((AnsLogTableSize - logCount) >> 1));

        if (r < 0)
        {
            return 0;
        }

        return r;
    }
}
