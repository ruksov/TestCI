using Gobi.Infrastructure.StaticData;
using UnityEngine;

namespace Gobi.Game.Services.Field.Generators
{
  public interface IFieldGenerator
  {
    void Init(Transform fieldObjectTransform, FieldStaticData fieldData);
    void Start();
  }
}