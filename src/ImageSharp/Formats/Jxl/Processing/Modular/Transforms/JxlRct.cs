// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Transforms;

/// <summary>
/// Reversible Color Transform (RCT)
/// </summary>
internal static class JxlRct
{
    public static void InverseRctRow(int transformType, Span<int> in0, Span<int> in1, Span<int> in2, Span<int> out0, Span<int> out1, Span<int> out2, int width)
    {
        DebugGuard.MustBeBetweenOrEqualTo(transformType, 0, 6, nameof(transformType));

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
}
