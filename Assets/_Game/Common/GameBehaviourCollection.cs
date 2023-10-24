﻿using System;
using System.Collections.Generic;

namespace _Game.Common
{
    [Serializable]
    public class GameBehaviourCollection 
    {
        private List<GameBehaviour> _behaviors = new List<GameBehaviour>();
        //private List<Transform> _transforms = new List<Transform>();

        public bool IsEmpty => _behaviors.Count == 0;

        public void Add(GameBehaviour behaviour)
        {
            _behaviors.Add(behaviour);
            //_transforms.Add(behaviour.transform);
        }

        public void GameUpdate()
        {
            for (var i = 0; i < _behaviors.Count; i++)
            {
                if (_behaviors[i].GameUpdate() == false)
                {
                    var lastIndex = _behaviors.Count - 1;
                    _behaviors[i] = _behaviors[lastIndex];
                    _behaviors.RemoveAt(lastIndex);
                    i -= 1;
                }
            }
        }
        
        public void GameLateUpdate()
        {
            foreach (var behavior in _behaviors)
            {
                behavior.GameLateUpdate();
            }
        }

        public void Clear()
        {
            for (var i = 0; i < _behaviors.Count; i++)
            {
                _behaviors[i].Recycle();
            }
            _behaviors.Clear();
        }

        //public Transform[] GetAllTransforms() => _transforms.ToArray();
    }
}
