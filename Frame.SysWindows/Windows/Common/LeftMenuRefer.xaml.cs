using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Mvvm.Native;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;
using Frame.Models.interfaces;
using Frame.SysWindows.Controls;
using Frame.Utils;

namespace Frame.SysWindows.Windows.Common
{
    /// <summary>
    /// LeftMenuRefer.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenuRefer : INotifyPropertyChanged
    {
        private readonly ILeftMenusManage _leftMenusManage = new LeftMenusManage();
        private readonly INavBarGroupsManage _navBarGroupsManage = new NavBarGroupsManage();
        public event PropertyChangedEventHandler PropertyChanged;
        
        private LeftMenus _selectedLeftMenus;
        private int _childLeftMenuId;
        /// <summary>
        /// 
        /// </summary>
        private readonly Action<LeftMenus> _callback;

        private List<LeftMenus> LeftMenusList { get; }

        public LeftMenuRefer(int leftMenuId, int childLeftMenuId, Action<LeftMenus> callback)
        {
            InitializeComponent();
            _childLeftMenuId = childLeftMenuId;
            _callback = callback;

            DataContext = this;
            var allLeftMenus = _leftMenusManage.GetAll();
            var leftMenu = new LeftMenu(allLeftMenus);
            leftMenu.NavBarControlMain.Groups.ForEach(a =>
            {
                var treeViewItems = ((CTreeView)a.Items.First()).Items;
                var treeViewItem = new TreeViewItem
                {
                    DataContext = new LeftMenus
                    {
                        DisplayName = Config.RootDisplayName,
                        NavBarGroupId = ((NavBarGroups)a.DataContext).Id
                    },
                    Header = Config.RootDisplayName
                };
                treeViewItem.Selected += LeftMenu_RootItemSelected;
                treeViewItems.Insert(0, treeViewItem);

                CollapsedSelectedItem(treeViewItems, childLeftMenuId);
            });
            leftMenu.ChildItemSelected += LeftMenuTreeViewItem_Selected;
            leftMenu.ParentItemSelected += LeftMenuTreeViewItem_Selected;
            GridLeftMenu.Children.Add(leftMenu);

            LeftMenusList = leftMenu.LeftMenusList;
            SelectedPath = GetSelectedPath(LeftMenusList, leftMenuId, "");
        }

        /// <summary>
        /// 选中根节点时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenu_RootItemSelected(object sender, RoutedEventArgs e)
        {
            var treeViewItem = (TreeViewItem)sender;
            _selectedLeftMenus = (LeftMenus)treeViewItem.DataContext;
            SelectedPath = Config.RootDisplayName;
            e.Handled = true;
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftMenuTreeViewItem_Selected(TreeViewItem sender, RoutedEventArgs e)
        {
            var navBarGroupName = "";
            _selectedLeftMenus = (LeftMenus)sender.DataContext;
            var leftMenuId = _selectedLeftMenus.Id;
            var navBarGroupId = _leftMenusManage.GetLeftMenuById(leftMenuId)?.NavBarGroupId;
            if (navBarGroupId != null)
                navBarGroupName = _navBarGroupsManage.GetNavBarGroupById((int)navBarGroupId)?.Name;

            SelectedPath = $"{navBarGroupName}\\{GetSelectedPath(LeftMenusList, leftMenuId, "")}";
            e.Handled = true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            _callback?.Invoke(_selectedLeftMenus);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        /// <summary>
        /// 隐藏被参照的 TreeViewItem
        /// </summary>
        /// <param name="items"></param>
        /// <param name="leftMenuId"></param>
        private void CollapsedSelectedItem(ItemCollection items, int leftMenuId)
        {
            foreach (var i in items)
            {
                var item = (TreeViewItem)i;
                var leftMenusModel = (LeftMenus)item.DataContext;
                if(leftMenusModel.Id == leftMenuId)
                {
                    items.Remove(item);
                    break;
                }

                if (item.HasItems)
                    CollapsedSelectedItem(item.Items, leftMenuId);
            }
        }


        /// <summary>
        /// 获取参照窗体 LeftMenuRefer.xaml 选择的父级的目录
        /// </summary>
        /// <param name="leftMenus"></param>
        /// <param name="currentLeftMenuId"></param>
        /// <param name="selectedPath"></param>
        /// <returns></returns>
        public static string GetSelectedPath<T>(List<T> leftMenus, int currentLeftMenuId, string selectedPath) where T : IMenus
        {
            if (!leftMenus.Exists(a => a.Id == currentLeftMenuId)) return Config.RootDisplayName;
            var leftMenu = leftMenus.First(a => a.Id == currentLeftMenuId);
            var parentId = leftMenu.ParentId;
            selectedPath = string.IsNullOrEmpty(selectedPath) ? leftMenu.DisplayName : $"{leftMenu.DisplayName}\\{selectedPath}";
            if (parentId != 0)
                return GetSelectedPath(leftMenus, parentId, selectedPath);
            return selectedPath;
        }


        #region MVVM Models
        private string _selectedPath;

        /// <summary>
        /// 已选择的路径
        /// </summary>
        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPath)));
            }
        }
        #endregion
    }
}
