// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Diagnostics;

#pragma warning disable SA1405 // Debug.Assert should provide message text

namespace SixLabors.ImageSharp.Formats.Jxl.Fields;

internal class JxlVisitorBase : JxlVisitor
{
    private readonly JxlExtensionStates extensionStates = new();
    private int depth;

    public override bool Visit(IJxlFields fields)
    {
        if (this.depth >= JxlBundle.MaxExtensions)
        {
            return false;
        }

        this.depth++;
        this.extensionStates.Push();

        bool visited = fields.Visit(this);

        if (visited)
        {
            // TODO: use DebugGuard
            Debug.Assert(!this.extensionStates.IsBegun || this.extensionStates.IsEnded);
        }

        this.extensionStates.Pop();

        // TODO: use DebugGuard
        Debug.Assert(this.depth != 0);
        this.depth--;

        return visited;
    }

    public override bool Boolean(bool defaultValue, ref bool value)
    {
        uint bits = value ? 1u : 0u;
        if (!this.Bits(1, defaultValue ? 1u : 0u, ref bits))
        {
            return false;
        }

        // TODO: use DebugGuard
        Debug.Assert(bits <= 1u);

        value = bits == 1u;

        return true;
    }

    public override bool BeginExtensions(ref ulong extensions)
    {
        if (!this.U64(0uL, ref extensions))
        {
            return false;
        }

        this.extensionStates.Begin();
        return true;
    }

    public override bool EndExtensions()
    {
        this.extensionStates.End();
        return true;
    }
}
