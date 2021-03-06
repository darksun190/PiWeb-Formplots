﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss IMT (IZfM Dresden)                   */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2013                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.IMT.PiWeb.Formplot.FileFormat
{
	#region usings

	using System;
	using System.Globalization;

	#endregion

	/// <summary>
	/// Represents a range of indices.
	/// </summary>
	public class Range
	{
		#region members

		private int _Start, _End;

		#endregion

		#region constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="Range"/> class.
		/// </summary>
		/// <param name="start">The starting index.</param>
		/// <param name="end">The last index.</param>
		public Range( int start = 0, int? end = null )
		{
			if( end.HasValue )
			{
				VerifyRange( start, end.Value );
				_Start = start;
				_End = end.Value;
			}
			else
			{
				_Start = _End = start;
			}
		}

		#endregion

		#region properties

		/// <summary>
		/// First index inside the range
		/// </summary>
		public int Start
		{
			get => _Start;
			set
			{
				VerifyRange( value, _End );
				_Start = value;
			}
		}

		/// <summary>
		/// Last index inside the range
		/// </summary>
		public int End
		{
			get => _End;
			set
			{
				VerifyRange( _Start, value );
				_End = value;
			}
		}

		#endregion

		#region methods

		/// <summary>
		/// Determines whether the specified <paramref name="value"/> is inside this range.
		/// </summary>
		public bool IsValueInRange( int value )
		{
			return value >= _Start && value <= _End;
		}

		/// <summary>
		/// Tries to parse a <see cref="Range"/> instance from a <see cref="string"/>.
		/// <remarks>In case <paramref name="rangeString"/> doesn't define a valid range. an empty <see cref="Range"/> instance is returned.</remarks>
		/// </summary>
		internal static Range TryParseOrNull( string rangeString )
		{
			if( !string.IsNullOrWhiteSpace( rangeString ) )
			{
				var index = rangeString.IndexOf( '-' );

				if( index == -1 )
				{
					if( int.TryParse( rangeString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value ) )
					{
						return new Range( value );
					}
				}
				else
				{
					var startString = rangeString.Substring( 0, index );
					var endString = rangeString.Substring( index + 1 );

					if( int.TryParse( startString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var start ) &&
					    int.TryParse( endString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var end ) && IsValidRange( start, end ) )
						return new Range( start, end );
				}
			}

			return null;
		}

		private static bool IsValidRange( int start, int end )
		{
			return end >= start;
		}

		private static void VerifyRange( int start, int end )
		{
			if( !IsValidRange( start, end ) )
			{
				throw new ArgumentOutOfRangeException( "start is greater then end", default ( Exception ) );
			}
		}

		/// <summary>
		/// Determines, whether the two specified <see cref="Range"/> instances are equal.
		/// </summary>
		private static bool Equals( Range rangeA, Range rangeB )
		{
			if( ReferenceEquals( rangeA, rangeB ) )
			{
				return true;
			}
			
			if( rangeA != null && rangeB != null )
			{
				return Equals( rangeA._Start, rangeB._Start ) &&
				       Equals( rangeA._End, rangeB._End );
			}

			return false;
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public override int GetHashCode()
		{
			return Start.GetHashCode() ^ End.GetHashCode();
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals( object obj )
		{
			return Equals( this, obj as Range );
		}

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		public override string ToString()
		{
			return _Start == _End ? _Start.ToString( CultureInfo.InvariantCulture ) : string.Format( CultureInfo.InvariantCulture, "{0}-{1}", _Start, _End );
		}

		#endregion
	}
}