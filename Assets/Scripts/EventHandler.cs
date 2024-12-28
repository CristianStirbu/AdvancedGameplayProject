using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public interface IEvent
    {
        void OnBegin(bool bFirstTime);
        void OnUpdate();
        void OnEnd();
        bool IsDone();
    }

    public abstract class GameEvent : IEvent
    {
        public virtual void OnBegin(bool bFirstTime)
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnEnd()
        {
        }

        public virtual bool IsDone()
        {
            return true;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    public abstract class GameEventBehaviour : MonoBehaviour, IEvent
    {
        public virtual void OnBegin(bool bFirstTime)
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnEnd()
        {
        }

        public virtual bool IsDone()
        {
            return true;
        }

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
