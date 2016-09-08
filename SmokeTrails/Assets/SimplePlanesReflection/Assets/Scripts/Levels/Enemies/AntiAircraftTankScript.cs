namespace Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using Parts.Targeting;

   public partial class AntiAircraftTankScript : MonoBehaviourProxyType<AntiAircraftTankScript>
   {
      private static Field<object> __groundTarget = CreateField<object>("_groundTarget");

      private static Property<bool> _isHostile = CreateProperty<bool>("IsHostile");

      protected AntiAircraftTankScript()
      {
      }

      public GroundTarget _groundTarget
      {
         get
         {
            return GroundTarget.Wrap(this.Get(__groundTarget));
         }
      }

      public bool IsHostile
      {
         get
         {
            return this.Get(_isHostile);
         }

         set
         {
            this.Set(_isHostile, value);
         }
      }
   }
}