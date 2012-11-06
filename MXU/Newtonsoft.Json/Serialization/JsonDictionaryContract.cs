#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
  /// <summary>
  /// Contract details for a <see cref="Type"/> used by the <see cref="JsonSerializer"/>.
  /// </summary>
  public class JsonDictionaryContract : JsonContract
  {
    internal Type DictionaryTypeToCreate { get; private set; }
    internal Type DictionaryKeyType { get; private set; }
    internal Type DictionaryValueType { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDictionaryContract"/> class.
    /// </summary>
    /// <param name="underlyingType">The underlying type for the contract.</param>
    public JsonDictionaryContract(Type underlyingType)
      : base(underlyingType)
    {
      Type keyType;
      Type valueType;
      ReflectionUtils.GetDictionaryKeyValueTypes(UnderlyingType, out keyType, out valueType);

      DictionaryKeyType = keyType;
      DictionaryValueType = valueType;

      if (IsTypeGenericDictionaryInterface(UnderlyingType))
      {
        DictionaryTypeToCreate = ReflectionUtils.MakeGenericType(typeof(Dictionary<,>), keyType, valueType);
      }
      else
      {
        DictionaryTypeToCreate = underlyingType;
      }
    }

    private bool IsTypeGenericDictionaryInterface(Type type)
    {
      if (!type.IsGenericType)
        return false;

      Type genericDefinition = type.GetGenericTypeDefinition();

      return (genericDefinition == typeof(IDictionary<,>));
    }
  }
}