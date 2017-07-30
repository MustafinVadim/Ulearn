using System;
using MarkdownDeep;

namespace uLearn
{
	public class Markdown2 : Markdown
	{
		private readonly string baseUrl;
		private readonly bool checkUrl;
		public event Action<string> RelativeUrl;

		public Markdown2(string baseUrl, bool checkUrl = true)
		{
			this.checkUrl = checkUrl;
			this.baseUrl = baseUrl;
			if (baseUrl != null && !baseUrl.EndsWith("/") && checkUrl)
				this.baseUrl += "/";
		}

		public override string OnQualifyUrl(string url)
		{
			if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
			{
				if (RelativeUrl != null)
					RelativeUrl.Invoke(url);
				if (!checkUrl)
					url = url.Replace("/", "_");
			}
			if (baseUrl != null && !url.StartsWith("/") && !url.Contains(":"))
				url = baseUrl + url;
			return base.OnQualifyUrl(url);
		}
	}
}