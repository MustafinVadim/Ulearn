﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.UlearnDriverComponents
{
	public interface IObservable
	{
		void AddObserver(IObserver observer);
		void RemoveObserver(IObserver observer);
		void NotifyObservers();
	}
}
