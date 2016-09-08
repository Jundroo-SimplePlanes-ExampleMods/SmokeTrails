namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.AircraftAi
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using ControlSystems;
   using Game.Settings;
   using UnityEngine;

   public class AiManagerScript : ProxyType<AiManagerScript>
   {
      private static Property<MonoBehaviour> _instance = CreateProperty<MonoBehaviour>("Instance");

      private static MethodInfo _spawnSandboxAiFromXml = GetMethod("SpawnSandboxAiFromXml");

      protected AiManagerScript()
      {
      }

      public static AiManagerScript Instance
      {
         get
         {
            return AiManagerScript.Wrap(GetStatic(_instance));
         }
      }

      public AiControlledAircraftScript SpawnSandboxAiFromXml(string aircraftId, string aircraftXml, bool autoDespawn, bool forceSpawnEvenIfUnflyable, StartingLocation location, AiCsSandboxAirTraffic.AiModeType? aiMode)
      {
         var aggressive = aiMode == AiCsSandboxAirTraffic.AiModeType.Aggressive;
         var mode = aggressive ? null : Enum.ToObject(AiCsSandboxAirTraffic.AiMode.RealType, (int)(aiMode ?? AiCsSandboxAirTraffic.AiModeType.Default));
         return AiControlledAircraftScript.Wrap(_spawnSandboxAiFromXml.Invoke(this.RealObject, new object[] { aircraftId, aircraftXml, autoDespawn, forceSpawnEvenIfUnflyable, location.RealObject, mode, aggressive }));
      }
   }
}