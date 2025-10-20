using Dynamitey;

namespace DesignPattern.Observer
{
    public class Exercise
    {
        public class Game : IObservable<int>
        {
            private event Action<int> RatsCountChanged;

            public IDisposable Subscribe(IObserver<int> observer)
            {
                Action<int> handler = new Action<int>(observer.OnNext);
                RatsCountChanged += handler;
                NotifyObservers(GetCurrentCount());

                return new Unsubscriber(
                () =>
                {
                    RatsCountChanged -= handler;
                    NotifyObservers(GetCurrentCount());
                }, 
                () =>
                {
                    NotifyObservers(GetCurrentCount());
                });
            }

            public int GetCurrentCount()
            {
                return RatsCountChanged?.GetInvocationList().Length ?? 0;
            }

            public void NotifyObservers(int newCount)
            {
                RatsCountChanged?.Invoke(newCount);
            }
        }

        public class Unsubscriber : IDisposable
        {
            private readonly Action _unsubscribe;
            private readonly Action _beforeUnsubscribe;
            private bool _disposed;

            public Unsubscriber(Action unsubscribe, Action beforeUnsubscribe)
            {
                _unsubscribe = unsubscribe;
                _beforeUnsubscribe = beforeUnsubscribe;
            }

            public void Dispose()
            {
                if (_disposed) return;
                _beforeUnsubscribe?.Invoke();
                _unsubscribe?.Invoke();
                _disposed = true;
            }
        }

        public class Rat : IObserver<int>, IDisposable
        {
            private readonly IDisposable _subscription;
            private readonly Game _game;
            public int Attack;

            public Rat(Game game)
            {

                _game = game;
                _subscription = game.Subscribe(this);
            }

            public void Dispose()
            {
                _subscription.Dispose();
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(int value) => Attack = value;
        }
    }
}
