using System;
using System.Collections.ObjectModel;
using System.Linq;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Staff;

namespace Frame.Business
{
    public class StaffManage : BaseManage<Staff, StaffService>, IStaffManage
    {
        private readonly StaffService _staffService = new StaffService();
        private readonly DeptService _deptService = new DeptService();
        public ObservableCollection<StaffAllResponseModel> All()
        {
            var collection = new ObservableCollection<StaffAllResponseModel>();
            var rowId = 0;
            using (var db = new FrameContext())
            {
                var list = _staffService.GetAll(db, false, a => a.Id)
                    .Select(a => new { a, a.DeptForeign }).ToList();
                list.ForEach(staff =>
                {
                    collection.Add(GenerateStaffAllResponseModel(staff.a, staff.DeptForeign, ++rowId));
                });
            }
            return collection;
        }

        public string GetNewCode()
        {
            var val = $"YG{ServerTime.ToString("yyyyMMdd")}";
            using (var db = new FrameContext())
            {
                var first = BaseService.FindList(db, a => a.Code.StartsWith(val)).OrderByDescending(a => a.Code).FirstOrDefault();
                if (first == null)
                    return $"{val}0001";
                else
                {
                    var startIndex = val.Length;
                    var index = first.Code.Substring(startIndex, first.Code.Length - startIndex);
                    var num = Convert.ToInt32(index) + 1;
                    return $"{val}{num:0000}";
                }
            }
        }

        public void StaffSearch(ObservableCollection<StaffAllResponseModel> staffs, string keywords)
        {
            staffs.Clear();
            var rowId = 0;
            using (var db = new FrameContext())
            {
                var list = _staffService.GetAll(db, false, a => a.Id);
                if (keywords != null && keywords.Trim().Length > 0)
                    list = list.Where(a => a.Code.StartsWith(keywords)
                            || a.Name.StartsWith(keywords)
                            || a.Tel.StartsWith(keywords)
                            || a.Add.StartsWith(keywords)
                            || a.Remark.StartsWith(keywords));
                var result = list.Select(a => new { a, a.DeptForeign }).ToList();
                result.ForEach(staff =>
                {
                    staffs.Add(GenerateStaffAllResponseModel(staff.a, staff.DeptForeign, ++rowId));
                });
            }
        }

        public void StaffReferSearch(ObservableCollection<StaffAllResponseModel> staffs, string keywords)
        {
            staffs.Clear();
            var rowId = 0;
            using (var db = new FrameContext())
            {
                var list = _staffService.GetAll(db, false, a => a.Id);
                if (keywords != null && keywords.Trim().Length > 0)
                    list = list.Where(a => a.Code.StartsWith(keywords)
                            || a.Name.StartsWith(keywords)
                            || a.Tel.StartsWith(keywords)
                            || a.Add.StartsWith(keywords)
                            || a.Remark.StartsWith(keywords));
                var result = list.Where(a => a.LogonName == null || a.LogonName == "")
                                .Select(a => new { a, a.DeptForeign }).ToList();
                result.ForEach(staff =>
                {
                    staffs.Add(GenerateStaffAllResponseModel(staff.a, staff.DeptForeign, ++rowId));
                });
            }
        }

        private StaffAllResponseModel GenerateStaffAllResponseModel(Staff staffModel, Dept deptModel, int rowId)
        {
            var a = staffModel;
            return new StaffAllResponseModel
            {
                RowId = rowId,
                StaffId = a.Id,
                Code = a.Code,
                DeptId = a.DeptId,
                DeptName = deptModel.Name,
                StaffName = a.Name,
                Sex = a.Sex == 1 ? "男" : "女",
                Birthday = a.Birth.ToString("yyyy-MM-dd"),
                InTime = a.InTime.ToString("yyyy-MM-dd"),
                Telephone = a.Tel,
                Address = a.Add,
                State = a.State,
                StateName = a.State ? "启用" : "未启用",
                Remark = a.Remark
            };
        }

        public CommandResult<Staff> GetModel(int staffId)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "该员工信息在系统中已不存在";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = _staffService.FindList(db, a => a.Id == staffId)
                        .Select(a => new {
                            Staff = a,
                            Dept = a.DeptForeign
                        }).Single();
                    entity.Staff.DeptForeign = entity.Dept;
                    result.ResultStatus = ResultStatus.Success;
                    result.Data = entity.Staff;
                }
            }

            return result;
        }

        public CommandResult<Staff> Add(StaffAddRequestModel requestModel)
        {
            var result = new CommandResult<Staff>();
            if (Exist(a => a.Code == requestModel.Code))
                result.Message = $"该员工编号“{requestModel.Code}”在系统中已存在";
            else if (Exist(a => a.Name == requestModel.Name))
                result.Message = $"该姓名“{requestModel.Name}”在系统中已存在";
            else
            {
                var entity = new Staff
                {
                    Code = requestModel.Code,
                    DeptId = requestModel.DeptId,
                    Name = requestModel.Name,
                    Sex = requestModel.Sex,
                    Birth = requestModel.Birth,
                    InTime = requestModel.InTime,
                    Tel = requestModel.Tel,
                    Add = requestModel.Add,
                    State = requestModel.State,
                    Oper = requestModel.Oper,
                    Supper = false
                };
                using (var db = new FrameContext())
                {
                    if (!BaseService.Add(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "员工信息添加成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<Staff> Update(int staffId, StaffUpdateRequestModel requestModel)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "该员工信息在系统中已不存在";
            else if (Exist(a => a.Id != staffId && a.Name == requestModel.Name))
                result.Message = $"该姓名“{requestModel.Name}”在系统中已存在";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == staffId);
                    entity.DeptId = requestModel.DeptId;
                    entity.Name = requestModel.Name;
                    entity.Sex = requestModel.Sex;
                    entity.Birth = requestModel.Birth;
                    entity.Tel = requestModel.Tel;
                    entity.Add = requestModel.Add;
                    entity.State = requestModel.State;
                    entity.InTime = requestModel.InTime;
                    entity.Remark = requestModel.Remark;
                    entity.Oper = requestModel.Oper;
                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "员工信息更新成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        public CommandResult<Staff> Delete(int staffId)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "该员工信息在系统中已不存在";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == staffId);
                    if (!BaseService.Delete(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "员工信息删除成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }
    }
}
