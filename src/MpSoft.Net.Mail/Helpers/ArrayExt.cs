#region using
using System;
#endregion using

namespace MpSoft.Collections.Helpers
{
	public static class ArrayExt
	{
		public static int FindArrIndex<T>(T[] source, T[] toFind)
		{
			return FindArrIndex<T>(source, toFind, 0, -1);
		}

		public static int FindArrIndex<T>(T[] source, T[] toFind, int startIndex)
		{
			return FindArrIndex<T>(source, toFind, startIndex, -1);
		}

		public static int FindArrIndex<T>(T[] source, T[] toFind, int startIndex, int count)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			if (toFind == null)
				throw new ArgumentNullException("toFind");
			int len = source.Length;
			if (len == 0)
				return -1;
			if (toFind.Length == 0)
				throw new ArgumentException("toFind");
			if (count == -1)
				count = len;

			T firstEl = toFind[0];
			int pos = Array.IndexOf(source, firstEl, startIndex, count - startIndex);
			while (pos != -1)
			{
				bool found = true;
				int a = 0;
				foreach (T el in toFind)
					if (!object.Equals(source[pos + (a++)], el))
					{
						found = false;
						break;
					}
				if (found)
					return pos;

				pos = pos + 1;
				pos = Array.IndexOf(source, firstEl, pos, count - pos);
			}
			return -1;
		}

		public static bool BinaryEquals<T>(T[] dataX, int indexX, T[] dataY, int indexY, int size)
		{
			if (dataX.Length < indexX + size)
				throw new IndexOutOfRangeException();
			if (dataY.Length < indexY + size)
				throw new IndexOutOfRangeException();
			for (int a = 0; a < size; a++)
				if (!object.Equals(dataX[a + indexX], dataY[a + indexY]))
					return false;
			return true;
		}
	}
}

