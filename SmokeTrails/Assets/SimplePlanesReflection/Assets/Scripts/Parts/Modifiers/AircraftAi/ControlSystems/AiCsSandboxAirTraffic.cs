namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.AircraftAi.ControlSystems
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;

   public class AiCsSandboxAirTraffic : ProxyType<AiCsSandboxAirTraffic>
   {
      protected AiCsSandboxAirTraffic()
      {
      }

      public enum AiModeType
      {
         Default = 0,

         AerobaticPath = 1,

         Buzz = 2,

         FollowTheLeader = 3,

         GeneralPath = 4,

         Kamakaze = 5,

         Land = 6,

         Race = 7,

         RandomLocations = 8,

         TakeOff = 9,

         Aggressive = 9999,
      }

      public class AiMode : ProxyType<AiMode>
      {
         protected AiMode()
         {
         }
      }
   }
}