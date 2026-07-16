// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Diagnostics.CodeAnalysis;

namespace SixLabors.ImageSharp.Formats.Jxl;

internal static class JxlThrowHelper
{
    private static readonly EndOfStreamException EndOfStream = new();

    [DoesNotReturn]
    public static void ThrowEndOfStream() => throw EndOfStream;
}
