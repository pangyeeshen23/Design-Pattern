using JetBrains.Annotations;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace DesignPattern.Observer
{
    public class Product : INotifyPropertyChanged
    {
        public string name;
        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberNameAttribute] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Product: {Name}";
        }
    }

    public class Window : INotifyPropertyChanged
    {
        // the observer
        public string productName;

        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return;
                productName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberNameAttribute] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public override string ToString()
        {
            return $"Window: {ProductName}";
        }
    }

    // bidirectional binding

    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;

        // first second
        // firstProp, secondProp

        public BidirectionalBinding(INotifyPropertyChanged first, Expression<Func<object>> firstProperty, INotifyPropertyChanged second, Expression<Func<object>> secondProperty)
        {
            // xxxProperty is MemeberExpression
            // Member 
            if (firstProperty.Body is MemberExpression firstExpr && secondProperty.Body is MemberExpression secondExpr)
            {
                if (firstExpr.Member is PropertyInfo firstProp && secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (sender, e) =>
                    {
                        if (!disposed)
                            secondProp.SetValue(second, firstProp.GetValue(first));
                    };

                    second.PropertyChanged += (sender, e) =>
                    {
                        if (!disposed)
                            firstProp.SetValue(first, secondProp.GetValue(second));
                    };
                }
            }

        }

        public void Dispose()
        {
            disposed = true;
        }
    }

    public class BidirectionalObserver
    {
        public static void Run()
        {
            Product product = new Product { Name = "Book" };
            Window window = new Window { ProductName = "Book" };

            //product.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == "Name")
            //    {
            //        Console.WriteLine("Name was changed in Product");
            //        window.ProductName = product.Name;
            //    }
            //};

            //window.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == "ProductName")
            //    {
            //        Console.WriteLine("Name was changed in Window");
            //        product.Name = window.ProductName;
            //    }
            //};

            //product.Name = "New Book";
            //Console.WriteLine(product);
            //Console.WriteLine(window);

            using (var binding = new BidirectionalBinding(product, () => product.Name, window, () => window.ProductName))
            {
                product.Name = "New Book";
                Console.WriteLine(product);
                Console.WriteLine(window);
                window.ProductName = "Another Book";
                Console.WriteLine(product);
                Console.WriteLine(window);
            }
        }
    }
}
