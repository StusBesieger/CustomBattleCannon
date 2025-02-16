using System;
using System.Xml.Serialization;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using Modding;
using Modding.Serialization;
using Modding.Modules;
using Modding.Blocks;

namespace CBCSpace
{
    [XmlRoot("CustomShootingModule")]
    [Reloadable]
    public class CBCShootingModule : BlockModule
    {
        [XmlElement("FireKey")]
        [RequireToValidate]
        public MKeyReference FireKey;

        [XmlElement("Caliber")]
        [RequireToValidate]
        public MSliderReference Caliber;

        [XmlElement("BarrelLength")]
        [RequireToValidate]
        public MSliderReference BarrelLength;
    }
    public class CBCShootingBehaviour : BlockModuleBehaviour<CBCShootingModule>
    {
        private MonoBehaviour monoBehaviour;
        private Transform projectilePool;
        private Transform effectPool;
        public static GameObject ShootingBlockNumber;
        public static GameObject ShootingEffectNumber;
        private ShootingPoolManager PNumber;
        private ShootingEffectManager ENumber;
        private int i = 1;


        public MSlider caliber;
        public MSlider barrellength;

        public float CaliberV;
        public float BarrelLengthV;

        public int ProjectilePool = 1;

        private int ProjectileNum = 0;

        public override void OnBlockPlaced()
        {
            base.OnBlockPlaced();
            //発射体ごとのプール生成ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
            projectilePool = GameObject.Find("CBC Projectile Manager").transform.Find("CBC Projectile Pool");
            while (projectilePool.transform.Find("Shooting Block Number " + i))
            {
                i++;
            }
            ShootingBlockNumber = new GameObject("Shooting Block Number " + i);
            ShootingEffectNumber = new GameObject("Shooting Effect Number " + i);

            ShootingBlockNumber.transform.parent = projectilePool.transform;
            ShootingEffectNumber.transform.parent = projectilePool.transform;

            PNumber = ShootingBlockNumber.AddComponent<ShootingPoolManager>();
            ENumber = ShootingBlockNumber.AddComponent<ShootingEffectManager>();

            PNumber.ProjectileIDNumber = i;
            ENumber.EffectIDNumber = i;

            this.gameObject.AddComponent<ShootingMonoBehaviour>();
            //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
            //砲身のメッシュ、砲身のコライダーの取得ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

            //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
            //マズルブレーキのメッシュの取得ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

            //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

        }
        public override void BuildingUpdate()
        {
            base.BuildingUpdate();

            caliber = GetSlider(Module.Caliber);
            CaliberV = (float)Math.Round((float)caliber.Value*10)/10;
            barrellength = GetSlider(Module.BarrelLength);
            BarrelLengthV = (float)Math.Round((float)barrellength.Value);



        }
        public override void OnSimulateStart()
        {
            base.OnSimulateStart();
            //プール内に弾のゲームオブジェクト生成ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
            while(ProjectileNum < ProjectilePool)
            {
            
                ProjectileNum++;
            }
            //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
        }
        public override void OnSimulateStop()
        {
            base.OnSimulateStop();
            //プール内の弾ゲームオブジェクト削除ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー

            //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
        }
        public override void SimulateUpdateAlways()
        {
            base.SimulateUpdateAlways();

        }
        public void FireProjectile()
        {

        }
    }
    public class ShootingMonoBehaviour : MonoBehaviour
    {
        public void OnDestroy()
        {

        }
            
    }
}
