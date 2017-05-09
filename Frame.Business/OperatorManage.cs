using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;
using Frame.Models.SysModels.Operator;
using Frame.Models.SysModels.Staff;

namespace Frame.Business
{
    public class OperatorManage : BaseManage<Staff, StaffService>, IOperatorManage
    {
        private readonly StaffRoleRelationshipsService _staffRoleRelationshipsService = new StaffRoleRelationshipsService();

        public ObservableCollection<OperatorAllResponseModel> All(string keywords)
        {
            var rowId = 0;
            var collection = new ObservableCollection<OperatorAllResponseModel>();

            using (var db = new FrameContext())
            {
                var list = BaseService.FindList(db, a => a.LogonName != null && a.LogonName != "");
                if (keywords != null && keywords.Trim().Length != 0)
                    list = list.Where(a => a.LogonName.StartsWith(keywords));

                var staffIdArray = list.Select(a => a.Id).ToArray();
                var relationships = _staffRoleRelationshipsService.FindList(db, a => staffIdArray.Any(id => id == a.StaffId))
                    .Select(a => new
                    {
                        a.StaffId,
                        a.RolesForeign.RoleName
                    })
                    .ToList();
                list.ToList().ForEach(a =>
                {
                    var roles = relationships.Where(z => z.StaffId == a.Id).Select(z => z.RoleName).ToArray();
                    collection.Add(new OperatorAllResponseModel
                    {
                        Id = a.Id,
                        RowId = ++rowId,
                        StaffName = a.Name,
                        LogonName = a.LogonName,
                        RoleName = string.Join(", ", roles)
                    });
                });
            }

            return collection;
        }

