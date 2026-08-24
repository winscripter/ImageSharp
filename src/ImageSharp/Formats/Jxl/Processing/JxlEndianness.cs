// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Processing;

/// <summary>
/// Specifies the ordering of multi-byte data.
/// </summary>
internal enum JxlEndianness : byte
{
    /// <summary>
    /// Use endianness of the CPU/system.
    /// </summary>
    Native,

    /// <summary>
    /// Force little endian.
    /// </summary>
    Little,

    /// <summary>
    /// Force big endian.
    /// </summary>
    Big
}
