using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;

public class ExceptionFilter : MonoBehaviour
{
	/// Initialize associative collections using indexers
	private Dictionary<int, string> webErrors = new Dictionary<int, string>
	{
		[404] = "Page not Found",
		[302] = "Page moved, but left a forwarding address.",
		[500] = "The web server can't come out to play today."
	};

	// Start is called before the first frame update
	async void Start()
    {
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

		TextManager.Instance.Set(nameof(ExceptionFilter), 1);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
