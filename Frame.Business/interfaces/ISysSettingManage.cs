using Frame.Models.SysModels;

namespace Frame.Business.interfaces
{
    public interface ISysSettingManage<T> where T : class, new()
    {
        /// <summary>
        /// 获取配置信息详细
        /// </summary>
        /// <returns></returns>
        T GetSettingModel();

        /// <summary>
        /// 添加或更新配置信息
        /// </summary>
        /// <param name="entity"></param>
        CommandResult<T> AddOrUpdate(T entity);
    }
}
