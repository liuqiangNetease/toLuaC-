//using UnityEngine;
//using System;
//using System.Collections;
//using System.Collections.Generic;

///// <summary>
///// Timer manager.
///// </summary>

//namespace Base
//{
//    public class TimerMng : MonoBehaviour
//    {

//        #region Timer Class
//        public class Timer
//        {
//            public int m_id;            //唯一标识
//            public float m_fDelay;   // delay 
//            public float m_fInterval;     // 一次循环持续时间  if m_fInterval = 0, trigger every frame
//            public bool m_bLoop;       // 是否循环
//            public bool m_bActive;    // 是否激活
//            public int m_iRepeat;        // 要执行次数


//            public int m_iRepeatElapsed; // 执行了多少次
//            private float m_fElapsed; //从开始到现在距离时间'
//            private bool m_bUseDelay;
//            public bool m_bStop;

//            public Action m_callBack;

//            public Timer(Action _callBack, int id, float delay, float interval, int repeatCount)
//            {
//                m_id = id;
//                m_fDelay = delay;
//                m_fInterval = interval;
//                m_iRepeat = repeatCount;
//                m_bLoop = repeatCount > 0 ? false : true;
//                m_callBack = _callBack;

//                m_fElapsed = 0;
//                m_iRepeatElapsed = 0;
//                m_bActive = false;
//                m_bUseDelay = true;
//                m_bStop = false;
//            }

//            public void Update()
//            {
//                if (m_bStop)
//                    return;
//                if (m_bActive == false)
//                    return;

//                m_fElapsed += Time.deltaTime;
//                if (m_bUseDelay)
//                {
//                    if (m_fElapsed < m_fDelay)
//                        return;

//                    m_fElapsed = m_fElapsed - m_fDelay;

//                    if (m_callBack != null)
//                        m_callBack();
//                    m_bUseDelay = false;

//                    m_iRepeatElapsed++;
//                    return;
//                }

//                if (m_fElapsed >= m_fInterval)
//                {
//                    m_fElapsed = 0;
//                    m_iRepeatElapsed++;
//                    if (m_callBack != null)
//                        m_callBack();
//                    if (!m_bLoop && m_iRepeatElapsed == m_iRepeat && m_iRepeat >= 0)
//                        m_bStop = true;
//                }
//            }
//        }
//        #endregion

//        #region params
//        public List<Timer> lstTimers = new List<Timer>();

//        int m_id;
//        #endregion

//        #region unity_functions

//        void FixedUpdate()
//        {
//            if (lstTimers != null)
//            {
//                lstTimers.ForEach(p =>
//                {
//                    p.Update();
//                });
//            }

//            foreach (var timer in lstTimers)
//            {
//                if (timer.m_bStop)
//                {
//                    lstTimers.Remove(timer);
//                    break;
//                }
//            }

//        }

//        #endregion

//        #region custom_functions
//        public void AddTimer(Timer timer)
//        {
//            var tmp = lstTimers.Find(p => p.m_id == timer.m_id);
//            if (tmp == null)
//            {
//                lstTimers.Add(timer);
//            }
//            else
//            {
//                Debug.Log("id exists");
//            }
//        }

//        public void AddTimers(List<Timer> lst)
//        {
//            lst.ForEach(p =>
//            {
//                AddTimer(p);
//            });
//        }

//        public void RemoveTimer(int id)
//        {
//            var tmp = lstTimers.Find(p => p.m_id == id);
//            if (tmp == null)
//            {
//                return;
//            }
//            RemoveTimer(tmp);
//        }

//        public void RemoveTimer(Timer ev)
//        {
//            if (lstTimers.Contains(ev))
//            {
//                lstTimers.Remove(ev);
//            }
//        }

//        public void RemoveTimers(List<Timer> lst)
//        {
//            lst.ForEach(p =>
//            {
//                lstTimers.Remove(p);
//            });
//        }

//        public void Clear()
//        {
//            lstTimers.Clear();
//        }

//        public Timer GetTimer(int id)
//        {
//            return lstTimers.Find(p => p.m_id == id);
//        }

//        public int StartTimer(Action callBackFn, float delay, float interval, int repeatCount)
//        {
//            m_id++;
//            Timer timer = GetTimer(m_id);
//            if (timer == null)
//            {
//                timer = new Timer(callBackFn, m_id, delay, interval, repeatCount);
//                lstTimers.Add(timer);
//            }
//            else
//            {
//                Debug.Log("timer is already start!");
//            }

//            return m_id;
//        }

//        public void PauseTimer(int id)
//        {
//            Timer timer = GetTimer(id);
//            if (timer != null)
//                timer.m_bActive = false;
//        }
//        public void ResumeTimer(int id)
//        {
//            Timer timer = GetTimer(id);
//            if (timer != null)
//                timer.m_bActive = true;
//        }

//        #endregion
//    }
//}

