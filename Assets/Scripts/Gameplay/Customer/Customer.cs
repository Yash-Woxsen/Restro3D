using System;
using UnityEngine;
using Gameplay.RestroResources.QueueSystem;
using Gameplay.RestroResources.QueueSystem.QueueOperations;
using Gameplay.RestroResources.TableSystem;

namespace Gameplay.Customer
{
    public class Customer : MonoBehaviour
    {        
        QueueSlot _currentQueueSlot,_behindQueueSlot,_aheadQueueSlot;

        public CustomerPool customerPool;

        Table _table;

        public event Action InvokeThisOnReachingTheQueuePosition;
        //========================================================================================================
        private void OnEnable()
        {
            customerPool = GetComponentInParent<CustomerPool>();
            var joinQueue = GetComponent<JoinQueue>();
            if (joinQueue == null){return;}
            
            joinQueue.CheckAndGetLastSlotOfQueueAndJoinTheQueue();
            
            
            InvokeThisOnReachingTheQueuePosition += SetAheadQueueSlot;
            InvokeThisOnReachingTheQueuePosition += SetBehindQueueSlot;
        }

        private void OnDisable()
        {
            InvokeThisOnReachingTheQueuePosition -= SetAheadQueueSlot;
            InvokeThisOnReachingTheQueuePosition -= SetBehindQueueSlot;
        }
        //======================================================================================================

        public QueueSlot GetCurrentQueueSlot(){return _currentQueueSlot;}
        public QueueSlot GetBehindQueueSlot() { return _behindQueueSlot; }
        public QueueSlot GetAheadQueueSlot() { return _aheadQueueSlot; }

        public void SetCurrentQueueSlot(QueueSlot queueSlot)
        {
            _currentQueueSlot = queueSlot; 
            if(queueSlot != null)_currentQueueSlot.ReserveTheSlot();
            
            //Assign This after Reaching The Slot Position in Ienumerator by Invoking Event
            _aheadQueueSlot = null;
            _behindQueueSlot = null;
        }
        void SetBehindQueueSlot()
        {
            var queueManager = customerPool.queueManager;

            if (_currentQueueSlot == null || queueManager == null || queueManager.queueSlots == null)
            {
                _behindQueueSlot = null;
                return;
            }

            int currentIndex = Array.IndexOf(queueManager.queueSlots, _currentQueueSlot);

            // If current slot is the last one, there is no slot behind
            if (currentIndex == -1 || currentIndex + 1 >= queueManager.queueSlots.Length)
            {
                _behindQueueSlot = null;
                return;
            }

            _behindQueueSlot = queueManager.queueSlots[currentIndex + 1];
        }
        
        void SetAheadQueueSlot()
        {
            var queueManager = customerPool.queueManager;

            if (_currentQueueSlot == null || queueManager == null || queueManager.queueSlots == null)
            {
                _aheadQueueSlot = null;
                return;
            }

            int currentIndex = Array.IndexOf(queueManager.queueSlots, _currentQueueSlot);

            // If current slot is the first one, there is no slot ahead
            if (currentIndex == -1 || currentIndex == 0)
            {
                _aheadQueueSlot = null;
                return;
            }

            // Safe assignment: slot before currentIndex
            _aheadQueueSlot = queueManager.queueSlots[currentIndex - 1];
        }

        public Table GetTable()
        {
            return _table;
        }
        public void SetTable(Table table)
        {
            _table = table;
        }

        public void InvokeFunctionToInvokeThisOnReachingTheQueuePosition()
        {
            InvokeThisOnReachingTheQueuePosition?.Invoke();
        }
    }
}