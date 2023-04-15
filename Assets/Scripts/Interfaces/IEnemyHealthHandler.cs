using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IEnemyHealthHandler
{
    public void TakeDamage(int amount, Vector3 hitPoint);

    public Transform transform { get; }
}
