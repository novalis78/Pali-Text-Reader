using System;

namespace BMSearch
{
	
	// Simple hash table for BMSearcher
	// This table is used instead of System.Collections.Hashtable 
	// for performance reasons
	public class BMHashTable
	{
		// Used internally by this class
		private struct BMHashItem
		{
			// Hash key
			public char Key;
			// Value
			public int[] Shifts;
		}

		private BMHashItem[][] Items;
		private int Count;

		public BMHashTable()
		{
			Items = new BMHashItem[256][];
		}
		
		// Returns value specified by Key
		// and null if the Key isn't found 
		public int[] Get(char Key)
		{
			int HashedKey = Key % 256;
			if (Items[HashedKey] != null)
			{
				// The most likely variant
				if (Items[HashedKey][0].Key == Key)
					return Items[HashedKey][0].Shifts;
				for(int i = 1; i < Items[HashedKey].Length; i++)
					if (Items[HashedKey][i].Key == Key)
						return Items[HashedKey][i].Shifts;
			}
			return null;
		}

		// Adds a key-value pair to the hash table
		// If the key is already present in the table this method does nothing
		public void Add(char Key, int[] Value)
		{
			if (Get(Key) != null)
				return;
			int HashedKey = Key % 256;
			BMHashItem HI = new BMHashItem();
			HI.Shifts = new int[Value.Length];
			HI.Key = Key;
			HI.Shifts = Value;
			if (Items[HashedKey] == null)
				Items[HashedKey] = new BMHashItem[1] {HI};
			else
			{
				BMHashItem[] NewItems = new BMHashItem[Items[HashedKey].Length+1];
				Items[HashedKey].CopyTo(NewItems, 0);
				NewItems[Items[HashedKey].Length] = HI;
				Items[HashedKey] = NewItems;
			}
			Count++;
		}

		// Returns an array of keys present in the table
		public char[] Keys
		{
			get
			{
				int index = 0;
				char[] keys = new char[Count];
				for(int i = 0; i < 256; i++)
                    if (Items[i] != null)
						for(int j = 0; j < Items[i].Length; j++)
						{
							keys[index] = Items[i][j].Key;
							index++;
						}
				return keys;
			}
		}
	}
}
