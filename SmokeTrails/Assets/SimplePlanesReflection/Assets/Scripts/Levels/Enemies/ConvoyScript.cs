namespace Assets.SimplePlanesReflection.Assets.Scripts.Levels.Enemies
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public partial class ConvoyScript : MonoBehaviourProxyType<ConvoyScript>
   {
      private static Field<MonoBehaviour> __convoyProvider = CreateField<MonoBehaviour>("_convoyProvider");

      private static Field<Transform[]> __startingWaypoints = CreateField<Transform[]>("_startingWaypoints");

      private static MethodInfo _initialize = GetMethod("Initialize");

      private static Property<bool> _isHostile = CreateProperty<bool>("IsHostile");

      private static Property<List<Transform>> _waypoints = CreateProperty<List<Transform>>("Waypoints");

      protected ConvoyScript()
      {
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

      public List<Transform> Waypoints
      {
         get
         {
            return this.Get(_waypoints);
         }

         set
         {
            this.Set(_waypoints, value);
         }
      }

      public void Initialize(IEnumerable<Transform> waypoints, bool initiallyHostile)
      {
         _initialize.Invoke(this.RealObject, new object[0]);
         
         this.Set(__startingWaypoints, new Transform[0]);
         this.Waypoints = new List<Transform>(waypoints);
         this.IsHostile = initiallyHostile;
      }

      public void SetConvoyProvider(BasicConvoyProviderScript convoyProvider)
      {
         this.Set(__convoyProvider, convoyProvider.RealObject);
      }
   }
}