﻿namespace Machete.Slices
{
    using System;


    /// <summary>
    /// A value, parsed from a slice and converted using a value factory. All parsing and conversion
    /// is lazy, performed when the value is accessed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SinglePositionSliceValue<T> :
        Value<T>
    {
        readonly TextSlice _slice;
        readonly int _position;
        readonly ValueFactory<T> _valueFactory;

        Value<T> _value;
        bool _valueComputed;

        public SinglePositionSliceValue(TextSlice slice, int position, ValueFactory<T> valueFactory)
        {
            _slice = slice;
            _position = position;
            _valueFactory = valueFactory;
        }

        Type IValue.ValueType => typeof(T);

        bool IValue.IsPresent => _valueComputed ? _value.IsPresent : GetValue().IsPresent;

        bool IValue.HasValue => _valueComputed ? _value.HasValue : GetValue().HasValue;

        T Value<T>.Value => _valueComputed ? _value.Value : GetValue().Value;

        Value<T> GetValue()
        {
            if (_slice.TryGetSlice(_position, out var slice))
            {
                _value = _valueFactory(slice) ?? Value.Invalid<T>(slice);
            }
            else
            {
                _value = Value.Missing<T>();
            }

            _valueComputed = true;

            return _value;
        }
    }
}