using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAnimation
{
    void HandleAnimation(Enemy enemy);

    void HandleDeathAnimation();

}
