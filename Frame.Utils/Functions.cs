using System;
using System.Windows.Controls;

namespace Frame.Utils
{
    public static class Functions
    {
        public static void CancelTreeViewItemsSelected(TreeView treeView)
        {
            CancelTreeViewItemsSelected(treeView.Items);
        }

        public static void CancelTreeViewItemsSelected(ItemCollection items)
        {
            foreach (var i in items)
            {
                var item = i as TreeViewItem;
                if(item == null) continue;
                if (item.IsSelected)
                    item.IsSelected = false;
                if (item.HasItems)
                    CancelTreeViewItemsSelected(item.Items);
            }
        }

        /// <summary>
        /// 获取最后的异常信息
        /// </summary>
        /// <param name="parentException"></param>
        /// <returns></returns>
        public static Exception GetLastChildException(Exception parentException)
        {
            if (parentException.InnerException != null)
                return GetLastChildException(parentException.InnerException);
            return parentException;
        }

        #region 拓展方法
        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this long timestamp)
        {
            var target = DateTime.Now;
            var start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }
        #endregion
    }
}
