// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Cms.TransferFunctions;

/// <summary>
/// Base class for HLG transfer function.
/// </summary>
internal abstract class JxlHybridLogGammaTransferFunctionBase
{
    // Shared constants used by transfer functions
    protected const float A = 0.17883277f;
    protected const float RA = 1.0f / A;
    protected const float B = 1 - (4 * A);
    protected const float C = 0.5599107295f;
    protected const float Inverse12 = 1.0f / 12.0f;

    /// <summary>
    /// Converts encoded signal to display signal.
    /// </summary>
    /// <param name="encoded">The encoded signal</param>
    /// <returns>The display signal</returns>
    protected static double DisplayFromEncoded(double encoded) => Ootf(InverseOotf(encoded));

    /// <summary>
    /// Converts display signal to encoded signal.
    /// </summary>
    /// <param name="display">The display signal</param>
    /// <returns>The encoded signal</returns>
    protected static double EncodedFromDisplay(double display) => Oetf(InverseOetf(display));

    /// <summary>
    /// Opto-Electronic Transfer Function - converts
    /// real-world scene light (s) into a digital video
    /// signal inside a camera.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Transfer_functions_in_imaging#Definition"/>
    /// </remarks>
    /// <param name="s">Scene light</param>
    /// <returns>Digital video signal</returns>
    private static double Oetf(double s)
    {
        if (s == 0)
        {
            return 0;
        }

        double originalSign = s;

        s = Math.Abs(s);

        if (s <= Inverse12)
        {
            return Math.CopySign(Math.Sqrt(3.0 * s), originalSign);
        }

        double e = (A * Math.Log((12 * s) - B)) + C;
        DebugGuard.MustBeGreaterThan(e, 0.0, nameof(e));

        return Math.CopySign(e, originalSign);
    }

    /// <summary>
    /// Inverse Opto-Electronic Transfer Function - converts
    /// digital video signal into a real-world scene light.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Transfer_functions_in_imaging#Definition"/>
    /// </remarks>
    /// <param name="e">Digital video signal</param>
    /// <returns>Scene light</returns>
    private static double InverseOetf(double e)
    {
        if (e == 0)
        {
            return 0;
        }

        double originalSign = e;

        e = Math.Abs(e);

        if (e <= 0.5)
        {
            return Math.CopySign(e * e * (1.0 / 3), originalSign);
        }

        double s = (Math.Exp((e - C) * RA) + B) * Inverse12;
        DebugGuard.MustBeGreaterThan(s, 0.0, nameof(s));

        return Math.CopySign(s, originalSign);
    }

    /// <summary>
    /// Opto-Optical Transfer Function - as-is.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Transfer_functions_in_imaging#Definition"/>
    /// </remarks>
    /// <param name="s">Input signal</param>
    /// <returns>Digital video signal</returns>
    private static double Ootf(double s) => s;

    /// <summary>
    /// Inverse Opto-Optical Transfer Function - as-is.
    /// </summary>
    /// <remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Transfer_functions_in_imaging#Definition"/>
    /// </remarks>
    /// <param name="s">Digital video signal</param>
    /// <returns>Scene light</returns>
    private static double InverseOotf(double s) => s;
}
