using System;

public static class DateTimeExtensions
{
	internal const int DAYS_PER_WEEK = 7;

	internal const int DAYS_PER_FORTNIGHT = 14;

	internal const int WEEKS_PER_FORTNIGHT = 2;

	internal const int YEARS_PER_DECADE = 10;

	internal const int YEARS_PER_CENTURY = 100;

	private const int JANUARY = 1;

	private const int FEBRUARY = 2;

	private const int MARCH = 3;

	private const int APRIL = 4;

	private const int MAY = 5;

	private const int JUNE = 6;

	private const int JULY = 7;

	private const int AUGUST = 8;

	private const int SEPTEMBER = 9;

	private const int OCTOBER = 10;

	private const int NOVEMBER = 11;

	private const int DECEMBER = 12;

	public static bool IsBetween(this DateTime value, DateTime from, DateTime to)
	{
		return from <= value && to >= value;
	}

	public static DateTime Midnight(this DateTime value)
	{
		return new DateTime(value.Year, value.Month, value.Day);
	}

	public static DateTime DateFromDay(this int day, int month)
	{
		return new DateTime(DateTime.Now.Year, month, day);
	}

	public static DateTime DateFromDay(this int day, int month, int year)
	{
		return new DateTime(year, month, day);
	}

	public static DateTime DateFromMonth(this int month, int day)
	{
		return new DateTime(DateTime.Now.Year, month, day);
	}

	public static DateTime DateFromMonth(this int month, int day, int year)
	{
		return new DateTime(year, month, day);
	}

	public static DateTime FirstOfMonth(this DateTime value)
	{
		return new DateTime(value.Year, value.Month, 1);
	}

	public static DateTime FirstOfMonth(this int month)
	{
		return new DateTime(DateTime.Now.Year, month, 1);
	}

	public static DateTime FirstOfMonth(this int month, int year)
	{
		return new DateTime(year, month, 1);
	}

	public static DateTime EndOfMonth(this DateTime value)
	{
		return new DateTime(value.Year, value.Month, 1).AddMonths(1).AddDays(-1.0);
	}

	public static DateTime EndOfMonth(this int month)
	{
		return new DateTime(DateTime.Now.Year, month, 1).AddMonths(1).AddDays(-1.0);
	}

	public static DateTime EndOfMonth(this int month, int year)
	{
		return new DateTime(year, month, 1).AddMonths(1).AddDays(-1.0);
	}

	public static DateTime Yesterday(this DateTime value)
	{
		return value.AddDays(-1.0);
	}

	public static DateTime YesterdayMidnight(this DateTime value)
	{
		return Midnight(Yesterday(value));
	}

	public static DateTime Tomorrow(this DateTime value)
	{
		return value.AddDays(1.0);
	}

	public static DateTime TomorrowMidnight(this DateTime value)
	{
		return Midnight(Tomorrow(value));
	}

	public static bool IsSameDay(this DateTime value, DateTime compareDate)
	{
		return Midnight(value).Equals(Midnight(compareDate));
	}

	public static bool IsLaterDate(this DateTime value, DateTime compareDate)
	{
		return value > compareDate;
	}

	public static bool IsOlderDate(this DateTime value, DateTime compareDate)
	{
		return value < compareDate;
	}

	public static bool IsToday(this DateTime date)
	{
		return date.Date == DateTime.Now.Date;
	}

	public static bool IsTomorrow(this DateTime date)
	{
		return date.Date == DateTime.Now.Date.AddDays(1.0);
	}

	public static bool IsYesterday(this DateTime date)
	{
		return date.Date == DateTime.Now.Date.AddDays(-1.0);
	}

	public static bool IsOlderThanASecond(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Seconds < 0;
	}

	public static bool IsOlderThanAMinute(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Minutes < 0;
	}

	public static bool IsOlderThanAnHour(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Hours < 0;
	}

	public static bool IsOlderThanADay(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days < 0;
	}

	public static bool IsOlderThanAWeek(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days < 7;
	}

	public static bool IsOlderThanAFortnight(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days < 14;
	}

	public static bool IsOlderThanAMonth(this DateTime date)
	{
		return IsOlderThan(date, DateTime.Now.AddMonths(-1));
	}

	public static bool IsOlderThanHalfYear(this DateTime date)
	{
		return IsOlderThan(date, DateTime.Now.AddMonths(-6));
	}

	public static bool IsOlderThanAYear(this DateTime date)
	{
		return IsOlderThan(date, DateTime.Now.AddYears(-1));
	}

