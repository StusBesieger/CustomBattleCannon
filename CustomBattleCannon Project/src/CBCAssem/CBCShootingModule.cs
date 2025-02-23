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
        private int i = 1; //���ˑ̂Ɣ��ˌ���R�Â���ID


        public MSlider caliber;
        public MSlider barrellength;
        public MKey FireKey;

        public float CaliberV = 1f;//���amm(�P�ʂ�1�u���b�N=50cm�Ƃ���)
        public float BarrelLengthV = 1f;//�C�g���A���a�~���{��
        public float ReloadTime = 1f;//���U����

        public int ProjectilePool = 1;

        private int ProjectileNum = 0;

        private float ReloadingTime;//���U�����܂ł̎���

        public override void OnBlockPlaced()
        {
            base.OnBlockPlaced();
            //���ˑ̂��Ƃ̃v�[�������[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
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
            //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
            //�C�g�̃��b�V���A�C�g�̃R���C�_�[�̎擾�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

            //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
            //�}�Y���u���[�L�̃��b�V���̎擾�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

            //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

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
            ReloadTime = ((float)Math.Pow(1.04, CaliberV / 2) * 1.25f - 1.2f) * (float)Math.Log(BarrelLengthV, 25f);
            ProjectilePool =(int)(5f * (float)Math.Ceiling(1f / ReloadTime));
            //�v�[�����ɒe�̃Q�[���I�u�W�F�N�g�����[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
            while (ProjectileNum < ProjectilePool)
            {
            
                ProjectileNum++;
            }
            //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
            FireKey = GetKey(Module.FireKey);
            ReloadingTime = ReloadTime;
        }
        public override void OnSimulateStop()
        {
            base.OnSimulateStop();
            //�v�[�����̒e�Q�[���I�u�W�F�N�g�폜�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[

            //�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[�[
        }
        public override void SimulateUpdateAlways()
        {
            base.SimulateUpdateAlways();

        }
        public override void SimulateFixedUpdateAlways()
        {
            base.SimulateFixedUpdateAlways();
            //���U�������ǂ���
            if(ReloadingTime <= 0f)
            {
                //���C�L�[�������Ă��邩�ǂ���
                if (FireKey.IsPressed || FireKey.EmulationPressed())
                {

                    FireProjectile();
                    ReloadingTime = ReloadTime;
                }

            }
            else
            {
                ReloadingTime = ReloadingTime - Time.fixedDeltaTime;
            }
        }
        public void FireProjectile()
        {
            //�e�I�u�W�F�N�g�Ŕ�L�����̃I�u�W�F�N�g��T���A���̒e�I�u�W�F�N�g���g��

        }
    }
    public class ShootingMonoBehaviour : MonoBehaviour
    {
        public void OnDestroy()
        {

        }
            
    }
}
