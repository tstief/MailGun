using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;

namespace MailGun
{
  public class MailClient
  {
    private HttpClient Client { get; set; }
    private string Domain { get; set; }
    private string MailKey { get; set; }

    public MailClient(string domain, string key)
    {
      Domain = domain;
      MailKey = key;
      Client = new HttpClient();
      Client.DefaultRequestHeaders.Authorization =
          new AuthenticationHeaderValue("Basic",
                                        Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes("api" +
                                         ":" + MailKey)));
    }

    public async Task<HttpResponseMessage> SendMessageAsync(string toAddress, string subject, string text)
    {
      var form = new Dictionary<string, string>();
      form["from"] = "noreply@mg.stieffamily.com";
      form["to"] = toAddress;
      form["subject"] = subject;
      form["text"] = text;
      return await Client.PostAsync("https://api.mailgun.net/v3/" + Domain + "/messages", new FormUrlEncodedContent(form));
    }
  }
}
