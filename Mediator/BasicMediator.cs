using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Mediator
{
    public class BasicMediator
    {

        public class Person
        {
            public string _name;
            public ChatRoom? _room;
            private List<string> _chatLog = new List<string>();

            public Person(string name)
            {
                if(name == null) throw new ArgumentNullException("name");
                _name = name;
            }

            public void Say(string message)
            {
                if (_room == null) throw new Exception("You are not in a chat room");
                _room.Broadcast(_name, message);
            }

            public void PrivateMessage(string who, string message)
            {
                if (_room == null) throw new Exception("You are not in a chat room");
                _room.Message(_name, who, message);
            }

            public void ReceiveMessage(string sender, string message)
            {
                string m = $"{ sender } : '{message}'";
                _chatLog.Add(m);
                Console.WriteLine($"{_name}'s chat session {m}");
            }
        }

        public class ChatRoom
        {
            private List<Person> _people = new List<Person>();

            public void Join(Person person)
            {
                if (person == null) throw new ArgumentNullException("person");
                string joinMsg = $"{person._name} joins the chat";
                Broadcast("room", joinMsg);
                person._room = this;
                _people.Add(person);
            }

            public void Broadcast(string source, string message)
            {
                foreach(Person person in _people)
                {
                    if (person._name != source)
                    {
                        person.ReceiveMessage(source, message);
                    }
                }
            }

            public void Message(string source, string destination, string mesage)
            {
                Person? person = _people.FirstOrDefault(p => p._name == destination);
                if (person != null)
                {
                    person.ReceiveMessage(source, mesage);
                }
                else
                {
                    throw new Exception("Person not found");
                }
            }


            public ChatRoom()
            {
                
            }
        }

        public void Run()
        {
            ChatRoom chatRoom = new ChatRoom();

            Person john = new Person("John");
            Person jane = new Person("Jane");
            
            chatRoom.Join(john);
            chatRoom.Join(jane);

            john.Say("hi room");
            jane.Say("oh, hey john");

            Person simon = new Person("Simon");
            chatRoom.Join(simon);
            simon.Say("hi everyone");
            jane.PrivateMessage("Simon", "glad you could join us");
        }
    }
}
