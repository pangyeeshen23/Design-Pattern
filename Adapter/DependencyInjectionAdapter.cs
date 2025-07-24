using Autofac;
using Autofac.Features.Metadata;

namespace DesignPattern.Adapter
{
    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Save a file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Open a file");
        }
    }

    public class Button
    {
        private ICommand _command;
        private string name;

        public Button(ICommand command, string name)
        {
            if (command == null)
                throw new ArgumentNullException(paramName: nameof(command));
            this._command = command;
            this.name = name;
        }

        public void Click()
        {
            this._command.Execute();
        }
        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {name}");
        }
    }

    public class Editor
    {
        public IEnumerable<Button> buttons;

        public IEnumerable<Button> Buttons 
        { 
            get { return buttons; } 
        }

        public Editor(IEnumerable<Button> buttons)
        {
            if(buttons == null)
                throw new ArgumentNullException(paramName: nameof(buttons));
            this.buttons = buttons;
        }

        public void ClickAllButtons()
        {
            foreach(Button btn in buttons)
            {
                btn.Click();
            }
        }

    }

    public class DependencyInjectionAdapter
    {
        public static void Run()
        {
            ContainerBuilder cb = new ContainerBuilder();
            cb.RegisterType<SaveCommand>().As<ICommand>().WithMetadata("Name", "Save");
            cb.RegisterType<OpenCommand>().As<ICommand>().WithMetadata("Name", "Open");
            // this only register the OpenCommand as a Button
            //cb.RegisterType<Button>();
            // this register every ICommand as a Button
            //cb.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd));
            cb.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
            cb.RegisterType<Editor>();

            using (var c = cb.Build())
            {
                Editor editor = c.Resolve<Editor>();
                editor.ClickAllButtons();

                foreach (Button btn in editor.buttons)
                {
                    btn.PrintMe();
                }
            }
        }
    }
}
