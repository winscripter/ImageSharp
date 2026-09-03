// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Cms.TransferFunctions;

/// <summary>
/// Base class for PQ transfer function.
/// </summary>
internal abstract class JxlPerceptualQuantizationTransferFunctionBase
{
    private const double M1 = 2610.0 / 16384;
    private const double M2 = (2523.0 / 4096) * 128;
    private const double C1 = 3424.0 / 4096;
    private const double C2 = (2413.0 / 4096) * 32;
    private const double C3 = (2392.0 / 4096) * 32;

    protected static double DisplayFromEncoded(float displayIntensityTarget, double e)
    {
        if (e == 0.0)
        {
            return 0.0;
        }

        double originalSign = e;

        e = Math.Abs(e);

        double xp = Math.Pow(e, 1.0 / M2);
        double num = Math.Max(xp - C1, 0.0);
        double den = C2 - (C3 * xp);
        double d = Math.Pow(num / den, 1.0 / M1);

        return Math.CopySign(d * (10000.0 / displayIntensityTarget), originalSign);
    }

    protected static double EncodedFromDisplay(float displayIntensityTarget, double d)
    {
        if (d == 0.0)
        {
            return 0.0;
        }

        double originalSign = d;

        d = Math.Abs(d);

        double xp = Math.Pow(d * (displayIntensityTarget * (1 / 10000)), M1);
        double num = C1 + (xp * C2);
        double den = 1.0 + (xp * C3);
        double e = Math.Pow(num / den, M2);

        return Math.CopySign(e, originalSign);
    }
}
