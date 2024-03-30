using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{
    public Button button;
    public float scaleFactor = 1.2f;
    public float shrinkSpeed = 3f;

    private bool isShrinking = false;
    
    public void OnClick()
    {
        GameManager.Instance.Gold += 10;
        button.transform.localScale *= scaleFactor;

        isShrinking = true;
    }
    void Update()
    {
        if (isShrinking)
        {
            button.transform.localScale -= Vector3.one * (shrinkSpeed * Time.deltaTime);

            if (button.transform.localScale.x < 1f)
            {
                button.transform.localScale = Vector3.one;
                isShrinking = false;
            }
        }
    }
}
