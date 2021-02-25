using System;
using System.Text;

namespace WojciechMikołajewicz.CharSpanBuilder
{
	public abstract class CharSpanBuilderBase
	{
		protected Memory<char> Buffer;

		public int Length { get; set; }

		public CharSpanBuilderBase(Memory<char> buffer)
		{
			this.Buffer=buffer;
		}

		protected abstract void ReallocateBuffer(int minNewSize);

		public void Append(bool value)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written))
			{
				ReallocateBuffer(Length+5);
			}
			Length+=written;
		}

		public void Append(char value)
		{
			if(Length>=Buffer.Length)
				ReallocateBuffer(Length+1);
			Buffer.Span[Length]=value;
			Length++;
		}

		public void Append(char value, int repeatCount)
		{
			if(Length+repeatCount>Buffer.Length)
				ReallocateBuffer(Length+repeatCount);
			Buffer.Span.Slice(Length, repeatCount).Fill(value);
			Length+=repeatCount;
		}

		public void Append(byte value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(double value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(float value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(int value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(long value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(sbyte value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(short value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(uint value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(ulong value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(ushort value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(in decimal value, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format, provider))
			{
				ReallocateBuffer(0);
			}
			Length+=written;
		}

		public void Append(Guid value, ReadOnlySpan<char> format = default)
		{
			int written;

			while(!value.TryFormat(Buffer.Span.Slice(Length), out written, format))
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
			if(!value.TryCopyTo(Buffer.Span.Slice(Length)))
			{
				ReallocateBuffer(Length+value.Length);
				value.CopyTo(Buffer.Span.Slice(Length));
			}
			Length+=value.Length;
		}

		public void Append(StringBuilder value)
		{
			Append(value, 0, value.Length);
		}

		public void Append(StringBuilder value, int start, int length)
		{
			if(Length+length>Buffer.Length)
				ReallocateBuffer(Length+length);
			value.CopyTo(start, Buffer.Span.Slice(Length), length);
			Length+=length;
		}
	}
}