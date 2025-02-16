using System;
using Modding;
using UnityEngine;

namespace CBCSpace
{
	public class Mod : ModEntryPoint
	{
		public static GameObject CBCProjectileManager;
		public static ModAssetBundle ProjectilePrefab;
		public static ModAssetBundle EffectAsset;

		public static GameObject CBCProjectilePool;
		public static GameObject CBCEffectPool;
		public static void Log(string msg)
		{
			Debug.Log("CBC Log: " + msg);
		}
		public static void Warning(string msg)
		{
			Debug.LogWarning("CBC Warning: " + msg);
		}
		public static void Error(string msg)
		{
			Debug.LogError("CBC Error: " + msg);
		}
		public override void OnLoad()
		{
			CBCProjectileManager = new GameObject("CBC Projectile Manager");
			UnityEngine.Object.DontDestroyOnLoad(CBCProjectileManager);

			CBCProjectilePool = new GameObject("CBC Projectile Pool");
			CBCEffectPool = new GameObject("CBC Explode Effect Pool");

			CBCProjectilePool.gameObject.transform.parent = GameObject.Find("CBC Projectile Manager").transform;
			CBCEffectPool.gameObject.transform.parent = GameObject.Find("CBC Projectile Manager").transform;

			Modding.Modules.CustomModules.AddBlockModule<CBCShootingModule, CBCShootingBehaviour>("CBCShootingModule", true);

			switch (Application.platform)   //OSñàÇ…ïœçX
			{
				case RuntimePlatform.WindowsPlayer:
					ProjectilePrefab = ModResource.GetAssetBundle("ProjectileAsset");
					break;
				case RuntimePlatform.OSXPlayer:
					ProjectilePrefab = ModResource.GetAssetBundle("ProjectileAssetMac");
					break;
				case RuntimePlatform.LinuxPlayer:
					ProjectilePrefab = ModResource.GetAssetBundle("ProjectileAssetMac");
					break;
				default:
					ProjectilePrefab = ModResource.GetAssetBundle("ProjectileAsset");
					break;
			}
			switch (Application.platform)   //OSñàÇ…ïœçX
			{
				case RuntimePlatform.WindowsPlayer:
					EffectAsset = ModResource.GetAssetBundle("CBCEffectAsset");
					break;
				case RuntimePlatform.OSXPlayer:
					EffectAsset = ModResource.GetAssetBundle("CBCEffectAssetMac");
					break;
				case RuntimePlatform.LinuxPlayer:
					EffectAsset = ModResource.GetAssetBundle("CBCEffectAssetMac");
					break;
				default:
					EffectAsset = ModResource.GetAssetBundle("CBCEffectAsset");
					break;
			}
		}
	}
}
