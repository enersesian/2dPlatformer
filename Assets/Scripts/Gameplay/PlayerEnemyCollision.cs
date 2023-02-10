using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="EnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            bool willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                Health enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement();
                    SpriteRenderer sprite = enemy.GetComponent<SpriteRenderer>();
                    sprite.color = new Color(0f, 1f, 0.63f, 1f);
                    if (!enemyHealth.IsAlive)
                    {
                        Schedule<EnemyDeath>().enemy = enemy;
                        player.Bounce(2);
                    }
                    else
                    {
                        player.Bounce(7);
                    }
                }
                else
                {
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else //hurt the player
            {
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.Decrement();
                    if (!playerHealth.IsAlive)
                    {
                        //player is dead, death is handled in PlayerDeath
                        //Schedule<PlayerDeath>();
                    }
                    else //player is still alive
                    {
                        //Vector2 bounceDirection = new Vector2(-100f, 20f);
                        //player.Bounce(bounceDirection);
                        //move player out of way of danger
                        Vector3 playerPos = player.GetComponent<Transform>().position;
                        Vector3 enemyPos = enemy.GetComponent<Transform>().position;
                        if (playerPos.x <= enemyPos.x)
                        {
                            playerPos = new Vector3(playerPos.x - 3, playerPos.y + 3, playerPos.z);
                        }
                        else
                        {
                            playerPos = new Vector3(playerPos.x + 3, playerPos.y + 3, playerPos.z);
                        }
                        player.Teleport(playerPos);
                    }
                }
                else
                {
                    Schedule<PlayerDeath>();
                }
            }
        }
    }
}