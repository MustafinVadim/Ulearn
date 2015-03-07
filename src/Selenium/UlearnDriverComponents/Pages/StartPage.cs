﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace Selenium.UlearnDriverComponents.Pages
{
	public class StartPage : UlearnPage, IObserver
	{
		private const string courseBlockClass = "col-md-6";

		public StartPage(IWebDriver driver, IObserver parent) 
			: base(driver, parent)
		{
			if (!driver.Title.Equals(Titles.StartPageTitle))
				throw new IllegalLocatorException("Это не стартовая страница, это: "
								+ driver.Title);
		}

		public UlearnDriver GoToSignInPage()
		{
			var loginLinkButton = driver.FindElement(ElementsId.SignInButton);
			loginLinkButton.Click();
			parent.Update();
			return new UlearnDriver(driver);
		}

		public UlearnDriver GoToCourse(string courseTitle)
		{
			var courseBlocks = driver.FindElements(By.ClassName(courseBlockClass)).ToList();
			if (courseTitle == Titles.BasicProgrammingTitle)
			{
				parent.Update();
				return ClickCourseButton(courseTitle, courseBlocks, 0);
			}
			if (courseTitle == Titles.LinqTitle)
			{
				parent.Update();
				return ClickCourseButton(courseTitle, courseBlocks, 1);
			}
			throw new NotImplementedException(string.Format("Для курса {0} нет реализации в методе GoToCourse", courseTitle));
		}

		private UlearnDriver ClickCourseButton(string courseTitle, IList<IWebElement> courseBlocks, int index)
		{
			if (courseBlocks.Count >= index + 1)
				courseBlocks[index].FindElement(By.LinkText("Поехали!")).Click();
			else
				throw new NotFoundException(string.Format("Не найдена кнопка перехода на курс {0}", courseTitle));
			parent.Update();
			return new UlearnDriver(driver);
		}

		public new void Update()
		{}
	}
}
