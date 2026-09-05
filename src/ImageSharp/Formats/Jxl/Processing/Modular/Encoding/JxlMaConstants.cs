// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Encoding;

internal static class JxlMaConstants
{
    /// <summary>
    /// Total number of MA tree contexts.
    /// </summary>
    public const int NumTreeContexts = 6;

    public const int MaxTreeSize = 1 << 22;

    public const int PropertyRangeFast = 512 << 4;
}
