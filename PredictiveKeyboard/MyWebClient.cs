using System;
using System.Collections.Generic;
using System.Web;
using System.Net;

/// <summary>
/// This class overides some methods of webclient class for automatic decompression and enabling cookies in request.
/// </summary>
public class MyWebClient : WebClient
{
	public MyWebClient()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    Uri _responseUri;

    public Uri ResponseUri
    {
        get { return _responseUri; }
    }

    protected override WebResponse GetWebResponse(WebRequest request)
    {
        WebResponse response = base.GetWebResponse(request);
        _responseUri = response.ResponseUri;
        return response;
    }

    ///// <summary>
    ///// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///// </summary>
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private CookieContainer m_container = new CookieContainer();
    protected override WebRequest GetWebRequest(Uri address)
    {
        WebRequest request = base.GetWebRequest(address);
        if (request is HttpWebRequest)
        {
            (request as HttpWebRequest).CookieContainer = m_container;
            //the proerty is set toautomatically decompress the gzip files...
            (request as HttpWebRequest).AutomaticDecompression = DecompressionMethods.GZip;
            (request as HttpWebRequest).AllowAutoRedirect = true;
            (request as HttpWebRequest).KeepAlive = true;


        }
        return request;
    }
}
