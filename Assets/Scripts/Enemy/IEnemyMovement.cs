using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void Move(Enemy enemy);

    void Stop();
}
