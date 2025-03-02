using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
//    [SerializeField] RandomTexture showedSymbol;
    [SerializeField] Button spinButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        if (spinButton != null) 
        {
//            showedSymbol.AssignRandomTexture();
        }
        else 
        {
        
        }
    }

}
