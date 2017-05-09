using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.TopMenus;

namespace Frame.Business.interfaces
{
    public interface ITopMenusManage : IBaseManage<TopMenus>
    {
        TopMenus GetTopMenuById(int topMenuId);
        List<TopMenus> GetAll();
        ObservableCollection<AllTopMenusHierarchicalDataModel> GetAllMenusHierarchicalData();
        CommandResult<TopMenus> Update(int topMenuId, long timestamp, TopMenusUpdateRequestModel requestModel);
        CommandResult<TopMenus> UpdateSort(int topMenuId, long timestamp, TopMenusUpdateSortRequestModel requestModel);
        CommandResult<TopMenus> Add(TopMenusAddRequestModel requestModel);
        bool Deletes(Expression<Func<TopMenus, bool>> whereLambda);
    }
}
