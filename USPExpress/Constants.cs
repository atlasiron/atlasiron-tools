#region header
//---------------------------------------------------------------------------------------
// USPExpress Parser .NET Pro
// Copyright (C) 2005 UNISOFT Plus Ltd.
// All rights reserved.
//---------------------------------------------------------------------------------------
#endregion
using System;
using System.Collections;

namespace USP.Express.Pro.Constants
{
	// -- Built-in Constants: --
	/// <summary>
	/// Pi.
	/// </summary>
	internal class PiConstant : Constant
	{
		public override string Name { get { return "PI"; } }
		public override object Value { get { return Math.PI; } }

		public override string Syntax { get { return "PI"; } }
		public override string Description { get { return "Represents the ratio of the circumference of a circle to its diameter, specified by the constant, 3.14159265358979323846."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

	}

	/// <summary>
	/// E.
	/// </summary>
	internal class EConstant : Constant
	{
		public override string Name { get { return "E"; } }
		public override object Value { get { return Math.E; } }

		public override string Syntax { get { return "E"; } }
		public override string Description { get { return "Represents the natural logarithmic base, specified by the constant, 2.7182818284590452354."; } }
		public override GroupType Group { get { return GroupType.MathAndStat; } }

	}

	/// <summary>
	/// True.
	/// </summary>
	internal class TrueConstant : Constant
	{
		public override string Name { get { return "TRUE"; } }
		public override object Value { get { return true; } }

		public override string Syntax { get { return "TRUE"; } }
		public override string Description { get { return "Returns the logical value TRUE."; } }
		public override GroupType Group { get { return GroupType.Logical; } }

	}

	/// <summary>
	/// False.
	/// </summary>
	internal class FalseConstant : Constant
	{
		public override string Name { get { return "FALSE"; } }
		public override object Value { get { return false; } }

		public override string Syntax { get { return "FALSE"; } }
		public override string Description { get { return "Returns the logical value FALSE."; } }
		public override GroupType Group { get { return GroupType.Logical; } }
	}


	// === DateInterval constants ===

/// <summary>
/// When you call date-related functions, you can use enumeration members in your code in place of the actual values.
///	The DateInterval enumeration defines constants used with date-related functions to identify how date intervals are determined and formatted. 
/// </summary>
	public enum DateInterval
	{
		/// <summary>Year</summary>
		Year = 0,			
		/// <summary>Quarter of year (1 through 4)</summary>
		Quarter = 1,		
		/// <summary>Month (1 through 12) </summary>
		Month = 2,			
		/// <summary>Day of year (1 through 366)</summary>
		DayOfYear = 3,		
		/// <summary>Day of month (1 through 31)</summary>
		Day = 4,			
		/// <summary>Week of year (1 through 53)</summary>
		WeekOfYear = 5,		 
		/// <summary>Day of week (1 through 7)</summary>
		Weekday = 6,		 
		/// <summary>Hour (1 through 24)</summary>
		Hour = 7,			
		/// <summary>Minute (1 through 60)</summary>
		Minute = 8,			 
		/// <summary>Second (1 through 60)</summary>
		Second = 9,			 
	}

	/// <summary>
	/// DateInterval.Year.
	/// </summary>
	internal class YearConstant : Constant
	{
		public override string Name { get { return "dtYear"; } }
		public override object Value { get { return DateInterval.Year; } }

		public override string Syntax { get { return "dtYear"; } }
		public override string Description { get { return "Year."; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }

	}

	/// <summary>
	/// DateInterval.Quarter.
	/// </summary>
	internal class QuarterConstant : Constant
	{		
		public override string Name { get { return "dtQuarter"; } }
		public override object Value { get { return DateInterval.Quarter; } }

		public override string Syntax { get { return "dtQuarter"; } }
		public override string Description { get { return "Quarter of year (1 through 4)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}
	
	
	/// <summary>
	/// DateInterval.Month.
	/// </summary>
	internal class MonthConstant : Constant
	{
		public override string Name { get { return "dtMonth"; } }
		public override object Value { get { return DateInterval.Month; } }

		public override string Syntax { get { return "dtMonth"; } }
		public override string Description { get { return "Month (1 through 12)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.DayOfYear.
	/// </summary>
	internal class DayOfYearConstant : Constant
	{
		public override string Name { get { return "dtDayOfYear"; } }
		public override object Value { get { return DateInterval.DayOfYear; } }

		public override string Syntax { get { return "dtDayOfYear"; } }
		public override string Description { get { return "Day of year (1 through 366)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.Day.
	/// </summary>
	internal class DayConstant : Constant
	{
		public override string Name { get { return "dtDay"; } }
		public override object Value { get { return DateInterval.Day; } }

		public override string Syntax { get { return "dtDay"; } }
		public override string Description { get { return "Day of month (1 through 31)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.WeekOfYear.
	/// </summary>
	internal class WeekOfYearConstant : Constant
	{
		public override string Name { get { return "dtWeekOfYear"; } }
		public override object Value { get { return DateInterval.WeekOfYear; } }

		public override string Syntax { get { return "dtWeekOfYear"; } }
		public override string Description { get { return "Week of year (1 through 53)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.Weekday.
	/// </summary>
	internal class WeekdayConstant : Constant
	{
		public override string Name { get { return "dtWeekday"; } }
		public override object Value { get { return DateInterval.Weekday; } }

		public override string Syntax { get { return "dtWeekday"; } }
		public override string Description { get { return "Day of week (1 through 7)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.Hour.
	/// </summary>
	internal class HourConstant : Constant
	{
		public override string Name { get { return "dtHour"; } }
		public override object Value { get { return DateInterval.Hour; } }

		public override string Syntax { get { return "dtHour"; } }
		public override string Description { get { return "Hour (1 through 24)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.Minute.
	/// </summary>
	internal class MinuteConstant : Constant
	{
		public override string Name { get { return "dtMinute"; } }
		public override object Value { get { return DateInterval.Minute; } }

		public override string Syntax { get { return "dtMinute"; } }
		public override string Description { get { return "Minute (1 through 60)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// DateInterval.Second.
	/// </summary>
	internal class SecondConstant : Constant
	{
		public override string Name { get { return "dtSecond"; } }
		public override object Value { get { return DateInterval.Second; } }

		public override string Syntax { get { return "dtSecond"; } }
		public override string Description { get { return "Second (1 through 60)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}


	// === FirstDayOfWeek constants ===

/// <summary>
/// When you call date-related functions, you can use the following enumeration members in your code in place of the actual values.
/// </summary>
	public enum FirstDayOfWeek
	{
		/// <summary>The First day of week specified in system settings</summary>
		System = 0,			
		/// <summary>Sunday (default)</summary>
		Sunday = 1,			
		/// <summary>Monday (complies with ISO standard 8601, section 3.17)</summary>
		Monday = 2,			
		/// <summary>Tuesday</summary>
		Tuesday = 3,		 
		/// <summary>Wednesday</summary>
		Wednesday = 4,		
		/// <summary>Thursday</summary>
		Thursday = 5,		  
		/// <summary>Friday</summary>
		Friday = 6,			  
		/// <summary>Saturday</summary>
		Saturday = 7		  
	}

	/// <summary>
	/// FirstDayOfWeek.System, FirstWeekOfYear.System.
	/// </summary>
	internal class SystemConstant : Constant
	{
		public override string Name { get { return "dtSystem"; } }
		public override object Value { get { return 0; } }

		public override string Syntax { get { return "dtSystem"; } }
		public override string Description { get { return "The first day of the week as specified in your system settings"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Sunday.
	/// </summary>
	internal class SundayConstant : Constant
	{
		public override string Name { get { return "dtSunday"; } }
		public override object Value { get { return FirstDayOfWeek.Sunday; } }

		public override string Syntax { get { return "dtSunday"; } }
		public override string Description { get { return "Sunday (default)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Monday.
	/// </summary>
	internal class MondayConstant : Constant
	{
		public override string Name { get { return "dtMonday"; } }
		public override object Value { get { return FirstDayOfWeek.Monday; } }

		public override string Syntax { get { return "dtMonday"; } }
		public override string Description { get { return "Monday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Tuesday.
	/// </summary>
	internal class TuesdayConstant : Constant
	{
		public override string Name { get { return "dtTuesday"; } }
		public override object Value { get { return FirstDayOfWeek.Tuesday; } }

		public override string Syntax { get { return "dtTuesday"; } }
		public override string Description { get { return "Tuesday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Wednesday.
	/// </summary>
	internal class WednesdayConstant : Constant
	{
		public override string Name { get { return "dtWednesday"; } }
		public override object Value { get { return FirstDayOfWeek.Wednesday; } }

		public override string Syntax { get { return "dtWednesday"; } }
		public override string Description { get { return "Wednesday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Thursday.
	/// </summary>
	internal class ThursdayConstant : Constant
	{
		public override string Name { get { return "dtThursday"; } }
		public override object Value { get { return FirstDayOfWeek.Thursday; } }

		public override string Syntax { get { return "dtThursday"; } }
		public override string Description { get { return "Thursday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Friday.
	/// </summary>
	internal class FridayConstant : Constant
	{
		// Friday
		public FridayConstant() {}
		public override string Name { get { return "dtFriday"; } }
		public override object Value { get { return FirstDayOfWeek.Friday; } }

		public override string Syntax { get { return "dtFriday"; } }
		public override string Description { get { return "Friday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstDayOfWeek.Saturday.
	/// </summary>
	internal class SaturdayConstant : Constant
	{
		public override string Name { get { return "dtSaturday"; } }
		public override object Value { get { return FirstDayOfWeek.Saturday; } }

		public override string Syntax { get { return "dtSaturday"; } }
		public override string Description { get { return "Saturday"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}


	/// <summary>
	/// Designates type of a function or a constant
	/// </summary>
	public enum GroupType
	{
		/// <summary>Custom</summary> 
		Custom = 0,
		/// <summary>Math and Stat</summary>
		MathAndStat  = 1,			
		/// <summary>Logical</summary>
		Logical = 2, 
		/// <summary>Text</summary>
		Text = 3,
		/// <summary>Lookup</summary> 
		Lookup = 4, 
		/// <summary>DateAndTime</summary> 
		DateAndTime = 5
	}


	/// <summary>
	/// When you call date-related functions, you can use the following enumeration members in your code in place of the actual values.
	/// </summary>
	public enum FirstWeekOfYear
	{
		/// <summary>First week of year specified in system settings</summary>
		System = 0,			  
		/// <summary>Week in which January 1 occurs (default)</summary>
		Jan1 = 1,			  
		/// <summary>Week that has at least four days in the new year (complies with ISO standard 8601, section 3.17)</summary>
		FirstFourDays = 2,	  
		/// <summary>First full week in the new year</summary>
		FirstFullWeek = 3,	
	}

	/// <summary>
	/// FirstWeekOfYear.Jan1.
	/// </summary>
	internal class Jan1Constant : Constant
	{
		public override string Name { get { return "dtJan1"; } }
		public override object Value { get { return FirstWeekOfYear.Jan1; } }

		public override string Syntax { get { return "dtJan1"; } }
		public override string Description { get { return "The week in which January 1 occurs (default)"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstWeekOfYear.FirstFourDays.
	/// </summary>
	internal class FirstFourDaysConstant : Constant
	{
		public override string Name { get { return "dtFirstFourDays"; } }
		public override object Value { get { return FirstWeekOfYear.FirstFourDays; } }

		public override string Syntax { get { return "dtFirstFourDays"; } }
		public override string Description { get { return "The first week that has at least four days in the new year"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/// <summary>
	/// FirstWeekOfYear.FirstFullWeek.
	/// </summary>
	internal class FirstFullWeekConstant : Constant
	{
		public override string Name { get { return "dtFirstFullWeek"; } }
		public override object Value { get { return FirstWeekOfYear.FirstFullWeek; } }

		public override string Syntax { get { return "dtFirstFullWeek"; } }
		public override string Description { get { return "The first full week of the year"; } }
		public override GroupType Group { get { return GroupType.DateAndTime; } }
	}

	/*
	/// <summary>
	/// System.DBNull.Value
	/// </summary>
	internal class DBNullConstant : Constant
	{
		public override string Name { get { return "NULL"; } }
		public override object Value { get { return System.DBNull.Value; } }
	}
	*/
}
