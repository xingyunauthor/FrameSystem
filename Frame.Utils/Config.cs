using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Frame.Utils
{
    public class Config
    {
        /// <summary>
        /// LeftMenu 菜单修改时添加的根目录选择项
        /// </summary>
        public static string RootDisplayName { get; } = "root";

        public static string IcoBasePath { get; set; } = $"{AppDomain.CurrentDomain.BaseDirectory}images\\";

        /// <summary>
        /// 二次开发存放的目录
        /// </summary>
        public static string DevPlatformPath { get; } = $"{AppDomain.CurrentDomain.BaseDirectory}platform\\";

        /// <summary>
        /// 配置文件存放目录
        /// </summary>
        public static string ConfigDirectory { get; } = "config\\";

        public static string SplashScreenDataFile { get; set; } = "splashscreendata.json";
    }
}
