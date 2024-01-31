namespace System
{
    public class CharSequence
    {
        // Main chars
        private readonly char[] chars = [];
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

        public CharSequence valueOf(char[] chrs) => new(chrs);

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

        public bool equals(CharSequence other)
        {
            if (length() != other.length())
                return false;

            for (int i = 0; i < length(); i++)
                if (charAt(i) != other.charAt(i))
                    return false;

            return true;
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

        public CharSequence[] split(char delimiter)
        {
            return split(delimiter.ToString());
        }

        public CharSequence[] split(string delimiter)
        {
            return toString().Split(delimiter).Select(x => x.toCharSequence()).ToArray();
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

        public CharSequence toLowerCase() => CharSequenceHelper.join(empty, toString().Select(x => x.ToString().ToLower().toCharSequence()).ToArray());
        
        public string toString() => string.Join(string.Empty, chars);

        public CharSequence toUpperCase() => CharSequenceHelper.join(empty, toString().Select(x => x.ToString().ToUpper().toCharSequence()).ToArray());

        public CharSequence trim() => toString().Trim();

        public static CharSequence? valueOf(object? val)
        {
            if (val == null) return null;
            return val.toCharSequence();
        }

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
        public static CharSequence toCharSequence(this char ch) => new(ch);
        public static CharSequence toCharSequence(this object obj) => new(obj);
        public static CharSequence toCharSequence(this string line) => new(line);
        public static CharSequence toCharSequence(this char[] chars) => new(chars);
        public static CharSequence toCharSequence(this Text.StringBuilder line) => new(line);
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

        public static char toLower(this char c) => c.ToString().ToLower()[0];

        public static char toChar(this byte b) => (char)b;
        public static char toChar(this string str) => str[0];
        public static int toInt(this string str) => int.Parse(str);
        public static long toLong(this string str) => long.Parse(str);
        public static short toShort(this string str) => short.Parse(str);
        public static float toFloat(this string str) => float.Parse(str);
        public static double toDouble(this string str) => double.Parse(str);

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
