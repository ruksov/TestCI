using System.Collections.Generic;
using UnityEngine;

namespace Gobi.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "WindowsData", menuName = "StaticData/WindowsData")]
  public class WindowsStaticData : ScriptableObject
  {
    public List<WindowData> Windows;
  }
}