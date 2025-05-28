// Codigo Mio
using System.Collections.Generic;
using UnityEngine;

public class EnemySlotManager : MonoBehaviour
{
    public static EnemySlotManager Instance;
    
    private Transform player; // Asigna el Player desde el Inspector
    private Vector3 tankOffset, normalOffset, sniperOffset;

    private Dictionary<string, List<Transform>> slotGroups = new Dictionary<string, List<Transform>>();
    private Dictionary<Transform, GameObject> occupiedSlots = new Dictionary<Transform, GameObject>();
    
    private List<Enemy> waitingEnemies = new List<Enemy>();

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSlots();
    }
    
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        if (player == null)
        {
            Debug.LogWarning("Player no asignado en EnemySlotManager.");
            return;
        }

        Transform tankGroup = transform.Find("TankAttackSpot");
        Transform normalGroup = transform.Find("NormalAttackSpot");
        Transform sniperGroup = transform.Find("SniperAttackSpot");

        if (tankGroup) tankOffset = tankGroup.position - player.position;
        if (normalGroup) normalOffset = normalGroup.position - player.position;
        if (sniperGroup) sniperOffset = sniperGroup.position - player.position;
    }
    
    void LateUpdate()
    {
        if (player == null) return;

        Transform tankGroup = transform.Find("TankAttackSpot");
        Transform normalGroup = transform.Find("NormalAttackSpot");
        Transform sniperGroup = transform.Find("SniperAttackSpot");

        if (tankGroup)
        {
            tankGroup.position = player.position + tankOffset;
            tankGroup.rotation = Quaternion.identity; // <- Esto bloquea la rotación
        }

        if (normalGroup)
        {
            normalGroup.position = player.position + normalOffset;
            normalGroup.rotation = Quaternion.identity;
        }

        if (sniperGroup)
        {
            sniperGroup.position = player.position + sniperOffset;
            sniperGroup.rotation = Quaternion.identity;
        }
    }




    void InitializeSlots()
    {
        // Buscar los hijos del EnemySlotManager por grupos (Tank, Normal, Sniper)
        Transform tankGroup = transform.Find("TankAttackSpot");
        Transform normalGroup = transform.Find("NormalAttackSpot");
        Transform sniperGroup = transform.Find("SniperAttackSpot");

        // Inicializar los diccionarios
        slotGroups["EnemyTank"] = new List<Transform>();
        slotGroups["Enemy"] = new List<Transform>();
        slotGroups["EnemySniper"] = new List<Transform>();

        // Agregar los slots por tipo
        if (tankGroup != null)
        {
            foreach (Transform slot in tankGroup)
            {
                slotGroups["EnemyTank"].Add(slot);
                occupiedSlots[slot] = null;
            }
        }

        if (normalGroup != null)
        {
            foreach (Transform slot in normalGroup)
            {
                slotGroups["Enemy"].Add(slot);
                occupiedSlots[slot] = null;
            }
        }

        if (sniperGroup != null)
        {
            foreach (Transform slot in sniperGroup)
            {
                slotGroups["EnemySniper"].Add(slot);
                occupiedSlots[slot] = null;
            }
        }
    }

    // Método para asignar un slot libre a un enemigo según su tag
    public Transform GetFreeSlot(string enemyTag)
    {
        if (!slotGroups.ContainsKey(enemyTag))
        {
            Debug.LogWarning("No se encontró un grupo de slots para el tag: " + enemyTag);
            return null;
        }

        foreach (Transform slot in slotGroups[enemyTag])
        {
            if (occupiedSlots[slot] == null)
            {
                return slot;
            }
        }

        return null; // No hay slot disponible
    }

    // Asignar el slot como ocupado por un enemigo
    public void AssignSlot(Transform slot, GameObject enemy)
    {
        if (slot != null && occupiedSlots.ContainsKey(slot))
        {
            Debug.Log("Slot ocupado: " + slot.name);
            occupiedSlots[slot] = enemy;
        }
    }

    // Liberar el slot cuando el enemigo muere
    public void ReleaseSlot(Transform slot)
    {
        if (slot != null && occupiedSlots.ContainsKey(slot))
        {
            Debug.Log("Slot liberado: " + slot.name);
            occupiedSlots[slot] = null;
            // Buscar un enemigo en espera del mismo tipo
            for (int i = 0; i < waitingEnemies.Count; i++)
            {
                Enemy enemy = waitingEnemies[i];
                if (enemy != null && enemy.gameObject.tag == GetTagForSlot(slot))
                {
                    AssignSlot(slot, enemy.gameObject);
                    enemy.AssignSlotExternally(slot);
                    waitingEnemies.RemoveAt(i);
                    break;
                }
            }
        }
    }
    
    private string GetTagForSlot(Transform slot)
    {
        if (slotGroups["EnemyTank"].Contains(slot)) return "EnemyTank";
        if (slotGroups["Enemy"].Contains(slot)) return "Enemy";
        if (slotGroups["EnemySniper"].Contains(slot)) return "EnemySniper";
        return "";
    }
    public void RegisterWaitingEnemy(Enemy enemy)
    {
        if (!waitingEnemies.Contains(enemy))
        {
            waitingEnemies.Add(enemy);
        }
    }

}
