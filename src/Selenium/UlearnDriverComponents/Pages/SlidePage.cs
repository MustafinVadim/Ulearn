﻿using System;
using OpenQA.Selenium;
using Selenium.UlearnDriverComponents.PageObjects;

namespace Selenium.UlearnDriverComponents.Pages
{
	public class SlidePage : UlearnContentPage, IObserver
	{
		private Lazy<Rates> rates;


		public SlidePage(IWebDriver driver, IObserver parent)
			: base(driver, parent)
		{
			Configure();
		}

		private new void Configure()
		{
			base.Configure();
			rates = new Lazy<Rates>(() => new Rates(driver));
		}

		public void RateSlide(Rate rate)
		{
			rates.Value.RateSlide(rate);
			parent.Update();
		}

		public bool IsRateActive(Rate rate)
		{
			return rates.Value.IsActive(rate);
		}

		public new void Update()
		{
			Configure();
		}
	}
}
