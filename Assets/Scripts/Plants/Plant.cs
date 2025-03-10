using UnityEngine;

public class Plant : MonoBehaviour
{
    public int health;
    public float reloadTime;
    private Cell cell;

    private void Start()
    {
        cell.hasPlant = true;
    }

    public void SetCell(Cell cell)
    {
        this.cell = cell; 
    }

    public void GetDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            cell.hasPlant = false;
        }
    }
}
