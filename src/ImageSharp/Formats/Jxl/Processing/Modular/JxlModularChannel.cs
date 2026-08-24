// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.Formats.Jxl.Memory;

namespace SixLabors.ImageSharp.Formats.Jxl.Processing.Modular;

/// <summary>
/// A wrapper over <see cref="JxlPlane{T}"/> for modular operations.
/// </summary>
internal sealed class JxlModularChannel
{
    /// <summary>
    /// Underlying plane buffer.
    /// </summary>
    private JxlPlane<int> plane;

    public JxlModularChannel(Configuration configuration, int width, int height, int horizShift, int vertShift)
    {
        this.HorizontalShift = horizShift;
        this.VerticalShift = vertShift;
        this.Width = width;
        this.Height = height;
        this.plane = JxlPlane<int>.Create(configuration, width, height);
    }

    /// <summary>
    /// Gets or sets the image width.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the image height.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the image horizontal shift.
    /// </summary>
    /// <remarks>
    /// width ~= width &gt;&gt; HorizontalShift
    /// </remarks>
    public int HorizontalShift { get; set; }

    /// <summary>
    /// Gets or sets the image vertical shift.
    /// </summary>
    /// <remarks>
    /// height ~= height &gt;&gt; VerticalShift
    /// </remarks>
    public int VerticalShift { get; set; }

    /// <summary>
    /// Gets or sets the index of the component.
    /// </summary>
    public int Component { get; set; } = -1;

    public void Shrink(Configuration configuration)
    {
        if (this.plane.XSize == this.Width && this.plane.YSize == this.Height)
        {
            return;
        }

        this.plane.Dispose();
        this.plane = JxlPlane<int>.Create(configuration, this.Width, this.Height);
    }

    public void Shrink(Configuration configuration, int newWidth, int newHeight)
    {
        this.Width = newWidth;
        this.Height = newHeight;
        this.Shrink(configuration);
    }

    public Span<int> GetRow(int y) => this.plane.GetRow(y);
}
