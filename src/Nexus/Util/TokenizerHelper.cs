using System;
using System.Globalization;

namespace Nexus.Util
{
	internal class TokenizerHelper
	{
		// Fields
		private char _argSeparator;
		private int _charIndex;
		internal int _currentTokenIndex;
		internal int _currentTokenLength;
		private bool _foundSeparator;
		private char _quoteChar;
		private string _str;
		private int _strLen;

		// Methods
		internal TokenizerHelper(string str, IFormatProvider formatProvider)
		{
			char numericListSeparator = GetNumericListSeparator(formatProvider);
			this.Initialize(str, '\'', numericListSeparator);
		}

		internal TokenizerHelper(string str, char quoteChar, char separator)
		{
			this.Initialize(str, quoteChar, separator);
		}

		internal string GetCurrentToken()
		{
			if (this._currentTokenIndex < 0)
			{
				return null;
			}
			return this._str.Substring(this._currentTokenIndex, this._currentTokenLength);
		}

		internal static char GetNumericListSeparator(IFormatProvider provider)
		{
			char ch = ',';
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			if ((instance.NumberDecimalSeparator.Length > 0) && (ch == instance.NumberDecimalSeparator[0]))
			{
				ch = ';';
			}
			return ch;
		}

		private void Initialize(string str, char quoteChar, char separator)
		{
			this._str = str;
			this._strLen = (str == null) ? 0 : str.Length;
			this._currentTokenIndex = -1;
			this._quoteChar = quoteChar;
			this._argSeparator = separator;
			while (this._charIndex < this._strLen)
			{
				if (!char.IsWhiteSpace(this._str, this._charIndex))
				{
					return;
				}
				this._charIndex++;
			}
		}

		internal void LastTokenRequired()
		{
			if (this._charIndex != this._strLen)
				throw new InvalidOperationException("Extra data encountered");
		}

		internal bool NextToken()
		{
			return this.NextToken(false);
		}

		internal bool NextToken(bool allowQuotedToken)
		{
			return this.NextToken(allowQuotedToken, this._argSeparator);
		}

		internal bool NextToken(bool allowQuotedToken, char separator)
		{
			this._currentTokenIndex = -1;
			this._foundSeparator = false;
			if (this._charIndex >= this._strLen)
			{
				return false;
			}
			char c = this._str[this._charIndex];
			int num = 0;
			if (allowQuotedToken && (c == this._quoteChar))
			{
				num++;
				this._charIndex++;
			}
			int num2 = this._charIndex;
			int num3 = 0;
			while (this._charIndex < this._strLen)
			{
				c = this._str[this._charIndex];
				if (num > 0)
				{
					if (c != this._quoteChar)
					{
						goto Label_00AA;
					}
					num--;
					if (num != 0)
					{
						goto Label_00AA;
					}
					this._charIndex++;
					break;
				}
				if (char.IsWhiteSpace(c) || (c == separator))
				{
					if (c == separator)
					{
						this._foundSeparator = true;
					}
					break;
				}
				Label_00AA:
				this._charIndex++;
				num3++;
			}
			if (num > 0)
				throw new InvalidOperationException("Missing end quote");
			this.ScanToNextToken(separator);
			this._currentTokenIndex = num2;
			this._currentTokenLength = num3;
			if (this._currentTokenLength < 1)
				throw new InvalidOperationException("Empty token");
			return true;
		}

		internal string NextTokenRequired()
		{
			if (!this.NextToken(false))
				throw new InvalidOperationException("Premature string termination");
			return this.GetCurrentToken();
		}

		internal string NextTokenRequired(bool allowQuotedToken)
		{
			if (!this.NextToken(allowQuotedToken))
				throw new InvalidOperationException("Premature string termination");
			return this.GetCurrentToken();
		}

		private void ScanToNextToken(char separator)
		{
			if (this._charIndex < this._strLen)
			{
				char c = this._str[this._charIndex];
				if ((c != separator) && !char.IsWhiteSpace(c))
					throw new InvalidOperationException("Extra data encountered");
				int num = 0;
				while (this._charIndex < this._strLen)
				{
					c = this._str[this._charIndex];
					if (c == separator)
					{
						this._foundSeparator = true;
						num++;
						this._charIndex++;
						if (num > 1)
							throw new InvalidOperationException("Empty token");
					}
					else
					{
						if (!char.IsWhiteSpace(c))
						{
							break;
						}
						this._charIndex++;
					}
				}
				if ((num > 0) && (this._charIndex >= this._strLen))
					throw new InvalidOperationException("Empty token");
			}
		}

		// Properties
		internal bool FoundSeparator
		{
			get
			{
				return this._foundSeparator;
			}
		}
	}
}