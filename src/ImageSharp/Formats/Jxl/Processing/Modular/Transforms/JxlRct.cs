// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Transforms;

/// <summary>
/// Reversible Color Transform (RCT)
/// </summary>
internal static class JxlRct
{
    /// <summary>
    /// Performs Inverse Reversible Color Transform (RCT) on one row.
    /// </summary>
    /// <param name="transformType">The kind of RCT.</param>
    /// <param name="in0">Input Y</param>
    /// <param name="in1">Input Co</param>
    /// <param name="in2">Input Cg</param>
    /// <param name="out0">Output R</param>
    /// <param name="out1">Output G</param>
    /// <param name="out2">Output B</param>
    private static void InverseRctRow(int transformType, Span<int> in0, Span<int> in1, Span<int> in2, Span<int> out0, Span<int> out1, Span<int> out2)
    {
        DebugGuard.MustBeBetweenOrEqualTo(transformType, 0, 6, nameof(transformType));

        int width = in0.Length; // All input & output channels have equal widths

        int second = transformType >> 1;
        int third = transformType & 1;

        int n = Vector<int>.Count;

        if (transformType == 6)
        {
            // SIMD-aligned loop
            int x;
            for (x = 0; x + n - 1 < width; x += n)
            {
                Vector<int> y = new(in0[x..]);
                Vector<int> co = new(in1[x..]);
                Vector<int> cg = new(in2[x..]);
                y -= cg >> 1;
                Vector<int> g = cg + y;
                y -= co >> 1;
                Vector<int> r = y + co;
                r.CopyTo(out0[x..]);
                g.CopyTo(out1[x..]);
                y.CopyTo(out2[x..]);
            }

            // Remainder (SIMD-unaligned)
            for (; x < width; x++)
            {
                int y = in0[x];
                int co = in1[x];
                int cg = in2[x];
                int tmp = y + -(cg >> 1);
                int g = cg + tmp;
                int b = tmp + -(co >> 1);
                int r = b + co;
                out0[x] = r;
                out1[x] = g;
                out2[x] = b;
            }
        }
        else
        {
            // SIMD-aligned loop
            int x;
            for (x = 0; x + n - 1 < width; x += n)
            {
                // Add a Vec suffix because the variables
                // 'second' and 'third' are already defined.
                // Though 'first' isn't, it's still suffixed
                // for consistency.
                Vector<int> firstVec = new(in0[x..]);
                Vector<int> secondVec = new(in1[x..]);
                Vector<int> thirdVec = new(in2[x..]);

                if (third > 0)
                {
                    thirdVec += firstVec;
                }

                if (second == 1)
                {
                    secondVec += firstVec;
                }
                else if (second == 2)
                {
                    secondVec += (firstVec + thirdVec) >> 1;
                }

                firstVec.CopyTo(out0[x..]);
                secondVec.CopyTo(out1[x..]);
                thirdVec.CopyTo(out2[x..]);
            }

            // Remainder (SIMD-unaligned)
            for (; x < width; x++)
            {
                int firstCoeff = in0[x];
                int secondCoeff = in1[x];
                int thirdCoeff = in2[x];

                if (third > 0)
                {
                    thirdCoeff += firstCoeff;
                }

                if (second == 1)
                {
                    secondCoeff += firstCoeff;
                }
                else if (second == 2)
                {
                    secondCoeff += (firstCoeff + thirdCoeff) >> 1;
                }

                out0[x] = firstCoeff;
                out1[x] = secondCoeff;
                out2[x] = thirdCoeff;
            }
        }
    }

    /// <summary>
    /// Performs Inverse Reversible Color Transform (RCT) on an entire
    /// image.
    /// </summary>
    /// <param name="configuration">
    /// The configuration is used to access maximum degree of parallelism.
    /// </param>
    /// <param name="img">
    /// Image to compute inverse RCT.
    /// </param>
    /// <param name="beginC">
    /// Offset of the color channel for Y, Co, Cg.
    /// </param>
    /// <param name="rctType">
    /// Type of Reversible Color Transform
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// Invoked when RCT/permutation is invalid.
    /// </exception>
    public static void InverseRct(Configuration configuration, JxlModularImage img, int beginC, int rctType)
    {
        JxlTransform.CheckEqualChannels(img, beginC, beginC + 2);

        int m = beginC;
        JxlModularChannel c0 = img.Channels[m + 0];
        int w = c0.Width;
        int h = c0.Height;

        if (rctType == 0)
        {
            // No-op
            return;
        }

        int permutation = rctType / 7;

        if (permutation >= 7)
        {
            throw new InvalidOperationException("Permutation must be <= 6");
        }

        int custom = rctType % 7;

        if (custom == 0)
        {
            // Permute-only.
            JxlModularChannel ch0 = img.Channels[m];
            JxlModularChannel ch1 = img.Channels[m + 1];
            JxlModularChannel ch2 = img.Channels[m + 2];
            img.Channels[m + (permutation % 3)] = ch0;
            img.Channels[m + ((permutation + 1 + (permutation / 3)) % 3)] = ch1;
            img.Channels[m + ((permutation + 2 - (permutation / 3)) % 3)] = ch2;
            return;
        }

        _ = Parallel.For(0, configuration.MaxDegreeOfParallelism, y =>
        {
            Span<int> in0 = img.Channels[m].GetRow(y);
            Span<int> in1 = img.Channels[m + 1].GetRow(y);
            Span<int> in2 = img.Channels[m + 2].GetRow(y);

            Span<int> out0 = img.Channels[m + (permutation % 3)].GetRow(y);
            Span<int> out1 = img.Channels[m + ((permutation + 1 + (permutation / 3)) % 3)].GetRow(y);
            Span<int> out2 = img.Channels[m + ((permutation + 2 - (permutation / 3)) % 3)].GetRow(y);

            InverseRctRow(custom, in0, in1, in2, out0, out1, out2);
        });
    }
}