	public static bool IsOlderThanADecade(this DateTime date)
	{
		return IsOlderThan(date, DateTime.Now.AddYears(-10));
	}

	public static bool IsOlderThanACentury(this DateTime date)
	{
		return IsOlderThan(date, DateTime.Now.AddYears(-100));
	}

	public static bool IsOlderThan(this DateTime firstDate, DateTime secondDate)
	{
		return firstDate.Subtract(secondDate).Ticks < 0;
	}

	public static bool IsYoungerThanASecond(this DateTime date)
	{
		return date.Subtract(DateTime.Now).TotalSeconds > 0.0;
	}

	public static bool IsYoungerThanAMinute(this DateTime date)
	{
		return date.Subtract(DateTime.Now).TotalMinutes > 0.0;
	}

	public static bool IsYoungerThanAnHour(this DateTime date)
	{
		return date.Subtract(DateTime.Now).TotalHours > 0.0;
	}

	public static bool IsYoungerThanADay(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days > 0;
	}

	public static bool IsYoungerThanAWeek(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days >= 7 && IsYoungerThan(date, DateTime.Now);
	}

	public static bool IsYoungerThanAFortnight(this DateTime date)
	{
		return date.Subtract(DateTime.Now).Days >= 14 && IsYoungerThan(date, DateTime.Now);
	}

	public static bool IsYoungerThanAMonth(this DateTime date)
	{
		return IsYoungerThan(date, DateTime.Now.AddMonths(1));
	}

	public static bool IsYoungerThanHalfYear(this DateTime date)
	{
		return IsYoungerThan(date, DateTime.Now.AddMonths(6));
	}

	public static bool IsYoungerThanAYear(this DateTime date)
	{
		return IsYoungerThan(date, DateTime.Now.AddYears(1));
	}

	public static bool IsYoungerThanADecade(this DateTime date)
	{
		return IsYoungerThan(date, DateTime.Now.AddYears(10));
	}

	public static bool IsYoungerThanACentury(this DateTime date)
	{
		return IsYoungerThan(date, DateTime.Now.AddYears(100));
	}

	public static bool IsYoungerThan(this DateTime firstDate, DateTime secondDate)
	{
		return firstDate.Subtract(secondDate).Ticks > 0;
	}

	public static DateTime AddASecond(this DateTime date)
	{
		return date.AddSeconds(1.0);
	}

	public static DateTime AddAMinute(this DateTime date)
	{
		return date.AddMinutes(1.0);
	}

	public static DateTime AddHalfAnHour(this DateTime date)
	{
		return date.AddMinutes(30.0);
	}

	public static DateTime AddAnHour(this DateTime date)
	{
		return date.AddHours(1.0);
	}

	public static DateTime AddADay(this DateTime date)
	{
		return date.AddDays(1.0);
	}

	public static DateTime AddAWeek(this DateTime date)
	{
		return AddWeeks(date, 1);
	}

	public static DateTime AddAFortnight(this DateTime date)
	{
		return AddFortnights(date, 1);
	}

	public static DateTime AddAMonth(this DateTime date)
	{
		return date.AddMonths(1);
	}

	public static DateTime AddAYear(this DateTime date)
	{
		return date.AddYears(1);
	}

	public static DateTime AddADecade(this DateTime date)
	{
		return AddDecades(date, 1);
	}

	public static DateTime AddWeeks(this DateTime date, int weeks)
	{
		return date.AddDays(7 * weeks);
	}

	public static DateTime AddFortnights(this DateTime date, int fortnights)
	{
		return date.AddDays(14 * fortnights);
	}

	public static DateTime AddACentury(this DateTime date)
	{
		return AddCenturies(date, 1);
	}

	public static DateTime AddDecades(this DateTime date, int decades)
	{
		return date.AddYears(10 * decades);
	}

	public static DateTime AddCenturies(this DateTime date, int centuries)
	{
		return date.AddYears(100 * centuries);
	}

	public static DateTime SubtractASecond(this DateTime date)
	{
		return SubtractSeconds(date, 1);
	}

	public static DateTime SubtractAMinute(this DateTime date)
	{
		return SubtractMinutes(date, 1);
	}

	public static DateTime SubtractHalfAnHour(this DateTime date)
	{
		return SubtractMinutes(date, 30);
	}

	public static DateTime SubtractAnHour(this DateTime date)
	{
		return SubtractHours(date, 1);
	}

	public static DateTime SubtractADay(this DateTime date)
	{
		return SubtractDays(date, 1);
	}

