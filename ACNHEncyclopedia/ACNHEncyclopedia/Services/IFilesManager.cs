using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ACNHEncyclopedia.Services
{
    public interface IFilesManager
    {
        void LoadFileObb();
        void LoadImagesFile();
        string GetObbFileText(string filename);
        Stream GetObbFileImageToStream(string filename);
        string GetTextFromAssetFile(string filename);
        Stream GetImageAssetToStream(string imageName);
    }
}