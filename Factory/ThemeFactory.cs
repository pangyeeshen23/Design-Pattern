using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Factory
{
    public interface ITheme
    {
        string TextColor { get; set; }
        string BgrColor { get; set; }
    }

    public class LightTheme : ITheme
    {
        public string TextColor { get; set; } = "black";
        public string BgrColor { get; set; } = "white";
    }

    public class DarkTheme : ITheme
    {
        public string TextColor { get; set; } = "white";
        public string BgrColor { get; set; } = "black";
    }

    public class Ref<T> where T : class
    {
        public T Value { get; set; }
        public Ref(T value)
        {
            Value = value;
        }
    }

    public class ReplaceableThemeFactory
    {
        private readonly List<WeakReference<Ref<ITheme>>> _themes = new();

        private ITheme CreateThemeImpl(bool dark)
        {
            return dark ? new DarkTheme() : new LightTheme();
        }

        public Ref<ITheme> CreateTheme(bool dark)
        {
            Ref<ITheme> themeRef = new Ref<ITheme>(CreateThemeImpl(dark));
            _themes.Add(new (themeRef));
            return themeRef;
        }

        public void ReplaceTheme(bool dark)
        {
            foreach(var wr in _themes)
            {
                if(wr.TryGetTarget(out var themeRef))
                {
                    themeRef.Value = CreateThemeImpl(dark);
                }
            }
        }
    }

    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> _themes = new List<WeakReference<ITheme>>();
        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            _themes.Add(new WeakReference<ITheme>(theme));
            return theme;
        }

        public string Info 
        { 
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(var reference in _themes)
                {
                    if(reference.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light").AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }
}
