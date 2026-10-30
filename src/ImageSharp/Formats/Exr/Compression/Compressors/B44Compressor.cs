// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics.Tensors;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp.Memory;

namespace SixLabors.ImageSharp.Formats.Exr.Compression.Compressors;

internal class B44Compressor : ExrBaseCompressor
{
    public B44Compressor(Stream output, MemoryAllocator allocator, uint bytesPerBlock, uint bytesPerRow, uint rowsPerBlock, int width)
        : base(output, allocator, bytesPerBlock, bytesPerRow, rowsPerBlock, width)
    {
    }

    public override uint CompressRowBlock(Span<byte> rows, int rowCount) => throw new NotImplementedException();

    private static int Pack(in InlineArray16<ushort> s, ref InlineArray14<byte> b, int flatFields, int exactMax)
    {
        Span<int> d = stackalloc int[16];
        Span<int> r = stackalloc int[16].Slice(0, 15);
        Span<ushort> t = stackalloc ushort[16];

        const int bias = 0x20;

        for (int i = 0; i < 16; ++i)
        {
            if ((s[i] & 0x7c00) == 0x7c00)
            {
                t[i] = 0x8000;
            }
            else if ((s[i] & 0x8000) != 0)
            {
                t[i] = (ushort)~s[i];
            }
            else
            {
                t[i] = (ushort)(s[i] | 0x8000);
            }
        }

        int tMax = TensorPrimitives.Max<ushort>(t);
        int shift = 0;

        int rMin = 0;
        int rMax = 0;

        do
        {
            shift += 1;

            // Compute absolute differences d[n] between
            // tMax and t[n]. Shift and round the
            // absolute differences.
            for (int i = 0; i < 16; i++)
            {
                d[i] = ShiftAndRound(tMax - t[i], shift);
            }

            // Convert d[n] into running differences.
            r[0] = d[0] - d[4] + bias;
            r[1] = d[4] - d[8] + bias;
            r[2] = d[8] - d[12] + bias;

            r[3] = d[0] - d[1] + bias;
            r[4] = d[4] - d[5] + bias;
            r[5] = d[8] - d[9] + bias;
            r[6] = d[12] - d[13] + bias;

            r[7] = d[1] - d[2] + bias;
            r[8] = d[5] - d[6] + bias;
            r[9] = d[9] - d[10] + bias;
            r[10] = d[13] - d[14] + bias;

            r[11] = d[2] - d[3] + bias;
            r[12] = d[6] - d[7] + bias;
            r[13] = d[10] - d[11] + bias;
            r[14] = d[14] - d[15] + bias;

            rMin = r[0];
            rMax = r[0];

            for (int i = 1; i < 15; ++i)
            {
                if (rMin > r[i])
                {
                    rMin = r[i];
                }

                if (rMax < r[i])
                {
                    rMax = r[i];
                }
            }
        }
        while (rMin < 0 || rMax > 0x3F);

        if (rMin == bias && rMax == bias && flatFields > 0)
        {
            // Special case - all pixels have the same value.
            // We encode this in 3 instead of 14 bytes by
            // storing the value 0xfc in the third output byte,
            // which cannot occur in the 14-byte encoding.
            b[0] = (byte)(t[0] >> 8);
            b[1] = (byte)t[0];
            b[2] = 0xFC;

            return 3;
        }

        // Pack t[0], shift, and r[n] into 14 bytes.
        b[0] = (byte)(t[0] >> 8);
        b[1] = (byte)t[0];
        b[2] = (byte)((shift << 2) | (r[0] >> 4));
        b[3] = (byte)((r[0] << 4) | (r[1] >> 2));
        b[4] = (byte)((r[1] << 6) | r[2]);
        b[5] = (byte)((r[3] << 2) | (r[4] >> 4));
        b[6] = (byte)((r[4] << 4) | (r[5] >> 2));
        b[7] = (byte)((r[5] << 6) | r[6]);
        b[8] = (byte)((r[7] << 2) | (r[8] >> 4));
        b[9] = (byte)((r[8] << 4) | (r[9] >> 2));
        b[10] = (byte)((r[9] << 6) | r[10]);
        b[11] = (byte)((r[11] << 2) | (r[12] >> 4));
        b[12] = (byte)((r[12] << 4) | (r[13] >> 2));
        b[13] = (byte)((r[13] << 6) | r[14]);

        return 14;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ShiftAndRound(int x, int shift)
    {
        // Compute y = x * pow (2, -shift), then round
        // y to the nearest integer.
        // In case of a tie, where y is exactly
        // halfway between two integers, round
        // to the even one.
        x <<= 1;
        int a = (1 << shift) - 1;
        shift++;
        int b = (x >> shift) & 1;
        return (x + a + b) >> shift;
    }

    protected override void Dispose(bool disposing) => throw new NotImplementedException();
}
