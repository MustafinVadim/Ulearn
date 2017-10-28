﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using uLearn.Extensions;

namespace uLearn.CSharp.ExponentiationValidation
{
	[TestFixture]
	public class ExponentiationValidator_should
	{
		private static readonly DirectoryInfo testDataDir = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..",
			"..", "CSharp", "ExponentiationValidation", "TestData"));
		private static readonly DirectoryInfo incorrectTestDataDir = testDataDir.GetDirectories("Incorrect").Single();
		private static readonly DirectoryInfo correctTestDataDir = testDataDir.GetDirectories("Correct").Single();

		private static IEnumerable<FileInfo> correctFiles = correctTestDataDir.EnumerateFiles();
		private static IEnumerable<FileInfo> incorrectFiles = incorrectTestDataDir.EnumerateFiles();	
		private static readonly DirectoryInfo basicProgrammingDirectory = new DirectoryInfo(@"C:\work\uLearn\BasicProgramming-master");
		private static IEnumerable<FileInfo> basicProgrammingFiles = basicProgrammingDirectory
			.EnumerateFiles("*.cs", SearchOption.AllDirectories)
			.Where(f => !f.Name.Equals("Settings.Designer.cs") &&
						!f.Name.Equals("Resources.Designer.cs") &&
						!f.Name.Equals("AssemblyInfo.cs"));
		
		private static readonly ExponentiationValidator validator = new ExponentiationValidator();

		[TestCaseSource(nameof(incorrectFiles))]
		public void FindErrors(FileInfo file)
		{
			var code = file.ContentAsUtf8();
			var errors = validator.FindError(code);
			
			errors.Should().NotBeNullOrEmpty();
		}

		[TestCaseSource(nameof(correctFiles))]
		public void NotFindErrors(FileInfo file)
		{
			var code = file.ContentAsUtf8();
			var errors = validator.FindError(code);
			if (errors != null)
			{
				Console.WriteLine(errors);
			}

			errors.Should().BeNullOrEmpty();
		}

		[Explicit]
		[TestCaseSource(nameof(basicProgrammingFiles))]
		public void NotFindErrors_InBasicProgramming(FileInfo file)
		{
			var fileContent = file.ContentAsUtf8();

			var errors = validator.FindError(fileContent);

			if (errors != null)
			{
				File.WriteAllText($@"C:\work\uLearn\errors\{file.Name}_errors.txt",
					$@"{fileContent}

{errors}");

				Assert.Fail();
			}
		}

		private static readonly DirectoryInfo uLearnSubmissionsDirectory = new DirectoryInfo(@"C:\work\uLearn\submissions");
		private static IEnumerable<FileInfo> submissionsFiles = uLearnSubmissionsDirectory
			.GetFiles("*.cs", SearchOption.AllDirectories)
			.Where(f => f.Name.Contains("Accepted"));

		[Explicit]
		[TestCaseSource(nameof(submissionsFiles))]
		public void NotFindErrors_InCheckAcceptedFiles(FileInfo file)
		{
			var fileContent = file.ContentAsUtf8();

			var errors = validator.FindError(fileContent);
			if (errors != null)
			{
				File.WriteAllText($@"C:\work\uLearn\submissions_errors\{file.Name}_errors.txt",
					$@"{fileContent}

{errors}");

				Assert.Fail();
			}
		}
	}
}