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
        //dirty but it works :)
        public static void HandleImages(SocketUserMessage msg)
        {
            Console.WriteLine("Uploading image!");
            DownloadImage(msg);
            DriveConfiguration();
            UploadImage();
            Console.WriteLine("Images uploaded to meme folder");
            DeleteImage();
            Console.WriteLine("Deleted image from local");
        }

        public static void DownloadImage(SocketUserMessage msg)
        {
            foreach (var img in msg.Attachments) //use msg to add other shit to img
            {
                var url = msg.Attachments.FirstOrDefault().Url;
                
                using (WebClient client = new WebClient())
                {
                    _file = @"C:\Users\camay\Documents\gomenasai-bot\gomenasai-bot\images\" + img.Id + ".png";
                    client.DownloadFile(new Uri(url), _file);

                    if (msg.Content == "")
                    {
                        string imageName = @"C:\Users\camay\Documents\gomenasai-bot\gomenasai-bot\images\Author" + msg.Author + "Time" + msg.Timestamp.Day + msg.Timestamp.Month + msg.Timestamp.Year + ".png";
                        RenameImage(imageName);
                    }
                    else
                    {
                        string imageName = @"C:\Users\camay\Documents\gomenasai-bot\gomenasai-bot\images\Author" + msg.Author + "Message" + msg.Content + "Time" + msg.Timestamp.Day + msg.Timestamp.Month + msg.Timestamp.Year + ".png";
                        RenameImage(imageName);
                    }
                }
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

        //search for last 5 images by the persons name
            //check each message to find images
            //check if image exists
                //if it doesnt exist
                    //upload

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
