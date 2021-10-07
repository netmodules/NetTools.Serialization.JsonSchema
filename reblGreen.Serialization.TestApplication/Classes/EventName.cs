﻿/*
    The MIT License (MIT)

    Copyright (c) 2019 reblGreen Software Ltd. (https://reblgreen.com/)
    Repository Url: https://bitbucket.org/reblgreen/reblgreen.netcore.modules/

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
 */

using reblGreen.Serialization.JsonSchemaEnums;
using reblGreen.Serialization.JsonSchemaAttributes;
using System;

namespace reblGreen.Serialization.TestApplication
{
    /// <summary>
    /// <see cref="EventName"/> This class acts as a simple string wrapper to offer a more self descriptive usage type.
    /// </summary>
    [Serializable]
    [JsonSchemaType(BasicType.String)]
    public sealed class EventName
    {
        readonly string Value;

        public EventName(string value, /*To test the implicit operator override this is a dummy second parameter*/ int dummy = 0)
        {
            Value = value;
        }

        #region Override IEqualityComparer methods

        public override bool Equals(object obj)
        {
            if (obj is EventName name)
            {
                return Value.Equals(name.Value);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        public override string ToString()
        {
            return Value;
        }

        public static EventName FromString(string s)
        {
            return new EventName(s);
        }

        #region Override operators.

        public static implicit operator EventName(string s)
        {
            return new EventName(s);
        }

        public static implicit operator string(EventName s)
        {
            return s.Value;
        }

        public static bool operator ==(EventName x, EventName y)
        {
            return x.Value == y.Value;
        }

        public static bool operator !=(EventName x, EventName y)
        {
            return x.Value != y.Value;
        }

        #endregion
    }
}
