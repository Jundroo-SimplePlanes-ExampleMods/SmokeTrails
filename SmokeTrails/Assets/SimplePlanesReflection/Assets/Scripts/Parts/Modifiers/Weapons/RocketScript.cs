namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.Weapons
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;

   public partial class RocketScript : MonoBehaviourProxyType<RocketScript>
   {
      private static EventInfo _exploded = GetEvent("Exploded");

      protected RocketScript()
      {
      }

      public event EventHandler Exploded
      {
         add
         {
            this.AddEvent(_exploded, value);
         }

         remove
         {
            this.RemoveEvent(_exploded, value);
         }
      }
   }
}