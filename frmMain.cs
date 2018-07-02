using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTotalCommander
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        private FileManager fileManager = new FileManager();
        private List<string> sources = new List<string>();
        private Dictionary<Keys, string> types = new Dictionary<Keys, string>() {
                { Keys.F1, "List" }, { Keys.F2, "Rename" }, {Keys.F3, "View" },  { Keys.F4, "Run" }, { Keys.F5, "Copy" }, {Keys.F6, "Move" },  { Keys.F7, "Create" }, { Keys.F8, "Delete" }, {Keys.F9, "Split" }, { Keys.F10, "Merge"} };

        private List<string> GetSources(string control)
        {
            List<string> sources = new List<string>();
            ListView lvCurrent;
            if (control == "left")
            {
                lvCurrent = lvLeft;
                sources.Add(lbLeft.Text);
                sources.Add(lbRight.Text);
            }
            else
            {
                lvCurrent = lvRight;
                sources.Add(lbRight.Text);
                sources.Add(lbLeft.Text);
            }
            foreach (ListViewItem item in lvCurrent.SelectedItems)
            {
                sources.Add(item.Text);
            }
            return sources;
        }
        private List<string> GetListSources(string control)
        {
            List<string> sources = GetSources(control);
            if (sources.Count > 2)
            {
                if (sources[2] == "..")
                {
                    if (sources[0].LastIndexOf('\\') != -1)
                    {
                        sources[0] = sources[0].Substring(0, sources[0].LastIndexOf('\\'));
                    }
                    else
                    {
                        sources[0] = "";
                    }
                }
                else
                {
                    if (sources[0] != "")
                        sources[0] += "\\" + sources[2];
                    else
                    {
                        sources[0] = sources[2];
                    }
                }
            }
            else { }
            return sources;
        }

        public void Answer()
        {
            Thread threadServer = new Thread(new ThreadStart(AnswerFunction));
            threadServer.IsBackground = true;
            threadServer.Start();
        }
        private void AnswerFunction()
        {
            try
            {
                TcpListener server = new TcpListener(IPAddress.Parse(fileManager.SourceIp), 12345);
                object serverStream;
                server.Start();
                object o;
                Message message;
                while (true)
                {
                    TcpClient serverClient = server.AcceptTcpClient();
                    serverStream = serverClient.GetStream();
                    o = new Message();
                    fileManager.Object2Object(serverStream, ref o);
                    message = o as Message;
                    sources = (List<string>)message.Argument;
                    switch (message.Type)
                    {
                        case "Forward":
                            Question(message.Argument);
                            break;
                        case "Message":
                            Invoke(new MethodInvoker(delegate
                            {
                                rtbQuestion.Text += message.Ip + ":" + ((List<string>)message.Argument)[0] + "\r\n";
                            }));
                            break;
                        case "List":
                            message.Result = fileManager.List((List<string>)message.Argument);
                            fileManager.Object2Object(o, ref serverStream);
                            break;
                        case "Rename":
                            fileManager.Rename((List<string>)message.Argument);
                            break;
                        case "View":
                            message.Result = fileManager.View((List<string>)message.Argument);
                            fileManager.Object2Object(o, ref serverStream);
                            break;
                        case "Run":
                            fileManager.Run((List<string>)message.Argument);
                            break;
                        case "Copy":
                            fileManager.CopyServer(ref serverStream);
                            break;
                        case "Move":
                            fileManager.CopyServer(ref serverStream);
                            break;
                        case "Create":
                            fileManager.Create((List<string>)message.Argument);
                            break;
                        case "Delete":
                            fileManager.Delete((List<string>)message.Argument);
                            break;
                        case "Split":
                            break;
                        case "Merge":
                            break;
                        case "Zip":
                            break;
                        case "Unzip":
                            break;
                    }
                }
            }
            catch (Exception)
            { }
        }
        public void Question(object o)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(QuestionFunction));
            thread.IsBackground = true;
            thread.Start(o);
        }
        private void QuestionFunction(object o)
        {
            try
            {
                Message message = o as Message;
                TcpClient client = new TcpClient();
                client.Connect(message.Ip, 12345);
                object clientStream = client.GetStream();
                fileManager.Object2Object(o, ref clientStream);
                switch (message.Type)
                {
                    case "Message":
                        Invoke(new MethodInvoker(delegate
                        {
                            rtbQuestion.Text += fileManager.SourceIp + ":" + ((List<string>)message.Argument)[0] + "\r\n";
                        }));
                        break;
                    case "List":
                        fileManager.Object2Object(clientStream, ref o);
                        message = o as Message;
                        Invoke(new MethodInvoker(delegate
                        {
                            if (((List<FileX>)message.Result).Count > 0)
                            {
                                ListViewDisplay(ref message);
                            }
                            else
                            {
                            }
                        }));
                        break;
                    case "Rename":
                        break;
                    case "View":
                        fileManager.Object2Object(clientStream, ref o);
                        message = o as Message;
                        Invoke(new MethodInvoker(delegate
                        {
                            rtbQuestion.Text = message.Result.ToString();
                        }));
                        break;
                    case "Copy":
                        fileManager.CopyClient((List<string>)message.Argument, ref clientStream);
                        break;
                    case "Move":
                        fileManager.CopyClient((List<string>)((Message)o).Argument, ref clientStream);
                        fileManager.Delete((List<string>)((Message)o).Argument);
                        break;
                }
                client.Close();
            }
            catch (Exception e)
            { }
        }
        // UI outputs
        private void ListViewDisplay(ref Message message)
        {
            ListView lvCurrent;
            Label lbCurrent;
            if (message.Control == "left")
            {
                lvCurrent = lvLeft;
                lbCurrent = lbLeft;
            }
            else
            {
                lvCurrent = lvRight;
                lbCurrent = lbRight;
            }
            if (((List<string>)message.Argument).Count > 0)
                lbCurrent.Text = ((List<string>)message.Argument)[0];
            else
                lbCurrent.Text = "";
            lvCurrent.Items.Clear();
            foreach (FileX file in (List<FileX>)message.Result)
            {
                if (file._type == "drive")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 0));
                }
                else if (file._type == "folder")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 1));
                }
                else if (file._type == ".doc" || file._type == ".docx")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 2));
                }
                else if (file._type == ".xls" || file._type == ".xlsx")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 3));
                }
                else if (file._type == ".pdf")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 4));
                }
                else if (file._type == ".mp3" || file._type == ".wav" || file._type == ".flv" || file._type == ".mp4")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 5));
                }
                else if (file._type == ".jpg" || file._type == ".png" || file._type == ".ico")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 6));
                }
                else if (file._type == ".zip" || file._type == ".rar")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 7));
                }
                else if (file._type == ".exe" || file._type == ".com" || file._type == ".bat")
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 8));
                }
                else
                {
                    lvCurrent.Items.Add(new ListViewItem(new string[] { file._name, file._type, file._size.ToString() }, 9));
                }
            }
        }
        // UI inputs
        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load all ip addresses
            Text = "F1 List F2 Rename F3 View F4 Run F5 Copy F6 Move F7 Create F8 Delete F9 Split F10 Merge F11 Zip F12 Unzip";
            fileManager.ListAllIps();
            Invoke(new MethodInvoker(delegate
            {
                cbLeft.Items.AddRange(fileManager.Ips.ToArray());
                cbRight.Items.AddRange(fileManager.Ips.ToArray());
                lvLeft.SmallImageList = fileManager.ImageList("img");
                lvRight.SmallImageList = fileManager.ImageList("img");
                cbLeft.SelectedIndex = 0;
                cbRight.SelectedIndex = 0;
            }));
            Answer();
        }
        private void cbLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            Question(new Message(cbLeft.Text, "left", "List", GetListSources("left"), ""));
        }
        private void cbRight_SelectedIndexChanged(object sender, EventArgs e)
        {
            Question(new Message(cbRight.Text, "right", "List", GetListSources("right"), ""));
        }
        private void lvLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Question(new Message(cbLeft.Text, "left", "List", GetListSources("left"), null));
        }
        private void lvRight_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Question(new Message(cbRight.Text, "right", "List", GetListSources("right"), null));
        }
        private void lvLeft_KeyUp(object sender, KeyEventArgs e)
        {
            Message message;
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                if (types[e.KeyCode] == "Copy" || (types[e.KeyCode] == "Move"))
                {
                    message = new Message(cbRight.Text, "left", types[e.KeyCode], GetSources("left"), null);
                    if (cbLeft.Text != fileManager.SourceIp)
                    {
                        message.Argument = message;
                        message.Ip = cbLeft.Text;
                        message.Type = "Forward";
                    }
                    else
                    {
                    }

                }
                else
                {
                    message = new Message(cbLeft.Text, "left", types[e.KeyCode], GetSources("left"), null);
                }
                Question(message);
            }
            else { }
        }
        private void lvRight_KeyUp(object sender, KeyEventArgs e)
        {
            Message message;
            if (types[e.KeyCode] == "Copy" || (types[e.KeyCode] == "Move"))
            {
                message = new Message(cbLeft.Text, "right", types[e.KeyCode], GetSources("right"), null);
                if (cbRight.Text != fileManager.SourceIp)
                {
                    message.Argument = message;
                    message.Ip = cbRight.Text;
                    message.Type = "Forward";
                }
                else
                {
                }

            }
            else
            {
                message = new Message(cbRight.Text, "right", types[e.KeyCode], GetSources("right"), null);
            }
            Question(message);
        }
        private void tbAnswer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Question(new Message("", "", "Message", new List<string>() { tbAnswer.Text }, ""));
            }
            else { }
        }
    }
}
