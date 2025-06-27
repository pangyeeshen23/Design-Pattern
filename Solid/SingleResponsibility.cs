using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Solid
{
    /// <summary>
    /// Single Responsibiltiy
    /// A single class is only responsible for one thing and has one reason to change.
    /// </summary>
    static class SingleResponsibility
    {
        public static void MainProcess()
        {
            Console.WriteLine("Hello, World!");
            Journal journal = new Journal();
            journal.AddEntry("I learned about the Memento pattern today.");
            journal.AddEntry("I implemented a simple journal application.");
            Console.WriteLine(journal.ToString());
            Persistance persistance = new Persistance();
            var filename = @"c:\temp\journal.txt";
            persistance.SaveToFile(journal, filename, true);
            Process.Start(filename);
        }

        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{count++}: {text}");
                return count;
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public void Save(string fileName)
            {
                File.WriteAllText(fileName, ToString());
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }
        }

        public class Persistance
        {
            public void SaveToFile(Journal j, string fileName, bool overwrite = false)
            {
                if (overwrite || !File.Exists(fileName))
                    File.WriteAllText(fileName, j.ToString());
            }
        }
    }
}
