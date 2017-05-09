using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Frame.Models;
using Frame.Proxy.Controls;

namespace Frame.SysWindows.Controls
{
    public class CTreeView : TreeView
    {
        public event Action<TreeViewItem, RoutedEventArgs> ChildItemSelected;
        public event Action<TreeViewItem, RoutedEventArgs> ParentItemSelected;
        public CTreeView(int navBarGroupId, List<LeftMenus> leftMenusList)
        {
            var leftMenuList = leftMenusList.Where(b => b.NavBarGroupId == navBarGroupId && b.ParentId == 0).OrderBy(a => a.Sort).ToList();
            leftMenuList.ForEach(b => {
                var item = new TreeViewImgItem { HeaderText = b.DisplayName, DataContext = b };
                
                Items.Add(item);
                ForeachTreeViewItem(leftMenusList, item, b.Id);
            });
        }

        /// <summary>
        /// 子节点递归显示
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="item"></param>
        /// <param name="parentId"></param>
        private void ForeachTreeViewItem(List<LeftMenus> menuList, TreeViewImgItem item, int parentId)
        {
            var list = menuList.Where(a => a.ParentId == parentId).OrderBy(a => a.Sort).ToList();
            InitSelectedAndIco(item, list.Count);

            list.ForEach(a => {
                var newItem = new TreeViewImgItem { HeaderText = a.DisplayName, DataContext = a};
                item.Items.Add(newItem);
                ForeachTreeViewItem(menuList, newItem, a.Id);
            });
        }

        /// <summary>
        /// 根据原有的 TreeViewItem 生成一个新的 TreeViewItem，其中也包括了事件
        /// </summary>
        /// <param name="oldItem"></param>
        /// <returns></returns>
        public TreeViewImgItem GenerateTreeViewItem(TreeViewImgItem oldItem)
        {
            var leftMenuModel = (LeftMenus)oldItem.DataContext;
            var item = new TreeViewImgItem
            {
                HeaderText = leftMenuModel.DisplayName,
                DataContext = leftMenuModel
            };
            for (var i = 0; i < oldItem.Items.Count; i++)
            {
                var old = oldItem.Items[i];
                oldItem.Items.Remove(old);
                item.Items.Add(old);

                --i;
            }
            item.IsExpanded = oldItem.IsExpanded;

            InitSelectedAndIco(item, item.Items.Count);

            return item;
        }

        public TreeViewImgItem GenerateTreeViewItem(LeftMenus leftMenusModel)
        {
            var treeViewItem = new TreeViewImgItem
            {
                HeaderText = leftMenusModel.DisplayName,
                DataContext = leftMenusModel
            };

            InitSelectedAndIco(treeViewItem, 0);

            return treeViewItem;
        }

        public void InitSelectedAndIco(TreeViewImgItem item, int itemsCount)
        {
            if (itemsCount == 0)
            {
                SetChildItemIco(item);
                item.Selected += (sender, e) => ChildItemSelected?.Invoke((TreeViewItem)sender, e);
            }
            else
            {
                SetParentItemIco(item);
                item.Selected += (sender, e) => ParentItemSelected?.Invoke((TreeViewItem)sender, e);
            }
        }

        /// <summary>
        /// 设置含有Items的Item的ICO
        /// </summary>
        /// <param name="item"></param>
        public static void SetParentItemIco(TreeViewImgItem item)
        {
            item.DefaultImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.SysWindows;component/Resources/Open_16x16.png"));
            item.ExpandedImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.SysWindows;component/Resources/Open2_16x16.png"));
        }

        /// <summary>
        /// 设置不含有Items的Item的ICO
        /// </summary>
        /// <param name="item"></param>
        public static void SetChildItemIco(TreeViewImgItem item)
        {
            item.DefaultImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.Proxy;component/Resources/TreeViewItem.gif"));
            item.ExpandedImageSource = item.DefaultImageSource;
        }
    }
}
