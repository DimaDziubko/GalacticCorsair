using System.Collections;
using UnityEngine;

namespace _Game.Core
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}