using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Solid
{
    static class InterfaceSegregation
    {
        public static void MainProcess()
        {

        }
    }

    public class Document
    {

    }

    public class MultiFunctionPrinter : IMultiFunctionDevice
    {
        private IPrinter printer;
        private IScanner scanner;

        public MultiFunctionPrinter(IPrinter printer, IScanner scanner)
        {
            if(printer == null) throw new ArgumentNullException(nameof(printer));
            if(scanner == null) throw new ArgumentNullException(nameof(scanner));
            this.printer = printer;
            this.scanner = scanner;
        }

        public void Print(Document document)
        {
            this.printer.Print(document);
        }// decorator pattern

        public void Scan(Document document)
        {
            this.scanner.Scan(document);
        }// decorator pattern

        public void Fax(Document document)
        {
            Console.WriteLine("Faxing document...");
        }
    }

    public class Photocopier : IScanner, IPrinter
    {
        public void Scan(Document document)
        {
            Console.WriteLine("Scanning document...");
        }
        public void Print(Document document)
        {
            Console.WriteLine("Printing document...");
        }
    }

    public class Printer : IPrinter
    {
        public void Print(Document document)
        {
            Console.WriteLine("Printing document...");
        }
    }

    public interface IMultiFunctionDevice : IPrinter, IScanner, IFaxer
    {
    }

    public interface IScanner
    {
        void Scan(Document document);
    }

    public interface IFaxer
    {
        void Fax(Document document);
    }

    public interface IPrinter
    {
        void Print(Document document);
    }

}
