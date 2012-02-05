  using System;
  using System.Security.Cryptography;
  using System.Collections.Generic;
  using System.Text;
  using System.Web;
  
  namespace TwitterOAuthTestHarness
  {
      public class OAuth
      {
          #region Protected Constant Fields
  
          protected const string OAuthVersion             = "1.0";
          protected const string OAuthParameterPrefix     = "oauth_";
  
          protected const string OAUTH_CONSUMER_KEY       = "oauth_consumer_key";
          protected const string OAUTH_CALLBACK           = "oauth_callback";
          protected const string OAUTH_VERSION            = "oauth_version";
          protected const string OAUTH_SIGNATURE_METHOD   = "oauth_signature_method";
          protected const string OAUTH_SIGNATURE          = "oauth_signature";
          protected const string OAUTH_TIMESTAMP          = "oauth_timestamp";
          protected const string OAUTH_NONCE              = "oauth_nonce";
          protected const string OAUTH_TOKEN              = "oauth_token";
          protected const string OAUTH_TOKEN_SECRET       = "oauth_token_secret";
          protected const string OAUTH_VERIFIER           = "oauth_verifier";
  
          protected const string PLAIN_TEXT               = "PLAINTEXT";
          protected const string HMAC_SHA1                = "HMAC-SHA1";
          protected const string RSA_SHA1                 = "RSA-SHA1";
  
          #endregion
  
          protected enum SignatureType
          {
              PLAINTEXT,
              HMACSHA1,
              RSASHA1
          }
  
          protected class QueryParameter
          {
              public string Name { get; private set; }
  
              public string Value { get; private set; }
  
              public QueryParameter(string name, string value)
              {
                  Name = name;
                  Value = value;
              }
          }
  
          protected class QueryParameterComparer : IComparer<QueryParameter>
          {
              // used to perform sorting of the query parameters
              public int Compare(QueryParameter x, QueryParameter y)
              {
                  if (x.Name == y.Name)
                      return string.Compare(x.Value, y.Value);
                  else
                      return string.Compare(x.Name, y.Name);
              }
          }
  
          protected string ComputeHash(HashAlgorithm hash, string data)
          {
              Console.WriteLine("Computing hash...");
  
              string result = string.Empty;
  
              if (hash == null)
                  throw new ArgumentNullException("hash");
  
              if (string.IsNullOrEmpty(data))
                  throw new ArgumentNullException("data");
  
              byte[] input = Encoding.ASCII.GetBytes(data);
              byte[] output = hash.ComputeHash(input);
  
              result = Convert.ToBase64String(output);
  
              Console.WriteLine("Hash computed. " + result);
  
              return result;
          }
  
          protected List<QueryParameter> GetQueryParameters(string parameters)
          {
              Console.WriteLine("Cutting out non-oauth parameters...");
  
              // function to cut out all non oauth query string parameters 
              // all parameters not begining with "oauth_"
  
              if (parameters.StartsWith("?"))
                  parameters = parameters.Remove(0, 1);
  
              List<QueryParameter> result = new List<QueryParameter>();
  
              if (!string.IsNullOrEmpty(parameters))
              {
                  string[] p = parameters.Split('&');
  
                  foreach (string s in p)
                  {
                      if (!string.IsNullOrEmpty(s) && !s.StartsWith(OAuthParameterPrefix))
                      {
                          if (s.IndexOf('=') > -1)
                          {
                              string[] temp = s.Split('=');
                              result.Add(new QueryParameter(temp[0], temp[1]));
                          }
                          else
                              result.Add(new QueryParameter(s, string.Empty));
                      }
                  }
              }
  
              return result;
          }
  
          public string UrlEncode(string value)
          {
              Console.WriteLine("Encoding URL...");
  
              //This is a different Url Encode implementation since the default .NET one outputs 
              //the percent encoding in lower case. While this is not a problem with the percent 
              //encoding spec, it is used in upper case throughout OAuth
              
              string unreserved = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
  
              StringBuilder result = new StringBuilder();
  
              foreach (char symbol in value)
              {
                  if (unreserved.IndexOf(symbol) != -1)
                      result.Append(symbol);
                  else
                      result.Append('%' + String.Format("{0:X2}", (int)symbol));
              }
  
              Console.WriteLine("URL encoded. " + result.ToString());
  
              return result.ToString();
          }
  
          protected string NormalizeParameters(IList<QueryParameter> parameters)
          {
              Console.WriteLine("Normalizing request parameters...");
  
              StringBuilder result = new StringBuilder();
  
              QueryParameter p = null;
  
              for (int i = 0; i < parameters.Count; i++)
              {
                  p = parameters[i];
  
                  result.AppendFormat("{0}={1}", p.Name, p.Value);
  
                  if (i < parameters.Count - 1)
                      result.Append("&");
              }
  
              Console.WriteLine("Request parameter normalized. " + result.ToString());
  
              return result.ToString();
          }
  
          protected string GenerateTimeStamp()
          {
              string result = string.Empty;
  
              Console.WriteLine("Generating timestamp...");
  
              // default implementation of UNIX time of the current UTC time
              TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
  
              result = Convert.ToInt64(ts.TotalSeconds).ToString();
  
              Console.WriteLine("Timestamp generated. " + result);
  
              return result;
          }
  
          protected string GenerateNonce()
          {
              Console.WriteLine("Generating Nonce...");
  
              string result = string.Empty;
  
              result = new Random().Next(123400, 9999999).ToString();
  
              Console.WriteLine("Nonce generated. " + result);
  
              return result;
          }
  
          protected string GenerateSignatureBase(Uri url, string consumerKey, string token, 
              string tokenSecret, string callbackUrl, string oauthVerifier, string httpMethod, 
              string timestamp, string nonce, string signatureType, 
              out string normalizedUrl, out string normalizedRequestParameters)
          {
              Console.WriteLine("Generating signature base...");
  
              if (token == null)
                  token = string.Empty;
  
              if (tokenSecret == null)
                  tokenSecret = string.Empty;
  
              if (string.IsNullOrEmpty(consumerKey))
                  throw new ArgumentNullException("Consumer Key");
  
              if (string.IsNullOrEmpty(httpMethod))
                  throw new ArgumentNullException("HTTP Method");
  
              if (string.IsNullOrEmpty(signatureType))
                  throw new ArgumentNullException("Signature Type");
  
              normalizedUrl = null;
              normalizedRequestParameters = null;
  
              List<QueryParameter> parameters = GetQueryParameters(url.Query);
  
              parameters.Add(new QueryParameter(OAUTH_VERSION, OAuthVersion));
              parameters.Add(new QueryParameter(OAUTH_NONCE, nonce));
              parameters.Add(new QueryParameter(OAUTH_TIMESTAMP, timestamp));
              parameters.Add(new QueryParameter(OAUTH_SIGNATURE_METHOD, signatureType));
              parameters.Add(new QueryParameter(OAUTH_CONSUMER_KEY, consumerKey));
  
              if (!string.IsNullOrEmpty(callbackUrl))
                  parameters.Add(new QueryParameter(OAUTH_CALLBACK, UrlEncode(callbackUrl)));
  
              if (!string.IsNullOrEmpty(oauthVerifier))
                  parameters.Add(new QueryParameter(OAUTH_VERIFIER, oauthVerifier));
  
              if (!string.IsNullOrEmpty(token))
                  parameters.Add(new QueryParameter(OAUTH_TOKEN, token));
  
              parameters.Sort(new QueryParameterComparer());
  
              normalizedUrl = string.Format("{0}://{1}", url.Scheme, url.Host);
              if (!((url.Scheme == "http" && url.Port == 80) || (url.Scheme == "https" && url.Port == 443)))
                  normalizedUrl += ":" + url.Port;
  
              normalizedUrl += url.AbsolutePath;
              normalizedRequestParameters = NormalizeParameters(parameters);
  
              StringBuilder signatureBase = new StringBuilder();
              signatureBase.AppendFormat("{0}&", httpMethod.ToUpper());
              signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
              signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));
  
              Console.WriteLine("Signature base generated. " + signatureBase.ToString());
  
              return signatureBase.ToString();
          }
  
          protected string GenerateSignatureUsingHash(string signatureBase, HashAlgorithm hash)
          {
              Console.WriteLine("Generating signature using hash...");
  
              string result = string.Empty;
  
              result = ComputeHash(hash, signatureBase);
  
              Console.WriteLine("Signature generated. " + result);
  
              return result;
          }
  
          protected string GenerateSignature(Uri url, string consumerKey, string consumerSecret, 
              string token, string tokenSecret, string callBackUrl, string oauthVerifier, 
              string httpMethod, string timeStamp, string nonce, 
              out string normalizedUrl, out string normalizedRequestParameters)
          {
              return GenerateSignature(url, consumerKey, consumerSecret, token, tokenSecret, callBackUrl, oauthVerifier, httpMethod, timeStamp, nonce, SignatureType.HMACSHA1, out normalizedUrl, out normalizedRequestParameters);
          }
  
          protected string GenerateSignature(Uri url, string consumerKey, string consumerSecret, 
              string token, string tokenSecret, string callBackUrl, string oauthVerifier, 
              string httpMethod, string timeStamp, string nonce, SignatureType signatureType, 
              out string normalizedUrl, out string normalizedRequestParameters)
          {
              Console.WriteLine("Generating signature...");
  
              normalizedUrl = null;
              normalizedRequestParameters = null;
  
              Console.WriteLine("Switching signature type to " + signatureType.ToString());
  
              switch (signatureType)
              {
                  case SignatureType.PLAINTEXT:
                      return HttpUtility.UrlEncode(string.Format("{0}&{1}", consumerSecret, tokenSecret));
  
                  case SignatureType.HMACSHA1:
                      url.
                      string signatureBase = GenerateSignatureBase(url, consumerKey, token, tokenSecret, callBackUrl, oauthVerifier, httpMethod, timeStamp, nonce, HMAC_SHA1, out normalizedUrl, out normalizedRequestParameters);
                      HMACSHA1 hash = new HMACSHA1();
                      hash.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(consumerSecret), string.IsNullOrEmpty(tokenSecret) ? string.Empty : UrlEncode(tokenSecret)));
                      return GenerateSignatureUsingHash(signatureBase, hash);
                  case SignatureType.RSASHA1:
                      throw new NotImplementedException();
                  default:
                      throw new ArgumentException("Unknown Signature Type", "Signature Type");
              }
          }
      }
  }

