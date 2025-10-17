using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DesignPattern.Observer
{
    public class MarketObserver
    {
        public class Market : INotifyPropertyChanged
        {
            private float volatility;

            public float Volatility
            {
                get => volatility;
                set
                {
                    if (value.Equals(volatility)) return;
                    volatility = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // a different observer implementation
        public class MarketTwo
        {
            public BindingList<float> Prices = new BindingList<float>();

            public void AddPrice(float price)
            {
                Prices.Add(price);
            }
        }

        public class Program
        {
            public static void Run()
            {
                Market market = new Market();
                market.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == "volatility")
                    {

                    }
                };

                MarketTwo marketTwo = new MarketTwo();
                marketTwo.Prices.ListChanged += (sender, args) =>
                {
                    if (args.ListChangedType == ListChangedType.ItemAdded)
                    {
                        float price = ((BindingList<float>)sender)[args.NewIndex];
                        Console.WriteLine($"Added new prices {price}");
                    }
                };
                marketTwo.AddPrice(123);
            }
        }
    }
    
}
