using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Launcher
{
    /// <summary>
    /// 控制器类
    /// </summary>
    class Control
    {
        //用户搜索文件夹路径列表
        private List<string> SearchPathList;
        //搜索到的可启动项
        private Dictionary<string, string> LaunchList;
        public Control()
        {
            SearchPathList = new List<string>();
            LaunchList = new Dictionary<string, string>();
            //读取用户定义搜索文件列表
            string res = ConfigurationManager.AppSettings["SearchPath"];
            SearchPathList = JsonConvert.DeserializeObject<List<string>>(res);

            FlashLaunchList();
        }
        /// <summary>
        /// 刷新文件列表
        /// </summary>
        public void FlashLaunchList()
        {
            foreach (var i in SearchPathList)
            { ListFileInFolder(i); }
        }
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
                    { LaunchList.Add(fileInfo.Name, fileInfo.FullName); }
                    else
                    { ListFileInFolder(j.FullName); }
                }
            }
            catch(Exception e)
            {

            }
        }
        //TODO:记得删除
        public string test()
        {
            return ConfigurationManager.AppSettings["SearchPath"];
        }
    }
}