	public static DateTime SubtractAWeek(this DateTime date)
	{
		return AddWeeks(date, -1);
	}

	public static DateTime SubtractAFortnight(this DateTime date)
	{
		return AddFortnights(date, -1);
	}

	public static DateTime SubtractAMonth(this DateTime date)
	{
		return SubtractMonths(date, 1);
	}

	public static DateTime SubtractAYear(this DateTime date)
	{
		return SubtractYears(date, 1);
	}

	public static DateTime SubtractADecade(this DateTime date)
	{
		return AddDecades(date, -1);
	}

	public static DateTime SubtractACentury(this DateTime date)
	{
		return AddCenturies(date, -1);
	}

	public static DateTime SubtractTicks(this DateTime date, int ticks)
	{
		return date.AddTicks(-ticks);
	}

	public static DateTime SubtractMilliSeconds(this DateTime date, int milliSeconds)
	{
		return date.AddMilliseconds(-milliSeconds);
	}

	public static DateTime SubtractSeconds(this DateTime date, int seconds)
	{
		return date.AddSeconds(-seconds);
	}

	public static DateTime SubtractMinutes(this DateTime date, int minutes)
	{
		return date.AddMinutes(-minutes);
	}

	public static DateTime SubtractHours(this DateTime date, int hours)
	{
		return date.AddHours(-hours);
	}

	public static DateTime SubtractDays(this DateTime date, int days)
	{
		return date.AddDays(-days);
	}

	public static DateTime SubtractWeeks(this DateTime date, int weeks)
	{
		return AddWeeks(date, -weeks);
	}

	public static DateTime SubtractFortnights(this DateTime date, int fortnights)
	{
		return AddFortnights(date, -fortnights);
	}

	public static DateTime SubtractMonths(this DateTime date, int months)
	{
		return date.AddMonths(-months);
	}

	public static DateTime SubtractYears(this DateTime date, int years)
	{
		return date.AddYears(-years);
	}

	public static DateTime SubtractDecades(this DateTime date, int decades)
	{
		return AddDecades(date, -decades);
	}

	public static DateTime SubtractCenturies(this DateTime date, int centuries)
	{
		return AddCenturies(date, -centuries);
	}

