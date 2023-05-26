using System.Collections;
using UnityEngine;

namespace Gobi.Infrastructure.Services.CoroutineRunner
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}