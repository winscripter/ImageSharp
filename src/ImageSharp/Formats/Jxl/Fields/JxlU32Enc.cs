// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Diagnostics;

namespace SixLabors.ImageSharp.Formats.Jxl.Fields;

internal readonly struct JxlU32Enc
{
    private readonly InlineArray4<JxlU32Distribution> d = default;

    public JxlU32Enc(JxlU32Distribution d0, JxlU32Distribution d1, JxlU32Distribution d2, JxlU32Distribution d3)
    {
        this.d[0] = d0;
        this.d[1] = d1;
        this.d[2] = d2;
        this.d[3] = d3;
    }

    public JxlU32Distribution GetDistribution(int selector)
    {
        Debug.Assert(selector < 4, "Selector out of range");

        return this.d[selector];
    }
}
