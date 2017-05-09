using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.NavBar;
using Frame.Business;
using Frame.Business.interfaces;
using Frame.Models;

namespace Frame.SysWindows.Controls
{
    /// <summary>
    /// LeftMenu.xaml 的交互逻辑
    /// </summary>
    public partial class LeftMenu
    {
        private readonly INavBarGroupsManage _barGroupsManage = new NavBarGroupsManage();

        public event Action<TreeViewItem, RoutedEventArgs> ChildItemSelected;
        public event Action<TreeViewItem, RoutedEventArgs> ParentItemSelected;

        public event Action<object, EventArgs> NavBarGroupActivate;

        public List<LeftMenus> LeftMenusList { get; set; }
        public LeftMenu(List<LeftMenus> leftMenusList)
        {
            InitializeComponent();
            LeftMenusList = leftMenusList;
            Init();
        }

        private void Init()
        {
            var navBarGroupsList = _barGroupsManage.GetAll().ToList();
            navBarGroupsList.ForEach(a => {
                var treeView = new CTreeView(a.Id, LeftMenusList) { BorderThickness = new Thickness(0) };
                treeView.ChildItemSelected += (sender, e) => ChildItemSelected?.Invoke(sender, e);
                treeView.ParentItemSelected += (sender, e) => ParentItemSelected?.Invoke(sender, e);
                var navBarGroup = new NavBarGroup
                {
                    DisplayMode = DisplayMode.ImageAndText,
                    Header = a.Name,
                    ImageSource = GetNavBarGroupImageSource(a.Ico),
                    DataContext = a
                };
                navBarGroup.Activate += (sender, e) => NavBarGroupActivate?.Invoke(sender, e);
                navBarGroup.Items.Add(treeView);
                NavBarControlMain.Groups.Add(navBarGroup);
            });
        }

        private ImageSource GetNavBarGroupImageSource(string icoRelativePath)
        {
            if (icoRelativePath == null || icoRelativePath.Trim().Length == 0)
                return null;
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, icoRelativePath);
            return !System.IO.File.Exists(path) 
                ? null 
                : new BitmapImage(new Uri(path));
        }

        public NavBarGroup GenerateNavBarGroup(NavBarGroups model)
        {
            var nav = new NavBarGroup
            {
                DisplayMode = DisplayMode.ImageAndText,
                Header = model.Name,
                ImageSource = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{model.Ico}")),
                DataContext = model
            };
            nav.Activate += (sender, e) => NavBarGroupActivate?.Invoke(sender, e);
            return nav;
        }

        /// <summary>
        /// 添加 NavBarGroup
        /// </summary>
        /// <param name="model"></param>
        public void AddNavBarGroup(NavBarGroups model)
        {
            var nav = GenerateNavBarGroup(model);
            NavBarControlMain.Groups.Add(nav);
        }

        /// <summary>
        /// 重新设置 ActiveGroup
        /// </summary>
        /// <param name="model"></param>
        public void ResetActiveGroup(NavBarGroups model)
        {
            var activeGroup = NavBarControlMain.ActiveGroup;
            activeGroup.Header = model.Name;
            activeGroup.ImageSource = new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{model.Ico}"));
            activeGroup.DataContext = model;
        }
    }
}
