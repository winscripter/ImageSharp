// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Buffers;

namespace SixLabors.ImageSharp.Formats.Jxl.Memory;

/// <summary>
/// Allows allocation and deallocation of managed memory.
/// </summary>
internal sealed class JxlMemoryManager : IJxlMemoryManager
{
    public static readonly JxlMemoryManager Instance = new();

    public IMemoryOwner<T> Allocate<T>(int size) => MemoryPool<T>.Shared.Rent(size);

    public IMemoryOwner<byte> Allocate(int size) => this.Allocate<byte>(size);
}
