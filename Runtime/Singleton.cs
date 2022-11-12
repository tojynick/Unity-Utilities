using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }
    
        protected virtual void Awake()
        {
            SetInstance();
        }
    
        private void SetInstance()
        {
            if(Instance)
                Destroy(Instance);
                
            Instance = (T) this;
        }
    }
}

