using System.Linq;
using System.Text;
using Dapper;
using Frame.Models;
using MySql.Data.MySqlClient;

namespace Frame.MetaData
{
    public class SysSettingService<T> : BaseService<SysSetting> where T : new()
    {
        protected readonly int GroupId;
        public SysSettingService(int groupId)
        {
            GroupId = groupId;
        }

        protected string CreateSql(params string[] propertyNames)
        {
            var sql = new StringBuilder();
            sql.Append("SELECT ");
            for (var i = 0; i < propertyNames.Length; i++)
            {
                var name = propertyNames[i];
                sql.Append($"MAX(CASE ColumnName WHEN '{name}' THEN VALUE END) '{name}'");
                if (i < propertyNames.Length - 1)
                    sql.Append(",");
            }
            sql.Append($" FROM SysSetting WHERE GroupId = {GroupId}");
            return sql.ToString();
        }

        public T GetSettingModel(FrameContext db)
        {
            var sql = CreateSql(typeof(T).GetProperties().Select(a => a.Name).ToArray());

            using (var conn = (MySqlConnection)db.Database.Connection)
            {
                var entity = conn.QuerySingleOrDefault<T>(sql);
                return entity;
            }
        }
    }
}
