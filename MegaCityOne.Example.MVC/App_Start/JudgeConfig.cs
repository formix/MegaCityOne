using MegaCityOne.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.MVC
{
    public class JudgeConfig
    {
        public static void RegisterJudge(Dispatcher dispatcher)
        {
            dispatcher.Summon += dispatcher_JudgeSummon;
        }

        static void dispatcher_JudgeSummon(object source, SummonEventArgs e)
        {
            JudgeDredd dredd = new JudgeDredd();
            dredd.Laws.Add("IsDomainUser", (principal, arguments) =>
            {
                string domain = Environment.GetEnvironmentVariable("USERDOMAIN");
                string domainUsers = domain + "\\Users";
                return principal.IsInRole(domainUsers);
            });
            e.Respondent = dredd;
        }
    }
}