namespace Assets.SimplePlanesReflection.Assets.Scripts.Parts.Modifiers.Weapons.Events
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using System.Text;
   using Explosions;
   using UnityEngine;

   public partial class BombExplodedEventArgs : GenericProxyType<BombExplodedEventArgs, EventArgs>
   {
      private static Property<Vector3> _blastDirection = CreateProperty<Vector3>("BlastDirection");

      private static Property<MonoBehaviour> _bomb = CreateProperty<MonoBehaviour>("Bomb");

      private static Property<MonoBehaviour> _explosiveForce = CreateProperty<MonoBehaviour>("ExplosiveForce");

      private static Property<bool> _groundImpact = CreateProperty<bool>("GroundImpact");

      private static Property<Vector3> _position = CreateProperty<Vector3>("Position");

      protected BombExplodedEventArgs()
      {
      }

      public Vector3 BlastDirection
      {
         get
         {
            return this.Get(_blastDirection);
         }
      }

      public BombScript Bomb
      {
         get
         {
            return BombScript.Wrap(this.Get(_bomb));
         }
      }

      public ExplosiveForceScript ExplosiveForce
      {
         get
         {
            return ExplosiveForceScript.Wrap(this.Get(_explosiveForce));
         }
      }

      public bool GroundImpact
      {
         get
         {
            return this.Get(_groundImpact);
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