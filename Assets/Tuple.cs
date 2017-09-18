using System.Collections.Generic;
using JetBrains.Annotations;

namespace Auxilaries
{
	/// <summary>
	/// A dumb pair of integers.
	/// </summary>
	public struct IntPair
	{
		//[Serialize]
		public readonly int x;
		//[Serialize]
		public readonly int y;

		public IntPair(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString() { return "{ " + x + ", " + y + " }"; }

		public static IntPair operator +(IntPair a, IntPair b) { return new IntPair(a.x + b.x, a.y + b.y); }

		[Pure]
		public bool Equals(IntPair other) { return x == other.x && y == other.y; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is IntPair && Equals((IntPair)obj);
		}

		public static bool operator ==(IntPair l, IntPair r) { return l.Equals(r); }
		public static bool operator !=(IntPair l, IntPair r) { return !l.Equals(r); }

		public override int GetHashCode()
		{
			unchecked
			{
				return (x * 397) ^ y;
			}
		}
	}
}

/// <summary>
/// To be used as a dictionary key.
/// </summary>
public struct Tuple<T1, T2>
{
	public readonly T1 item1;
	public readonly T2 item2;

	public Tuple(T1 item1, T2 item2)
	{
		this.item1 = item1;
		this.item2 = item2;
	}

	public bool Equals(Tuple<T1, T2> other)
	{
		return EqualityComparer<T1>.Default.Equals(item1, other.item1)
			&& EqualityComparer<T2>.Default.Equals(item2, other.item2);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		return obj is Tuple<T1, T2> && Equals((Tuple<T1, T2>)obj);
	}

	public override int GetHashCode()
	{
		unchecked
		{
			return (EqualityComparer<T1>.Default.GetHashCode(item1) * 397) ^ EqualityComparer<T2>.Default.GetHashCode(item2);
		}
	}

	public override string ToString() { return item1 + ", " + item2; }
}

/// <summary>
/// To be used as a dictionary key.
/// </summary>
public struct Tuple<T1, T2, T3>
{
	public readonly T1 item1;
	public readonly T2 item2;
	public readonly T3 item3;

	public Tuple(T1 item1, T2 item2, T3 item3)
	{
		this.item1 = item1;
		this.item2 = item2;
		this.item3 = item3;
	}

	public bool Equals(Tuple<T1, T2, T3> other)
	{
		return EqualityComparer<T1>.Default.Equals(item1, other.item1)
			&& EqualityComparer<T2>.Default.Equals(item2, other.item2)
			&& EqualityComparer<T3>.Default.Equals(item3, other.item3);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		return obj is Tuple<T1, T2, T3> && Equals((Tuple<T1, T2, T3>)obj);
	}

	public override int GetHashCode()
	{
		unchecked
		{
			int hashCode = EqualityComparer<T1>.Default.GetHashCode(item1);
			hashCode = (hashCode * 397) ^ EqualityComparer<T2>.Default.GetHashCode(item2);
			hashCode = (hashCode * 397) ^ EqualityComparer<T3>.Default.GetHashCode(item3);
			return hashCode;
		}
	}

	public override string ToString() { return item1 + ", " + item2 + ", " + item3; }
}
