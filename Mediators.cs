using DesignPattern.Mediators;

namespace DesignPattern
{
    public class Mediatorss
    {
        public void Run()
        {
            //BasicMediator basicMediator = new BasicMediator();
            //basicMediator.Run();

            //EventBroker eventBroker = new EventBroker();
            //eventBroker.Run();

            MediatorR mediatR = new MediatorR();
            mediatR.Run();
        }
    }
}
