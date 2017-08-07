using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;

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
            string filter = "AVI|*.avi|MOV|*.mov|其他视频格式|*.*";
            string file = "";
            OpenFile(ref file, filter);
            return file;
        }
    };

    public class Copy {

        public static T DeepCopy<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopy(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
    };

    public class XMLParser
    {
        private XmlDocument m_cXmlReader = new XmlDocument();
        private string m_sXmlFilePath;

        public bool Load(string xml_file)
        {
            m_sXmlFilePath = xml_file;
            try {
                m_cXmlReader.Load(xml_file);
            }
            catch (Exception e) 
            {
                throw e;
            }

            return true;
        }

        public void Save()
        {
            try {
                m_cXmlReader.Save(m_sXmlFilePath);
            }
            catch (Exception e) 
            {
                throw e;
            }
        }

        public int GetValueInt(string first_key, string secord_key)
        {
            try
            {
                XmlNode node = m_cXmlReader["config"][first_key];
                string val = node[secord_key].InnerXml;
                return Int16.Parse(val);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    };

}
