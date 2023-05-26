using System;
using UnityEngine;

namespace Gobi.Game.Common
{
  public class EnterTrigger : MonoBehaviour
  {
    public Action Triggered;
    public bool OneTimeTrigger;

    public void Reset() =>
      gameObject.SetActive(true);

    private void OnTriggerEnter2D(Collider2D other)
    {
      Triggered?.Invoke();

      if (OneTimeTrigger)
        gameObject.SetActive(false);
    }
  }
}