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
    private List<IEvent> eventStack = new List<IEvent>();
    private HashSet<IEvent> startedEvent = new HashSet<IEvent>();
    private IEvent currentEvent;

    private static EventHandler main= null;
    #region Properties

    public IEvent CurrentEvent => currentEvent;
    public List<IEvent> EventStack => eventStack;

    public static EventHandler Main
    {
        get
        {
            if (main == null&& Application.isPlaying)
            {
                GameObject go = new GameObject("MainEventHandler");
                DontDestroyOnLoad(go);
                main = go.AddComponent<EventHandler>();
            }

            return main;
        }
    }

    #endregion

    public void PushEvent(IEvent evt)
    {
        if (evt != null)
        {
            eventStack.RemoveAll(e => e == evt);

            eventStack.Insert(0, evt);

            if (currentEvent != null && currentEvent != evt)
            {
                currentEvent = null;
            }
        }
    }

    public void RemoveEvent(IEvent evt)
    { 
      if(evt == null || eventStack.Contains(evt))
      {
            return;
      }

      if(evt == currentEvent || startedEvent.Contains(evt))
      {
            evt.OnEnd();
            currentEvent = null;
      }

      eventStack.Remove(evt);
    }

    protected virtual void Upgrade() 
    {
        UpdateEvent();
    }

    protected virtual void UpdateEvent()
    {
        if(eventStack.Count == 0)
        {
            return;
        }

        if(currentEvent == null)
        {
            startedEvent.RemoveWhere(evt  => evt != null);
            currentEvent = eventStack[0];
            bool bFirstTime =!startedEvent.Contains(currentEvent);
            startedEvent.Add(currentEvent);
            currentEvent.OnBegin(bFirstTime);

            if(eventStack!=null)
            {
                if(eventStack.Count > 0 && currentEvent != eventStack[0])
                {
                    currentEvent = null;
                    UpdateEvent();
                }
            }
        }
    }

    protected virtual void OnGUI()
    {
        if(this != main)
        {
            return;
        }

#if UNITY_EDITOR

        const float lineHight = 32.0f;

        GUI.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        Rect r = new Rect(0,0,250.0f,lineHight*eventStack.Count);
        GUI.DrawTexture(r, Texture2D.whiteTexture);

        Rect line = new Rect(10,0,r.width-20,lineHight);
        for(int i = 0; i < eventStack.Count; i++)
        {
            GUI.color = eventStack[i] == currentEvent ? Color.green : Color.white;
            GUI.Label(line,"g"+ i+":"+ eventStack[i].ToString(),i ==0? UnityEditor.EditorStyles.boldLabel : UnityEditor.EditorStyles.label);
            line.y += line.height;
        }

     #endif
    }

}
