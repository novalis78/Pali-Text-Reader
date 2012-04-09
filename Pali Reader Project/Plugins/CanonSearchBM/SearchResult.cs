///
///
/// Copyright (C) 2005  Lennart Lopin <novalis78@gmx.net> 
/// All Rights Reserved.
///
/// This program is free software; you can redistribute it and/or
/// modify it under the terms of the GNU General Public License as
/// published by the Free Software Foundation; either version 2 of the
/// License, or (at your option) any later version.
/// 
/// This program is distributed in the hope that it will be useful, but
/// WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
/// General Public License for more details.
/// 
/// You should have received a copy of the GNU General Public License
/// along with this program; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
/// 02111-1307, USA.
/// 
///

using System;
using System.IO;
using System.Text;
using System.Drawing;
using BMSearch;
using System.Windows.Forms;
using System.ComponentModel;
using PluginInterface;
using System.Text.RegularExpressions;
using System.Collections;

namespace PaliReaderPlugin
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchResult
    {
        private Hashtable wordList;
        private Hashtable bookList;
        private string searchTerm;

        public SearchResult()
        {
            wordList = new Hashtable();
            bookList = new Hashtable();
        }

        public void AddMatch(string book, string word)
        {
            if (word == "" | book == "")
                return;
            word = Normalize(word);
            book = Normalize(book);

            if (!wordList.Contains(word))
                wordList.Add(word, book);
            else
            {
                string books = (string)wordList[word];
                books += "," + book;
                wordList[word] = books;
            }
            if (!bookList.Contains(book))
                bookList.Add(book, word);
            else
            {
                string words = (string)bookList[book];
                words += "," + word;
                bookList[book] = words;
            }
        }
        
        public void SetSearchTerm(string s)
        {
        	searchTerm = s;
        }
        
        public string[] GetWordList()
        {
        	int i = wordList.Count;
        	string[] x = new string[i];
        	for(int q = 0; q < i; q++)
        		x[q] = "";
        	wordList.Keys.CopyTo(x, 0);
        	Array.Sort(x);
        	return x;
        }
        
        public string[] GetBookList()
        {
        	int i = bookList.Count;
        	string[] x = new string[i];
        	for(int q = 0; q < i; q++)
        		x[q] = "";
        	bookList.Keys.CopyTo(x, 0);
        	return x;
        }

        public string[] GetMatchingBooks(string word)
        {
            string t = ((string)wordList[word]);
            if (t != null)
                return t.Split(',');
            else
                return null;
        }

        public string[] GetMatchingWords(string book)
        {
            string t = ((string)bookList[book]);
            if (t != null)
                return t.Split(',');
            else
                return null;
        }
        
        public string GetSearchTerm()
        {
        	return this.searchTerm;
        }

        /// <summary>
        /// Count how many times a word, which contains the match(es)
        /// occures in a certain books
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public int CountMatchesInBook(string book)
        {
            string t = ((string)wordList[book]);
            if (t != null)
                return t.Split(',').Length;
            else
                return -1;
        }

        /// <summary>
        /// Count how many times a word, which contains the match(es)
        /// occures in all available books
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public int CountMatchesOfWord(string word)
        {
            string books = (string)wordList[word];
            string[] t = books.Split(',');
            if (t != null)
                return t.Length;
            else
                return -1;
        }
        public int CountMatchesOfWordInBook(string book, string word)

        {
            int cnt = -1;
            string books = (string)wordList[word];
            string[] t = books.Split(',');
            if (t == null)
                return -1;
            for (int i = 0; i < t.Length; i++)
                if (t[i] == book)
                    cnt++;
            return cnt;
        }

        public int SumOfMatchingWords()
        {
            return wordList.Count;
        }
     	
        public int SumOfOccurences()
        {
        	int cnt = -1;
        	IDictionaryEnumerator ide = wordList.GetEnumerator();
        	while(ide.MoveNext())
        	{
        		cnt++;
        		string[] t = ((string)ide.Value).Split(',');
        		if (t == null)
                 continue;
        		for (int i = 0; i < t.Length; i++)
        		 cnt++;
        	}
        	return cnt;
        }

        public int SumOfBooks()
        {
            return bookList.Count;
        }

        private string Normalize(string a)
        {
        	a = a.Replace(".txt", "");
        	a = a.Replace("\\", "/");
        	a = a.Substring(a.LastIndexOf("/") + 1);
            a = a.Trim(' ', '?', '\"', ',', '\'', ';', ':', '.', '(', ')', '[', ']', '*', '-').ToLower();
            return a;
        }


    }
}
