#region using
using MpSoft.Collections.Helpers;
using System;
using System.Text;
#endregion using

namespace MpSoft.IO
{
	public class BytesReader
	{
		byte[] _bytes;
		int _position;
		public BytesReader(byte[] bytes)
		{
			_bytes = bytes;
		}

		static byte[] _newLine = new byte[] { 13, 10 };
		public string ReadLine(Encoding encoding)
		{
			int newPos = ArrayExt.FindArrIndex(_bytes, _newLine, _position);
			string result = encoding.GetString(_bytes, _position, newPos - _position);
			_position = newPos + 2;
			return result;
		}

		public string ReadToEnd(Encoding encoding)
		{
			int newPos = _bytes.Length;
			string result = encoding.GetString(_bytes, _position, newPos - _position);
			_position = newPos;
			return result;
		}

		public string ReadToBreak(Encoding encoding, byte[] partsDelimiter)
		{
			int newPos = ArrayExt.FindArrIndex(_bytes, partsDelimiter, _position);
			string result = encoding.GetString(_bytes, _position, newPos - _position);
			_position = newPos + partsDelimiter.Length;
			return result;
		}

		public byte[] ReadToBreak(byte[] partsDelimiter)
		{
			int newPos = ArrayExt.FindArrIndex(_bytes, partsDelimiter, _position);
			int len = newPos - _position;
			byte[] result = new byte[len];
			Array.Copy(_bytes, _position, result, 0, len);
			_position = newPos + partsDelimiter.Length;
			return result;
		}

		public void ReadBytes(byte[] array, int countToRead)
		{
			Array.Copy(_bytes, _position, array, 0, countToRead);
			_position += countToRead;
		}

		public int Position { get { return _position; } set { _position = value; } }
		public int Length { get { return _bytes.Length; } }
	}
}