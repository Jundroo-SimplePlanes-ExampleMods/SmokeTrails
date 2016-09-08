namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.AircraftAi
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public class AiControlledAircraftScript : ProxyType<AiControlledAircraftScript>
   {
      private static Property<MonoBehaviour> _aiAircraftScript = CreateProperty<MonoBehaviour>("AiAircraftScript");

      private static MethodInfo _setTarget = GetMethod("SetTarget", typeof(Vector3), typeof(bool));

      private static MethodInfo _setTargetFunc = GetMethod("SetTarget", typeof(Func<Vector3>), typeof(bool));

      protected AiControlledAircraftScript()
      {
      }

      public AircraftScript AiAircraftScript
      {
         get
         {
            return AircraftScript.Wrap(this.Get(_aiAircraftScript));
         }
      }

      public void SetTarget(System.Func<Vector3> positionFunc, bool mainTarget)
      {
         _setTargetFunc.Invoke(this.RealObject, new object[] { positionFunc, mainTarget });
      }

      public void SetTarget(Vector3 targetPositionFloatingOrigin, bool mainTarget)
      {
         _setTarget.Invoke(this.RealObject, new object[] { targetPositionFloatingOrigin, mainTarget });
      }
   }
}