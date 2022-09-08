using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox.Pool;

namespace Entities.Items
{
    public class MoneyBagParticles : MonoBehaviour
    {
        [Header("Moneys bags")]
        public MoneyBag[] moneyBags = null;

        /// Pool design pattern
        private ObjectPooler objectPooler = null;

        private void Start()
        {
            objectPooler = ObjectPooler.Instance;
        }

        public void PoolObject(Vector3 position)
        {
            objectPooler.SpawnFromPool("MoneyBagParticles", position, Quaternion.Euler(-90, 0, 0));
        }

        private void OnEnable()
        {
            for (int i = 0; i < moneyBags.Length; i++)
                moneyBags[i].OnDestroy += PoolObject;
        }

        private void OnDisable()
        {
            for (int i = 0; i < moneyBags.Length; i++)
                moneyBags[i].OnDestroy -= PoolObject;
        }
    }
}