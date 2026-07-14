// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Collections.Generic;

namespace SixLabors.ImageSharp.Formats.Jxl;

internal class JxlFormat : IImageFormat
{
    public string Name => "JPEG XL";

    public string DefaultMimeType => "image/jxl";

    IEnumerable<string> IImageFormat.MimeTypes => new[] { "image/jxl" };

    IEnumerable<string> IImageFormat.FileExtensions => new[] { "jxl" };
}
