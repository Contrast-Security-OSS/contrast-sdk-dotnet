/*
 * Copyright (c) 2015, Contrast Security, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * Redistributions of source code must retain the above copyright notice, this list of
 * conditions and the following disclaimer.
 *
 * Redistributions in binary form must reproduce the above copyright notice, this list of
 * conditions and the following disclaimer in the documentation and/or other materials
 * provided with the distribution.
 *
 * Neither the name of the Contrast Security, Inc. nor the names of its contributors may
 * be used to endorse or promote products derived from this software without specific
 * prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
 * THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
 * OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF
 * THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using Newtonsoft.Json;

namespace Contrast.Serialization
{
    public class EpochDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if (objectType != typeof(DateTime?) && objectType != typeof(DateTimeOffset?))
                    throw new JsonSerializationException($"Cannot convert null value to {objectType}");
                return null;
            }
            else if (reader.TokenType == JsonToken.Integer)
            {
                long epochTime = (long)reader.Value;
                DateTime dateTime = DateTimeConverter.ConvertFromEpochTime(epochTime);

                if (((objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>)) ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset(dateTime);
                }

                return dateTime;
            }
            else
            {
                throw new JsonSerializationException("Must be a long integer value");
            }

        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is DateTime)
            {
                long epochTime = DateTimeConverter.ConvertToEpochTime((DateTime)value);
                writer.WriteValue(epochTime);
            }
            else
            {
                if (!(value is DateTimeOffset))
                {
                    throw new JsonSerializationException("Expected date object value.");
                }
                var datetime = ((DateTimeOffset)value).ToUniversalTime().UtcDateTime;
                writer.WriteValue(DateTimeConverter.ConvertToEpochTime(datetime));
            }
        }
    }
}
