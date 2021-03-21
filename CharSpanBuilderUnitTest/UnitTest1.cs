using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace WojciechMikołajewicz.CharSpanBuilderUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			using CharSpanBuilder.CharSpanBuilder builder = new();
			StringBuilder sb = new();

			builder.Append(true);
			builder.Append('Ø');
			builder.Append('Ω', 10);
			builder.Append(byte.MaxValue);
			builder.Append(double.PositiveInfinity);
			builder.Append(float.NegativeInfinity);
			builder.Append(int.MinValue);
			builder.Append(long.MinValue);
			builder.Append(sbyte.MinValue);
			builder.Append(short.MinValue);
			builder.Append(uint.MaxValue);
			builder.Append(ulong.MaxValue);
			builder.Append(ushort.MaxValue);
			builder.Append(decimal.MinValue);
			builder.Append(new DateTime(1999, 12, 31, 23, 59, 59, 999));
			builder.Append(TimeSpan.MinValue);
			builder.Append(Guid.Empty);
			builder.Append("Galapagos");

			sb.Append(true);
			sb.Append('Ø');
			sb.Append('Ω', 10);
			sb.Append(byte.MaxValue);
			sb.Append(double.PositiveInfinity);
			sb.Append(float.NegativeInfinity);
			sb.Append(int.MinValue);
			sb.Append(long.MinValue);
			sb.Append(sbyte.MinValue);
			sb.Append(short.MinValue);
			sb.Append(uint.MaxValue);
			sb.Append(ulong.MaxValue);
			sb.Append(ushort.MaxValue);
			sb.Append(decimal.MinValue);
			sb.Append(new DateTime(1999, 12, 31, 23, 59, 59, 999));
			sb.Append(TimeSpan.MinValue);
			sb.Append(Guid.Empty);
			sb.Append("Galapagos");

			Assert.AreEqual(builder.ToString(), sb.ToString());
		}
	}
}