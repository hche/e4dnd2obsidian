using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace OAuth
{
    public class ObsidianOAuth : OAuthBase
    {
        public enum HttpMethod
        {
            GET,
            POST,
            DELETE,
            PUT
        }

        protected const string REQUEST_TOKEN_URL = "https://www.obsidianportal.com/oauth/request_token";
        protected const string AUTHORIZE_URL = "https://www.obsidianportal.com/oauth/authorize";
        protected const string ACCESS_TOKEN_URL = "https://www.obsidianportal.com/oauth/access_token";

        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }
        public string CallBackUrl { get; set; }
        public string OAuthVerifier { get; set; }

        public string GetAuthorizationUrl()
        {
            string result = null;
            string response;
            try
            {


                response = ExecuteOAuth(HttpMethod.GET, REQUEST_TOKEN_URL, String.Empty);
            }
            catch (Exception e)
            {
                Exception e2 = new Exception("Fehler bei ExecuteOAuth:", e);
                throw e2;
            }

            if (response.Length > 0)
            {
                // response contains token and token secret. we only need the toke.n
                NameValueCollection parameters = HttpUtility.ParseQueryString(response);

                if (parameters["oauth_callback_confirmed"] != null)
                {
                    if (parameters["oauth_callback_confirmed"] != "true")
                        throw new Exception("OAuth Callback Not Confirmed");
                }

                // added by param
                TokenKey = parameters["oauth_token"];

                if (parameters["oauth_token"] != null)
                    result = AUTHORIZE_URL + "?oauth_token=" + parameters["oauth_token"];
            }

            return result;
        }

        public void GetAccessToken()
        {
            GetAccessToken(TokenKey, OAuthVerifier);
        }

        public void GetAccessToken(string verifier)
        {
            GetAccessToken(TokenKey, verifier);
        }

        public void GetAccessToken(string token, string verifier)
        {
            TokenKey = token;

            OAuthVerifier = verifier;

            string response = ExecuteOAuth(HttpMethod.GET, ACCESS_TOKEN_URL, String.Empty);

            if (response.Length > 0)
            {
                // store token & token secret
                NameValueCollection parameters = HttpUtility.ParseQueryString(response);

                if (parameters["oauth_token"] != null)
                    TokenKey = parameters["oauth_token"];

                if (parameters["oauth_token_secret"] != null)
                    TokenSecret = parameters["oauth_token_secret"];
            }
        }

        public string ExecuteOAuth(HttpMethod method, string url, string data)
        {
            string result = string.Empty;
            
            //if (method == HttpMethod.POST || method == HttpMethod.DELETE)
            //{
            //    if (data.Length > 0)
            //    {
            //        //Decode the parameters and re-encode using the oAuth UrlEncode method.
            //        NameValueCollection parameters = HttpUtility.ParseQueryString(data);

            //        data = string.Empty;

            //        foreach (string key in parameters.AllKeys)
            //        {
            //            if (data.Length > 0)
            //                data += "&";

            //            parameters[key] = HttpUtility.UrlDecode(parameters[key]);
            //            parameters[key] = UrlEncode(parameters[key]);

            //            // 14.08.11 he: JSON-String, hat nur eine einzige Value ohne Key.
            //            if (parameters.Count == 1)
            //            {
            //                data = parameters[0];
            //                break;
            //            }

            //            data += key + "=" + parameters[key];
            //        }

            //        if (url.IndexOf("?") > 0)
            //            url += "&";
            //        else
            //            url += "?";

            //        url += data;
            //    }
            //}

            string nurl = string.Empty;
            string nquery = string.Empty;

             //Add verifier if no token request or authorize url
            if (!String.IsNullOrEmpty(OAuthVerifier)
                && url != REQUEST_TOKEN_URL
                && url != AUTHORIZE_URL)
            {
                if (url.Contains("?"))
                    url += "&";
                else
                    url += "?";
                
                url += "oauth_verifier=" + OAuthVerifier;
            }

            Uri uri = new Uri(url);
            string nonce = GenerateNonce();
            string timeStamp = GenerateTimeStamp();

            string signature = GenerateSignature(uri,
                               ConsumerKey, ConsumerSecret,
                               TokenKey, TokenSecret,
                               method.ToString(),
                               timeStamp, nonce,
                               out nurl, out nquery);

            nquery += "&oauth_signature=" + this.UrlEncode(signature);

            //Convert the nquery to data
            if (method == HttpMethod.POST || method == HttpMethod.DELETE)
            {

                // Original:
                //data = nquery;
                //nquery = "";
            }

            if (nquery.Length > 0)
                nurl += "?";

            result = Execute(method, nurl + nquery, data);

            return result;
        }

        protected string Execute(HttpMethod method, string url, string data)
        {
            string response = string.Empty;

            HttpWebRequest request = System.Net.WebRequest.Create(url) as HttpWebRequest;
            request.Method = method.ToString();
            request.ServicePoint.Expect100Continue = false;
            //!!request.ClientCertificates.
            //request.UserAgent  = "Identify your application please.";
            //request.Timeout = 20000;
            //request.ServicePoint.S

            ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                //if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors)
                //{
                //    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                //    {
                //        if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown)
                //        {
                //            return false;
                //        }
                //    }
                //    return true;
                //}

                //return false;
                return true;
            };


            if (method == HttpMethod.POST || method == HttpMethod.DELETE || method == HttpMethod.PUT)
            {
                request.ContentType = "application/x-www-form-urlencoded";

                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                }
            }

            response = GetWebResponse(request);

            request = null;

            return response;
        }

        protected string GetWebResponse(HttpWebRequest request)
        {
            string result = string.Empty;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            if (response != null)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}

