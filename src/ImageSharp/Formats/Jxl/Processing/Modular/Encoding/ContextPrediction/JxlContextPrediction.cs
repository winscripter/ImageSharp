// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.InteropServices;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Encoding.ContextPrediction;

/// <summary>
/// Context prediction helper methods.
/// </summary>
internal static class JxlContextPrediction
{
    public static void SetPredictorMode(int i, JxlModularHeader header)
    {
        ref uint wr = ref header.GetWReference();
        Span<uint> w = MemoryMarshal.CreateSpan(ref wr, 4);

        switch (i)
        {
            case 0:
                // ~ lossless16 predictor
                w[0] = 0xd;
                w[1] = 0xc;
                w[2] = 0xc;
                w[3] = 0xc;
                header.P1C = 16;
                header.P2C = 10;
                header.P3Ca = 7;
                header.P3Cb = 7;
                header.P3Cc = 7;
                header.P3Cd = 0;
                header.P3Ce = 0;
                break;

            case 1:
                // ~ default lossless8 predictor
                w[0] = 0xd;
                w[1] = 0xc;
                w[2] = 0xc;
                w[3] = 0xb;
                header.P1C = 8;
                header.P2C = 8;
                header.P3Ca = 4;
                header.P3Cb = 0;
                header.P3Cc = 3;
                header.P3Cd = 23;
                header.P3Ce = 2;
                break;

            case 2:
                // ~ west lossless8 predictor
                w[0] = 0xd;
                w[1] = 0xc;
                w[2] = 0xd;
                w[3] = 0xc;
                header.P1C = 10;
                header.P2C = 9;
                header.P3Ca = 7;
                header.P3Cb = 0;
                header.P3Cc = 0;
                header.P3Cd = 16;
                header.P3Ce = 9;
                break;

            case 3:
                // ~ north lossless8 predictor
                w[0] = 0xd;
                w[1] = 0xd;
                w[2] = 0xc;
                w[3] = 0xc;
                header.P1C = 16;
                header.P2C = 8;
                header.P3Ca = 0;
                header.P3Cb = 16;
                header.P3Cc = 0;
                header.P3Cd = 23;
                header.P3Ce = 0;
                break;

            case 4:
            default:
                // something else, because why not
                w[0] = 0xd;
                w[1] = 0xc;
                w[2] = 0xc;
                w[3] = 0xc;
                header.P1C = 10;
                header.P2C = 10;
                header.P3Ca = 5;
                header.P3Cb = 5;
                header.P3Cc = 5;
                header.P3Cd = 12;
                header.P3Ce = 4;
                break;
        }
    }
}
