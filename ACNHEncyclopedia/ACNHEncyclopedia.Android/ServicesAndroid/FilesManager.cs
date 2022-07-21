using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using ACNHEncyclopedia.Droid.ServicesAndroid;
using ACNHEncyclopedia.Services;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Vending.Expansion.ZipFile;
using Xamarin.Forms;
using ICSharpCode.SharpZipLib.Zip;

[assembly: Dependency(typeof(FilesManager))]
namespace ACNHEncyclopedia.Droid.ServicesAndroid
{
    public class FilesManager : IFilesManager
    {
        private string DocumentsPath { get; } = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        static List<ZipResourceFile.ZipEntryRO> FilesEntries;
        public ZipResourceFile Files;
        public ZipFile ImagesFile;
        public FilesManager()
        {
            LoadFileObb();
            LoadImagesFile();
        }

        public void LoadFileObb()
        {
            var context = Android.App.Application.Context;
            var packageInfo = Android.App.Application.Context.PackageManager.GetPackageInfo(context.PackageName, 0);
            Files = APKExpansionSupport.GetAPKExpansionZipFile(context, packageInfo.VersionCode, 0);
            /*Stream gzaedatae = Files.GetInputStream("Data/GameDataRelease.json");
            FilesEntries = Files.GetAllEntries().ToList();
            AssetFileDescriptor afd = Files.GetAssetFileDescriptor("Data/GameDataRelease.json");
            AssetFileDescriptor afd2 = Files.GetAssetFileDescriptor("Data/GameDataRelease");*/
            //if (Files == null) Xamarin.Forms.Application.Current.Quit();
        }

        public void LoadImagesFile()
        {
            try
            {
                using (Stream stream = Android.App.Application.Context.Assets.Open("Data/ABCDEFGHIJKLMNOPQRSTUVWXYZ.gbdev"))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    stream.CopyTo(memoryStream);
                    ImagesFile = new ZipFile(memoryStream);
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        public string GetObbFileText(string filename)
        {
            // ADD TRY CATCH
            string inputTxt = null;
            try
            {
                Stream input = Files.GetInputStream(filename); // get stream of file !
                inputTxt = new StreamReader(input, Encoding.UTF8).ReadToEnd();

            }
            catch (Exception ex)
            {

            }
            return inputTxt;
        }

        public Stream GetObbFileImageToStream(string filename)
        {
            // ADD TRY CATCH
            Stream input = null;
            try
            {
                input = Files.GetInputStream($"Images\\{filename}"); // get stream of file !
            }
            catch (Exception ex)
            {

            }
            return input;
        }

        public string GetTextFromAssetFile(string filename)
        {
            string text = null;
            try
            {
                using (Stream stream = Android.App.Application.Context.Assets.Open(filename))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        text = sr.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return text;
        }

        public Stream GetImageAssetToStream(string imageName)
        {
            Stream stream = null;
            try
            {
                string fullname = "Images/"+imageName;
                ZipEntry zipEntry = ImagesFile.GetEntry(fullname);
                stream = ImagesFile.GetInputStream(zipEntry);
            }
            catch(Exception ex)
            {

            }
            return stream;
        }
    }
}