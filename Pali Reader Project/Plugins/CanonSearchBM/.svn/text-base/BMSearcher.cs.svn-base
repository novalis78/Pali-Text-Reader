using System;
using System.Collections;

namespace BMSearch
{
	public class BMSearcher
	{
		protected BMHashTable PatternCharShifts; // Shift table for chars present in the pattern
		private int[] OtherCharShifts;  // Shifts for all other chars
		private int PatternLength;  // Length of the search pattern

		/// <summary>
		/// Class constructor. Pattern is a string to search for.
		/// </summary>

		public BMSearcher(string Pattern)
		{
			PatternCharShifts = new BMHashTable();
			// Building shift table
			PatternLength = Pattern.Length;
			int MaxShift = PatternLength;
			// Constructing the table where number of columns is equal to PatternLength
			// and number of rows is equal to the number of distinct chars in the pattern
			for (int i = 0; i < PatternLength; i++)
				PatternCharShifts.Add(Pattern[i], new int[PatternLength]);
			OtherCharShifts = new int[PatternLength];
			// Filling the last column of the table with maximum shifts (pattern length)
			foreach(char key in PatternCharShifts.Keys)
				PatternCharShifts.Get(key)[PatternLength - 1] = MaxShift;
			OtherCharShifts[PatternLength - 1] = MaxShift;
			// Calculating other shifts (filling each column from PatternLength - 2 to 0 (from right to left)
			for(int i = PatternLength - 1; i >= 0; i--)
			{
				// Suffix string contains the characters right to the character being processsed
				string Suffix = new String(Pattern.ToCharArray(), i + 1,  PatternLength - i - 1);
				// if Pattern begins with Suffix the maximum shift is equal to i + 1
				if (Pattern.StartsWith(Suffix))
					MaxShift = i + 1;
				// Store shift for characters not present in the pattern
				OtherCharShifts[i] = MaxShift;
				// We shorten patter by one char in NewPattern.
				string NewPattern = new string(Pattern.ToCharArray(), 0, Pattern.Length -1);
				if ((NewPattern.LastIndexOf(Suffix) > 0) || (Suffix.Length == 0))
					foreach(char key in PatternCharShifts.Keys)
					{
						string NewSuffix  = key + Suffix;
						// Calculate shifts:
						//Check if there are other occurences of the new suffix in the pattern
						// If several occurences exist, we need the rightmost one
						int NewSuffixPos = NewPattern.LastIndexOf(NewSuffix);
						if (NewSuffixPos >= 0) 
							PatternCharShifts.Get(key)[i] = i - NewSuffixPos;
						else 
							PatternCharShifts.Get(key)[i] = MaxShift;
						// Storing 0 if characters in a row and a columnt are the same
						if (key == Pattern[i])
							PatternCharShifts.Get(key)[i] = 0;
					}
				else
					foreach(char key in PatternCharShifts.Keys)
					{
						// if Suffix doesn't occure in NewPattern we simply use previous shift value
						PatternCharShifts.Get(key)[i] = MaxShift;
						if (key == Pattern[i]) 
							PatternCharShifts.Get(key)[i] = 0;
					}
			}
		}

		/// <summary>
		/// This method returns a string representation of the shift table
		/// for debug purposes.
		/// </summary>

		public string GetTable()
		{
			string result = "";
			foreach(char key in PatternCharShifts.Keys)
			{
				result = result + key + ": ";
				for(int i = 0; i < PatternCharShifts.Get(key).Length; i++)
					result = result + PatternCharShifts.Get(key)[i].ToString()+ " ";
				result += "\r\n";
			}
			result += "*: ";
			for(int i = 0; i < OtherCharShifts.Length; i++)
				result = result + OtherCharShifts[i].ToString()+ " ";
			return result;
		}


		/// <summary>
		/// This method performs the search.
		/// Arguments: StartPos is a zero-based index in the text
		/// from where the search sould start.
		/// Text is the string where to search.
		/// Returns the index of the leftmost occurrence of the pattern.
		/// If the pattern is not present in the text, the method returns -1
		/// </summary>

		public int Search(string Text, int StartPos)
		{
			int Pos = StartPos;
			while (Pos <= Text.Length - PatternLength)
				for(int i = PatternLength - 1; i >= 0; i--)
				{
					int[] shifts = PatternCharShifts.Get(Text[Pos + i]); 
					if (shifts != null)
					{
						// pattern contains char Text[Pos + i]
						int Shift = shifts[i];
						if (Shift != 0)
						{
							Pos += Shift; // shifting
							break;
						}
						else
							if (i == 0)  // we came to the leftmost pattern character so pattern matches
							return Pos;  // return matching substring start index
					}
					else
					{
						Pos += OtherCharShifts[i]; // shifting
						break;
					}
				}
			// Nothing is found
			return -1;
		}
	}

	public class CIBMSearcher : BMSearcher
	{
		public CIBMSearcher(string Pattern, bool CaseSensitive) : base(Pattern)
		{
			if (!CaseSensitive)
			{
				BMHashTable TmpHT = new BMHashTable();
				foreach(char key in PatternCharShifts.Keys)
				{
					TmpHT.Add(key, PatternCharShifts.Get(key));
					char ch;
					if (char.IsLower(key))
						ch = char.ToUpper(key);
					else
						ch = char.ToLower(key);
					TmpHT.Add(ch, PatternCharShifts.Get(key));
				}
				PatternCharShifts = TmpHT;
			}
		}
	}
}
