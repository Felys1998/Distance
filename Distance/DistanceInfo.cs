﻿using System;
using System.Numerics;

namespace Distance
{
	public struct DistanceInfo
	{
		public bool IsValid { get; set; }
		public Dalamud.Game.ClientState.Objects.Enums.ObjectKind TargetKind { get; set; }
		public UInt32 ObjectID { get; set; }
		public UInt32 BNpcID { get; set; }
		public Vector3 PlayerPosition { get; set; }
		public Vector3 TargetPosition { get; set; }
		public float TargetRadius_Yalms { get; set; }
		public bool HasAggroRangeData { get; set; }
		public float AggroRange_Yalms { get; set; }

		public void Invalidate()
		{
			IsValid = false;
			TargetKind = Dalamud.Game.ClientState.Objects.Enums.ObjectKind.None;
			ObjectID = 0;
			BNpcID = 0;
			PlayerPosition = Vector3.Zero;
			TargetPosition = Vector3.Zero;
			TargetRadius_Yalms = 0;
			HasAggroRangeData = false;
			AggroRange_Yalms = 0;
		}

		public float DistanceFromTarget_Yalms
		{
			get
			{
				return Vector2.Distance( new Vector2( PlayerPosition.X, PlayerPosition.Z ), new Vector2( TargetPosition.X, TargetPosition.Z ) );
			}
			private set
			{
			}
		}

		public float DistanceFromTargetRing_Yalms
		{
			get
			{
				return DistanceFromTarget_Yalms - TargetRadius_Yalms;
			}
			private set
			{
			}
		}

		public float EffectiveRangeFromTarget_Yalms
		{
			get
			{
				return DistanceFromTarget_Yalms - TargetRadius_Yalms - PlayerHitRingRadius;
			}
			private set
			{
			}
		}

		public float DistanceFromTargetAggro_Yalms
		{
			get
			{
				return DistanceFromTargetRing_Yalms - AggroRange_Yalms;
			}
			private set
			{
			}
		}

		public override string ToString()
		{
			string str = "";

			str += $"Is Valid: {IsValid}\r\n";
			str += $"Target Kind: {TargetKind}\r\n";
			str += $"Object ID: {ObjectID:X8}\r\n";
			str += $"BNpc ID: {BNpcID}\r\n";
			str += $"Player Position: {PlayerPosition.X:F3}, {PlayerPosition.Y:F3}, {PlayerPosition.Z:F3}\r\n";
			str += $"Target Position: {TargetPosition.X:F3}, {TargetPosition.Y:F3}, {TargetPosition.Z:F3}\r\n";
			str += $"Aggro Range: {( HasAggroRangeData ? $"{AggroRange_Yalms:F3}" : "No Data" )}\r\n";
			str += $"Target Radius (y): {TargetRadius_Yalms:F3}\r\n";
			str += $"Distance To Target (y): {DistanceFromTarget_Yalms:F3}\r\n";
			str += $"Distance To Ring (y): {DistanceFromTargetRing_Yalms:F3}\r\n";
			str += $"Effective Range (y): {EffectiveRangeFromTarget_Yalms:F3}\r\n";
			str += $"Distance To Aggro (y): {( HasAggroRangeData ? $"{DistanceFromTargetAggro_Yalms:F3} " : "No Data" )}";
			
			return str;
		}

		public const float PlayerHitRingRadius = 0.5f;
	}
}
