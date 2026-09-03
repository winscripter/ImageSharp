// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics;
using SixLabors.ImageSharp.Formats.Jxl.Processing;

namespace SixLabors.ImageSharp.Formats.Jxl.Cms.TransferFunctions;

internal sealed class JxlHybridLogGammaTransferFunction : JxlHybridLogGammaTransferFunctionBase
{
    private const float HiAdd = B * Inverse12;
    private const float HiMul = 0.003639807079052639f; // MathF.Exp(-C * RA) * Inverse12
    private const float HiPow = 8.067285659607931f; // RA * JxlMath.InverseLog2E

    /// <summary>
    /// Initializes a new instance of the <see cref="JxlHybridLogGammaTransferFunction"/> class.
    /// </summary>
    /// <remarks>
    /// Use static methods. Don't instantiate this class.
    /// </remarks>
    private JxlHybridLogGammaTransferFunction()
    {
    }

    public static Vector<float> EncodedFromDisplay(Vector<float> x)
    {
        Vector<float> sign = Vector.Create(0x80000000u).As<uint, float>();
        Vector<float> originalSign = x & sign;
        x = Vector.AndNot(sign, x);
        Vector<int> belowInverse12 = Vector.LessThan(x, Vector.Create(Inverse12));

        Vector<float> lo = Vector.SquareRoot(Vector.Create(3.0f) * x);
        Vector<float> hi = (Vector.Create(A * JxlMath.InverseLog2E) * Vector.Log2((Vector.Create(12f) * x) + Vector.Create(-B))) + Vector.Create(C);
        Vector<float> magnitude = Vector.ConditionalSelect(belowInverse12, lo, hi);
        return Vector.AndNot(sign, magnitude) | originalSign;
    }

    public static Vector<float> DisplayFromEncoded(Vector<float> x)
    {
        Vector<float> sign = Vector.Create(0x80000000u).As<uint, float>();
        Vector<float> originalSign = x & sign;
        x = Vector.AndNot(sign, x);
        Vector<int> below05 = Vector.LessThan(x, Vector.Create(0.5f));

        Vector<float> lo = x * (x * Vector.Create(1f / 3f));
        Vector<float> hi = (Pow2(x * Vector.Create(HiPow)) * Vector.Create(HiMul)) + Vector.Create(HiAdd);
        Vector<float> magnitude = Vector.ConditionalSelect(below05, lo, hi);

        return Vector.AndNot(sign, magnitude) | originalSign;
    }

    private static Vector<float> Pow2(Vector<float> x) => x * x;
}
