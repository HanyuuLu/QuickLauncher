using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// 控制器类
    /// 主要的控制逻辑
    /// </summary>
    class Control
    {
        //单例
        public static Control Instance { get; } = new Control();
        public string Name { get; set; }
        //用户搜索文件夹路径列表
        public List<string> SearchPathList { get; }
        //搜索到的可启动项
        public Dictionary<string, string> CompleteDict { get; }
        public List<FileInfoItem> SearchList { get; private set; }
        private const string CONFIG_PATH = "config.json";
        private Control()
        {
            SearchPathList = new List<string>();
            CompleteDict = new Dictionary<string, string>();
            SearchList = new List<FileInfoItem>();
            Name = "";
            //读取用户定义搜索文件列表
            try
            {
                if (!File.Exists(CONFIG_PATH))
                {
                    File.Create(CONFIG_PATH);
                    File.WriteAllText(CONFIG_PATH, JsonSerializer.Serialize(SearchPathList));
                }
                else
                { SearchPathList = JsonSerializer.Deserialize<List<String>>(File.ReadAllText("config.json")); }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            FlashLaunchList();
        }
        /// <summary>
        /// 刷新文件列表
        /// </summary>
        public void FlashLaunchList()
        { foreach (var i in SearchPathList) { ListFileInFolder(i); } }
        /// <summary>
        /// 将当前路径下的文件添加进启动列表
        /// </summary>
        /// <param name="directoryName"></param>
        private void ListFileInFolder(string directoryName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(directoryName);
                FileSystemInfo[] fileList = dir.GetFileSystemInfos();
                foreach (var j in fileList)
                {
                    FileInfo fileInfo = j as FileInfo;
                    if (fileInfo != null)
                    { CompleteDict.Add(fileInfo.FullName, fileInfo.Name); }
                    else
                    { ListFileInFolder(j.FullName); }
                }
            }
            catch (Exception e)
            { MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public void search(string src = null)
        {
            src = src ?? Name;
            src = src.TrimStart().TrimEnd();
            if (src!="")
            {
                //await1 Task.Run(() => { 
                IEnumerable<FileInfoItem> res =
                    from item in this.CompleteDict
                    where item.Value.Contains(src)
                    select new FileInfoItem(item.Key);
                SearchList = res.Take(10).ToList();
                //});
            }else
            {
                SearchList.Clear();
            }
        }
        public static void bindRes(Object src)
        {
            src = Control.Instance.SearchList;
        }
    }
    class FileInfoItem
    {
        public ImageSource icon { get; private set; }
        public string FullFileName { get; private set; }
        public string FileName
        {
            get
            { return Path.GetFileNameWithoutExtension(FullFileName); }
        }
        public FileInfoItem(string fullName)
        {
            try
            {
                FullFileName = fullName;
                icon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    Icon.ExtractAssociatedIcon(fullName).Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                    ) ;
            }
            catch
            {  }
        }
    }
}
