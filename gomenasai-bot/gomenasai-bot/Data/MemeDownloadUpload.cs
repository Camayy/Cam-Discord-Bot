using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v3;
using System.Text.RegularExpressions;

namespace gomenasai_bot.Data
{
    public class MemeDownloadUpload
    {
        public static string _file = null;
        public static DriveService _service = null;
        private string location = null;
        private static bool _uploaded = false;
        private static string _fileType = null;
        private static SocketUserMessage _msg;
        //dirty but it works :)
        public static void HandleImages(SocketUserMessage msg, bool uploaded, string filetype)
        {
            _uploaded = uploaded;
            _fileType = filetype;
            _msg = msg;
            try
            {
                Console.WriteLine("Downloading image");
                DownloadImage(msg);
                DriveConfiguration();
                Console.WriteLine("Uploading image");
                UploadImage();
                Console.WriteLine("Images uploaded to meme folder");
                DeleteImage();
                Console.WriteLine("Deleted image from local");
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: " + e);
                Console.WriteLine("USER: " + _msg.Author);
                Console.WriteLine("IMAGE: "+ msg.Id);
            }
        }

        public static void DownloadImage(SocketUserMessage msg)
        {
            if (_uploaded)
            {
                foreach (var img in msg.Attachments) //use msg to add other shit to img
                {
                    DownloadFileType(img.Id.ToString());
                }
            }
            else
            {
                DownloadFileType(msg.Id.ToString());
            }
        }

        private static void DownloadFileType(string imgName)
        {
            var url = "";

            using (WebClient client = new WebClient())
            {
                if (_uploaded == true)
                {
                    url = _msg.Attachments.FirstOrDefault().Url;
                    _file = Path.Combine(Environment.CurrentDirectory, @"images\", imgName + ".png");
                    _fileType = "png";
                }
                else
                {
                    url = _msg.Content;
                    _file = Path.Combine(Environment.CurrentDirectory, @"images\", imgName + "." + _fileType);
                }


                client.DownloadFile(new Uri(url), _file);

                //C:\Users\camay\Documents\gomenasai-bot\gomenasai-bot
                string imageName = Path.Combine(Environment.CurrentDirectory, @"images\", _msg.Author + "Time" + _msg.Timestamp.Day + _msg.Timestamp.Month + _msg.Timestamp.Year + "." + _fileType);
                RenameImage(imageName);

                //make it so linked files can be uploaded with msg.content to attatch a word
            }
        }

        public static void RenameImage(string imageName)
        {
            if (File.Exists(_file))
            {
                File.Copy(_file, imageName, true);
                File.Delete(_file);
            }
            _file = imageName;
        }

        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "memestorage";

        static void DriveConfiguration()
        {
            UserCredential credential;

            credential = GetCredentials();

            // Create Drive API service.
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            
        }

        public static void UploadImage()
        {
            var imagefile = new Google.Apis.Drive.v3.Data.File();
            imagefile.Name = Path.GetFileName(_file);
            imagefile.Parents = new List<string> { "1FUmrpO1LYtYAWQPeyfVyvvbW4f7ON_p7" };
            imagefile.MimeType = "image/png";
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream(_file, System.IO.FileMode.Open))
            {
                request = _service.Files.Create(imagefile, stream, "image/png");
                request.Fields = "id";
                request.Upload();
            }

            var file = request.ResponseBody;

            Console.WriteLine("File ID: " + file.Id);
        }

        private static UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/newcredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }

        private static void DeleteImage()//check where images are being saved to on laptop
        {
            if (File.Exists(_file))
            {
                File.Delete(_file);
            }
        }
    }
}
