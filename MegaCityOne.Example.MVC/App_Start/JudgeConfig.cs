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
            dredd.Laws.Add("CanDisplayMainPage", (principal, arguments) =>
            {
                HttpContextBase httpContext = (HttpContextBase)arguments[0];

                // The main page can be displayed if the current user is in the "admininstrators" role or
                // is named "formix" and only if we are between 1am and 11pm.
                var startTime = DateTime.MinValue.AddHours(1);
                var endTime = DateTime.MinValue.AddHours(23);
                var time = DateTime.MinValue.Add(
                    DateTime.Now.Subtract(DateTime.UtcNow.Date));

                return (principal.IsInRole("administrators") || principal.Identity.Name == "formix") &&
                    (time.CompareTo(startTime) >= 0) && 
                    (time.CompareTo(endTime) < 0);
            });
            e.Respondent = dredd;
        }
    }
}