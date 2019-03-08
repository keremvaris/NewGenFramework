using System.Collections.Generic;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public static class UploadUtils
    {
        static HashSet<string> kabulEdilenTurler = new HashSet<string>
        {
            ".jpeg", ".gif" ,".jpg", ".png"
        };
        static HashSet<string> kabulEdilenDosyaTurleri = new HashSet<string>
        {
           ".pdf", ".doc",".docx" ,".xls",".xlsx" , ".ppt",".pptx"
        };

        public static bool IsImage(string extension)
        {
            return kabulEdilenTurler.Contains(extension);
        }

        public static bool IsFile(string extension)
        {
            return kabulEdilenDosyaTurleri.Contains(extension);
        }
    }
}