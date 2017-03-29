﻿#region copyright
/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss IMT (IZfM Dresden)                   */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2013                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */
#endregion

namespace Zeiss.IMT.PiWeb.Formplot.FileFormat
{
	/// <summary>
	/// Segment types
	/// </summary>
	public enum SegmentTypes
	{
		/// <summary>
		/// No specific type.
		/// </summary>
		None,
		/// <summary>
		/// A cylindrical arc or a circle.
		/// </summary>
		Circle,
		/// <summary>
		/// A linear segment.
		/// </summary>
		Line,
		/// <summary>
		/// A helix segment.
		/// </summary>
		Helix,
		/// <summary>
		/// Touching points.
		/// </summary>
		TouchingPoint,
		/// <summary>
		/// Maximum gap point
		/// </summary>
		MaxGapPoint,
		/// <summary>
		/// Bending circle
		/// </summary>
		BendingCircle,
		/// <summary>
		/// Axis
		/// </summary>
		Axis
	}
}
