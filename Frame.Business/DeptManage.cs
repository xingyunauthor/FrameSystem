using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Dept;

namespace Frame.Business
{
    public class DeptManage : BaseManage<Dept, DeptService>, IDeptManage
    {
        private readonly StaffService _staffService = new StaffService();
        public ObservableCollection<DeptAllResponseModel> All()
        {
            List<Dept> list;
            var nodes = new ObservableCollection<DeptAllResponseModel>();
            using (var db = new FrameContext())
            {
                list = BaseService.GetAll(db, true, a => a.Id).ToList();
            }

            AddNodes(list, 0, nodes);
            return nodes;
        }

        public CommandResult<Dept> GetModel(int deptId)
        {
            var result = new CommandResult<Dept>();
            using (var db = new FrameContext())
            {
                if (!Exist(a => a.Id == deptId))
                    result.Message = "部门不存在";
                else
                {
                    result.Data = BaseService.Find(db, a => a.Id == deptId);
                    result.ResultStatus = ResultStatus.Success;
                }
            }
            return result;
        }

        private void AddNodes(List<Dept> list, int parentId, ObservableCollection<DeptAllResponseModel> nodes)
        {
            list.Where(a => a.PId == parentId).OrderBy(a => a.Id).ToList().ForEach(a =>
            {
                var node = new DeptAllResponseModel
                {
                    DeptId = a.Id,
                    DeptName = a.Name,
                    ParentId = a.PId,
                    Nodes = new ObservableCollection<DeptAllResponseModel>()
                };
                nodes.Add(node);
                AddNodes(list, a.Id, node.Nodes);
            });
        }

        public CommandResult<Dept> Add(DeptAddRequestModel requestModel){
            var result = new CommandResult<Dept>();
            using (var db = new FrameContext())
            {
                if (Exist(a => a.Name == requestModel.DeptName))
                    result.Message = "该部门名称在系统中已存在";
                else if (_staffService.Exist(db, a => a.DeptId == requestModel.ParentId))
                {
                    var staff = _staffService.Find(db, a => a.DeptId == requestModel.ParentId);
                    result.Message = $"该部门信息已被分配到了员工“{staff.Name}”上，请先取消";
                }
                else
                {
                    var entity = new Dept
                    {
                        Name = requestModel.DeptName,
                        PId = requestModel.ParentId
                    };
                    if (!BaseService.Add(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "部门新增成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<Dept> Update(int id, DeptUpdateRequestModel requestModel)
        {
            var result = new CommandResult<Dept>();
            if (!Exist(a => a.Id == id))
                result.Message = "该部门信息在系统中已不存在";
            else if (Exist(a => a.Id != id && a.Name == requestModel.DeptName))
                result.Message = $"部门名称“{requestModel.DeptName}”在系统中已存在";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == id);
                    entity.Name = requestModel.DeptName;
                    entity.PId = requestModel.ParentId;
                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "部门信息更新成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<Dept> Delete(int id)
        {
            var result = new CommandResult<Dept>();
            using (var db = new FrameContext())
            {
                if (!Exist(a => a.Id == id))
                    result.Message = "该部门信息在系统中已不存在";
                else if (Exist(a => a.PId == id))
                    result.Message = "该部门分类下还有子部门";
                else if (_staffService.Exist(db, a => a.DeptId == id))
                    result.Message = "还有员工属于该部门";
                else
                {
                    var entity = BaseService.Find(db, a => a.Id == id);
                    if (!BaseService.Delete(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "部门信息删除成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }
    }
}
