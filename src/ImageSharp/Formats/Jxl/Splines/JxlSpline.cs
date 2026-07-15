// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Splines;

internal sealed class JxlSpline
{
    public List<PointF> ControlPoints { get; set; } = [];

    public JxlDct32[] ColorDct { get; set; } = [];

    public JxlDct32 SigmaDct { get; set; }
}
