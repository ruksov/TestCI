using System;
using Gobi.UI.Services.Window;
using UnityEngine;

namespace Gobi.Infrastructure.StaticData
{
  [Serializable]
  public class WindowData
  {
    public WindowId Id;
    public GameObject Prefab;
  }
}