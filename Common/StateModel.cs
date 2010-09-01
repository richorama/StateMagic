using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StateMagic.Common
{

    public class StateModel
    {
        public int ModelID { get; set; }

        public List<State> States { get; set; }

        public List<Transition> Transitions { get; set; }

        public string ModelName { get; set; }

        public IEnumerable<State> GetNextStates(State currentState)
        {
            foreach (Transition trans in this.Transitions)
            {
                if (trans.SourceStateRef == currentState.StateID)
                {
                    yield return (from state in this.States where state.StateID == trans.DestinationStateRef select state).First();
                }
            }
        }

        public State GetStateByName(string stateName)
        {
            return (from state in this.States where string.Compare(stateName, state.Name,StringComparison.InvariantCultureIgnoreCase) == 0 select state).First();
        }

    }
}
