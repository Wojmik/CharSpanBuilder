using System;
using System.Text;

namespace WojciechMikołajewicz.CharSpanBuilder
{
	public abstract class CharSpanBuilderBase
	{
		public int Length { get; protected set; }

		public abstract ReadOnlyMemory<char> AsMemory();

		/// <summary>
		/// Method should return free part of buffer as <see cref="Span{T}"/>
		/// </summary>
		/// <returns>Free part of buffer as <see cref="Span{T}"/></returns>
		protected abstract Span<char> GetFreeSpan();

		protected abstract void ReallocateBuffer(int minNewSize);

		public void Clear()
		{
			Length=0;
		}

		public string ToString(int startIndex, int length)
		{
			return new string(AsMemory().Span.Slice(startIndex, length));
		}

		public override string ToString()
		{
			return ToString(0, Length);
		}

		public void AppendLine()
		{
			Append(Environment.NewLine);
		}

		public void Append(bool value)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written))
			{
				ReallocateBuffer(Length+5);
			}
			Length+=written;
		}

		public void Append(char value)
		{
			Span<char> freeBuffer = GetFreeSpan();
			if(freeBuffer.IsEmpty)
			{
				ReallocateBuffer(Length+1);
				freeBuffer = GetFreeSpan();
			}
			freeBuffer[0]=value;
			Length++;
		}

		public void Append(char value, int repeatCount)
		{
			Span<char> freeBuffer = GetFreeSpan();
			if(freeBuffer.Length<repeatCount)
			{
				ReallocateBuffer(Length+repeatCount);
				freeBuffer = GetFreeSpan();
			}
			freeBuffer.Slice(0, repeatCount).Fill(value);
			Length+=repeatCount;
		}

		public void Append(byte value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(double value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(float value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(int value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(long value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(sbyte value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(short value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(uint value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(ulong value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(ushort value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(in decimal value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(DateTime value, ReadOnlySpan<char> format = default)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(TimeSpan value, ReadOnlySpan<char> format = default)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(DateTimeOffset value, ReadOnlySpan<char> format = default)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(Guid value, ReadOnlySpan<char> format = default)
		{
			int written;

			while(!value.TryFormat(GetFreeSpan(), out written, format))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(object value)
		{
			Append(value?.ToString());
		}

		public void Append(string value)
		{
			Append(value.AsSpan());
		}

		public void Append(string value, int start, int length)
		{
			Append(value.AsSpan(start, length));
		}

		public void Append(ReadOnlySpan<char> value)
		{
			if(!value.TryCopyTo(GetFreeSpan()))
			{
				ReallocateBuffer(Length+value.Length);
				value.CopyTo(GetFreeSpan());
			}
			Length+=value.Length;
		}

		public void Append(StringBuilder value)
		{
			Append(value, 0, value.Length);
		}

		public void Append(StringBuilder value, int start, int length)
		{
			Span<char> freeBuffer = GetFreeSpan();
			if(freeBuffer.Length<length)
			{
				ReallocateBuffer(Length+length);
				freeBuffer = GetFreeSpan();
			}
			value.CopyTo(start, freeBuffer, length);
			Length+=length;
		}
	}
}