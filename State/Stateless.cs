using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.State
{
    public enum Health
    {
        NonReporductive,
        Pregnant,
        Reproductive
    }

    public enum Activity
    {
        GiveBirth,
        ReachPuberty,
        HaveAbortion,
        HaveUnprotectedSex,
        Historectomy
    }

    public static class StatelessMachine
    {
        public static bool ParentsNotWacthing { get; private set; }

        public static void Run()
        {
            var machine = new StateMachine<Health, Activity>(Health.NonReporductive);
            machine.Configure(Health.NonReporductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive)
                .OnActivateAsync(() =>
                {
                    return Task.Run(() =>
                    {
                        Console.WriteLine("You have reached puberty!");
                    });
                }, "Reach Puberty");
            machine.Configure(Health.Reproductive)
                .Permit(Activity.Historectomy, Health.NonReporductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, () => ParentsNotWacthing);
            machine.Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive)
                .Permit(Activity.HaveAbortion, Health.Reproductive);

            if(machine.CanFire(Activity.ReachPuberty))
            {
                machine.Fire(Activity.ReachPuberty);
            }

            Console.WriteLine($"Machine : {machine.State}");

            machine.Fire(Activity.GiveBirth);
            Console.WriteLine($"Machine : {machine.State}");

            machine.Fire(Activity.HaveUnprotectedSex);
            Console.WriteLine($"Machine : {machine.State}");
        }
    }
}
