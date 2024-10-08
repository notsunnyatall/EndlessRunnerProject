using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RainbowAssets.StateMachine
{
    [CreateAssetMenu(menuName = "Rainbow Assets/New State Machine")]
    public class StateMachine : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] EntryState entryState;
        [SerializeField] AnyState anyState;
        [SerializeField] List<State> states = new();
        Vector2 entryStateOffset = new(250, 0);
        Vector2 anyStateOffset = new(250, 50);
        Dictionary<string, State> stateLookup = new();
        State currentState;

        public void Bind(StateMachineController controller)
        {
            foreach(var state in states)
            {
                state.Bind(controller);
            }
        }

        public StateMachine Clone()
        {
            StateMachine clone = Instantiate(this);

            clone.states.Clear();
            clone.stateLookup.Clear();

            foreach(var state in states)
            {
                clone.AddState(state.Clone());
            }

            clone.entryState = clone.GetState(entryState.GetUniqueID()) as EntryState;
            clone.anyState = clone.GetState(anyState.GetUniqueID()) as AnyState;

            return clone;
        }

        public State GetState(string stateID)
        {
            if(!stateLookup.ContainsKey(stateID))
            {
                Debug.LogError($"State with ID {stateID} not found");
                return null;
            }

            return stateLookup[stateID];
        }

        public IEnumerable<State> GetStates()
        {
            return states;
        }

        public void Enter()
        {
            SwitchState(entryState.GetEntryStateID());
        }

        public void Tick()
        {
            currentState.Tick();
            anyState.Tick();
        }

        public void SwitchState(string newStateID)
        {
            currentState?.Exit();
            currentState = GetState(newStateID);
            currentState.Enter();
        }

#if UNITY_EDITOR
        public State CreateState(Type type, Vector2 position)
        {
            State newState = MakeState(type, position);
            Undo.RegisterCreatedObjectUndo(newState, "State Created");
            Undo.RecordObject(this, "State Added");
            AddState(newState);
            return newState;
        }

        public void RemoveState(State stateToRemove)
        {
            Undo.RecordObject(this, "State Removed");
            states.Remove(stateToRemove);
            OnValidate();
            Undo.DestroyObjectImmediate(stateToRemove);
        }

        State MakeState(Type type, Vector2 position)
        {
            State newState = CreateInstance(type) as State;
            newState.name = type.Name;
            newState.SetUniqueID(Guid.NewGuid().ToString());
            newState.SetPosition(position);
            return newState;
        }
#endif

        void OnEnable()
        {
            currentState = null;
        }

        void Awake()
        {
            OnValidate();
        }

        void OnValidate()
        {
            stateLookup.Clear();

            foreach(var state in states)
            {
                if(state != null)
                {
                    stateLookup[state.GetUniqueID()] = state;
                }
            }
        }
        
        void AddState(State newState)
        {
            states.Add(newState);
            OnValidate();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if(AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(var state in states)
                {
                    if(AssetDatabase.GetAssetPath(state) == "")
                    {
                        AssetDatabase.AddObjectToAsset(state, this);
                    }
                }

                if(entryState == null)
                {
                    entryState = MakeState(typeof(EntryState), entryStateOffset) as EntryState;
                    entryState.SetTitle("Entry");
                    AddState(entryState);
                }

                if(anyState == null)
                {
                    anyState = MakeState(typeof(AnyState), anyStateOffset) as AnyState;
                    anyState.SetTitle("Any");
                    AddState(anyState);
                }
            }
#endif
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }  
    }
}
