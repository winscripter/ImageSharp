// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.IO;

namespace SixLabors.ImageSharp.Formats.Jxl.Metadata;

internal sealed class JxlToneMapping : IJxlFields
{
    public bool AllDefault { get; set; }

    public float IntensityTarget { get; set; }

    public float LowerBoundIntensityLevel { get; set; }

    public bool RelativeToMaxDisplay { get; set; }

    public float LinearBelow { get; set;}

    public bool Visit(JxlVisitor visitor) => throw new NotImplementedException();
}
