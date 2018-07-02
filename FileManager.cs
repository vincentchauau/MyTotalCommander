using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MyTotalCommander
{
    [Serializable]
    public class FileX
    {
        public string _name { get; set; }
        public string _type { get; set; }
        public long _size { get; set; }
        //XML Serializer has this function only
        public FileX()
        { }
        //Binary Serilializer has this function
        public FileX(string name, string type, long size)
        {
            _name = name;
            _type = type;
            _size = size;
        }
    }
    public class FileManager
    {
        public string SourceIp = "";
        public List<string> TargetIps = new List<string>();
        public List<string> Ips = new List<string>();
        public void ListAllIps()
        {
            Network[] networks = Network.GetLocalNetwork();
            foreach (Network network in networks)
            {
                Ips.Add(network.Addresses[network.Addresses.Length - 1].ToString());
            }
            Ips.Reverse();
            SourceIp = Ips[0];
        }
        public List<FileX> List(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            List<FileX> files = new List<FileX>();
            if (source == "")
            {
                try
                {
                    foreach (string drive in Directory.GetLogicalDrives())
                    {
                        DriveInfo info = new DriveInfo(drive);
                        files.Add(new FileX(drive.TrimEnd('\\'), "drive", info.TotalSize / (1024 * 1024 * 1024) + 1));
                    }
                }
                catch (Exception e)
                { }
            }
            else
            {
                if (Directory.Exists(source) == true)
                {
                    try
                    {
                        files.Add(new FileX("..", "parent", 0));
                        foreach (string file in (Directory.EnumerateFiles(source + "\\", "*", SearchOption.TopDirectoryOnly)))
                        {
                            FileInfo info = new FileInfo(file);
                            files.Add(new FileX(info.Name, info.Extension, info.Length / 1024 + 1));
                        }
                        foreach (string directory in Directory.EnumerateDirectories(source + "\\", "*", SearchOption.TopDirectoryOnly))
                        {
                            DirectoryInfo info = new DirectoryInfo(directory);
                            files.Add(new FileX(info.Name, "folder", 0));
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                    }
                }
                else { }
            }
            return files;
        }
        public void Rename(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            if (sources.Count > 0)
            {
                string name = sources[0];
                for (int i = 0; i < sources.Count; ++i)
                {
                    if (File.Exists(sources[i]) == true)
                    {
                        File.Move(source + sources[i], source + name + i);
                    }
                    else
                    {
                        Directory.Move(source + sources[i], source + name + i);
                    }
                }
            }
            else { }
        }
        public string View(List<string> sources)
        {
            string result = "";
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            foreach (string file in sources)
            {
                StreamReader reader = new StreamReader(source + "\\" + sources[0]);
                result += file + "\r\n";
                result += reader.ReadToEnd() + "\r\n";
                reader.Close();
            }
            return result;
        }
        public void Run(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            foreach (string file in sources)
            {
                System.Diagnostics.Process.Start(source + "\\" + file);
            }
        }
        sealed class AllowAllAssemblyVersionsDeserializationBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type typeToDeserialize = null;
                String currentAssembly = Assembly.GetExecutingAssembly().FullName;
                assemblyName = currentAssembly;
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                return typeToDeserialize;
            }
        }
        // Convert between string, byte[], object, stream
        // string/object/byte[]/stream
        // string => byte[]
        public int LengthCopy(List<Section> iSections)
        {
            int length = 0;
            foreach (Section section in iSections)
            {
                length += section._length;
            }
            return length;
        }
        // Copy iObject with iSections to oObject with oSections
        public void Object2Object(object iObject, ref object oObject, List<Section> iSections = null, List<Section> oSections = null, int type = 1)
        {
            // byte[], stream, object, string
            Stream iStream = new MemoryStream();
            Stream oStream = new MemoryStream();
            byte[] iBytes = null;
            byte[] oBytes = null;
            BinaryReader reader = null;
            BinaryWriter writer = null;
            // 1. Copy iObject to iStream
            if (iObject == null)
            {
                return;
            }
            else
            {
                if (iObject.GetType() == typeof(byte[]))
                {
                    iBytes = (byte[])iObject;
                    iStream.Write(iBytes, 0, iBytes.Length);
                }
                else if (iObject.GetType() == typeof(string))
                {
                    iBytes = new byte[((string)iObject).Length * sizeof(char)];
                    Buffer.BlockCopy(((string)iObject).ToCharArray(), 0, iBytes, 0, iBytes.Length);
                    iStream.Write(iBytes, 0, iBytes.Length);
                }
                else if (iObject.GetType().BaseType == typeof(Stream))
                {
                    iStream = (Stream)iObject;
                }
                else
                {
                    switch (type)
                    {
                        case 0:
                            XmlSerializer xmlSerializer = new XmlSerializer(iObject.GetType());
                            xmlSerializer.Serialize(iStream, iObject);
                            break;
                        case 1:
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(iStream, iObject);
                            break;
                        case 2:
                            iBytes = new byte[Marshal.SizeOf(iObject)];
                            var ptr = Marshal.AllocHGlobal(iBytes.Length);
                            Marshal.Copy(iBytes, 0, ptr, iBytes.Length);
                            iObject = Marshal.PtrToStructure(ptr, iObject.GetType());
                            Marshal.FreeHGlobal(ptr);
                            iStream.Write(iBytes, 0, iBytes.Length);
                            break;
                    }
                }
            }
            if (iStream.CanSeek)
            {
                iStream.Seek(0, SeekOrigin.Begin);
                if (iSections == null)
                {
                    iSections = new List<Section>() { new Section(0, (int)iStream.Length) };
                }
                else { }
            }
            else
            {
                reader = new BinaryReader((Stream)iObject);
                iSections = new List<Section> { new Section(0, reader.ReadInt32()) };
            }
            // 2. Copy oObject to stream
            if (oObject == null)
            {

            }
            else
            {
                if (oObject.GetType() == typeof(string))
                {
                    oBytes = new byte[((string)oObject).Length * sizeof(char)];
                    Buffer.BlockCopy(((string)oObject).ToCharArray(), 0, oBytes, 0, oBytes.Length);
                    oStream.Write(oBytes, 0, oBytes.Length);
                }
                else if (oObject.GetType() == typeof(byte[]))
                {
                    oBytes = (byte[])oObject;
                    oStream.Write(oBytes, 0, oBytes.Length);
                }
                else if (oObject.GetType().BaseType == typeof(Stream))
                {
                    oStream = (Stream)oObject;
                }
                else
                {
                    switch (type)
                    {
                        case 0:
                            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
                            xmlSerializer.Serialize(oStream, oObject);
                            break;
                        case 1:
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(oStream, oObject);
                            break;
                        case 2:
                            oBytes = new byte[Marshal.SizeOf(oObject)];
                            var ptr = Marshal.AllocHGlobal(oBytes.Length);
                            Marshal.Copy(oBytes, 0, ptr, oBytes.Length);
                            oObject = Marshal.PtrToStructure(ptr, oObject.GetType());
                            Marshal.FreeHGlobal(ptr);
                            oStream.Write(oBytes, 0, oBytes.Length);
                            break;
                    }
                }
            }
            if (oStream.CanSeek)
            {
                oStream.Seek(0, SeekOrigin.Begin);
                if (oSections == null)
                {
                    oSections = new List<Section>(iSections);
                }
                else { }
            }
            else
            {
                writer = new BinaryWriter((Stream)oObject);
                writer.Write((int)iStream.Length);
                oSections = new List<Section>(iSections);
            }
            // 3. Copy iStream with iSections to oStream with oSections
            int i = 0, j = 0;
            iBytes = new byte[1024];
            for (; i < iSections.Count;)
            {
                for (; j < oSections.Count;)
                {
                    if (iStream.CanSeek)
                    {
                        iStream.Seek(iSections[i]._start, SeekOrigin.Begin);
                    }
                    else { }
                    if (oStream.CanSeek)
                    {
                        oStream.Seek(oSections[j]._start, SeekOrigin.Begin);
                    }
                    else { }
                    if (iSections[i]._length < oSections[j]._length)
                    {
                        int k;
                        for (k = 0; k < iSections[i]._length / 1024; ++k)
                        {
                            iStream.Read(iBytes, 0, 1024);
                            oStream.Write(iBytes, 0, 1024);
                        }
                        iStream.Read(iBytes, 0, iSections[i]._length % 1024);
                        oStream.Write(iBytes, 0, iSections[i]._length % 1024);
                        oSections[j]._start += iSections[i]._length;
                        oSections[j]._length -= iSections[i]._length;
                        ++i;
                    }
                    else
                    {
                        int k;
                        for (k = 0; k < oSections[j]._length / 1024; ++k)
                        {
                            iStream.Read(iBytes, 0, 1024);
                            oStream.Write(iBytes, 0, 1024);
                        }
                        iStream.Read(iBytes, 0, oSections[j]._length % 1024);
                        oStream.Write(iBytes, 0, oSections[j]._length % 1024);
                        iSections[i]._start += oSections[j]._length;
                        iSections[i]._length -= oSections[j]._length;
                        ++j;
                    }
                    if (i == iSections.Count || j == oSections.Count)
                    {
                        break;
                    }
                    else { }
                }
                if (i == iSections.Count || j == oSections.Count)
                {
                    break;
                }
                else { }
            }
            // 4. Copy oStream to oObject
            if (oStream.CanSeek)
            {
                oStream.Seek(0, SeekOrigin.Begin);
            }
            else { }
            if (oObject.GetType() == typeof(string))
            {
                oBytes = new byte[oStream.Length];
                oStream.Read(oBytes, 0, oBytes.Length);
                char[] oChars = new char[oStream.Length / sizeof(char)];
                Buffer.BlockCopy(oBytes, 0, oChars, 0, oBytes.Length);
                oObject = new string(oChars);
            }
            else if (oObject.GetType() == typeof(byte[]))
            {
                oBytes = new byte[oStream.Length];
                oStream.Read(oBytes, 0, oBytes.Length);
                oObject = oBytes;
            }
            else if (oObject.GetType().BaseType == typeof(Stream))
            {
                oObject = oStream;
            }
            else
            {
                switch (type)
                {
                    case 0:
                        XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
                        XmlReader xr = new XmlTextReader(oStream);
                        oObject = xmlSerializer.Deserialize(oStream);
                        break;
                    case 1:
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Binder = new AllowAllAssemblyVersionsDeserializationBinder();
                        oObject = formatter.Deserialize(oStream);
                        break;
                    case 2:
                        var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(oObject));
                        Marshal.Copy(oBytes, 0, ptr, Marshal.SizeOf(oObject));
                        oObject = Marshal.PtrToStructure(ptr, oObject.GetType());
                        Marshal.FreeHGlobal(ptr);
                        break;
                }
            }
        }
        public void Copy(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            for (int i = 0; i < sources.Count; ++i)
            {
                // sources[i] is a file then copy the file
                if (File.Exists(source + "\\" + sources[i]) == true)
                {
                    File.Copy(source + "\\" + sources[i], target + "\\" + sources[i], true);
                }
                else // sources[i] is a folder
                {
                    // create the folder
                    Directory.CreateDirectory(target + "\\" + sources[i]);
                    // create all the sub folders
                    foreach (string dirPath in Directory.GetDirectories(source + "\\" + sources[i], "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(source, target));
                    // copy all the sub files
                    foreach (string newPath in Directory.GetFiles(source + "\\" + sources[i], "*.*", SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(source, target), true);
                }
            }
        }
        public void CopyClient(List<string> sources, ref object clientStream)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            object filex;
            Stream iStream;
            for (int i = 0; i < sources.Count; ++i)
            {
                // if copy the file sources[i]
                if (File.Exists(source + "\\" + sources[i]) == true)
                {
                    iStream = new FileStream(source + "\\" + sources[i], FileMode.Open);
                    filex = new FileX(target + "\\" + sources[i], "file", (int)iStream.Length);
                    Object2Object(filex, ref clientStream);
                    Object2Object(iStream, ref clientStream);
                    ((Stream)iStream).Close();
                }
                else // copy the folder sources[i]
                {
                    // create the folder
                    filex = new FileX(target + "\\" + sources[i], "folder", 0);
                    Object2Object(filex, ref clientStream);
                    // create all the sub folders
                    foreach (string path in Directory.GetDirectories(source + "\\" + sources[i], "*", SearchOption.AllDirectories))
                    {
                        filex = new FileX(path.Replace(source, target), "folder", 0);
                        Object2Object(filex, ref clientStream);
                    }
                    // copy all the sub files
                    foreach (string path in Directory.GetFiles(source + "\\" + sources[i], "*.*", SearchOption.AllDirectories))
                    {
                        iStream = new FileStream(path, FileMode.Open);
                        filex = new FileX(path.Replace(source, target), "file", (int)iStream.Length);
                        Object2Object(filex, ref clientStream);
                        Object2Object(iStream, ref clientStream);
                        ((Stream)iStream).Close();
                    }
                }
            }
            filex = new FileX("end", "end", 0);
            Object2Object(filex, ref clientStream);
        }
        public void CopyServer(ref object serverStream)
        {
            while (true)
            {
                object filex = new FileX();
                Object2Object(serverStream, ref filex);
                if (((FileX)filex)._type == "folder")
                {
                    Directory.CreateDirectory(((FileX)filex)._name);
                }
                else if (((FileX)filex)._type == "file")
                {
                    object oStream = new FileStream(((FileX)filex)._name, FileMode.OpenOrCreate);
                    Object2Object(serverStream, ref oStream);
                    ((Stream)oStream).Close();
                }
                else
                {
                    break;
                }
            }
        }
        public void Move(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            for (int i = 0; i < sources.Count; ++i)
            {
                if (File.Exists(source + "\\" + sources[i]) == true)
                {
                    File.Move(source + "\\" + sources[i], target + "\\" + sources[i]);
                }
                else
                {
                    Directory.Move(source + "\\" + sources[i], target + "\\" + sources[i]);
                }
            }
        }
        public void Create(List<string> sources, bool directory = true)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            if (sources.Count == 0)
            {
                if (directory == true)
                {
                    sources.Add("New folder");
                }
                else
                {
                    sources.Add("New file");
                }
            }
            else { }
            for (int i = 0; i < sources.Count; ++i)
            {
                if (directory == true)
                {
                    Directory.CreateDirectory(source + "\\" + sources[i]);
                }
                else
                {
                    File.Create(source + "\\" + sources[i]);
                }
            }

        }
        public void Delete(List<string> sources)
        {
            string source = sources[0];
            string target = sources[1];
            sources.RemoveRange(0, 2);
            for (int i = 0; i < sources.Count; ++i)
            {
                if (File.Exists(source + "\\" + sources[i]) == true)
                {
                    File.Delete(source + "\\" + sources[i]);
                }
                else
                {
                    Directory.Delete(source + "\\" + sources[i], true);
                }
            }
        }
        public void Split(string iFileName, int iNumberOfFiles)
        {
            FileStream iStream = new FileStream(iFileName, FileMode.Open);
            int iFileSize = (int)iStream.Length / iNumberOfFiles;
            int iBlockSize = 1024;
            int iNumberOfBlocks;
            byte[] bytes = new byte[1024];
            int i;
            FileStream oStream;
            int iPercent = 0;
            for (i = 0; i < iNumberOfFiles - 1; ++i)
            {
                iPercent = (int)((double)i / (double)iNumberOfFiles * 100);
                iNumberOfBlocks = iFileSize / iBlockSize;
                oStream = new FileStream(iFileName + "." + i.ToString(), FileMode.Create);
                for (int j = 0; j < iNumberOfBlocks; ++j)
                {
                    iStream.Read(bytes, 0, iBlockSize);
                    oStream.Write(bytes, 0, iBlockSize);
                }
                iStream.Read(bytes, 0, iFileSize % iBlockSize);
                oStream.Write(bytes, 0, iFileSize % iBlockSize);
                oStream.Close();
            }
            iFileSize = (int)iStream.Length - (int)iStream.Position;
            iNumberOfBlocks = iFileSize / iBlockSize;
            oStream = new FileStream(iFileName + "." + i.ToString(), FileMode.Create);
            for (int j = 0; j < iNumberOfBlocks; ++j)
            {
                iStream.Read(bytes, 0, iBlockSize);
                oStream.Write(bytes, 0, iBlockSize);
            }
            iStream.Read(bytes, 0, iFileSize % iBlockSize);
            oStream.Write(bytes, 0, iFileSize % iBlockSize);
            oStream.Close();
            iStream.Close();
        }
        public void Merge(string iFileName)
        {
            List<string> fileNames = new List<string>();
            string sExtension = Path.GetExtension(iFileName);
            string sFileWithoutExtension = iFileName.Substring(0, iFileName.Length - sExtension.Length);
            string sFileWithinExtension = "";
            for (int i = 0; i < 100; ++i)
            {
                sFileWithinExtension = sFileWithoutExtension + "." + i.ToString();
                if (File.Exists(sFileWithinExtension))
                {
                    fileNames.Add(sFileWithinExtension);
                }
                else
                {
                    break;
                }
            }
            FileStream oStream = new FileStream(sFileWithoutExtension, FileMode.Create);
            int iFileSize;
            int iBlockSize = 1024;
            int iNumberOfBlocks;
            byte[] bytes = new byte[1024];
            int iPercent;
            int iNumberOfFiles = fileNames.Count;
            for (int i = 0; i < iNumberOfFiles; ++i)
            {
                iPercent = (int)((double)i / (double)iNumberOfFiles * 100);
                FileStream iStream = new FileStream(fileNames[i], FileMode.Open);
                int j;
                iFileSize = (int)iStream.Length;
                iNumberOfBlocks = iFileSize / iBlockSize;
                for (j = 0; j < iNumberOfBlocks; ++j)
                {
                    iStream.Read(bytes, 0, iBlockSize);
                    oStream.Write(bytes, 0, iBlockSize);
                }
                iStream.Read(bytes, 0, iFileSize % iBlockSize);
                oStream.Write(bytes, 0, iFileSize % iBlockSize);
                iStream.Close();
            }
            oStream.Close();
        }
        public ImageList ImageList(string path)
        {
            ImageList imageList = new ImageList();
            imageList.ImageSize = new System.Drawing.Size(32, 32);
            string[] files = Directory.GetFiles(Environment.CurrentDirectory + "\\" + path);
            for (int i = 0; i < files.Length; ++i)
            {
                imageList.Images.Add(Image.FromFile(path + "\\" + i.ToString() + ".png"));
            }
            return imageList;
        }
    }
    [Serializable]
    public class Section
    {
        public int _start { get; set; }
        public int _length { get; set; }
        public Section()
        { }
        public Section(int start, int length)
        {
            _start = start;
            _length = length;
        }
    }
}