	public static long GetTicksSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Ticks);
	}

	public static long GetMilliSecondsSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Milliseconds);
	}

	public static long GetSecondsSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Seconds);
	}

	public static long GetMinutesSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Minutes);
	}

	public static long GetHoursSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Hours);
	}

	public static long GetDaysSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).Days);
	}

	public static long GetWeeksSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).GetWeeks());
	}

	public static long GetFortnightsSince(this DateTime time)
	{
		return Math.Abs((DateTime.Now - time).GetWeeks() / 2);
	}

	public static long GetMonthsSince(this DateTime time)
	{
		DateTime now = DateTime.Now;
		long count = -1;
		if (IsYoungerThan(time, now))
		{
			do
			{
				now = now.AddMonths(1);
				++count;
			}
			while (IsOlderThan(now, time));
		}
		else
		{
			do
			{
				++count;
				now = SubtractMonths(now, 1);
			}
			while (IsYoungerThan(now, time));
		}
		return count;
	}

	public static long GetYearsSince(this DateTime time)
	{
		DateTime now = DateTime.Now;
		long count = -1;
		if (IsYoungerThan(time, now))
		{
			do
			{
				now = now.AddYears(1);
				++count;
			}
			while (IsOlderThan(now, time));
		}
		else
		{
			do
			{
				++count;
				now = SubtractYears(now, 1);
			}
			while (IsYoungerThan(now, time));
		}
		return count;
	}

	public static long GetDecadesSince(this DateTime time)
	{
		return Math.Abs(GetYearsSince(time) / 10);
	}

	public static long GetCenturiesSince(this DateTime time)
	{
		return Math.Abs(GetYearsSince(time) / 100);
	}

	public static bool IsAMonday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Monday;
	}

	public static bool IsATuesday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Tuesday;
	}

	public static bool IsAWednesday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Wednesday;
	}

	public static bool IsAThursday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Thursday;
	}

	public static bool IsAFriday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Friday;
	}

	public static bool IsASaturday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Saturday;
	}

	public static bool IsASunday(this DateTime date)
	{
		return date.DayOfWeek == DayOfWeek.Sunday;
	}

	public static bool IsInJanuary(this DateTime date)
	{
		return date.Month == 1;
	}

	public static bool IsInFebruary(this DateTime date)
	{
		return date.Month == 2;
	}

	public static bool IsInMarch(this DateTime date)
	{
		return date.Month == 3;
	}

	public static bool IsInApril(this DateTime date)
	{
		return date.Month == 4;
	}

	public static bool IsInMay(this DateTime date)
	{
		return date.Month == 5;
	}

	public static bool IsInJune(this DateTime date)
	{
		return date.Month == 6;
	}

	public static bool IsInJuly(this DateTime date)
	{
		return date.Month == 7;
	}

	public static bool IsInAugust(this DateTime date)
	{
		return date.Month == 8;
	}

	public static bool IsInSeptember(this DateTime date)
	{
		return date.Month == 9;
	}

	public static bool IsInOctober(this DateTime date)
	{
		return date.Month == 10;
	}

	public static bool IsInNovember(this DateTime date)
	{
		return date.Month == 11;
	}

	public static bool IsInDecember(this DateTime date)
	{
		return date.Month == 12;
	}

	public static DateTime GetFirstDayOccurrenceOfTheMonth(this DateTime date, DayOfWeek day)
	{
		DateTime first = GetFirstDayOfTheMonth(date);
		return first.DayOfWeek == day ? first : GetNextDay(first, day);
	}

	public static DateTime GetFirstMondayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Monday);
	}

	public static DateTime GetFirstTuesdayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetFirstWednesdayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetFirstThursdayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Thursday);
	}

	public static DateTime GetFirstFridayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Friday);
	}

	public static DateTime GetFirstSaturdayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Saturday);
	}

	public static DateTime GetFirstSundayOfTheMonth(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheMonth(date, DayOfWeek.Sunday);
	}

	public static DateTime GetFirstDayOfTheMonth(this DateTime date)
	{
		return new DateTime(date.Year, date.Month, 1);
	}

	public static DateTime GetLastDayOfTheMonth(this DateTime date)
	{
		return SubtractDays(AddAMonth(GetFirstDayOfTheMonth(date)), 1);
	}

	public static DateTime GetLastDayOccurrenceOfTheMonth(this DateTime date, DayOfWeek day)
	{
		DateTime last = GetLastDayOfTheMonth(date);
		return last.DayOfWeek == day ? last : GetPreviousDay(last, day);
	}

	public static DateTime GetLastMondayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Monday);
	}

	public static DateTime GetLastTuesdayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetLastWednesdayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetLastThursdayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Thursday);
	}

	public static DateTime GetLastFridayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Friday);
	}

	public static DateTime GetLastSaturdayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Saturday);
	}

	public static DateTime GetLastSundayOfTheMonth(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheMonth(date, DayOfWeek.Sunday);
	}

	public static DateTime GetFirstDayOccurrenceOfTheYear(this DateTime date, DayOfWeek day)
	{
		DateTime first = GetFirstDayOfTheYear(date);
		return first.DayOfWeek == day ? first : GetNextDay(first, day);
	}

	public static DateTime GetFirstMondayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Monday);
	}

	public static DateTime GetFirstTuesdayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetFirstWednesdayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetFirstThursdayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Thursday);
	}

	public static DateTime GetFirstFridayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Friday);
	}

	public static DateTime GetFirstSaturdayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Saturday);
	}

	public static DateTime GetFirstSundayOfTheYear(this DateTime date)
	{
		return GetFirstDayOccurrenceOfTheYear(date, DayOfWeek.Sunday);
	}

	public static DateTime GetFirstDayOfTheYear(this DateTime date)
	{
		return new DateTime(date.Year, 1, 1);
	}

	public static DateTime GetLastDayOfTheYear(this DateTime date)
	{
		return new DateTime(date.Year, 12, 31);
	}

	public static DateTime GetLastDayOccurrenceOfTheYear(this DateTime date, DayOfWeek day)
	{
		DateTime last = GetLastDayOfTheYear(date);
		return last.DayOfWeek == day ? last : GetPreviousDay(last, day);
	}

	public static DateTime GetLastMondayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Monday);
	}

	public static DateTime GetLastTuesdayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetLastWednesdayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetLastThursdayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Thursday);
	}

	public static DateTime GetLastFridayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Friday);
	}

	public static DateTime GetLastSaturdayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Saturday);
	}

	public static DateTime GetLastSundayOfTheYear(this DateTime date)
	{
		return GetLastDayOccurrenceOfTheYear(date, DayOfWeek.Sunday);
	}

	public static DateTime GetNextMonday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Monday);
	}

	public static DateTime GetNextTuesday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetNextWednesday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetNextThursday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Thursday);
	}

	public static DateTime GetNextFriday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Friday);
	}

	public static DateTime GetNextSaturday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Saturday);
	}

	public static DateTime GetNextSunday(this DateTime date)
	{
		return GetNextDay(date, DayOfWeek.Sunday);
	}

	public static DateTime GetNextDay(this DateTime date, DayOfWeek day)
	{
		DateTime result = date;
		do { result = AddADay(result); } while (result.DayOfWeek != day);
		return result;
	}

	public static DateTime GetPreviousMonday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Monday);
	}

	public static DateTime GetPreviousTuesday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Tuesday);
	}

	public static DateTime GetPreviousWednesday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Wednesday);
	}

	public static DateTime GetPreviousThursday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Thursday);
	}

	public static DateTime GetPreviousFriday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Friday);
	}

	public static DateTime GetPreviousSaturday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Saturday);
	}

	public static DateTime GetPreviousSunday(this DateTime date)
	{
		return GetPreviousDay(date, DayOfWeek.Sunday);
	}

	public static DateTime GetPreviousDay(this DateTime date, DayOfWeek day)
	{
		DateTime result = date;
		do { result = SubtractDays(result, 1); } while (result.DayOfWeek != day);
		return result;
	}

	public static string GetDayString(this DateTime date)
	{
		return date.DayOfWeek.ToString();
	}

	public static string GetMonthString(this DateTime date)
	{
		return date.ToString("MMMM");
	}

	public static string ToDdMmYySlash(this DateTime date)
	{
		return date.ToString("dd/MM/yy");
	}

	public static string ToDdMmYyDot(this DateTime date)
	{
		return date.ToString("dd.MM.yy");
	}

	public static string ToDdMmYyHyphen(this DateTime date)
	{
		return date.ToString("dd-MM-yy");
	}

	public static string ToDdMmYyWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("dd{0}MM{0}yy", separator));
	}

	public static string ToDdMmYyyySlash(this DateTime date)
	{
		return date.ToString("dd/MM/yyyy");
	}

	public static string ToDdMmYyyyDot(this DateTime date)
	{
		return date.ToString("dd.MM.yyyy");
	}

	public static string ToDdMmYyyyHyphen(this DateTime date)
	{
		return date.ToString("dd-MM-yyyy");
		return date.ToString("dd-MM-yyyy");
	}

	public static string ToDdMmYyyyWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("dd{0}MM{0}yyyy", separator));
		return date.ToString(string.Format("dd{0}MM{0}yyyy", separator));
	}

	public static string ToMmDdYySlash(this DateTime date)
	{
		return date.ToString("MM/dd/yy");
	}

	public static string ToMmDdYyDot(this DateTime date)
	{
		return date.ToString("MM.dd.yy");
	}

	public static string ToMmDdYyHyphen(this DateTime date)
	{
		return date.ToString("MM-dd-yy");
	}

	public static string ToMmDdYyWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("MM{0}dd{0}yy", separator));
	}

	public static string ToMmDdYyyySlash(this DateTime date)
	{
		return date.ToString("MM/dd/yyyy");
	}

	public static string ToMmDdYyyyDot(this DateTime date)
	{
		return date.ToString("MM.dd.yyyy");
	}

	public static string ToMmDdYyyyHyphen(this DateTime date)
	{
		return date.ToString("MM-dd-yyyy");
	}

	public static string ToMmDdYyyyWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("MM{0}dd{0}yyyy", separator));
	}

	public static string ToYyMmDdSlash(this DateTime date)
	{
		return date.ToString("yy/MM/dd");
	}

	public static string ToYyMmDdDot(this DateTime date)
	{
		return date.ToString("yy.MM.dd");
	}

	public static string ToYyMmDdHyphen(this DateTime date)
	{
		return date.ToString("yy-MM-dd");
	}

	public static string ToYyMmDdWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("yy{0}MM{0}dd", separator));
	}

	public static string ToYyyyMmDdSlash(this DateTime date)
	{
		return date.ToString("yyyy/MM/dd");
	}

	public static string ToYyyyMmDdDot(this DateTime date)
	{
		return date.ToString("yyyy.MM.dd");
	}

	public static string ToYyyyMmDdHyphen(this DateTime date)
	{
		return date.ToString("yyyy-MM-dd");
	}

	public static string ToYyyyMmDdWithSep(this DateTime date, string separator)
	{
		return date.ToString(string.Format("yyyy{0}MM{0}dd", separator));
	}
}
