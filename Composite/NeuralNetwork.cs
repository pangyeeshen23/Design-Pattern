using System.Collections;
using System.Collections.ObjectModel;
using static DesignPattern.Composite.NeuralNetwork;

namespace DesignPattern.Composite
{
    public static class ExtensionMethod
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;
            foreach (var from in self)
            {
                foreach (var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }

    public class NeuralNetwork
    {
        public class Neuron : IEnumerable<Neuron>
        {
            private float value;
            public List<Neuron> In, Out;

            public IEnumerator GetEnumerator()
            {
                yield return this;
            }

            IEnumerator<Neuron> IEnumerable<Neuron>.GetEnumerator()
            {
                return ((IEnumerable<Neuron>)In).GetEnumerator();
            }
        }

        public class NeuronLayer : Neuron
        {

        }
        
        public void Run()
        {
            Neuron n1 = new Neuron();
            Neuron n2 = new Neuron();

            n1.ConnectTo(n2);

            NeuronLayer layer1 = new NeuronLayer();
            NeuronLayer layer2 = new NeuronLayer();
            layer1.ConnectTo(layer2);
        }
    }
}
