// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Transforms;

/// <summary>
/// Palette/indexed coding
/// </summary>
internal static class JxlPalette
{
    /// <summary>
    /// Represents maximum number of colors in a palette.
    /// </summary>
    private const int MaxPaletteLookupTableSize = 1 << 16;

    /// <summary>
    /// Represents number of channels for RGB. This is 3 because RGB
    /// has three channels: R (Red), G (Green), B (Blue).
    /// </summary>
    private const int RgbChannels = 3;

    /// <summary>
    /// 5x5x5 color cube for the larger cube.
    /// </summary>
    private const int LargeCube = 5;

    /// <summary>
    /// Smaller interleaved color cube to fill the holes of the larger cube.
    /// </summary>
    private const int SmallCube = 4;

    /// <summary>
    /// Number of bits required to represent a small cube.
    /// </summary>
    private const int SmallCubeBits = 2; // 2 bits gives us 0..3 inclusive, so it's perfect to represent a small cube

    /// <summary>
    /// Cube of SmallCube
    /// </summary>
    private const int LargeCubeOffset = SmallCube * SmallCube * SmallCube;

    private const int ImplicitPaletteSize = LargeCubeOffset + (LargeCube * LargeCube * LargeCube);

    /// <summary>
    /// Static delta palette used by GetPaletteValue.
    /// </summary>
    private static readonly int[][] DeltaPalette =
    [
        [0, 0, 0], [4, 4, 4], [11, 0, 0],
        [0, 0, -13], [0, -12, 0], [-10, -10, -10],
        [-18, -18, -18], [-27, -27, -27], [-18, -18, 0],
        [0, 0, -32], [-32, 0, 0], [-37, -37, -37],
        [0, -32, -32], [24, 24, 45], [50, 50, 50],
        [-45, -24, -24], [-24, -45, -45], [0, -24, -24],
        [-34, -34, 0], [-24, 0, -24], [-45, -45, -24],
        [64, 64, 64], [-32, 0, -32], [0, -32, 0],
        [-32, 0, 32], [-24, -45, -24], [45, 24, 45],
        [24, -24, -45], [-45, -24, 24], [80, 80, 80],
        [64, 0, 0], [0, 0, -64], [0, -64, -64],
        [-24, -24, 45], [96, 96, 96], [64, 64, 0],
        [45, -24, -24], [34, -34, 0], [112, 112, 112],
        [24, -45, -45], [45, 45, -24], [0, -32, 32],
        [24, -24, 45], [0, 96, 96], [45, -24, 24],
        [24, -45, -24], [-24, -45, 24], [0, -64, 0],
        [96, 0, 0], [128, 128, 128], [64, 0, 64],
        [144, 144, 144], [96, 96, 0], [-36, -36, 36],
        [45, -24, -45], [45, -45, -24], [0, 0, -96],
        [0, 128, 128], [0, 96, 0], [45, 24, -45],
        [-128, 0, 0], [24, -45, 24], [-45, 24, -45],
        [64, 0, -64], [64, -64, -64], [96, 0, 96],
        [45, -45, 24], [24, 45, -45], [64, 64, -64],
        [128, 128, 0], [0, 0, -128], [-24, 45, -45]
    ];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int Scale(int denominator, int value, int bitDepth)
    {
        DebugGuard.IsTrue(denominator == 4, "Denominator is defined as 4");

        return (value * ((1 << bitDepth) - 1)) >> 2;
    }

    public static int GetPaletteValue(Span<int> palette, int index, int c, int oneRow, int bitDepth)
    {
        if (index < 0)
        {
            if (c >= RgbChannels)
            {
                return 0;
            }

            index = -(index + 1);
            index %= 1 + (2 * (DeltaPalette.Length - 1));

            // JPEG XL reference uses:
            //    static constexpr int kMultiplier[] = {-1, 1};
            //    kMultiplier[index & 1]
            //
            // we use:
            //    ((index & 1) != 0 ? 1 : -1)
            //
            // The latter avoids multiplication and can make use
            // of CPU registers instead of memory access (if the JIT allows it).
            int result = DeltaPalette[(index + 1) >> 1][c] * ((index & 1) != 0 ? 1 : -1);

            if (bitDepth > 8)
            {
                result *= 1 << (bitDepth - 8);
            }

            return result;
        }
        else if (palette.Length <= index && index < palette.Length + LargeCubeOffset)
        {
            if (c >= RgbChannels)
            {
                return 0;
            }

            index -= palette.Length;
            index >>= c * SmallCubeBits;
            return Scale(SmallCube, index % SmallCube, bitDepth) + (1 << Math.Max(0, bitDepth - 3));
        }
        else if (palette.Length + LargeCubeOffset <= index)
        {
            if (c >= RgbChannels)
            {
                return 0;
            }

            switch (c)
            {
                case 1:
                    index /= LargeCube;
                    break;

                case 2:
                    index /= LargeCube * LargeCube;
                    break;

                default:
                    break;
            }

            return Scale(LargeCube - 1, index % LargeCube, bitDepth);
        }

        return palette[(c * oneRow) + index];
    }
}
