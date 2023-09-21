﻿using Sorling.SqlExec.mapper.typemaps;
using System;

namespace Sorling.SqlExec.mapper;

public static class MapType
{
   private static bool? IsTypeNullable(Type type)
      => !type.IsValueType
      ? true
      : Nullable.GetUnderlyingType(type) != null
      ? true
      : null;

   public static ITypeMapper? GetTypeMapper(Type type, bool? isNullable = null)
      => type == typeof(byte) || type == typeof(sbyte)
      ? new ByteGetterMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(short) || type == typeof(ushort)
      ? new Int16Mapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(int) || type == typeof(uint)
      ? new Int32Mapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(long) || type == typeof(ulong)
      ? new Int64Mapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(float)
      ? new FloatMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(double)
      ? new DoubleMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(decimal)
      ? new DecimalMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(bool)
      ? new BooleanMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(string)
      ? new StringMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(char)
      ? new CharMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(Guid)
      ? new GuidMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(DateTime)
      ? new DateTimeMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(DateTimeOffset)
      ? new DateTimeOffsetMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(TimeSpan)
      ? new DateTimeOffsetMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(byte[])
      ? new ByteArrayMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(char[])
      ? new CharArrayMapper(isNullable ?? IsTypeNullable(type) ?? false)
      : type == typeof(byte?) || type == typeof(sbyte?)
      ? new ByteGetterMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(short?) || type == typeof(ushort?)
      ? new Int16Mapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(int?) || type == typeof(uint?)
      ? new Int32Mapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(long?) || type == typeof(ulong?)
      ? new Int64Mapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(float?)
      ? new FloatMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(double?)
      ? new DoubleMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(decimal?)
      ? new DecimalMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(bool?)
      ? new BooleanMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(char?)
      ? new CharMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(Guid?)
      ? new GuidMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(DateTime?)
      ? new DateTimeMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(DateTimeOffset?)
      ? new DateTimeOffsetMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(TimeSpan?)
      ? new TimeSpanMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(byte?[])
      ? new ByteArrayMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type == typeof(char?[])
      ? new CharArrayMapper(isNullable ?? IsTypeNullable(type) ?? true)
      : type.IsEnum
      ? new EnumMapper(isNullable ?? IsTypeNullable(type) ?? true, type)
      : throw new NotSupportedException($"No parameter mapper found for type '{type.FullName}'");
}