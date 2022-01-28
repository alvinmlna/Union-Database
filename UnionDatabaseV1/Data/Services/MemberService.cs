using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Models;

namespace UnionDatabaseV1.Data.Services
{
    public class MemberService : IMemberService
    {
        private readonly Entities context;

        public MemberService(Entities context)
        {
            this.context = context;
        }

        public bool DeleteByPUK(string puk)
        {

            var toDelete = context.Members.Where(x => x.PUK.PUK1 == puk).Select(x => x.MemberID);
            if (toDelete.Count() <= 0)
                return false;

            var memberIds = string.Join("','", toDelete);
            using (context)
            {
                string sql = ("DELETE FROM [Union].[Member] WHERE MemberID in ('" + memberIds +  "')");
                var result = context.Database.ExecuteSqlCommand(sql);
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<ChartModel> GetChart()
        {
            IEnumerable<ChartModel> result =  null;
            string sql = ("select PUK1 as PUK, count(*) as [Count] from [Union].[Member] m, [Union].[PUK] p where m.PUK_ID = p.Id group by PUK1 order by Count desc");
            result  = context.Database.SqlQuery<ChartModel>(sql);

            return result.ToList();
        }

        public Member FindByMemberId(string memberId)
        {
            return context.Members.FirstOrDefault(x => x.MemberID == memberId);
        }

        public User FindAccessByMemberId(string memberId)
        {
            return context.Users.FirstOrDefault(x => x.MemberID == memberId);
        }

        public IEnumerable<Member> GeAll()
        {
            return context.Members.ToList();
        }
    }
}