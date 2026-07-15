// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Diagnostics;
using SixLabors.ImageSharp.Formats.Jxl.Memory.ImageTypes;

namespace SixLabors.ImageSharp.Formats.Jxl.Ac;

internal sealed class JxlAcStrategyImage : IDisposable
{
    private const byte Invalid = byte.MaxValue; // 255

    private JxlImageB? layers;
    private Memory<byte> row;
    private int stride;

    public int XSize => this.layers!.XSize;

    public int YSize => this.layers!.YSize;

    public int PixelsPerRow => this.layers!.PixelsPerRow;

    public JxlAcStrategyRow GetRow(int y, int xPrefix = 0)
    {
        ReadOnlyMemory<byte> layerRow = this.layers!.GetRowBytesMemory(y);
        ReadOnlyMemory<byte> row = layerRow[xPrefix..];

        return new JxlAcStrategyRow(row);
    }

    public static JxlAcStrategyImage Create(Configuration memoryManager, int xSize, int ySize)
    {
        JxlAcStrategyImage image = new()
        {
            layers = new JxlImageB(memoryManager, xSize, ySize)
        };

        image.row = image.layers.GetRowBytesMemory(0);
        image.stride = image.layers.PixelsPerRow;

        return image;
    }

    public int CountBlocks(JxlAcStrategyType type)
    {
        int value = 0;
        int compare = ((int)type << 1) | 1;

        for (int y = 0; y < this.layers!.YSize; y++)
        {
            ReadOnlySpan<byte> row = this.layers!.GetRow(y);

            for (int x = 0; x < this.layers!.XSize; x++)
            {
                if (row[x] == compare)
                {
                    value++;
                }
            }
        }

        return value;
    }

    public JxlAcStrategyRow GetRow(in Rectangle rect, int y) => this.GetRow(rect.Y + y, rect.X);

    public bool IsValid(int x, int y) => this.row.Span[(y * this.stride) + x] != Invalid;

    public bool SetNoBoundsChecks(int x, int y, JxlAcStrategyType type, bool check = true)
    {
        JxlAcStrategy strategy = new(type);
        Span<byte> rowSpan = this.row.Span;
        int rawType = (int)type;
        int rawTypeTimes2 = rawType << 1;

        for (int iy = 0; iy < strategy.CoveredBlocksX; iy++)
        {
            for (int ix = 0; ix < strategy.CoveredBlocksX; ix++)
            {
                int pos = ((y + iy) * this.stride) + x + ix;

                if (check && rowSpan[pos] != Invalid)
                {
                    Debug.Fail("Invalid AC strategy. Blocks overlap.");

                    return false;
                }

                rowSpan[pos] = (byte)(rawTypeTimes2 | ((iy | ix) == 0 ? 1 : 0));
            }
        }

        return true;
    }

    public bool Set(int x, int y, JxlAcStrategyType type)
    {
#if DEBUG
        JxlAcStrategy strategy = new(type);

        Debug.Assert(y + strategy.CoveredBlocksY <= this.layers!.YSize, "Invalid range");
        Debug.Assert(x + strategy.CoveredBlocksX <= this.layers.XSize, "Invalid range");
#endif

        return this.SetNoBoundsChecks(x, y, type, check: false);
    }

    public void FillDct8(in Rectangle rect) => this.FillPlane(((int)JxlAcStrategyType.DCT << 1) | 1, this.layers, in rect);

    public void FillDct8() => this.FillDct8(in this.layers.GetRectangle());

    public void FillInvalid() => this.FillImage(Invalid, this.layers);

    public void Dispose()
    {
        this.layers?.Dispose();
        GC.SuppressFinalize(this);
    }
}
