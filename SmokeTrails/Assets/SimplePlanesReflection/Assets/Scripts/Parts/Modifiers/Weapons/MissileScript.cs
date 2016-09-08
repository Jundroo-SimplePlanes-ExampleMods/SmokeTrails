namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.Weapons
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public partial class MissileScript : MonoBehaviourProxyType<MissileScript>
   {
      private static EventInfo _exploded = GetEvent("Exploded");

      private static Property<MonoBehaviour> _partScript = CreateProperty<MonoBehaviour>("PartScript");

      protected MissileScript()
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

      public PartScript PartScript
      {
         get
         {
            return PartScript.Wrap(this.Get(_partScript));
         }
      }
   }
}