        public CommandResult<Staff> GetModel(int staffId)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "该员工信息在系统中不存在";
            else if (Exist(a => a.Id == staffId && (a.LogonName == null || a.LogonName == "")))
                result.Message = "该员工已不是操作员";
            else
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.Id == staffId);
                    result.Data = entity;
                    result.ResultStatus = ResultStatus.Success;
                }
            return result;
        }

        public CommandResult<Staff> Add(OperatorAddResponseModel paramsModel)
        {
            var result = new CommandResult<Staff>();
            if (Exist(a => a.Id == paramsModel.StaffId && a.LogonName != null && a.LogonName != ""))
                result.Message = "该员工已经是操作员";
            else if (Exist(a => a.LogonName == paramsModel.LogonName))
                result.Message = "系统中已存在该登陆名称";
            else
                using (var db = new FrameContext())
                {
                    using (var scope = new TransactionScope())
                    {
                        paramsModel.RoleIdes.ForEach(roleId =>
                        {
                            if (!_staffRoleRelationshipsService.Exist(db,
                                a => a.RoleId == roleId && a.StaffId == paramsModel.StaffId))
                            {
                                var relationship = new StaffRoleRelationships
                                {
                                    RoleId = roleId,
                                    StaffId = paramsModel.StaffId
                                };
                                _staffRoleRelationshipsService.Add(db, relationship);
                            }
                        });

                        var entity = BaseService.Find(db, a => a.Id == paramsModel.StaffId);
                        entity.LogonName = paramsModel.LogonName;
                        entity.LogonPwd = paramsModel.LogonPwd;
                        if (!BaseService.Update(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "操作员信息添加成功";
                            result.Data = entity;
                        }
                        scope.Complete();
                    }
                }
            return result;
        }

        public CommandResult<Staff> Edit(int staffId, OperatorEditResponseModel paramsModel)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "操作员信息在系统中已不存在";
            else if (Exist(a => a.Id == staffId && (a.LogonName == null || a.LogonName == "")))
                result.Message = "该员工已不是操作员";
            if (Exist(a => a.Id != staffId && a.LogonName == paramsModel.LogonName))
                result.Message = "系统中已存在该登陆名称";
            else
                using (var db = new FrameContext())
                {
                    using (var scope = new TransactionScope())
                    {
                        var oldRelationships =
                            _staffRoleRelationshipsService.FindList(db, a => a.StaffId == staffId).ToList();
                        var deletes = oldRelationships.Where(a => paramsModel.RoleIdes.All(roleId => roleId != a.RoleId)).ToList();
                        var addes = paramsModel.RoleIdes.Where(a => oldRelationships.All(z => z.RoleId != a)).ToList();

                        deletes.ForEach(relationship =>
                        {
                            _staffRoleRelationshipsService.Delete(db, relationship);
                        });
                        addes.ForEach(roleId =>
                        {
                            var model = new StaffRoleRelationships
                            {
                                RoleId = roleId,
                                StaffId = staffId
                            };
                            _staffRoleRelationshipsService.Add(db, model);
                        });

                        var entity = BaseService.Find(db, a => a.Id == staffId);
                        entity.LogonName = paramsModel.LogonName;
                        entity.LogonPwd = paramsModel.LogonPwd;
                        if (!BaseService.Update(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "操作员信息修改成功";
                            result.Data = entity;
                        }

                        scope.Complete();
                    }
                }
            return result;
        }

        public CommandResult<Staff> EditPwd(int staffId, string oldLogonPwd, string newLogonPwd)
        {
            var result = new CommandResult<Staff>();

            var model = GetModel(staffId);
            if (model.ResultStatus == ResultStatus.Error)
                return model;

            using (var db = new FrameContext())
            {
                var entity = model.Data;
                if (entity.LogonPwd != oldLogonPwd)
                    result.Message = "输入的原密码不正确";
                else
                {
                    entity.LogonPwd = newLogonPwd;
                    if (!BaseService.Update(db, entity))
                        result.Message = "未知原因";
                    else
                    {
                        result.ResultStatus = ResultStatus.Success;
                        result.Message = "修改密码成功";
                        result.Data = entity;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 删除操作员信息
        /// </summary>
        /// <returns></returns>
        public CommandResult<Staff> Delete(int staffId)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.Id == staffId))
                result.Message = "操作员信息在系统中已不存在";
            else if (Exist(a => a.Id == staffId && (a.LogonName == null || a.LogonName == "")))
                result.Message = "该员工已不是操作员";
            else
                using (var db = new FrameContext())
                {
                    using (var scope = new TransactionScope())
                    {
                        var relationships = _staffRoleRelationshipsService.FindList(db, a => a.StaffId == staffId).ToList();
                        foreach (var relationship in relationships)
                            _staffRoleRelationshipsService.Delete(db, relationship);

                        var entity = BaseService.Find(db, a => a.Id == staffId);
                        entity.LogonId = null;
                        entity.LogonName = null;
                        entity.LogonPwd = null;
                        if (!BaseService.Update(db, entity))
                            result.Message = "未知原因";
                        else
                        {
                            result.ResultStatus = ResultStatus.Success;
                            result.Message = "操作员删除成功";
                            result.Data = entity;
                        }

                        scope.Complete();
                    }
                }
            return result;
        }

        public CommandResult<Staff> Login(OperatorLogonRequestModel paramsModel)
        {
            var result = new CommandResult<Staff>();
            if (!Exist(a => a.LogonName == paramsModel.LogonName))
                result.Message = "请检查您输入的账号或密码是否正确";
            else
            {
                using (var db = new FrameContext())
                {
                    var entity = BaseService.Find(db, a => a.LogonName == paramsModel.LogonName);
                    if (entity.LogonPwd != paramsModel.LogonPwd)
                        result.Message = "请检查您输入的账号或密码是否正确";
                    else
                    {
                        var existRole = _staffRoleRelationshipsService.Exist(db,
                            a => a.RoleId == paramsModel.RoleId && a.StaffId == entity.Id);
                        if (!existRole)
                            result.Message = "该账号不具有该角色";
                        else
                        {
                            result.Message = "登陆成功";
                            result.ResultStatus = ResultStatus.Success;
                            result.Data = entity;
                        }
                    }
                }
            }
            return result;
        }
    }
}
