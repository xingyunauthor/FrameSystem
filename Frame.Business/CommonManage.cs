using System.Collections.Generic;
using System.Linq;
using Frame.Models.interfaces;

namespace Frame.Business
{
    public class CommonManage
    {
        /// <summary>
        /// 根据父级编号获取所有子集的数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="leftMenuId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static void GetAllChildrenItem<T>(List<T> list, int leftMenuId, List<T> result) where T : IMenus
        {
            var childrenList = list.Where(a => a.ParentId == leftMenuId).ToList();
            foreach (var leftMenuModel in childrenList)
            {
                result.Add(leftMenuModel);
                GetAllChildrenItem(list, leftMenuModel.Id, result);
            }
        }
    }
}
