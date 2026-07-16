// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Runtime.InteropServices;

namespace SixLabors.ImageSharp.Formats.Jxl.IO;

[StructLayout(LayoutKind.Sequential)]
internal struct JxlAnsEntry
{
    // Although the Entry struct looks like this:
    //     uint8_t cutoff;
    //     uint8_t right_value;
    //     uint16_t freq0;
    //     uint16_t offsets1;
    //     uint16_t freq1_xor_freq0;
    // and clearly uses smaller types (e.g. byte or ushort),
    // prefer using int here as otherwise we have to
    // introduce many casts: some to assign values, others to
    // convert from unsigned to signed kinds.
    //
    // This struct is 20 bytes which is more than the recommended
    // maximum of 16 bytes, but I believe it justifies more due to
    // reduced heap allocations that would be introduced if this
    // struct would be turned into a class.
    public int Entry;
    public int RightValue;
    public int Frequency0;
    public int Offsets1;
    public int Frequency1XorFrequency0;
}
