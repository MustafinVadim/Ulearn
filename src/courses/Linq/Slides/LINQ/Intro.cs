﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable ClassNeverInstantiated.Global

namespace uLearn.Courses.Linq.Slides.LINQ
{
	public class S010_Intro
	{
		public List<int> GetNewLettersIds_ClassicWay()
		{
			var res = new List<int>();
			for(int i=0; i<letters.Length; i++)
			{
				if (letters[i].IsNew)
					res.Add(letters[i].Id);
			}
			return res;
		}

		public IEnumerable<int> GetNewLettersIds_LinqWay()
		{
			return letters.Where(l => l.IsNew).Select(l => l.Id);
		}

		[Test]
		public void Test()
		{
			CollectionAssert.AreEqual(GetNewLettersIds_ClassicWay(), GetNewLettersIds_LinqWay());
		}

		private readonly Letter[] letters = new Letter[0];

		public class Letter
		{
			public bool IsNew { get; set; }
			public int Id { get; set; }
		}
	}
}