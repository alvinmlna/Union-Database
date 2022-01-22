using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnionDatabaseV1.DAL;
using UnionDatabaseV1.Models;

namespace UnionDatabaseV1.Data.Services
{
    public interface IMemberService
    {
        IEnumerable<Member> GeAll();

        Member FindByMemberId(string memberId);
        User FindAccessByMemberId(string memberId);

        bool DeleteByPUK(string puk);
        IEnumerable<ChartModel> GetChart();
    }
}