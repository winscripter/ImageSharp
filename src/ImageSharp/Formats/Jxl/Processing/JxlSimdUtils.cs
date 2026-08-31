// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using SixLabors.ImageSharp.Common.Helpers;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing;

/// <summary>
/// Shared SIMD-accelerated utilities.
/// </summary>
internal static partial class JxlSimdUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<int> ConcatLowerLower(Vector256<int> a, Vector256<int> b) => Vector256.Create(a.GetLower(), b.GetLower());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector256<int> ConcatUpperUpper(Vector256<int> a, Vector256<int> b) => Vector256.Create(a.GetUpper(), b.GetUpper());

    public static void Transpose8x8Block(Span<int> fromSpan, Span<int> toSpan, int stride)
    {
        ref int from = ref MemoryMarshal.GetReference(fromSpan);
        ref int to = ref MemoryMarshal.GetReference(toSpan);

        if (Vector256.IsHardwareAccelerated)
        {
            Vector256<int> i0 = Vector256.LoadUnsafe(ref from);
            Vector256<int> i1 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, stride));
            Vector256<int> i2 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 2 * stride));
            Vector256<int> i3 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 3 * stride));
            Vector256<int> i4 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 4 * stride));
            Vector256<int> i5 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 5 * stride));
            Vector256<int> i6 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 6 * stride));
            Vector256<int> i7 = Vector256.LoadUnsafe(ref Unsafe.Add(ref from, 7 * stride));

            Vector256<int> q0 = Vector256_.InterleaveLower(i0, i2);
            Vector256<int> q1 = Vector256_.InterleaveLower(i1, i3);
            Vector256<int> q2 = Vector256_.InterleaveUpper(i0, i2);
            Vector256<int> q3 = Vector256_.InterleaveUpper(i1, i3);
            Vector256<int> q4 = Vector256_.InterleaveLower(i4, i6);
            Vector256<int> q5 = Vector256_.InterleaveLower(i5, i7);
            Vector256<int> q6 = Vector256_.InterleaveUpper(i4, i6);
            Vector256<int> q7 = Vector256_.InterleaveUpper(i5, i7);

            Vector256<int> r0 = Vector256_.InterleaveLower(q0, q1);
            Vector256<int> r1 = Vector256_.InterleaveUpper(q0, q1);
            Vector256<int> r2 = Vector256_.InterleaveLower(q2, q3);
            Vector256<int> r3 = Vector256_.InterleaveUpper(q2, q3);
            Vector256<int> r4 = Vector256_.InterleaveLower(q4, q5);
            Vector256<int> r5 = Vector256_.InterleaveUpper(q4, q5);
            Vector256<int> r6 = Vector256_.InterleaveLower(q6, q7);
            Vector256<int> r7 = Vector256_.InterleaveUpper(q6, q7);

            i0 = ConcatLowerLower(r4, r0);
            i1 = ConcatLowerLower(r5, r1);
            i2 = ConcatLowerLower(r6, r2);
            i3 = ConcatLowerLower(r7, r3);
            i4 = ConcatUpperUpper(r4, r0);
            i5 = ConcatUpperUpper(r5, r1);
            i6 = ConcatUpperUpper(r6, r2);
            i7 = ConcatUpperUpper(r7, r3);

            i0.StoreUnsafe(ref to);
            i1.StoreUnsafe(ref Unsafe.Add(ref to, 8));
            i2.StoreUnsafe(ref Unsafe.Add(ref to, 16));
            i3.StoreUnsafe(ref Unsafe.Add(ref to, 24));
            i4.StoreUnsafe(ref Unsafe.Add(ref to, 32));
            i5.StoreUnsafe(ref Unsafe.Add(ref to, 40));
            i6.StoreUnsafe(ref Unsafe.Add(ref to, 48));
            i7.StoreUnsafe(ref Unsafe.Add(ref to, 56));
        }
        else
        {
            // Vector128 fallback
            for (int n = 0; n < 8; n += 4)
            {
                for (int m = 0; m < 8; m += 4)
                {
                    Vector128<int> p0 = Vector128.LoadUnsafe(ref Unsafe.Add(ref from, (n * stride) + m));
                    Vector128<int> p1 = Vector128.LoadUnsafe(ref Unsafe.Add(ref from, ((n + 1) * stride) + m));
                    Vector128<int> p2 = Vector128.LoadUnsafe(ref Unsafe.Add(ref from, ((n + 2) * stride) + m));
                    Vector128<int> p3 = Vector128.LoadUnsafe(ref Unsafe.Add(ref from, ((n + 3) * stride) + m));

                    Vector128<int> q0 = Vector128_.InterleaveLower(p0, p2);
                    Vector128<int> q1 = Vector128_.InterleaveLower(p1, p3);
                    Vector128<int> q2 = Vector128_.InterleaveUpper(p0, p2);
                    Vector128<int> q3 = Vector128_.InterleaveUpper(p1, p3);

                    Vector128<int> r0 = Vector128_.InterleaveLower(q0, q1);
                    Vector128<int> r1 = Vector128_.InterleaveUpper(q0, q1);
                    Vector128<int> r2 = Vector128_.InterleaveLower(q2, q3);
                    Vector128<int> r3 = Vector128_.InterleaveUpper(q2, q3);

                    r0.StoreUnsafe(ref Unsafe.Add(ref to, (m * 8) + n));
                    r1.StoreUnsafe(ref Unsafe.Add(ref to, ((m + 1) * 8) + n));
                    r2.StoreUnsafe(ref Unsafe.Add(ref to, ((m + 2) * 8) + n));
                    r3.StoreUnsafe(ref Unsafe.Add(ref to, ((m + 3) * 8) + n));
                }
            }
        }
    }
}
