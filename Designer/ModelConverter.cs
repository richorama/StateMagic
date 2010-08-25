using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using StateMagic.Common;
using System.Linq;


namespace StateMagic.Designer
{
    public static class ModelConverter
    {
        public static StateModel ToCommon(IList<StateControl> stateControls, int modelId, string modelName)
        {
            var sm = new StateModel();
            sm.States = new List<State>();
            sm.Transitions = new List<Transition>();
            int i = 0;
            foreach (var control in stateControls)
            {
                var state = new State();
                state.Default = control.StartState;
                state.Name = control.DisplayName;
                state.StateID = i;
                control.StateId = i;
                i++;
                sm.States.Add(state);
            }

            foreach (var control in stateControls)
            {
                foreach (var arrow in control.ArrowsOut)
                {
                    var trans = new Transition();
                    trans.DestinationStateRef = arrow.HeadControl.StateId;
                    trans.SourceStateRef = arrow.TailControl.StateId;
                    sm.Transitions.Add(trans);
                }
            }
            sm.ModelID = modelId;
            sm.ModelName = modelName;
            return sm;
        }

        public static IList<StateControl> FromCommon(StateModel stateModel)
        {
            List<StateControl> stateControls = new List<StateControl>();
            foreach (var state in stateModel.States)
            {
                var stateControl = new StateControl();
                stateControl.DisplayName = state.Name;
                stateControl.StateId = state.StateID;
                stateControl.StartState = state.Default;
                stateControls.Add(stateControl);
            }

            foreach (var transition in stateModel.Transitions)
            {
                var sourceControl = (from sc in stateControls where sc.StateId == transition.SourceStateRef select sc).Single();
                var destControl = (from sc in stateControls where sc.StateId == transition.DestinationStateRef select sc).Single();
                if (sourceControl == null || destControl == null)
                {
                    throw new System.InvalidOperationException("cannot find states for transition");
                }
                var arrow = new Arrow();
                arrow.TailControl = sourceControl;
                arrow.HeadControl = destControl;
                sourceControl.AddArrow(arrow, StateControl.ArrowDirection.Out);
                destControl.AddArrow(arrow, StateControl.ArrowDirection.In);
            }
            return stateControls;
        }

    }
}
