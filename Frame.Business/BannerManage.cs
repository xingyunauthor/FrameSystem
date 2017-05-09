using Frame.Business.interfaces;
using Frame.Models.SettingModels;

namespace Frame.Business
{
    public class BannerManage : SysSettingManage<Banner>, IBannerManage
    {
        public BannerManage() : base(1) { }
    }
}
