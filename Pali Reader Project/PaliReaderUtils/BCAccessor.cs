
using System;
using AMS.Profile;
using BuddhistCalendar;

namespace PaliReaderUtils
{
	/// <summary>
	/// Description of BCAccessor.
	/// </summary>
	public class BCAccessor
	{
		private string NextPoya;
		private string BuddhistYear;
		
		public BCAccessor()
		{
			TheravadaCalendar tc = new TheravadaCalendar(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			NextPoya = tc.getNextFullMoonDate().ToShortDateString();
			BuddhistYear = tc.BuddhistYear.ToString();
		}
		
		public string GetNextPoya()
		{
			return NextPoya;
		}
		
		public string GetBuddhistYear()
		{
			return BuddhistYear;
		}
	}
		
}
