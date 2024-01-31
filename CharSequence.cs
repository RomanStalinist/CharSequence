namespace System
{
    public class CharSequence
    {
        // Main chars
        private char[] chars = [];
        public CharSequence super = this;
        public static CharSequence empty = "";

        // Constructors
        public CharSequence(char otherChar) => chars = [otherChar];
        public CharSequence(char[] otherChars) => chars = otherChars;
        public CharSequence(CharSequence other) => chars = other.chars;
        public CharSequence(string otherString) => chars = otherString.ToCharArray();
        public CharSequence(object otherObject) => chars = otherObject.ToString()!.ToCharArray();
        public CharSequence(byte[] otherBytes)
        {
            chars = new char[otherBytes.Length];

            for (int i = 0; i < otherBytes.Length; i++)
            {
                chars[i] = (char)otherBytes[i];
            }
        }
        public CharSequence(Text.StringBuilder otherStringBuilder) => chars = otherStringBuilder.ToString().ToCharArray();

        // Operators
        public static CharSequence operator +(CharSequence it, string other) => new(it.toString() + other);
        public static CharSequence operator +(CharSequence it, char[] chars) => new(it.toString() + chars.toCharSequence());
        public static CharSequence operator +(CharSequence it, CharSequence other) => new(it.toString() + other.ToString());
        public static CharSequence operator +(CharSequence it, Text.StringBuilder other) => new(it.toString() + other.ToString());
        public static CharSequence operator *(CharSequence it, int count) => new(it.repeat(count));

        // Methods
        public CharSequence append(bool b) => chars.toCharSequence() + b.ToString()!;
        public CharSequence append(char c) => chars.toCharSequence() + c.ToString()!;
        public CharSequence append(char[] str) => chars.toCharSequence() + new CharSequence(str);
        public CharSequence append(char[] str, int offset, int end) => chars.toCharSequence() + new CharSequence(str.Where((chr, ind) => ind >= offset && ind <= end).ToArray());
        public CharSequence append(double d) => chars.toCharSequence() + d.ToString();
        public CharSequence append(float f) => chars.toCharSequence() + f.ToString();
        public CharSequence append(int i) => chars.toCharSequence() + i.ToString();
        public CharSequence append(long lng) => chars.toCharSequence() + lng.ToString();
        public CharSequence append(CharSequence s) => chars.toCharSequence() + s;
        public CharSequence append(CharSequence s, int offset, int end) => chars.toCharSequence() + new CharSequence(s.toString().Where((chr, ind) => ind >= offset && ind <= end).ToArray());
        public CharSequence append(object obj) => chars.toCharSequence() + obj.ToString()!;
        public CharSequence append(string str) => chars.toCharSequence() + str;
        public CharSequence append(Text.StringBuilder sb) => chars.toCharSequence() + sb.ToString();


        public char charAt(int index) => chars[index];
        public int compareTo(CharSequence other)
        {
            int minLength = Math.Min(length(), other.length()),
                result = 0;
            for (int i = 0; i < minLength; i++)
            {
                result += charAt(i) - other.charAt(i);
            }
            return result;
        }
        public int compareToIgnoreCase(CharSequence other)
        {
            for (int i = 0; i < length(); i++)
            {
                chars[i] = charAt(i).toLower();
            }
            for (int i = 0; i < other.length(); i++)
            {
                other.chars[i] = other.charAt(i).toLower();
            }
            return compareTo(other);
        }

        public CharSequence concat(string other) => this + other;
        public CharSequence concat(CharSequence other) => this + other;
        public CharSequence concat(Text.StringBuilder other) => this + other.ToString();

        public bool contains(CharSequence other) => Text.RegularExpressions.Regex.IsMatch(toString(), other.toString());
        public bool contains(string other) => contains(other.toCharSequence());
        public bool contains(Text.StringBuilder other) => contains(other.toCharSequence());

        public CharSequence delete(int start, int end) => toString().Where((chr, ind) => ind < start || ind > end).ToArray().toCharSequence();
        public CharSequence deleteCharAt(int index) => delete(index, index);

        public void ensureCapacity(int minCapacity)
        {
            if (minCapacity <= length())
                return;

            char[] oldChars = chars;
            chars = new char[minCapacity];
            for (int i = 0; i < oldChars.Length; i++)
            {
                chars[i] = oldChars[i];
            }
        }

        public bool endsWith(char letter)
        {
            if (charAt(length() - 1) == letter)
                return true;
            return false;
        }
        public bool endsWith(string str)
        {
            if (substring(length() - str.Length).equals(str))
                return true;
            return false;
        }

        public bool equalsIgnoreCase(CharSequence other)
        {
            for (int i = 0; i < length(); i++)
            {
                chars[i] = charAt(i).toLower();
            }
            for (int i = 0; i < other.length(); i++)
            {
                other.chars[i] = other.charAt(i).toLower();
            }
            return equals(other);
        }

        public int indexOf(char c)
        {
            for (int i = 0; i < length(); i++)
            {
                if (charAt(i) == c) return i;
            }
            return -1;
        }
        public int indexOf(char c, int fromIndex)
        {
            for (int i = fromIndex; i < length(); i++)
            {
                if (charAt(i) == c) return i;
            }
            return -1;
        }
        public int indexOf(string s)
        {
            if (contains(s))
                return Text.RegularExpressions.Regex.Match(toString(), s).Index;
            return -1;
        }
        public int indexOf(string s, int fromIndex)
        {
            if (substring(fromIndex, length()).contains(s))
                return Text.RegularExpressions.Regex.Match(substring(fromIndex, length()).toString(), s).Index + fromIndex;
            return -1;
        }

        public CharSequence insert(int offset, bool b) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + b.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, char c) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + c.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, char[] str) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + str.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int index, char[] str, int offset, int len)
        {
            if (offset > str.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "Offset is out of range");

            if (len > str.Length - offset)
                throw new ArgumentOutOfRangeException(nameof(len), "Length is out of range");

            string insertedString = new(str, offset, len);
            string result = substring(0, index - 1) + insertedString + substring(index);
            return result.toCharSequence();
        }
        public CharSequence insert(int offset, double d) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + d.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, float f) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + f.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, int i) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + i.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, long l) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + l.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, CharSequence s) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + s + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int dstOffset, CharSequence s, int start, int end)
        {
            if (start < 0 || start >= end)
                throw new ArgumentOutOfRangeException(nameof(start), "Start out of range");

            if (end >= length())
                throw new ArgumentOutOfRangeException(nameof(end), "End index is out of range");

            return toString().Where((_, ind) => ind < dstOffset).ToArray().toCharSequence() + s.toString().Where((_, ind) => ind >= start && ind <= end).ToArray().toCharSequence() + toString().Where((_, ind) => ind >= dstOffset).ToArray().toCharSequence();
        }
        public CharSequence insert(int offset, object b) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + b.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();
        public CharSequence insert(int offset, string str) => toString().Where((_, ind) => ind < offset).ToArray().toCharSequence() + str.toCharSequence() + toString().Where((_, ind) => ind >= offset).ToArray().toCharSequence();

        public bool isEmpty() => chars.Length == 0;

        public int lastIndexOf(char c)
        {
            int index = -1;
            for (int i = 0; i < length(); i++)
            {
                if (charAt(i) == c)
                    index = i;
            }
            return index;
        }
        public int lastIndexOf(string str)
        {
            int index = -1;
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = length() - 1; i >= 0; i--)
                {
                    if (substring(i).startsWith(str))
                    {
                        index = i;
                        return index;
                    }
                }
            }
            return index;
        }

        public int length() => chars.Length;

        public CharSequence replaceAll(char oldChar, char newChar) => replaceAll(oldChar.toCharSequence(), newChar.toCharSequence());
        public CharSequence replaceAll(CharSequence oldCharSequence, CharSequence newCharSequence) => toString().Replace(oldCharSequence, newCharSequence);
        public CharSequence replaceFirst(char oldChar, char newChar) => chars[indexOf(oldChar)] = newChar;
        public CharSequence replaceFirst(CharSequence oldCharSequence, CharSequence newCharSequence)
        {
            int index = indexOf(oldCharSequence);
            if (index != -1)
            {
                CharSequence result = toString().Remove(index, oldCharSequence.length());
                return result.toString().Insert(index, newCharSequence);
            }
            return this;
        }

        public CharSequence reverse() => toString().Reverse().ToArray().toCharSequence();

        public CharSequence[] split(char delimiter)
        {
            return split(delimiter.ToString());
        }
        public CharSequence[] split(string delimiter)
        {
            return toString().Split(delimiter).Select(x => x.toCharSequence()).ToArray();
        }

        public CharSequence setCharAt(int index, char ch) => chars[index] = ch;
        public CharSequence setLength(int len)
        {
            if (len < 0)
                throw new ArgumentOutOfRangeException(nameof(len), "Length out of range");

            char[] old = chars;
            chars = new char[len];

            for (int i = 0; i < old.Length; i++)
                chars[i] = old[i];

            return chars;
        }

        public bool startsWith(char c) => charAt(0).Equals(c);
        public bool startsWith(string str) => substring(0, str.Length - 1).equals(str);

        public CharSequence substring(int start)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(start);
            Text.StringBuilder sb = new();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i >= start)
                {
                    sb.Append(chars[i]);
                }
            }
            return sb.toCharSequence();
        }
        public CharSequence substring(int start, int end)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(start);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(end, chars.Length);
            Text.StringBuilder sb = new();

            for (int i = 0; i < chars.Length; i++)
            {
                if (i >= start && i <= end)
                {
                    sb.Append(chars[i]);
                }
            }
            return sb.toCharSequence();
        }

        public char[] toCharArray() => chars;
        public string toString() => string.Join(string.Empty, chars);

        public CharSequence toLowerCase() => CharSequenceHelper.join(empty, toString().Select(x => x.ToString().ToLower().toCharSequence()).ToArray());
        public CharSequence toUpperCase() => CharSequenceHelper.join(empty, toString().Select(x => x.ToString().ToUpper().toCharSequence()).ToArray());

        public CharSequence trim() => toString().Trim();

        public static CharSequence? valueOf(object? val)
        {
            if (val == null) return null;
            return val.toCharSequence();
        }

        public static CharSequence valueOf(char[] chrs) => new(chrs);

        // Super methods
        public CharSequence clone() => this;
        public bool equals(CharSequence other)
        {
            if (length() != other.length())
                return false;

            for (int i = 0; i < length(); i++)
                if (charAt(i) != other.charAt(i))
                    return false;

            return true;
        }
        public Type getClass() => typeof(CharSequence);
        public int hashCode() => GetHashCode();


        // Overrided methods
        public override string ToString() => toString();

        // Implicit operators
        public static implicit operator CharSequence(char chr) => new(chr);
        public static implicit operator CharSequence(string value) => new(value);
        public static implicit operator CharSequence(char[] chars) => new(chars);
        public static implicit operator CharSequence(List<char> chars) => new(chars);
        public static implicit operator CharSequence(Text.StringBuilder value) => new(value);
        public static implicit operator string(CharSequence it) => it.toString();

        // This methods
        public char this[int index]
        {
            get => chars[index];
            set { chars[index] = value; }
        }
    }

    /// <summary>
    /// Cooperation with other Stringable classes
    /// </summary>
    public static class CharSequenceHelper
    {
        public static CharSequence toCharSequence(this bool b) => new(b);
        public static CharSequence toCharSequence(this char c) => new(c);
        public static CharSequence toCharSequence(this char[] chars) => new(chars);
        public static CharSequence toCharSequence(this double d) => new(d);
        public static CharSequence toCharSequence(this float f) => new(f);
        public static CharSequence toCharSequence(this int i) => new(i);
        public static CharSequence toCharSequence(this long l) => new(l);
        public static CharSequence toCharSequence(this CharSequence s) => new(s);
        public static CharSequence toCharSequence(this Text.StringBuilder sb) => new(sb);
        public static CharSequence toCharSequence(this object obj) => new(obj);
        public static CharSequence toCharSequence(this string str) => new(str);
        public static CharSequence repeat(this CharSequence cs, int count)
        {
            Text.StringBuilder sb = new();
            for (int i = 0; i < count; i++)
            {
                sb.Append(cs.ToString());
            }
            return sb.toCharSequence();
        }

        public static char[] bytesToChars(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                chars[i] = (char)bytes[i];
            }
            return chars;
        }
        public static byte[] charsToBytes(this char[] chars)
        {
            byte[] bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }
            return bytes;
        }
        
        public static char toChar(this byte b) => (char)b;
        public static char toChar(this string str) => str[0];
        public static char toLower(this char c) => c.ToString().ToLower()[0];
        public static char toUpper(this char c) => c.ToString().ToUpper()[0];

        public static int toInt(this string str) => int.Parse(str);
        public static long toLong(this string str) => long.Parse(str);
        public static short toShort(this string str) => short.Parse(str);
        public static float toFloat(this string str) => float.Parse(str);
        public static double toDouble(this string str) => double.Parse(str);
        public static Text.StringBuilder toStringBuilder(this string str) => new(str);

        public static CharSequence join(char separator, params CharSequence[] elements)
        {
            int totalLength = elements.Sum(e => e.length());
            totalLength += elements.Length - 1;

            char[] chars = new char[totalLength];
            int currentIndex = 0;

            for (int i = 0; i < elements.Length; i++)
            {
                CharSequence element = elements[i];

                for (int j = 0; j < element.length(); j++)
                {
                    chars[currentIndex++] = element[j];
                }

                if (i < elements.Length - 1)
                {
                    chars[currentIndex++] = separator;
                }
            }

            return new CharSequence(chars);
        }

        public static CharSequence join(string separator, params CharSequence[] elements)
        {
            int totalLength = elements.Sum(e => e.length());
            totalLength += (elements.Length - 1) * separator.Length;

            char[] chars = new char[totalLength];
            int currentIndex = 0;

            for (int i = 0; i < elements.Length; i++)
            {
                CharSequence element = elements[i];

                for (int j = 0; j < element.length(); j++)
                {
                    chars[currentIndex++] = element[j];
                }

                if (i < elements.Length - 1)
                {
                    for (int j = 0; j < separator.Length; j++)
                    {
                        chars[currentIndex++] = separator[j];
                    }
                }
            }

            return new CharSequence(chars);
        }

        public static CharSequence format(string format, params object[] args) => string.Format(format, args);
    }
}
