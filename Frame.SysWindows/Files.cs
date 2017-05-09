using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Frame.Proxy.Controls;
using Frame.Utils;

namespace Frame.SysWindows
{
    public class Files
    {
        public static void TreeViewAddItems(TreeView tv, string rootPath, params string[] filExtension)
        {
            var list = GetAllFiles(rootPath, filExtension, new Guid(), null);

            var rootes = list.Where(a => a.ParentId == new Guid()).ToList();
            rootes.ForEach(a =>
            {
                var root = new TreeViewImgItem
                {
                    HeaderText = a.FileName,
                    DataContext = a
                };
                AddImage(a.FileType, root);
                tv.Items.Add(root);
                TvAddItems(root, list, a.Id);
            });
        }

        /// <summary>
        /// 获取开发目录下的所有文件（包括文件夹）
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filExtension"></param>
        /// <param name="parentId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<FilePathSelTreeView> GetAllFiles(string path, string[] filExtension, Guid parentId, List<FilePathSelTreeView> list)
        {
            if (list == null) list = new List<FilePathSelTreeView>();

            var directories = System.IO.Directory.GetDirectories(path);

            // 获取所有文件夹
            foreach (var directory in directories)
            {
                var id = Guid.NewGuid();
                list.Add(new FilePathSelTreeView
                {
                    Id = id,
                    FileName = System.IO.Path.GetFileName(directory),
                    FullPath = directory,
                    ParentId = parentId,
                    FileType = FileTypeEnum.Directory
                });
                GetAllFiles(directory, filExtension, id, list);
            }

            // 获取所有 "指定后缀名" の文件
            var files = System.IO.Directory.GetFiles(path)
                .Where(a => filExtension.Contains(System.IO.Path.GetExtension(a))).ToList();
            foreach (var file in files)
            {
                list.Add(new FilePathSelTreeView
                {
                    Id = Guid.NewGuid(),
                    FileName = System.IO.Path.GetFileName(file),
                    FullPath = file,
                    ParentId = parentId,
                    FileType = FileTypeEnum.File
                });
            }
            return list;
        }

        /// <summary>
        /// 向节点添加图片
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="item"></param>
        private static void AddImage(FileTypeEnum fileType, TreeViewImgItem item)
        {
            if (fileType == FileTypeEnum.Directory)
            {
                item.DefaultImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.SysWindows;component/Resources/Open_16x16.png"));
                item.ExpandedImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.SysWindows;component/Resources/Article_16x16.png"));
            }
            else
            {
                item.DefaultImageSource = new BitmapImage(new Uri("pack://application:,,,/Frame.SysWindows;component/Resources/New_16x16.png"));
                item.ExpandedImageSource = item.DefaultImageSource;
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="treeItem"></param>
        /// <param name="list"></param>
        /// <param name="parentId"></param>
        private static void TvAddItems(TreeViewImgItem treeItem, List<FilePathSelTreeView> list, Guid parentId)
        {
            var items = list.Where(a => a.ParentId == parentId).ToList();
            foreach (var myClass in items)
            {
                var item = new TreeViewImgItem
                {
                    HeaderText = myClass.FileName,
                    DataContext = myClass
                };
                AddImage(myClass.FileType, item);
                treeItem.Items.Add(item);
                TvAddItems(item, list, myClass.Id);
            }
        }

    }


    public class FilePathSelTreeView
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件的目录
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// 父级编号 Id
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 文件的类型
        /// </summary>
        public FileTypeEnum FileType { get; set; }
    }
}
