using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.TopMenus;

namespace Frame.Business
{
    public class TopMenusManage : BaseManage<TopMenus, TopMenusService>, ITopMenusManage
    {
        public TopMenus GetTopMenuById(int topMenuId)
        {
            using (var db = new FrameContext())
            {
                return BaseService.Find(db, a => a.Id == topMenuId);
            }
        }

        public List<TopMenus> GetAll()
        {
            using (var db = new FrameContext())
            {
                return BaseService.GetAll(db, true, a => a.Sort).ToList();
            }
        }

        public ObservableCollection<AllTopMenusHierarchicalDataModel> GetAllMenusHierarchicalData()
        {
            var list = GetAll();
            var nodes = new ObservableCollection<AllTopMenusHierarchicalDataModel>();
            GenerateHierarchicalData(list, 0, nodes);

            return nodes;
        }

        public CommandResult<TopMenus> Update(int topMenuId, long timestamp, TopMenusUpdateRequestModel requestModel)
        {
            var result = new CommandResult<TopMenus>();
            using (var db = new FrameContext())
            {
                if (!BaseService.Exist(db, a => a.Id == topMenuId))
                    result.Message = "该数据在系统中已不存在，请检查后再试……";
                else if (!BaseService.Exist(db, a => a.Id == topMenuId && a.Timestamp == timestamp))
                    result.Message = "该节点已在别处被更改过！";
                else if (requestModel.MenuId != null && requestModel.MenuId.Trim().Length != 0 && BaseService.Exist(db, a => a.Id != topMenuId && a.MenuId == requestModel.MenuId))
                    result.Message = $"菜单唯一标识 MenuId “{requestModel.MenuId}”在系统中已存在！";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == topMenuId);
                    entity.DisplayName = requestModel.DisplayName;
                    entity.DllPath = requestModel.DllPath;
                    entity.EntryFunction = requestModel.EntryFunction;
                    entity.Ico = requestModel.Ico;
                    entity.MenuId = requestModel.MenuId;
                    entity.ParentId = requestModel.ParentId;
                    entity.Timestamp = requestModel.Timestamp;

                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "更新数据成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<TopMenus> UpdateSort(int topMenuId, long timestamp, TopMenusUpdateSortRequestModel requestModel)
        {
            var result = new CommandResult<TopMenus>();
            if (!Exist(a => a.Id == topMenuId))
                result.Message = $"节点名称“{requestModel.DisplayName}”在系统中已不存在";
            else if (!Exist(a => a.Id == topMenuId && a.Timestamp == timestamp))
                result.Message = $"节点名称为“{requestModel.DisplayName}”的节点排序已在别处被修改";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == topMenuId);
                    entity.Sort = requestModel.Sort;
                    entity.Timestamp = requestModel.Timestamp;
                    if (!BaseService.Update(db, entity))
                        result.Message = "系统出现未知原因，节点排序更新失败！";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "节点排序更新成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<TopMenus> Add(TopMenusAddRequestModel requestModel)
        {
            var result = new CommandResult<TopMenus>();
            using (var db = new FrameContext())
            {
                if (requestModel.MenuId != null && requestModel.MenuId.Trim().Length != 0 && BaseService.Exist(db, a => a.MenuId == requestModel.MenuId))
                    result.Message = $"菜单唯一标识 MenuId “{requestModel.MenuId}”,在系统中已存在，请重新输入……";
                else
                {
                    var entity = new TopMenus
                    {
                        DisplayName = requestModel.DisplayName,
                        DllPath = requestModel.DllPath,
                        EntryFunction = requestModel.EntryFunction,
                        MenuId = requestModel.MenuId,
                        ParentId = requestModel.ParentId,
                        Sort = requestModel.Sort,
                        Timestamp = requestModel.Timestamp
                    };
                    if (!BaseService.Add(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "新的节点信息添加成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 删除该节点以及该节点一下的所有数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Deletes(Expression<Func<TopMenus, bool>> whereLambda)
        {
            using (var db = new FrameContext())
            {
                var list = GetAll();
                var entity = BaseService.Find(db, whereLambda);
                var deleteList = new List<TopMenus>();
                CommonManage.GetAllChildrenItem(list, entity.Id, deleteList);

                using (var scope = new TransactionScope())
                {
                    deleteList.ForEach(a =>
                    {
                        BaseService.Delete(db, a);
                    });
                    BaseService.Delete(db, entity);

                    scope.Complete();
                }
            }
            return true;
        }


        private static void GenerateHierarchicalData(List<TopMenus> list, int parentId, ObservableCollection<AllTopMenusHierarchicalDataModel> nodes)
        {
            list.Where(a=>  a.ParentId == parentId).ToList().ForEach(a =>
            {
                var node = new AllTopMenusHierarchicalDataModel
                {
                    Id = a.Id,
                    DisplayName = a.DisplayName,
                    DllPath = a.DllPath,
                    EntryFunction = a.EntryFunction,
                    MenuId = a.MenuId,
                    Nodes = new ObservableCollection<AllTopMenusHierarchicalDataModel>()
                };
                nodes.Add(node);
                GenerateHierarchicalData(list, a.Id, node.Nodes);
            });
        }
    }
}
