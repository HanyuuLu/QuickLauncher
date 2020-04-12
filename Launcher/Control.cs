﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

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
        public Dictionary<string, string> LaunchList { get; }
        public Dictionary<string, string> SearchList { get; }
        private const string CONFIG_PATH = "config.json";

        private Control()
        {
            SearchPathList = new List<string>();
            SearchList = new Dictionary<string, string>();
            LaunchList = new Dictionary<string, string>();
            Name = "Empty";
            //读取用户定义搜索文件列表
            try
            {
                if (!File.Exists(CONFIG_PATH))
                {
                    File.Create(CONFIG_PATH);
                    File.WriteAllText(CONFIG_PATH, JsonSerializer.Serialize(SearchPathList));
                }
                else
                {
                    SearchPathList = JsonSerializer.Deserialize<List<String>>(File.ReadAllText("config.json"));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                    { LaunchList.Add(fileInfo.FullName, fileInfo.Name); }
                    else
                    { ListFileInFolder(j.FullName); }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async void search(string src=null)
        {
            src = src ?? Name;
            SearchList.Clear();
            foreach (var (key, value) in LaunchList)
            {
                if (value.Contains(src))
                {
                    SearchList.Add(key, value);
                }
            }
            
        }
    }
}
