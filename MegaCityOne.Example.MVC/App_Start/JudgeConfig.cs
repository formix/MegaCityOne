using MegaCityOne.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MegaCityOne.Example.Mvc
{
    public class JudgeConfig
    {
        public static void RegisterJudge(Dispatcher dispatcher)
        {
            dispatcher.Summon += dispatcher_Summon;
        }


        static void dispatcher_Summon(object source, SummonEventArgs e)
        {
            JudgeDredd dredd = new JudgeDredd();
            dredd.Laws.Add("IsLocalAdmin", (principal, arguments) =>
            {
                string domain = Environment.GetEnvironmentVariable("COMPUTERNAME");
                string domainUsers = domain + "\\Administrators";
                return principal.IsInRole(domainUsers);
            });
            e.Respondent = dredd;
        }
    }
}