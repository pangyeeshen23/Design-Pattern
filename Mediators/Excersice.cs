using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Mediators
{
    class Excersice
    {
        public class Participant
        {
            private readonly Mediator _mediator;
            public int Value { get; set; }

            public Participant(Mediator mediator)
            {
                _mediator = mediator;
                _mediator.Join(this);
            }

            public void Say(int n)
            {
                _mediator.Broadcast(this, n);
            }
        }

        public class Mediator
        {
            private readonly List<Participant> _participants = new List<Participant>();

            public void Join(Participant participant)
            {
                if(participant == null) throw new ArgumentNullException("participant");
                _participants.Add(participant);
            }

            public void Broadcast(Participant source, int n)
            {
                foreach(Participant participant in _participants)
                {
                    if(participant != source)
                    {
                        participant.Value += n;
                    }
                }
            }
        }
    }
}
