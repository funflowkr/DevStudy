using UnityEngine;
using System;
using System.Net.Http;
using System.Collections;
//using System.Threading.Tasks;
//using System.Runtime.CompilerServices;

public class AsyncAwait : MonoBehaviour
{
	// Start is called before the first frame update
	async void Start()
    {
		Util.LogCurrentThread();

		HttpClientHandler webRequestHandler = new HttpClientHandler();
		webRequestHandler.AllowAutoRedirect = false;
		using (HttpClient client = new HttpClient(webRequestHandler))
		{
			var stringTask = client.GetStringAsync("https://docs.microsoft.com/en-us/dotnet/about/");
			try
			{

				var responseText = await stringTask;
				TextManager.Instance.Set(responseText);
			}
			catch (System.Net.Http.HttpRequestException e) when (e.Message.Contains("301"))
			{
				TextManager.Instance.Set("Site Moved");
			}
		}

		Util.LogCurrentThread();
	}
}

