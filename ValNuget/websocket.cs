using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace ValAPINet
{
    public class Websocket
    {
        public static Auth GetAuthLocal(bool WaitForLockfile = true)
        {
            string lockfile = "";
            if (WaitForLockfile == true)
            {
                while (lockfile == "")
                {
                    try
                    {
                        using (var fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Riot Games\\Riot Client\\Config\\lockfile", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var sr = new StreamReader(fs, Encoding.Default))
                        {
                            lockfile = sr.ReadToEnd();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            else
            {
                try
                {
                    using (var fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Riot Games\\Riot Client\\Config\\lockfile", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var sr = new StreamReader(fs, Encoding.Default))
                    {
                        lockfile = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            RestClient versionclient = new RestClient("https://valorant-api.com/v1/version");
            RestRequest versionrequest = new RestRequest(Method.GET);
            string json = versionclient.Execute(versionrequest).Content;
            var version = JsonConvert.DeserializeObject(json);
            JToken versionobj = JObject.FromObject(version);
            JToken versiondata = JObject.FromObject(versionobj["data"]);

            //Get ID list
            string versionformat = versiondata["branch"].Value<string>() + "-shipping-" + versiondata["buildVersion"].Value<string>() + "-" + versiondata["version"].Value<string>().Substring(versiondata["version"].Value<string>().Length - 6);

            string[] lf = lockfile.Split(":");
            RestClient GetClient = new RestClient(new Uri($"https://127.0.0.1:{lf[2]}/entitlements/v1/token"));
            GetClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            RestRequest GetRequest = new RestRequest(Method.GET);
            GetRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"riot:{lf[3]}"))}");
            GetRequest.AddHeader("X-Riot-ClientPlatform", "ew0KCSJwbGF0Zm9ybVR5cGUiOiAiUEMiLA0KCSJwbGF0Zm9ybU9TIjogIldpbmRvd3MiLA0KCSJwbGF0Zm9ybU9TVmVyc2lvbiI6ICIxMC4wLjE5MDQyLjEuMjU2LjY0Yml0IiwNCgkicGxhdGZvcm1DaGlwc2V0IjogIlVua25vd24iDQp9");
            GetRequest.AddHeader("X-Riot-ClientVersion", "release-02.06-shipping-14-540727");
            IRestResponse getResp = GetClient.Get(GetRequest);
            var obj = new JObject();
            if (getResp.IsSuccessful)
                obj = JObject.Parse(getResp.Content);
            else
                return null;
            Auth au = new Auth();
            au.AccessToken = (string)obj["accessToken"];
            au.EntitlementToken = (string)obj["token"];
            au.subject = (string)obj["subject"];
            au.version = versionformat;
            au.cookies = new CookieContainer();

            IRestClient RegClient = new RestClient(new Uri($"https://127.0.0.1:{lf[2]}/player-affinity/product/v1/token"));
            RegClient.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            IRestRequest RegRequest = new RestRequest(Method.POST);
            RegRequest.AddHeader("Authorization", $"Basic {Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"riot:{lf[3]}"))}");
            var valorantData = new
            {
                product = "valorant"
            };
            RegRequest.AddJsonBody(JsonConvert.SerializeObject(valorantData));
            IRestResponse RegResp = RegClient.Post(RegRequest);
            var regobj = JObject.Parse(RegResp.Content);
            string reg = (string)regobj["affinities"]["live"];
            if (reg == "NA")
            {
                au.region = Region.NA;
            }
            else if (reg == "AP")
            {
                au.region = Region.AP;
            }
            else if (reg == "EU")
            {
                au.region = Region.EU;
            }
            else if (reg == "KO")
            {
                au.region = Region.KO;
            }
            return au;
        }
        public static Auth StartAndGetAuthLocal(Region region)
        {
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Riot Games\\Riot Client\\RiotClientServices.exe";
            p.StartInfo.Arguments = "--launch-product=valorant --launch-patchline=live";
            p.Start();
            return GetAuthLocal();
        }
    }
}
