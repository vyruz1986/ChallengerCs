﻿using System.Collections.Generic;
using System.ComponentModel;

using DataConverter.Shared.ConvertedValueTypes;

namespace DataConverter.Shared
{
    public interface IConverterViewModel : INotifyPropertyChanged
    {
        int SelectedConverterIndex { get; set; }
        IEnumerable<string> Converters { get; }

        ConvertedBinaryString BinaryString { get; }
        ConvertedOctalString OctalString { get; }
        ConvertedDecimalString DecimalString { get; }
        ConvertedHexString HexString { get; }
        ConvertedAsciiString AsciiString { get; }
        ConvertedUtf8String Utf8String { get; }
        ConvertedUtf32String Utf32String { get; }
        ConvertedValue<ushort> UShort { get; }
        ConvertedValue<short> Short { get; }
        ConvertedValue<uint> UInt { get; }
        ConvertedValue<int> Int { get; }
        ConvertedValue<ulong> ULong { get; }
        ConvertedValue<long> Long { get; }
        ConvertedValue<float> Float { get; }
        ConvertedValue<double> Double { get; }
        ConvertedValue<decimal> Decimal { get; }
    }
}
