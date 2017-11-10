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

namespace contrast_rest_dotnet.Serialization
{
    public static class DateTimeConverter 
    {
        private static DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a Unix time (or epoch) representation to a DateTime object with UTC timezone.
        /// </summary>
        /// <param name="epochTime">Unix time in milliseconds.</param>
        /// <returns>A DateTime object for the given time.</returns>
        public static DateTime? ConvertToDateTime(long? epochTime)
        {
            if (epochTime != null)
                return _epoch.AddMilliseconds((long)epochTime);
            else
                return null;
        }

        /// <summary>
        /// Converts a DateTime object to Unix time representation in milliseconds.
        /// </summary>
        /// <param name="dateTime">DateTime object to be converted.</param>
        /// <returns>A milliseconds representation of Unix time.</returns>
        public static long? ConvertToUnixTime(DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            TimeSpan difference = dateTime.Value.ToUniversalTime() - _epoch;
            return Convert.ToInt64(Math.Floor(difference.TotalMilliseconds));
        }
    }
}
