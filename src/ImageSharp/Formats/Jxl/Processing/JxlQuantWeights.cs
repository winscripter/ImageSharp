// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Jxl.Processing;

internal static class JxlQuantWeights
{
    public const int MaxQuantTableSize = JxlAcStrategy.MaximumCoefficientArea;

    public const int NumPredefinedTables = 1;

    public const int CeilLog2NumPredefinedTables = 0;

    public const int Log2NumQuantModes = 3;

    /// <summary>
    /// DCT quantizer encoding. (6 distance bands)
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [3150f, 0f, -0.4f, -0.4f, -0.4f, -2f],
                [560f, 0f, -0.3f, -0.3f, -0.3f, -0.3f],
                [512f, -2f, -1f, 0f, -1f, -2f]
            ],
            6));

    /// <summary>
    /// Identity quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Identity = JxlQuantizerEncoding.Identity(
        [
            [280f, 3160f, 3160f],
            [60f, 864f, 864f],
            [18f, 200f, 200f],
        ]);

    /// <summary>
    /// DCT2X2 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct2x2 = JxlQuantizerEncoding.Dct2(
        [
            [3840f, 2560f, 1280f, 640f, 480f, 300f],
            [960f, 640f, 320f, 180f, 140f, 120f],
            [640f, 320f, 128f, 64f, 32f, 16f],
        ]);

    /// <summary>
    /// DCT4X4 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct4x4 = JxlQuantizerEncoding.Dct4(
        new JxlDctQuantWeightParameters(
            [
                [2200, 0, 0, 0],
                [392, 0, 0, 0],
                [112, -0.25f, -0.25f, -0.5f]
            ],
            4),
        [
            [1, 1],
            [1, 1],
            [1, 1]
        ]);

    /// <summary>
    /// DCT16x16 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct16x16 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    8996.8725711814115328f,
                    -1.3000777393353804f,
                    -0.49424529824571225f,
                    -0.439093774457103443f,
                    -0.6350101832695744f,
                    -0.90177264050827612f,
                    -1.6162099239887414f,
                ],
                [
                    3191.48366296844234752f,
                    -0.67424582104194355f,
                    -0.80745813428471001f,
                    -0.44925837484843441f,
                    -0.35865440981033403f,
                    -0.31322389111877305f,
                    -0.37615025315725483f,
                ],
                [
                    1157.50408145487200256f,
                    -2.0531423165804414f,
                    -1.4f,
                    -0.50687130033378396f,
                    -0.42708730624733904f,
                    -1.4856834539296244f,
                    -4.9209142884401604f,
                ]
            ],
            7));

    /// <summary>
    /// DCT32x32 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct32x32 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    15718.40830982518931456f,
                    -1.025f,
                    -0.98f,
                    -0.9012f,
                    -0.4f,
                    -0.48819395464f,
                    -0.421064f,
                    -0.27f,
                ],
                [
                    7305.7636810695983104f,
                    -0.8041958212306401f,
                    -0.7633036457487539f,
                    -0.55660379990111464f,
                    -0.49785304658857626f,
                    -0.43699592683512467f,
                    -0.40180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    3803.53173721215041536f,
                    -3.060733579805728f,
                    -2.0413270132490346f,
                    -2.0235650159727417f,
                    -0.5495389509954993f,
                    -0.4f,
                    -0.4f,
                    -0.3f,
                ]
            ],
            7));

    /// <summary>
    /// DCT8x16 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct8x16 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    7240.7734393502f,
                    -0.7f,
                    -0.7f,
                    -0.2f,
                    -0.2f,
                    -0.2f,
                    -0.5f,
                ],
                [
                    1448.15468787004f,
                    -0.5f,
                    -0.5f,
                    -0.5f,
                    -0.2f,
                    -0.2f,
                    -0.2f,
                ],
                [
                    506.854140754517f,
                    -1.4f,
                    -0.2f,
                    -0.5f,
                    -0.5f,
                    -1.5f,
                    -3.6f,
                ]
            ],
            7));

    /// <summary>
    /// DCT8x32 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct8x32 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    16283.2494710648897f,
                    -1.7812845336559429f,
                    -1.6309059012653515f,
                    -1.0382179034313539f,
                    -0.85f,
                    -0.7f,
                    -0.9f,
                    -1.2360638576849587f,
                ],
                [
                    5089.15750884921511936f,
                    -0.320049391452786891f,
                    -0.35362849922161446f,
                    -0.30340000000000003f,
                    -0.61f,
                    -0.5f,
                    -0.5f,
                    -0.6f,
                ],
                [
                    3397.77603275308720128f,
                    -0.321327362693153371f,
                    -0.34507619223117997f,
                    -0.70340000000000003f,
                    -0.9f,
                    -1.0f,
                    -1.0f,
                    -1.1754605576265209f,
                ]
            ],
            8));

    /// <summary>
    /// DCT16x32 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct16x32 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    13844.97076442300573f,
                    -0.97113799999999995f,
                    -0.658f,
                    -0.42026f,
                    -0.22712f,
                    -0.2206f,
                    -0.226f,
                    -0.6f,
                ],
                [
                    4798.964084220744293f,
                    -0.61125308982767057f,
                    -0.83770786552491361f,
                    -0.79014862079498627f,
                    -0.2692727459704829f,
                    -0.38272769465388551f,
                    -0.22924222653091453f,
                    -0.20719098826199578f,
                ],
                [
                    1807.236946760964614f,
                    -1.2f,
                    -1.2f,
                    -0.7f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT4x8 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct4x8 = JxlQuantizerEncoding.Dct4x8(
        new JxlDctQuantWeightParameters(
            [
                [
                    2198.050556016380522f,
                    -0.96269623020744692f,
                    -0.76194253026666783f,
                    -0.6551140670773547f
                ],
                [
                    764.3655248643528689f,
                    -0.92630200888366945f,
                    -0.9675229603596517f,
                    -0.27845290869168118f
                ],
                [
                    527.107573587542228f,
                    -1.4594385811273854f,
                    -1.450082094097871593f,
                    -1.5843722511996204f
                ]
            ],
            4),
        [1, 1, 1]);

    /// <summary>
    /// AFV quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Afv = JxlQuantizerEncoding.Afv(
        Dct4x8.DctParameters!,
        Dct4x4.DctParameters!,
        [
            [3072, 3072, 256, 256, 256, 414, 0, 0, 0],
            [1024, 1024, 50, 50, 50, 58, 0, 0, 0],
            [384, 384, 12, 12, 12, 22, -0.25f, -0.25f, -0.25f]
        ]);

    /// <summary>
    /// DCT64x64 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct64x64 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    0.9f * 26629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    0.9f * 9311.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    0.9f * 4992.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT32x64 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct32x64 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    0.65f * 23629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    0.65f * 8611.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    0.65f * 4492.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT128x128 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct128x128 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    1.8f * 26629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    1.8f * 9311.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    1.8f * 4992.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT64x128 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct64x128 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    1.3f * 23629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    1.3f * 8611.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    1.3f * 4492.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT256x256 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct256x256 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    3.6f * 26629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    3.6f * 9311.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    3.6f * 4992.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));

    /// <summary>
    /// DCT128x256 quantizer encoding.
    /// </summary>
    public static readonly JxlQuantizerEncoding Dct128x256 = JxlQuantizerEncoding.Dct(
        new JxlDctQuantWeightParameters(
            [
                [
                    2.6f * 23629.073922049845f,
                    -1.025f,
                    -0.78f,
                    -0.65012f,
                    -0.19041574084286472f,
                    -0.20819395464f,
                    -0.421064f,
                    -0.32733845535848671f,
                ],
                [
                    2.6f * 8611.3238710010046f,
                    -0.3041958212306401f,
                    -0.3633036457487539f,
                    -0.35660379990111464f,
                    -0.3443074455424403f,
                    -0.33699592683512467f,
                    -0.30180866526242109f,
                    -0.27321683125358037f,
                ],
                [
                    2.6f * 4492.2486445538634f,
                    -1.2f,
                    -1.2f,
                    -0.8f,
                    -0.7f,
                    -0.7f,
                    -0.4f,
                    -0.5f,
                ]
            ],
            8));
}
