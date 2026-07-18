// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.Fields;

namespace SixLabors.ImageSharp.Formats.Jxl.Metadata;

internal sealed class JxlBitDepth : IJxlFields
{
    /// <summary>
    /// Gets or sets a value indicating whether
    /// the original (uncompressed) samples are floating point or
    /// unsigned integer.
    /// </summary>
    public bool FloatingPointSample { get; set; }

    /// <summary>
    /// Gets or sets the bit depth of the original (uncompressed) image samples.
    /// Must be in the range [1, 32].
    /// </summary>
    public int BitsPerSample { get; set; }

    /// <summary>
    /// <para>
    ///   Gets or sets floating point exponent bits of the original (uncompressed) image samples,
    ///   only used if <see cref="FloatingPointSample"/>  is <see langword="true"/>.
    /// </para>
    /// <para>
    ///   If used, the samples are floating point with:
    ///   <list type="bullet">
    ///     <item>1 sign bit</item>
    ///     <item><see cref="ExponentBitsPerSample"/> exponent bits</item>
    ///     <item>(<see cref="BitsPerSample"/> - <see cref="ExponentBitsPerSample"/> - 1) mantissa bits</item>
    ///   </list>
    ///   If used, <see cref="ExponentBitsPerSample"/> must be in the range
    ///   [2, 8] and amount of mantissa bits must be in the range [2, 23].
    /// </para>
    /// </summary>
    public int ExponentBitsPerSample { get; set; }

    public bool Visit(JxlVisitor visitor) => throw new NotImplementedException();
}
