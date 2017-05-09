using System;
using System.Transactions;
using Frame.Business.interfaces;
using Frame.MetaData;
using Frame.Models;
using Frame.Models.SysModels;

namespace Frame.Business
{
    public class SysSettingManage<T> : ISysSettingManage<T> where T : class, new()
    {
        private readonly SysSettingService<T> _sysSettingService;
        private readonly int _groupId;
        public SysSettingManage(int groupId)
        {
            _groupId = groupId;
            _sysSettingService = new SysSettingService<T>(groupId);
        }

        public DateTime ServerTime => _sysSettingService.ServerTime;

        public T GetSettingModel()
        {
            using (var db = new FrameContext())
            {
                return _sysSettingService.GetSettingModel(db);
            }
        }

        public CommandResult<T> AddOrUpdate(T model)
        {
            var properties = model.GetType().GetProperties();
            using (var db = new FrameContext())
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var property in properties)
                    {
                        var value = property.GetValue(model, null)?.ToString();
                        var entity = _sysSettingService.Find(db, a => a.ColumnName == property.Name && a.GroupId == _groupId);
                        if (entity == null)
                            _sysSettingService.Add(db, new SysSetting { ColumnName = property.Name, Value = value, GroupId = _groupId });
                        else
                        {
                            var oldEntity = _sysSettingService.Find(db, a => a.Id == entity.Id);
                            oldEntity.Value = value;
                            _sysSettingService.Update(db, oldEntity);
                        }
                    }
                    scope.Complete();
                }
            }
            return new CommandResult<T> { Data = model, ResultStatus = ResultStatus.Success };
        }
    }
}
