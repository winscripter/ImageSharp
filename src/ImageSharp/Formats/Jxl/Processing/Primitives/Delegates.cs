// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Primitives;

internal delegate void JxlImageOutputCallback(
    Span<byte> data,
    int x,
    int y,
    int numPixels,
    Span<byte> pixels);

internal delegate void JxlImageOutputInitializeCallback(
    Span<byte> data,
    int numThreads,
    int numPixelsPerThread);

internal delegate void JxlImageOutputRunCallback(
    Span<byte> data,
    int x,
    int y,
    int numPixels,
    Span<byte> pixels);

internal delegate void JxlImageOutputDestroyCallback(Span<byte> data);
