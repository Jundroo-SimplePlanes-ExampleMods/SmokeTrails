namespace Assets.SimplePlanesReflection.Assets.Game.AircraftIo.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using Scripts.Parts;
   using UnityEngine;

   public partial class AircraftGeneratedEventArgs : GenericProxyType<AircraftGeneratedEventArgs, EventArgs>
   {
      private static Property<MonoBehaviour> _aircraftScript = CreateProperty<MonoBehaviour>("AircraftScript");

      protected AircraftGeneratedEventArgs()
      {
      }

      public AircraftScript AircraftScript
      {
         get
         {
            return AircraftScript.Wrap(this.Get(_aircraftScript));
         }
      }
   }
}