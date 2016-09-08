namespace Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public partial class BasicConvoyProviderScript : MonoBehaviourProxyType<BasicConvoyProviderScript>
   {
      private static Field<object> __convoyPrefabs = CreateField<object>("_convoyPrefabs");

      protected BasicConvoyProviderScript()
      {
      }

      public void ConfigureConvoy(params ConvoyVehicleType[] vehicleTypes)
      {
         if (vehicleTypes == null)
         {
            return;
         }

         var prefabs = new GameObject[vehicleTypes.Length];
         for (int i = 0; i < vehicleTypes.Length; i++)
         {
            switch (vehicleTypes[i])
            {
               case ConvoyVehicleType.AATank:

                  prefabs[i] = Resources.Load<GameObject>("Prefabs/Convoy/Vehicles/APCConvoy");
                  break;

               case ConvoyVehicleType.Truck:

                  prefabs[i] = Resources.Load<GameObject>("Prefabs/Convoy/Vehicles/ConvoyTruck");
                  break;

               default:

                  Debug.LogErrorFormat("Convoy vehicle type '{0}' is not currently supported.", vehicleTypes[i]);
                  break;
            }
         }

         this.Set(__convoyPrefabs, prefabs);
      }
   }
}