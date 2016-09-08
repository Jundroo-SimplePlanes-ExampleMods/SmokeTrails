namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using UnityEngine;

   public partial class PartDisconnectExplosionEventArgs : GenericProxyType<PartDisconnectExplosionEventArgs, EventArgs>
   {
      private static Property<MonoBehaviour> _aircraft = CreateProperty<MonoBehaviour>("Aircraft");

      private static Property<int> _cascadeCount = CreateProperty<int>("CascadeCount");

      private static Property<float> _force = CreateProperty<float>("Force");

      private static Property<MonoBehaviour> _part = CreateProperty<MonoBehaviour>("Part");

      private static Property<Vector3> _position = CreateProperty<Vector3>("Position");

      protected PartDisconnectExplosionEventArgs()
      {
      }

      public AircraftScript Aircraft
      {
         get
         {
            return AircraftScript.Wrap(this.Get(_aircraft));
         }
      }

      public int CascadeCount
      {
         get
         {
            return this.Get(_cascadeCount);
         }
      }

      public float Force
      {
         get
         {
            return this.Get(_force);
         }
      }

      public PartScript Part
      {
         get
         {
            return PartScript.Wrap(this.Get(_part));
         }
      }

      public Vector3 Position
      {
         get
         {
            return this.Get(_position);
         }
      }
   }
}