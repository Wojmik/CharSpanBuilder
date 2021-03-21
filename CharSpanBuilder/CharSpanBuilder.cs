using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WojciechMikołajewicz.CharSpanBuilder
{
	public class CharSpanBuilder : CharSpanBuilderBase, IDisposable
	{
		const int DefaultBufferSize = 128;

		private char[] Buffer;

		public int Capacity { get => this.Buffer.Length; }

		public bool ClearBufferAfterUse { get; }

		public CharSpanBuilder(int capacity = DefaultBufferSize, bool clearBufferAfterUse = true)
		{
			this.ClearBufferAfterUse = clearBufferAfterUse;
			this.Buffer = ArrayPool<char>.Shared.Rent(capacity);
		}

		protected override Span<char> GetFreeSpan()
		{
			return new Span<char>(Buffer, Length, Buffer.Length-Length);
		}

		protected override void ReallocateBuffer(int minNewSize)
		{
			var oldBuffer = Interlocked.Exchange(ref Buffer, ArrayPool<char>.Shared.Rent(Math.Max(Math.Max(minNewSize, Buffer.Length*2), DefaultBufferSize)));
			Array.Copy(oldBuffer, Buffer, Length);
			ArrayPool<char>.Shared.Return(oldBuffer, ClearBufferAfterUse);
		}

		public override ReadOnlyMemory<char> AsMemory()
		{
			return new ReadOnlyMemory<char>(Buffer, 0, Length);
		}

		public Memory<char> GetFreeBuffer()
		{
			return new Memory<char>(Buffer, Length, Buffer.Length-Length);
		}

		public virtual void Dispose()
		{
			var buffer = Interlocked.Exchange(ref Buffer, null);
			if(buffer!=null)
				ArrayPool<char>.Shared.Return(buffer, ClearBufferAfterUse);
		}
	}
}