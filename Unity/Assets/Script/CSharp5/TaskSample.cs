using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class TaskSample
{
	public static async Task<string> MakeRequest()
	{
		HttpClientHandler webRequestHandler = new HttpClientHandler();
		webRequestHandler.AllowAutoRedirect = false;
		using (HttpClient client = new HttpClient(webRequestHandler))
		{
			var stringTask = client.GetStringAsync("https://docs.microsoft.com/en-us/dotnet/about/");
			try
			{
				Util.LogCurrentThread();

				for (int a = 0; a < 5; ++a)
				{
					await Task.Delay(1000);
					Util.LogCurrentThread();
				}

				var responseText = await stringTask;
				return responseText;
			}
			catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
			{
				return "Site Moved";
			}
		}
	}
}
