// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics.Tensors;
using SixLabors.ImageSharp.Formats.Jxl.Fields;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular.Transforms;

internal sealed class JxlTransform : IJxlFields
{
    public bool Visit(JxlVisitor visitor) => throw new NotImplementedException();

    public static void CheckEqualChannels(JxlModularImage image, int c1, int c2)
    {
        int channelsCount = image.Channels.Count;

        if (c1 > channelsCount || c2 >= channelsCount || c2 < c1)
        {
            throw new InvalidOperationException($"Invalid channel range: {c1}..{c2} (there are only {channelsCount} channels)");
        }

        if (c1 < image.NbMetaChannels && c2 >= image.NbMetaChannels)
        {
            throw new InvalidOperationException("Invalid: transforming mix of meta and nonmeta");
        }

        JxlModularChannel ch1 = image.Channels[c1];
        for (int c = c1 + 1; c <= c2; c++)
        {
            JxlModularChannel ch2 = image.Channels[c];
            if (ch1.Width != ch2.Width ||
                ch1.Height != ch2.Height ||
                ch1.HorizontalShift != ch2.HorizontalShift ||
                ch1.VerticalShift != ch2.VerticalShift)
            {
                throw new InvalidOperationException($"Channel {c} is not equal");
            }
        }
    }

    public static void ComputeMinMax(JxlModularChannel channel, out int min, out int max)
    {
        // Start with opposite bounds so the first iteration
        // guarantees to set these values
        min = int.MaxValue;
        max = int.MinValue;

        for (int y = 0; y < channel.Height; y++)
        {
            ReadOnlySpan<int> p = channel.GetRow(y);

            int minRow = TensorPrimitives.Min(p);
            int maxRow = TensorPrimitives.Max(p);

            min = Math.Min(minRow, min);
            max = Math.Max(maxRow, max);
        }
    }
}
