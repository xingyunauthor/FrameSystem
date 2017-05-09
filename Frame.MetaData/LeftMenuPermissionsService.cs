using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Frame.Models;
using MySql.Data.MySqlClient;

namespace Frame.MetaData
{
    public class LeftMenuPermissionsService : BaseService<LeftMenuPermissions>
    {
        private readonly PermissionsService _permissionsService = new PermissionsService();

        public DataTable GetLeftMenuPermissions(DbContext db, int roleId, string displayNameSearchKey)
        {
            var cols = new StringBuilder();
            var permissions = _permissionsService.GetAll(db, true, a => a.Sort).ToList();
            permissions.ForEach(a =>
            {
                if (cols.Length > 0)
                    cols.Append(",");
                cols.Append($"MAX(CASE a.PermissionsId WHEN {a.Id} THEN IFNULL(a.Have, FALSE) END) C{a.Id}");
            });

            var sql = $@"
SELECT a.LeftMenuId, a.DisplayName, a.RoleId, a.NavBarGroupName, {cols}
FROM (
SELECT a.PermissionsId, a.PermissionsName, a.LeftMenuId, a.DisplayName, a.LeftMenuSort, c.Name NavBarGroupName, c.Sort NavBarGroupSort, {roleId} RoleId, b.Have 
FROM (SELECT a.Id PermissionsId, a.PermissionsName, b.Id LeftMenuId, b.DisplayName, b.Sort LeftMenuSort, b.NavBarGroupId FROM Permissions a,LeftMenus b)a
LEFT JOIN LeftMenuPermissions b ON a.PermissionsId = b.PermissionsId AND a.LeftMenuId = b.LeftMenuId AND b.RoleId = {roleId}
LEFT JOIN NavBarGroups c ON a.NavBarGroupId = c.Id)a
WHERE a.DisplayName LIKE '{displayNameSearchKey}%'
GROUP BY a.LeftMenuId, a.DisplayName, a.RoleId, NavBarGroupName
ORDER BY NavBarGroupSort, LeftMenuSort";

            return SqlQueryForDataTatable(db.Database, sql);
        }

        public static DataTable SqlQueryForDataTatable(Database db, string sql)
        {
            var conn = (MySqlConnection)db.Connection;
            var cmd = new MySqlCommand
            {
                Connection = conn,
                CommandText = sql
            };

            var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);

            conn.Close();//连接需要关闭
            conn.Dispose();
            return table;
        }
    }
}
