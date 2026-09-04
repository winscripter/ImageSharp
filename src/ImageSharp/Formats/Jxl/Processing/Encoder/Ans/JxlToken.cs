// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.Processing.Splines;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Encoder.Ans;

internal struct JxlToken(JxlSplineEntropyContext c, uint value)
{
    public bool IsLz77Length;
    public JxlSplineEntropyContext Context = c;
    public uint Value = value;
}
