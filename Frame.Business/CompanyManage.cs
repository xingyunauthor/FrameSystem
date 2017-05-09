using Frame.Business.interfaces;
using Frame.Models.SettingModels;

namespace Frame.Business
{
    /// <summary>
    /// 公司信息是为当前使用公司的公司信息，表里面肯定只有一条数据
    /// </summary>
    public class CompanyManage : SysSettingManage<Company>, ICompanyManage
    {
        public CompanyManage() : base(2) { }
    }
}
