namespace Assets.SimplePlanesReflection.Assets.Game.Settings
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using UnityEngine;

   public class StartingLocation : ProxyType<StartingLocation>
   {
      protected StartingLocation()
      {
      }
      
      public StartingLocation(string name, string areaName, LocationType type, Vector3 position, Vector3 rotation, float initialVelocity, bool isRunwayTakeoff)
      {
         this.RealObject = CreateUnwrapped(name, areaName, (int)type, position, rotation, initialVelocity, isRunwayTakeoff, null);
      }
   }
}
