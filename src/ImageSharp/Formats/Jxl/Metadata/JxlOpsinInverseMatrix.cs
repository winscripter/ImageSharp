// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.Fields;
using SixLabors.ImageSharp.Formats.Jxl.Processing;

namespace SixLabors.ImageSharp.Formats.Jxl.Metadata;

internal sealed class JxlOpsinInverseMatrix : IJxlFields
{
    public bool AllDefault { get; set; }

    public JxlMatrix3x3F InverseMatrix { get; set; }

    public InlineArray3<float> OpsinBiases { get; set; }

    public InlineArray4<float> QuantBiases { get; set; }

    public bool Visit(JxlVisitor visitor) => throw new NotImplementedException();
}
