// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers;

namespace SixLabors.ImageSharp.Formats.Jxl.Memory;

internal interface IJxlMemoryManager
{
    IMemoryOwner<T> Allocate<T>(int size);
    IMemoryOwner<byte> Allocate(int size);
}
