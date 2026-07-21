// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.Fields;

namespace SixLabors.ImageSharp.Formats.Jxl.IO.Entropy;

internal sealed class JxlAnsLz77Parameters : IJxlFields
{
    public bool Enabled { get; set; }

    public uint MinimumSymbol { get; set; }

    public uint MinimumLength { get; set; }

    public JxlAnsHybridUIntConfiguration LengthUintConfig { get; set; } = new(0, 0, 0);

    public int NonserializedDistanceContext { get; set; }

    public bool Visit(JxlVisitor visitor) => throw new NotImplementedException();
}
