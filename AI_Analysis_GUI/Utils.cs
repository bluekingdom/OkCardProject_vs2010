using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Utils
{
    public class FileSystem
    {
        public static bool OpenFile(ref string file, string filter) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C://";
            openFileDialog.Filter = filter;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            file = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public static string OpenImageFile()
        {
            string filter = "JPG|*.jpg|PNG|*.png|BMP|*.bmp|其他图片格式|*.*";
            string file = "";
            OpenFile(ref file, filter);
            return file;
        }

        public static string OpenVideoFile()
        {
            string filter = "AVI|*.avi|其他视频格式|*.*";
            string file = "";
            OpenFile(ref file, filter);
            return file;
        }
    };

}
