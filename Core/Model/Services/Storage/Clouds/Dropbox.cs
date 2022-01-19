using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Logging;
using Newtonsoft.Json.Linq;

namespace Core.Model.Storage.Clouds
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
    public class Dropbox
    {
        //public CloudBrand Brand => CloudBrand.Dropbox;

        readonly HttpClient _apiClient;
        readonly HttpClient _contentClient;


        public Dropbox()
        {
            var tempAuth = "…";

            _apiClient = new HttpClient { BaseAddress = new Uri("https://api.dropboxapi.com/") };
            _apiClient.DefaultRequestHeaders.Add("Authorization", tempAuth);
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _contentClient = new HttpClient { BaseAddress = new Uri("https://content.dropboxapi.com/") };
            _contentClient.DefaultRequestHeaders.Add("Authorization", tempAuth);
        }

        public async Task<JObject> SendApiRequest(string path, HttpContent? data = null)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/2" + path);
            request.Content = data;

            using var response = await _apiClient.SendAsync(request);
            Dlog.Info(response);

            var rawContent = await response.Content.ReadAsStringAsync();
            return JObject.Parse(rawContent);
        }

        //void Authenticate()
        //{
            // This isn't an API call—it's the web page that lets the user sign in to Dropbox and authorize your app.
            // After the user decides whether or not to authorize your app, they will be redirected to the URI specified by redirect_uri.

            // go back with a URL scheme on iOS? what's on Android?

            //_httpClient.BaseAddress = new Uri("https://www.dropbox.com");
            //var parameters = "/oauth2/authorize?client_id=…";
            //var response = Task.Run(() => _httpClient.GetAsync(parameters)).Result;
        //}

        public async Task<string> FetchAccountNameAsync()
        {
            var content = await SendApiRequest("/users/get_current_account");

            return content["email"].ToString();
        }

        public async Task<List<string>> FetchDirectoryContentsAsync(string directoryPath)
        {
            var json = "{\"path\": \"" + directoryPath + "\"," +
                        "\"recursive\": false," +
                        "\"include_media_info\": false," +
                        "\"include_deleted\": false," +
                        "\"include_has_explicit_shared_members\": false," +
                        "\"include_mounted_folders\": true," +
                        "\"include_non_downloadable_files\": true}";

            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var content = await SendApiRequest("/files/list_folder", data);

            var hasMore = content["has_more"].Value<bool>();
            var cursor = content["cursor"].ToString();
            var entries = content["entries"] as JArray;

            while (hasMore)
            {
                var cursorJson = "{\"cursor\": \"" + cursor + "\"}";
                var cursorData = new StringContent(cursorJson, Encoding.UTF8, "application/json");
                var remainingContent = await SendApiRequest("/files/list_folder/continue", cursorData);

                hasMore = remainingContent["has_more"].Value<bool>();
                cursor = remainingContent["cursor"].ToString();

                var remainingEntries = remainingContent["entries"] as JArray;
                for (int i = 0; i < remainingEntries.Count; i++)
                {
                    entries.Add(remainingEntries[i]);
                }
            }

            var names = new List<string>();
            foreach (var item in entries)
            {
                names.Add(item.ToString());
                //files.Add(CreateFileFromJson(item));
            }

            return names;
        }

        // TODO: result
        public async Task<StreamReader> DownloadFileAsync(string filePath)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/2/files/download");
            request.Headers.Add("Dropbox-API-Arg", "{\"path\": \"" + filePath + "\"}");

            using var response = await _contentClient.SendAsync(request);
            if (response.Headers.TryGetValues("dropbox-api-result", out var results))
            {
                var value = results.FirstOrDefault();
                if (string.IsNullOrEmpty(value) == false)
                {
                    var content = JObject.Parse(value);
                    var isDownloadable = content["is_downloadable"].Value<bool>();
                    var contentHash = content["content_hash"].ToString();
                    var entries = content["entries"] as JArray;

                    var stream = await response.Content.ReadAsStreamAsync();
                    return new StreamReader(stream);
                }
            }

            return null!;
        }

        //public async Task<bool> UploadFileAsync()
        //{
        //    //": {\"path\": \"/Homework/math/Prime_Numbers.txt\"}"
        //}

        //CloudFile CreateFileFromJson(JToken token)
        //{
        //    //Qtodo: finish
        //    var file = new CloudFile("TEST");
        //    return file;
        //}
    }

    public class CloudFile
    {
        public string TempText { get; }

        public CloudFile(string tempText)
        {
            TempText = tempText;
        }
    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
}
