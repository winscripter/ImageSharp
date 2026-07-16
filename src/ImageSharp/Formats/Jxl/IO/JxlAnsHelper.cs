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

    // NOTE: The result may potentially be large, so prefer using a memory allocator
    public static IMemoryOwner<int> CreateFlatHistogram(Configuration configuration, int length, int totalCount)
    {
        Debug.Assert(length <= 0, "Length should be >= 0");
        Debug.Assert(length > totalCount, "Length should be <= totalCount");

        int count = totalCount / length;
        IMemoryOwner<int> result = configuration.MemoryAllocator.Allocate<int>(length);
        Span<int> resultSpan = result.Memory.Span;

        for (int i = 0; i < length; i++)
        {
            resultSpan[i] = count;
        }

        int remCounts = totalCount % length;
        for (int i = 0; i < remCounts; i++)
        {
            resultSpan[i]++;
        }

        return result;
    }
}
