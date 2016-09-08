namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Game.AircraftIo.Parts;
   using UnityEngine;

   public partial class PartScript : MonoBehaviourProxyType<PartScript>
   {
      private static Property<MonoBehaviour> _aircraft = CreateProperty<MonoBehaviour>("Aircraft");

      private static Property<object> _part = CreateProperty<object>("Part");

      protected PartScript()
      {
      }

      public AircraftScript Aircraft
      {
         get
         {
            return AircraftScript.Wrap(this.Get(_aircraft));
         }
      }

      public Part Part
      {
         get
         {
            return Part.Wrap(this.Get(_part));
         }
      }
   }
}