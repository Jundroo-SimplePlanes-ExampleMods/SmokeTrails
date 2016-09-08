namespace Assets.SimplePlanesReflection.Assets.Scripts.Levels.Damage
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public partial class DamageableBody : MonoBehaviourProxyType<DamageableBody>
   {
      private static MethodInfo _onDamageReceived = GetMethod("OnDamageReceived", RealTypes<DamageType>.RealType, typeof(float), typeof(Vector3), typeof(Vector3?));

      protected DamageableBody()
      {
      }

      public void OnDamageReceived(DamageType type, float damage, Vector3 position, Vector3? normal)
      {
         _onDamageReceived.Invoke(this.RealObject, new object[] { (int)type, damage, position, normal });
      }
   }
}