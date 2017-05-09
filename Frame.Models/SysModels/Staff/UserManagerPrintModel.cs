using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Frame.Models.SysModels.Staff
{
    public class UserManagerPrintModel
    {
        public int TotalCount { get; set; }
        public ObservableCollection<StaffAllResponseModel> StaffAll { get; set; }
    }
}
