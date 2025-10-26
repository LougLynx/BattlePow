using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    public GameObject[] guns; // Danh sách các súng
    private int currentGunIndex = -1; // Không có súng ban đầu
    public Transform gunHolder; // Vị trí gắn súng trên Player

    void Start()
    {
        if (guns.Length > 0)
        {
            EquipGun(1); // Trang bị súng đầu tiên nếu có
        }
        else
        {
            Debug.LogWarning("Không có súng nào trong danh sách guns!");
        }
    }

    void Update()
    {
        //// Chọn súng bằng phím số (1 -> n)
        //for (int i = 0; i < guns.Length; i++)
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
        //    {
        //        EquipGun(i + 1);
        //    }
        //}
    }

    public void EquipGun(int index)
    {
        if (index <= 0 || index > guns.Length) return;

        // Vô hiệu hóa tất cả súng
        for (int i = 0; i < guns.Length; i++)
        {
            bool isSelected = (i == index - 1);
            guns[i].SetActive(isSelected);

            if (isSelected)
            {
                guns[i].transform.SetParent(gunHolder);
                guns[i].transform.localPosition = Vector3.zero; // Gắn vào Player
                guns[i].transform.localRotation = Quaternion.identity;
            }
        }

        currentGunIndex = index;
        Debug.Log("Đã trang bị súng: " + index);
    }
}
